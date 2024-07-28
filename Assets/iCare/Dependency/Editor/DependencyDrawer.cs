using UnityEditor;
using UnityEngine;

namespace iCare.Editor
{
    [CustomPropertyDrawer(typeof(Dependency<>))]
    public sealed class DependencyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Application.isPlaying)
                return;

            // Get the properties
            var resolveFromProp = property.FindPropertyRelative("resolveFrom");
            var valueProp = property.FindPropertyRelative("value");
            var idProp = property.FindPropertyRelative("id");

            // Determine the type of T
            var fieldType = fieldInfo.FieldType;
            var genericType = fieldType.GetGenericArguments()[0];

            // Create a label with the modified name
            var modifiedLabel = label.text + " - " + genericType.Name;

            // Determine the color based on the resolveFrom value
            var fieldColor = GetColorForResolveFrom(resolveFromProp.enumValueIndex);

            // Save the original GUI color
            var originalColor = GUI.color;

            // Set the new GUI color
            GUI.color = fieldColor;

            // Start drawing the property
            EditorGUI.BeginProperty(position, label, property);

            // Calculate the height needed
            var totalHeight = EditorGUIUtility.singleLineHeight;
            position.height = totalHeight;

            // Draw the fields in a horizontal group
            var fieldRect = new Rect(position.x, position.y, position.width, totalHeight);

            // Begin horizontal group
            EditorGUI.LabelField(new Rect(fieldRect.x, fieldRect.y, EditorGUIUtility.labelWidth, totalHeight),
                modifiedLabel);

            var remainingWidth = fieldRect.width - EditorGUIUtility.labelWidth;
            var xOffset = fieldRect.x + EditorGUIUtility.labelWidth;

            // Calculate dynamic widths based on visibility
            var showValue = (ResolveFrom)resolveFromProp.enumValueIndex == ResolveFrom.Manuel;
            var showID = (ResolveFrom)resolveFromProp.enumValueIndex != ResolveFrom.Manuel;

            var visibleFields = 1 + (showValue ? 1 : 0) + (showID ? 1 : 0);
            var fieldWidth = remainingWidth / visibleFields;

            // Draw the resolveFrom field
            var resolveFromRect = new Rect(xOffset, fieldRect.y, fieldWidth, totalHeight);
            EditorGUI.PropertyField(resolveFromRect, resolveFromProp, GUIContent.none);
            xOffset += fieldWidth;

            // Conditionally draw the value field
            if (showValue)
            {
                var valueRect = new Rect(xOffset, fieldRect.y, fieldWidth, totalHeight);
                EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none);
                xOffset += fieldWidth;
            }

            // Conditionally draw the id field
            if (showID)
            {
                var idRect = new Rect(xOffset, fieldRect.y, fieldWidth, totalHeight);
                EditorGUI.PropertyField(idRect, idProp, GUIContent.none);
            }

            EditorGUI.EndProperty();

            // Restore the original GUI color
            GUI.color = originalColor;
        }

        static Color GetColorForResolveFrom(int resolveFromIndex)
        {
            return (ResolveFrom)resolveFromIndex switch
            {
                ResolveFrom.FromGlobal => Color.magenta,
                ResolveFrom.FromScene => Color.yellow,
                ResolveFrom.FromGameObject => Color.cyan,
                ResolveFrom.Manuel => Color.red,
                _ => Color.white
            };
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}