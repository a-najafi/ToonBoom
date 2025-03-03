
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Utility;

namespace ToonBoomEditor
{

[CustomPropertyDrawer(typeof(SerializedType<PowerupEntity>))]
public class SerializedTypePowerUpDrawer : PropertyDrawer
{
    private static string[] typeNames;
    private static Type[] powerUpTypes;

    static SerializedTypePowerUpDrawer()
    {
        // Cache all subclasses of PowerUpEntity
        powerUpTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(PowerupEntity).IsAssignableFrom(t) && !t.IsAbstract)
            .ToArray();

        typeNames = powerUpTypes.Select(t => t.Name).ToArray();
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty typeNameProp = property.FindPropertyRelative("typeName");

        // Ensure selectedIndex is valid
        int selectedIndex = Array.IndexOf(typeNames, typeNameProp.stringValue);
        if (selectedIndex < 0) selectedIndex = 0; // Default to first item

        EditorGUI.BeginProperty(position, label, property);

        // Create a button that opens a dropdown
        if (GUI.Button(position, selectedIndex >= 0 && selectedIndex < typeNames.Length ? typeNames[selectedIndex] : "Select Type", EditorStyles.miniButton))
        {
            PopupWindow.Show(position, new TypeSelectionPopup(typeNameProp));
        }

        EditorGUI.EndProperty();
    }

    private class TypeSelectionPopup : PopupWindowContent
    {
        private SerializedProperty typeNameProp;

        public TypeSelectionPopup(SerializedProperty typeNameProp)
        {
            this.typeNameProp = typeNameProp;
        }

        public override void OnGUI(Rect rect)
        {
            foreach (var typeName in typeNames)
            {
                if (GUILayout.Button(typeName))
                {
                    typeNameProp.stringValue = typeName;
                    typeNameProp.serializedObject.ApplyModifiedProperties();
                    editorWindow.Close();
                }
            }
        }
    }
}


}

