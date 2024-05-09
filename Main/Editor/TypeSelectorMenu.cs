using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace AnimFlex.Editor
{
    /// <summary>
    /// An advanced dropdown menu that's searchable and shows all serializable sub-types of <see cref="T"/>. 
    /// It uses <see cref="CategoryAttribute"/> for foldering, and <see cref="DisplayNameAttribute"/> for the 
    /// item name, if it's not available, it'll use the type name (after nicifying it)
    /// </summary>
    public sealed class TypeSelectorMenu<T> : AdvancedDropdown
    {
        private static readonly List<Type> _classTypes =
            (from assemblyDomain in AppDomain.CurrentDomain.GetAssemblies()
             from t in assemblyDomain.GetTypes()
             where t.IsSubclassOf(typeof(T)) && !t.IsAbstract
             select t).ToList();

        private static readonly Dictionary<int, (Type type, string niceName, string categoryName)> _typeDics =
            _classTypes
                .Select(t => (t, GetNiceNameOf(t), GetCategoryNameOf(t)))
                .ToDictionary(i => i.t.FullName.GetHashCode());

        private static AdvancedDropdownItem _root;

        private readonly Action<T> _selected;

        public TypeSelectorMenu(Action<T> selected) : base(new AdvancedDropdownState())
        {
            minimumSize = new Vector2(280, 250);
            _selected = selected;
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            EnsureRootIsCreated();
            return _root;
        }

        private void EnsureRootIsCreated()
        {
            if (_root == null)
            {
                _root = new AdvancedDropdownItem(GetNiceNameOf(typeof(T)) + "s");
                var item = new Item();
                foreach (var (id, (_, niceName, categoryName)) in _typeDics)
                {
                    var parts = categoryName.Split('/');
                    if (parts.Length > 0)
                    {
                        if (!item.children.ContainsKey(parts[0]))
                            item.children[parts[0]] = new() { label = parts[0], id = parts[0].GetHashCode() };
                        var child = item.children[parts[0]];
                        for (int i = 1; i < parts.Length - 1; i++)
                        {
                            if (!child.children.ContainsKey(parts[i]))
                                child.children[parts[i]] = new() { label = parts[i], id = parts[i].GetHashCode() };
                            child = child.children[parts[i]];
                        }
                        // last (actual) item
                        child.children[niceName] = new() { id = id, label = niceName };
                    }
                    else
                    {
                        if (!item.children.ContainsKey(niceName))
                        {
                            item.children[niceName] = new() { id = id, label = niceName };
                        }
                        else
                        {
                            item.children[niceName + " "] = new() { id = id, label = niceName };
                        }
                    }
                }

                // apply
                ApplyToRoot(_root, item);
            }
        }

        private void ApplyToRoot(AdvancedDropdownItem root, Item item)
        {
            foreach (var child in item.children.Values)
            {
                var newItem = new AdvancedDropdownItem(child.label) { id = child.id };
                root.AddChild(newItem);
                if (child.children.Count > 0)
                {
                    ApplyToRoot(newItem, child);
                }
                else
                {
                    newItem.icon = (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image;
                }
            }
        }

        private class Item
        {
            public Dictionary<string, Item> children = new();
            public string label;
            public int id;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);
            var val = Activator.CreateInstance(_typeDics[item.id].type);
            _selected?.Invoke((T)val);
        }

        private static string GetNiceNameOf(Type type)
        {
            return type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .FirstOrDefault() is DisplayNameAttribute displayNameAttr
                ? displayNameAttr.DisplayName
                : ObjectNames.NicifyVariableName(type.Name.Replace("_", " "));
        }

        private static string GetCategoryNameOf(Type type)
        {
            return type.GetCustomAttributes(typeof(CategoryAttribute), true)
                .FirstOrDefault() is CategoryAttribute displayNameAttr
                ? displayNameAttr.Category
                : ObjectNames.NicifyVariableName(type.Name).Replace("_", " ");
        }
    }
}