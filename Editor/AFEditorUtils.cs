using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using AnimFlex.Sequencer;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Component = UnityEngine.Component;

namespace AnimFlex.Editor
{
    public static class AFEditorUtils
    {
        private const string EDITOR_RESOURCES_INDEXER_NAME = "AnimFlexEditorResources.ind";


        /// <summary>
        /// returns the path relative to the indexer file located at the root of the plugin
        /// </summary>
        public static string GetPathRelative(string path)
        {
            string r = "Assets/";

            // find the indexer file
            foreach (var fpath in Directory.EnumerateFiles(Application.dataPath, "**", SearchOption.AllDirectories))
            {
                var file = new FileInfo(fpath);
                if (file.Name == EDITOR_RESOURCES_INDEXER_NAME)
                {
                    var root_dir = MakeRelativePath(Application.dataPath + "/", file.Directory.FullName);
                    r = "Assets/" + root_dir.Replace("\\", "/") + "/" + path.Replace("\\", "/");
                    return r;
                }
            }

            throw new FileNotFoundException(
                $"The indexer file not found: " +
                $"create a file named AnimFlexEditor.ind at the root of the editor resources of AnimFlex.");
        }


        /// <summary>
        /// Creates a relative path from one file or folder to another.
        /// Credits to Dave : https://stackoverflow.com/a/340454/17089583
        /// </summary>
        /// <param name="fromPath">Contains the directory that defines the start of the relative path.</param>
        /// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
        /// <returns>The relative path from the start directory to the end path or <c>toPath</c> if the paths are not related.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UriFormatException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static string MakeRelativePath(string fromPath, string toPath)
        {
	        if (string.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
	        if (string.IsNullOrEmpty(toPath))   throw new ArgumentNullException("toPath");

	        Uri fromUri = new Uri(fromPath);
	        Uri toUri = new Uri(toPath);

	        if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

	        Uri relativeUri = fromUri.MakeRelativeUri(toUri);
	        string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

	        if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
	        {
		        relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
	        }

	        return relativePath;
        }

        /// <summary>
        /// Gets value from SerializedProperty, even if it's nested
        /// thanks to vedram : https://forum.unity.com/threads/get-a-general-object-value-from-serializedproperty.327098/#post-4098484
        /// </summary>
        public static object GetValue(this SerializedProperty property, bool syncData = false)
        {
            if (syncData == false)
            {
                property.serializedObject.ApplyModifiedProperties();
                property.serializedObject.Update();
            }
            object obj = property.serializedObject.targetObject;

            var split = property.propertyPath.Split('.');
            for (var i = 0; i < split.Length; i++)
            {
                object parent_obj = obj;

                var path = split[i];
                var type = obj.GetType();
                var field = type.GetField(path,
                    BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                obj = field.GetValue(obj);
                if (field.FieldType.IsArray)
                {
                    i += 2;
                    path = split[i].Replace("data[", "").Replace("]", "");
                    obj = (field.GetValue(parent_obj) as Array).GetValue(int.Parse(path));
                }
            }

            return obj;
        }

        #nullable enable
        /// <summary>
        /// finds a Type from the given name
        /// </summary>
        public static Type? FindType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null)
                return type;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null)
                    return type;
            }

            return null;
        }
        #nullable restore

        /// <summary>
        /// gets the type name based on the given Type.
        /// if categoryName is true, it'll return the Category attribute name, otherwise it'll return the DisplayName attribute name
        /// </summary>
        public static string GetTypeName(Type type, bool categoryName = true)
        {
            if (categoryName)
            {
                return type.GetCustomAttributes(typeof(CategoryAttribute), true)
                    .FirstOrDefault() is CategoryAttribute displayNameAttr
                    ? displayNameAttr.Category
                    : type.Name;
            }
            else
            {
                return type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() is DisplayNameAttribute displayNameAttr
                    ? displayNameAttr.DisplayName
                    : type.Name;
            }
        }

        /// <summary>
        /// Opens a popup for selecting a component from the newly assigned reference.
        /// </summary>
        public static void OpenComponentReferenceSelectionMenu(SerializedProperty property)
        {
            var selectedComponent = property.objectReferenceValue;
            if (selectedComponent is Component component)
            {
                // show component selection generic context menu
                var menu = new GenericMenu();
                var components = component.GetComponents<Component>();
                foreach (var c in components)
                {
                    if (c == null) continue;

                    var comp = c; // caching to avoid losing value in loop
                    var type = comp.GetType();
                    menu.AddItem(new GUIContent(type.Name), type == typeof(Transform), () =>
                    {
                        property.objectReferenceValue = comp;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                }

                menu.ShowAsContext();
            }
        }

        /// <summary>
        /// Opens a popup for selecting a sub-class type of the given type T
        /// </summary>
        public static void CreateTypeInstanceFromHierarchy<T>(Action<T> onSelect)
        {
            var classTypes =
                from assemblyDomain in AppDomain.CurrentDomain.GetAssemblies()
                from type in assemblyDomain.GetTypes()
                where type.IsSubclassOf(typeof(T)) && !type.IsAbstract
                select type;

            var menu = new GenericMenu();
            foreach (var type in classTypes)
                menu.AddItem(new GUIContent(ObjectNames.NicifyVariableName(GetTypeName(type))),
                    false, () =>
                    {
                        var val = Activator.CreateInstance(type);
                        onSelect((T)val);
                    });
            menu.ShowAsContext();
        }


        public static void DrawNodeSelectionPopup(Rect position, SerializedProperty property, GUIContent label, Sequence sequence)
        {
            var displayedOptions = sequence.nodes
                .Select((node, i) => $"({i}) {node.name}").ToArray()
                .Select(p => new GUIContent(p)).ToArray();

            if (property.intValue < 0 || property.intValue >= displayedOptions.Length)
                property.intValue = 0;

            position.height = AFStyles.Height;

            property.intValue = EditorGUI.Popup(
                position: position,
                label: label,
                selectedIndex: property.intValue,
                displayedOptions: displayedOptions,
                style: AFStyles.Popup);
        }

        public static void DrawFieldNameSelectionPopup(Type type, SerializedProperty componentProp, Rect pos, SerializedProperty valueNameProp)
        {
            if (componentProp.objectReferenceValue == null)
            {
                AFStyles.DrawHelpBox(pos, $"--{componentProp.name} is empty--", MessageType.Warning);
            }
            else
            {
                var valueOptions = componentProp.objectReferenceValue
                    .GetType()
                    .GetFields(BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(fieldInfo => fieldInfo.FieldType == type)
                    .Select(fieldInfo => fieldInfo.Name).ToList();

                if (valueOptions.Count == 0)
                {
                    AFStyles.DrawHelpBox(pos, $"--No field of type {type.Name} found--", MessageType.Warning);
                }
                else
                {
                    var selectedIndex = valueOptions.IndexOf(valueNameProp.stringValue);
                    if (selectedIndex == -1)
                    {
                        valueNameProp.stringValue = valueOptions[0];
                        selectedIndex = 0;
                    }

                    valueNameProp.stringValue = valueOptions[
                        EditorGUI.Popup(pos,
                            new GUIContent("Value :", valueNameProp.tooltip),
                            selectedIndex,
                            valueOptions.Select(val => new GUIContent(val)).ToArray(),
                            AFStyles.Popup)];
                }
            }
        }

    }
}
