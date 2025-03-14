namespace ToonBoomEditor.Objective
{


    using System;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;



    //this editor allows the user to select any subclass of PowerupEntity to assign to serializable PowerupEntity fields
    //this is used mainly for grid and level editors
    [CustomPropertyDrawer(typeof(ToonBoomCore.Level.Design.ObjectiveBase), true)]
    public class ObjectiveDrawer : PropertyDrawer
    {
        private static Type[] derivedTypes;

        static ObjectiveDrawer()
        {
            // Find all non-abstract child classes of BaseClass
            derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsSubclassOf(typeof(ToonBoomCore.Level.Design.ObjectiveBase)) && !t.IsAbstract)
                .ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float buttonHeight = EditorGUIUtility.singleLineHeight;
            Rect buttonRect = new Rect(position.x, position.y, position.width, buttonHeight);
            Rect fieldRect = new Rect(position.x, position.y + buttonHeight + 2, position.width,
                position.height - buttonHeight - 2);

            // Use reflection to get the actual field value
            object target = GetTargetObjectOfProperty(property);
            bool isNull = target == null;

            // Dropdown to select/change class
            if (GUI.Button(buttonRect, isNull ? "Select Class" : $"Change Class ({target.GetType().Name})"))
            {
                GenericMenu menu = new GenericMenu();
                foreach (Type type in derivedTypes)
                {
                    menu.AddItem(new GUIContent(type.Name), false, () =>
                    {
                        property.serializedObject.Update();
                        property.managedReferenceValue = Activator.CreateInstance(type); // Replace the instance
                        property.serializedObject.ApplyModifiedProperties();

                        // 🚀 Force Inspector to refresh immediately!
                        EditorApplication.delayCall += () =>
                        {
                            EditorUtility.SetDirty(property.serializedObject.targetObject);
                            property.serializedObject.Update();
                            property.serializedObject.ApplyModifiedProperties();
                            EditorWindow.focusedWindow?.Repaint(); // Force a full refresh of the Inspector
                        };
                    });
                }

                menu.ShowAsContext();
            }

            if (!isNull)
            {
                // Display fields of the selected subclass dynamically
                EditorGUI.PropertyField(fieldRect, property, GUIContent.none, true);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Dynamically adjust height when used inside a list
            return EditorGUI.GetPropertyHeight(property, label, true) + EditorGUIUtility.singleLineHeight + 20;
        }

        // Reflection helper to get the actual object value
        private object GetTargetObjectOfProperty(SerializedProperty prop)
        {
            if (prop == null) return null;

            string path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            string[] elements = path.Split('.');

            foreach (string element in elements)
            {
                if (element.Contains("["))
                {
                    string elementName = element.Substring(0, element.IndexOf("["));
                    int index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "")
                        .Replace("]", ""));
                    obj = GetFieldValue(obj, elementName, index);
                }
                else
                {
                    obj = GetFieldValue(obj, element);
                }
            }

            return obj;
        }

        private object GetFieldValue(object source, string fieldName, int index = -1)
        {
            if (source == null) return null;
            Type type = source.GetType();
            FieldInfo field = type.GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field == null) return null;
            object val = field.GetValue(source);
            if (index >= 0 && val is System.Collections.IList list)
            {
                return list[index];
            }

            return val;
        }
    }

}


