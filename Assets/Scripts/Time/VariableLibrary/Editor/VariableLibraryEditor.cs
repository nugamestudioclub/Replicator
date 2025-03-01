using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

namespace Time
{
    [CustomEditor(typeof(VariableLibrary))]
    public class VariableLibraryEditor : Editor
    {
        private ReorderableList reorderableList;

        private void OnEnable()
        {
            SerializedProperty mappingsProperty = serializedObject.FindProperty("mappings");

            reorderableList = new ReorderableList(serializedObject, mappingsProperty, true, true, true, true)
            {
                drawHeaderCallback = (Rect rect) => {
                    EditorGUI.LabelField(rect, "Variable Mappings");
                },

                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                    SerializedProperty element = mappingsProperty.GetArrayElementAtIndex(index);
                    SerializedProperty variableNameProp = element.FindPropertyRelative("variableName");
                    SerializedProperty initialValueProp = element.FindPropertyRelative("initialValue");

                    rect.y += 2;
                    float halfWidth = rect.width / 2 - 10;

                    EditorGUI.PropertyField(
                        new Rect(rect.x, rect.y, halfWidth, EditorGUIUtility.singleLineHeight),
                        variableNameProp, GUIContent.none);

                    EditorGUI.PropertyField(
                        new Rect(rect.x + halfWidth + 10, rect.y, halfWidth, EditorGUIUtility.singleLineHeight),
                        initialValueProp, GUIContent.none);
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            reorderableList.DoLayoutList();

            if (GUILayout.Button("Open Extended View"))
            {
                ExtendedVariableLibraryWindow.ShowWindow((VariableLibrary)target);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    public class ExtendedVariableLibraryWindow : EditorWindow
    {
        private VariableLibrary library;
        private Vector2 scrollPosition;
        private SerializedObject serializedObject;
        private SerializedProperty mappingsProperty;
        private ReorderableList reorderableList;
        private bool showDescriptions = false; // Toggle for descriptions

        public static void ShowWindow(VariableLibrary library)
        {
            ExtendedVariableLibraryWindow window = GetWindow<ExtendedVariableLibraryWindow>("Extended Variable Library");
            window.library = library;
            window.Init();
            window.Show();
        }

        private void Init()
        {
            serializedObject = new SerializedObject(library);
            mappingsProperty = serializedObject.FindProperty("mappings");

            reorderableList = new ReorderableList(serializedObject, mappingsProperty, true, true, true, true)
            {
                drawHeaderCallback = (Rect rect) => {
                    EditorGUI.LabelField(rect, "Extended Variable Mappings");
                },

                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                    SerializedProperty element = mappingsProperty.GetArrayElementAtIndex(index);
                    SerializedProperty variableNameProp = element.FindPropertyRelative("variableName");
                    SerializedProperty initialValueProp = element.FindPropertyRelative("initialValue");
                    SerializedProperty editorDescriptionProp = element.FindPropertyRelative("editorDescription");

                    rect.y += 2;
                    float halfWidth = rect.width / 2 - 10;

                    // Variable Name
                    EditorGUI.PropertyField(
                        new Rect(rect.x, rect.y, halfWidth, EditorGUIUtility.singleLineHeight),
                        variableNameProp, GUIContent.none);

                    // Initial Value
                    EditorGUI.PropertyField(
                        new Rect(rect.x + halfWidth + 10, rect.y, halfWidth, EditorGUIUtility.singleLineHeight),
                        initialValueProp, GUIContent.none);

                    // Description Field (Only if showDescriptions is enabled)
                    if (showDescriptions)
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + 2;
                        EditorGUI.PropertyField(
                            new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                            editorDescriptionProp, new GUIContent("Description"));
                    }
                },

                elementHeightCallback = (int index) => {
                    return showDescriptions ? EditorGUIUtility.singleLineHeight * 2 + 8 : EditorGUIUtility.singleLineHeight + 4;
                }
            };
        }

        private void OnGUI()
        {
            serializedObject.Update();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            // Toggle button for showing descriptions
            showDescriptions = EditorGUILayout.Toggle("Show Descriptions", showDescriptions);

            reorderableList.DoLayoutList();
            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("Save Changes"))
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(library);
            }
        }
    }
}
