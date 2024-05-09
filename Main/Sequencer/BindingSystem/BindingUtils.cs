using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AnimFlex.Sequencer.Clips;
using UnityEngine;

namespace AnimFlex.Sequencer.BindingSystem
{
    public static class BindingUtils
    {
        private const BindingFlags SelectableBindingFlags = BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance;
        private static readonly Dictionary<(Type clipType, Type valueType), string[]> cachedBindableFields = new Dictionary<(Type, Type), string[]>();
        private static readonly Dictionary<(Type clipType, Type valueType), GUIContent[]> cachedBindableFieldsGuiContent = new Dictionary<(Type, Type), GUIContent[]>();

        /// <summary>
        /// returns all bindable fields on the clip given the valueType
        /// </summary>
        public static string[] GetAllBindableFieldsOnClip(Clip clip, Type valueType)
        {
            var key = (getBindableTypeValue(clip).GetType(), valueType);
            if (!cachedBindableFields.TryGetValue(key, out var values))
            {
                values = getBindableTypeValue(clip)
                    .GetType()
                    .GetFields(SelectableBindingFlags)
                    .Where(field => field.FieldType == valueType || field.FieldType.IsAssignableFrom(valueType))
                    .Select(field => field.Name)
                    .ToArray();
                cachedBindableFields[key] = values;
            }
            return values;
        }

        /// <summary>
        /// returns all bindable fields on the clip given the valueType, as <see cref="GUIContent"/> (without tooltip)
        /// </summary>
        public static GUIContent[] GetAllBindableFieldsOnClipGuiContent(Clip clip, Type valueType)
        {
            var key = (getBindableTypeValue(clip).GetType(), valueType);
            var clipType = getBindableTypeValue(clip).GetType();
            if (!cachedBindableFieldsGuiContent.TryGetValue(key, out var values))
            {
                values = clipType
                    .GetFields(SelectableBindingFlags)
                    .Where(field => field.FieldType == valueType || field.FieldType.IsAssignableFrom(valueType))
                    .Select(field => new GUIContent(field.Name))
                    .ToArray();
                cachedBindableFieldsGuiContent[key] = values;
            }

            return values;
        }
        
        private static object getBindableTypeValue(Clip clip) => clip is CTweener ctweener ? ctweener.GetTweenerGenerator() : clip;
    }
}