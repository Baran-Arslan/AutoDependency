using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare.Di.Editor {
    [CustomPropertyDrawer(typeof(ObjectService))]
    public sealed class ObjectServiceDrawer : PropertyDrawer {
        const float TYPE_SELECTOR_WIDTH_RATIO = 0.3f;
        const float ICON_SIZE = 17f;
        
        SerializedProperty _serviceTypeProp;
        SerializedProperty _valueProp;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            var (valueRect, typeRect) = SplitRect(position);
            GetProperties(property);

            var icon = Textures2D.InjectionIconTexture.Get();
            if (icon != null) GUI.DrawTexture(new Rect(position.x, position.y, ICON_SIZE, ICON_SIZE), icon);

            // Adjust the position of other elements to account for the icon
            var iconOffset = icon != null ? ICON_SIZE + 2 : 0;
            valueRect.x += iconOffset;
            valueRect.width -= iconOffset;
            typeRect.x += iconOffset;
            typeRect.width -= iconOffset;

            DrawValueField(valueRect, _valueProp, _serviceTypeProp);
            DrawTypeSelector(typeRect, _valueProp, _serviceTypeProp);
            ValidateTypeString();

            EditorGUI.EndProperty();
        }

        void ValidateTypeString() {
            if (_valueProp.objectReferenceValue.IsUnityNull() && !string.IsNullOrEmpty(_serviceTypeProp.stringValue))
                _serviceTypeProp.stringValue = string.Empty;
        }

        void GetProperties(SerializedProperty property) {
            _valueProp = property.FindPropertyRelative("value");
            _serviceTypeProp = property.FindPropertyRelative("serviceType");
        }

        static (Rect valueRect, Rect typeRect) SplitRect(Rect position) {
            var width = position.width;
            var height = EditorGUIUtility.singleLineHeight;
            var typeSelectorWidth = width * TYPE_SELECTOR_WIDTH_RATIO;

            var valueRect = new Rect(position.x, position.y, width - typeSelectorWidth, height);
            var typeRect = new Rect(position.x + width - typeSelectorWidth, position.y, typeSelectorWidth, height);

            return (valueRect, typeRect);
        }

        static void DrawValueField(Rect rect, SerializedProperty valueProp, SerializedProperty serviceTypeProp) {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(rect, valueProp, GUIContent.none);
            if (EditorGUI.EndChangeCheck()) UpdateServiceType(valueProp, serviceTypeProp);
        }

        static void DrawTypeSelector(Rect rect, SerializedProperty valueProp, SerializedProperty serviceTypeProp) {
            if (valueProp.objectReferenceValue == null) return;

            var availableTypes = GetAvailableTypes(valueProp.objectReferenceValue).ToArray();
            var currentType = Type.GetType(serviceTypeProp.stringValue);
            var currentIndex = Array.FindIndex(availableTypes, t => t == currentType);

            EditorGUI.BeginChangeCheck();
            var newIndex = EditorGUI.Popup(rect, currentIndex, availableTypes.Select(t => t.Name).ToArray());
            if (EditorGUI.EndChangeCheck() && newIndex >= 0 && newIndex != currentIndex) {
                var newType = availableTypes[newIndex];
                serviceTypeProp.stringValue = newType.AssemblyQualifiedName;
                SetValueToType(valueProp, newType);
            }
        }

        static void UpdateServiceType(SerializedProperty valueProp, SerializedProperty serviceTypeProp) {
            serviceTypeProp.stringValue = valueProp.objectReferenceValue == null
                ? string.Empty
                : valueProp.objectReferenceValue.GetType().AssemblyQualifiedName;
        }

        static IEnumerable<Type> GetAvailableTypes(Object targetObject) {
            if (targetObject == null) return Enumerable.Empty<Type>();

            var objType = targetObject.GetType();

            return targetObject switch {
                GameObject gameObj => new[] { typeof(GameObject) }.Concat(gameObj.GetComponents<Component>().Select(c => c.GetType())),
                MonoBehaviour or ScriptableObject => objType.GetTypesUntilBase(typeof(MonoBehaviour)),
                Behaviour => new[] { objType },
                Component => objType.GetTypesUntilBase(typeof(Component), false),
                _ => new[] { objType }
            };
        }

        static void SetValueToType(SerializedProperty valueProp, Type newType) {
            if (!typeof(Component).IsAssignableFrom(newType) && newType != typeof(GameObject)) return;

            var currentObject = valueProp.objectReferenceValue;
            if (currentObject == null) return;

            if (currentObject is GameObject gameObject) {
                if (newType == typeof(GameObject)) {
                    valueProp.objectReferenceValue = gameObject;
                }
                else {
                    var component = gameObject.GetComponent(newType);
                    if (component != null) valueProp.objectReferenceValue = component;
                }
            }
            else if (currentObject is Component component) {
                if (newType == typeof(GameObject)) {
                    valueProp.objectReferenceValue = component.gameObject;
                }
                else {
                    var newComponent = component.GetComponent(newType);
                    if (newComponent != null) valueProp.objectReferenceValue = newComponent;
                }
            }
        }
    }
}