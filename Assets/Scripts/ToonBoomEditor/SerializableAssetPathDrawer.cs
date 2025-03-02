using ToonBoomCore.Utility;

namespace ToonBoomEditor
{
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(SerializableAssetPath))]
    public class SerializableAssetPathDrawer : PropertyDrawer
    {
        
        const string Trim = "Assets/Resources/";
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Get the serialized property for the assetPath string
            SerializedProperty assetPathProp = property.FindPropertyRelative("assetPath");

            // Draw label
            Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
            EditorGUI.LabelField(labelRect, label);

            // Object field for drag-and-drop
            Rect objectFieldRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, position.height);
            Object obj = EditorGUI.ObjectField(objectFieldRect, null, typeof(Object), false);

            // If an object is dragged in
            if (obj != null)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                path = path.TrimStart(Trim.ToCharArray());
                path = path.Split(".".ToCharArray())[0];
                assetPathProp.stringValue = path;
            }

            // Draw the stored path as a non-editable text field
            EditorGUI.BeginDisabledGroup(true);
            Rect textFieldRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y + 20, position.width - EditorGUIUtility.labelWidth, position.height);
            EditorGUI.TextField(textFieldRect, assetPathProp.stringValue);
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label)  ; // Extra space for path display
        }
    }

}