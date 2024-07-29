using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare.Editor {
    [CustomPropertyDrawer(typeof(Service))]
    public sealed class ServiceDrawer : PropertyDrawer {
        const float TYPE_SELECTOR_WIDTH_RATIO = 0.3f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            var (valueRect, typeRect) = SplitRect(position);
            var valueProp = property.FindPropertyRelative("value");
            var serviceTypeProp = property.FindPropertyRelative("serviceType");

            DrawValueField(valueRect, valueProp, serviceTypeProp);
            DrawTypeSelector(typeRect, valueProp, serviceTypeProp);

            if (valueProp.objectReferenceValue == null && !string.IsNullOrEmpty(serviceTypeProp.stringValue))
                serviceTypeProp.stringValue = string.Empty;

            EditorGUI.EndProperty();
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
            if (!typeof(Component).IsAssignableFrom(newType) && newType != typeof(GameObject)) {
                return;
            }
            var currentObject = valueProp.objectReferenceValue;
            if (currentObject == null) {
                return; 
            }

            if (currentObject is GameObject gameObject) {
                if (newType == typeof(GameObject)) {
                    valueProp.objectReferenceValue = gameObject; // Just set the GameObject itself.
                } else {
                    var component = gameObject.GetComponent(newType);
                    if (component != null) {
                        valueProp.objectReferenceValue = component; // Set the component if found.
                    }
                }
            } else if (currentObject is Component component) {
                if (newType == typeof(GameObject)) {
                    valueProp.objectReferenceValue = component.gameObject; // Set the GameObject from the component.
                } else {
                    var newComponent = component.GetComponent(newType);
                    if (newComponent != null) {
                        valueProp.objectReferenceValue = newComponent; // Set the new component if found.
                    }
                }
            }
        }

    }
}