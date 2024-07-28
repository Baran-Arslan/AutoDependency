using UnityEditor;
using UnityEngine;

namespace iCare
{
    [CustomPropertyDrawer(typeof(ObjectService))]
    public class ObjectServiceDrawer : PropertyDrawer
    {
        SerializedProperty registerID;
        SerializedProperty registerTo;
        SerializedProperty service;
        SerializedProperty targetContainer;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            GetFields(property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var lineHeight = EditorGUIUtility.singleLineHeight;
            var padding = 2;

            var showTargetContainer = (RegisterTo)registerTo.enumValueIndex == RegisterTo.ToGameObject;
            var fieldCount = showTargetContainer ? 4 : 3;
            var fieldWidth = (position.width - padding * (fieldCount - 1)) / fieldCount;

            var rects = new Rect[4];
            for (var i = 0; i < rects.Length; i++)
                rects[i] = new Rect(position.x + i * (fieldWidth + padding), position.y, fieldWidth, lineHeight);
            var serviceRect = rects[0];
            var registerToRect = rects[1];
            var registerIDRect = rects[2];
            var targetContainerRect = rects[3];

            EditorGUI.PropertyField(serviceRect, service, GUIContent.none);
            EditorGUI.PropertyField(registerToRect, registerTo, GUIContent.none);
            EditorGUI.PropertyField(registerIDRect, registerID, GUIContent.none);
            if (showTargetContainer)
                EditorGUI.PropertyField(targetContainerRect, targetContainer, GUIContent.none);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        void GetFields(SerializedProperty property)
        {
            service = property.FindPropertyRelative("service");
            registerTo = property.FindPropertyRelative("registerTo");
            targetContainer = property.FindPropertyRelative("targetContainer");
            registerID = property.FindPropertyRelative("registerID");
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}