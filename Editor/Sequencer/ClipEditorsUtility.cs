using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Component = UnityEngine.Component;

namespace AnimFlex.Sequencer.Editor
{
    public static class ClipEditorsUtility
    {
        [CanBeNull]
        public static Type FindType(string typeName)
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

        public static string GetTypeName(Type type)
        {
            return type.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() is DisplayNameAttribute
                displayNameAttr
                ? displayNameAttr.DisplayName
                : type.Name;
        }

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
    }
}