using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
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
                    var root_dir = Path.GetRelativePath(Application.dataPath, file.Directory.FullName);
                    r = "Assets/" + root_dir.Replace("\\", "/") + "/" + path.Replace("\\", "/");
                    return r;
                }
            }

            throw new FileNotFoundException(
                $"The indexer file not found: " +
                $"create a file named AnimFlexEditor.ind at the root of the editor resources of AnimFlex.");
        }

        /// <summary>
        /// Gets value from SerializedProperty, even if it's nested
        /// thanks to vedram : https://forum.unity.com/threads/get-a-general-object-value-from-serializedproperty.327098/#post-4098484
        /// </summary>
        public static object GetValue(this SerializedProperty property)
        {
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

        /// <summary>
        /// gets the type name based on the given Type
        /// </summary>
        public static string GetTypeName(Type type, bool groupsRemoved = true)
        {
            return type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() is DisplayNameAttribute displayNameAttr
                ? groupsRemoved 
                    ? displayNameAttr.DisplayName.Substring(Mathf.Max(0, 1 + displayNameAttr.DisplayName.LastIndexOf("/")))
                    : displayNameAttr.DisplayName
                : type.Name;
        }

        /// <summary>
        /// Opens a popup for selecting a component from the newly assigned reference.
        /// it only operates if the new reference is a Transform, so if Unity changes the default drag & drop reference,
        /// this needs to change as well
        /// </summary>
        public static void OpenComponentReferenceSelectionMenu(SerializedProperty property)
        {
            var selectedComponent = property.objectReferenceValue;
            if (selectedComponent is Transform transform)
            {
                // show component selection generic context menu
                var menu = new GenericMenu();
                var components = transform.GetComponents<Component>();
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
                menu.AddItem(new GUIContent(ObjectNames.NicifyVariableName(GetTypeName(type, false))),
                    false, () =>
                    {
                        var val = Activator.CreateInstance(type);
                        onSelect((T)val);
                    });
            menu.ShowAsContext();
        }
    }
}