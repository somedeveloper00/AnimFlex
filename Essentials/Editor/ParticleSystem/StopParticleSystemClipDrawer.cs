using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    [CustomPropertyDrawer(typeof(StopParticleSystem))]
    public class StopParticleSystemClipDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var particleSystemProp = property.FindPropertyRelative(nameof(StopParticleSystem.particleSystem));
            var withChildrenProp = property.FindPropertyRelative(nameof(StopParticleSystem.withChildren));
            var systemStopBehaviorProp = property.FindPropertyRelative(nameof(StopParticleSystem.systemStopBehavior));
            var waitTillFinishedProp = property.FindPropertyRelative(nameof(StopParticleSystem.waitTillFinished));

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position.height = AFStyles.Height;
                EditorGUI.PropertyField(position, particleSystemProp);
                position.y += AFStyles.Height + AFStyles.VerticalSpace;
                EditorGUI.PropertyField(position, withChildrenProp);
                position.y += AFStyles.Height + AFStyles.VerticalSpace;
                EditorGUI.PropertyField(position, systemStopBehaviorProp);
                position.y += AFStyles.Height + AFStyles.VerticalSpace;
                if (systemStopBehaviorProp.enumValueIndex == (int)ParticleSystemStopBehavior.StopEmitting)
                {
                    EditorGUI.PropertyField(position, waitTillFinishedProp);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var systemStopBehaviorProp = property.FindPropertyRelative(nameof(StopParticleSystem.systemStopBehavior));
            if (systemStopBehaviorProp.enumValueIndex == (int)ParticleSystemStopBehavior.StopEmitting)
            {
                return (AFStyles.Height + AFStyles.VerticalSpace) * 4;
            }

            return (AFStyles.Height + AFStyles.VerticalSpace) * 3;
        }
    }
}