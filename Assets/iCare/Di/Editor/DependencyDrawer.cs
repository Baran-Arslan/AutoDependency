using UnityEditor;
using UnityEngine;

namespace iCare.Di.Editor {
    [CustomPropertyDrawer(typeof(Dependency<>))]
    public sealed class DependencyDrawer : PropertyDrawer {
        const float LINE_HEIGHT = 14f;
        const float ICON_SIZE = 17f;

        SerializedProperty _fromProp;
        SerializedProperty _isManualProp;
        SerializedProperty _manualValueProp;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var labelWidth = EditorGUIUtility.labelWidth / 1.2f;
            var fieldWidth = (position.width - labelWidth) / 2.05f;

            GetProperties(property);

            var fieldGenericType = fieldInfo.FieldType.GetGenericArguments()[0];

            var icon = Textures2D.InjectionIconTexture.Get();

            if (icon != null) {
                EditorGUI.DrawRect(new Rect(position.x, position.y, position.width, LINE_HEIGHT), new Color(0.25f, 0.52f, 0.12f, 0.5f));
                GUI.DrawTexture(new Rect(position.x, position.y, ICON_SIZE, ICON_SIZE), icon);
            }

            //Get main class that implements this
            var mainObj = property.serializedObject.targetObject;
            var showEnums = mainObj is MonoBehaviour;

            // Adjust positions for the label and fields
            var iconOffset = icon != null ? ICON_SIZE + 2 : 0;
            var labelRect = new Rect(position.x + iconOffset, position.y, labelWidth * 1.3f, LINE_HEIGHT);
            EditorGUI.LabelField(labelRect, fieldGenericType.Name, EditorStyles.boldLabel);

            var isManualFieldRect = new Rect(position.x + labelWidth + fieldWidth - 20 + iconOffset, position.y, 20, LINE_HEIGHT);
            var enumFieldRect = new Rect(isManualFieldRect.x + 20, position.y, fieldWidth, LINE_HEIGHT);

            if (_isManualProp.boolValue)
                EditorGUI.PropertyField(enumFieldRect, _manualValueProp, GUIContent.none);
            else if (showEnums) EditorGUI.PropertyField(enumFieldRect, _fromProp, GUIContent.none);
            if (fieldGenericType.IsSubclassOf(typeof(Object))) EditorGUI.PropertyField(isManualFieldRect, _isManualProp, GUIContent.none);
        }

        void GetProperties(SerializedProperty property) {
            _isManualProp = property.FindPropertyRelative("isManual");
            _fromProp = property.FindPropertyRelative("from");
            _manualValueProp = property.FindPropertyRelative("manualValue");
        }
    }
}