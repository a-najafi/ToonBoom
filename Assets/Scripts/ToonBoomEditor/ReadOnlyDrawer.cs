using ToonBoomCore.Utility;
using UnityEditor;
using UnityEngine;

namespace ToonBoomEditor
{
    
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false; // Disable editing (gray it out)
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true; // Re-enable UI
        }
    }
}