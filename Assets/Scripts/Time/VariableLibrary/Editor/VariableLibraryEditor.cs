using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

namespace Time
{
    [CustomEditor(typeof(VariableLibrary))]
    public class VariableLibraryEditor : Editor
    {
        private SerializedProperty groupsProperty;
        private List<ReorderableList> groupLists = new List<ReorderableList>();
        private List<bool> groupFoldouts = new List<bool>();

        private void OnEnable()
        {
            groupsProperty = serializedObject.FindProperty("groups");
            InitializeGroupLists();
        }

        private void InitializeGroupLists()
        {
            groupLists.Clear();
            groupFoldouts.Clear();

            for (int i = 0; i < groupsProperty.arraySize; i++)
            {
                SerializedProperty groupProperty = groupsProperty.GetArrayElementAtIndex(i);
                SerializedProperty variablesProperty = groupProperty.FindPropertyRelative("variables");

                groupFoldouts.Add(true);

                ReorderableList list = new ReorderableList(serializedObject, variablesProperty, true, true, true, true)
                {
                    drawHeaderCallback = (Rect rect) => {
                        EditorGUI.LabelField(rect, "Variables");
                    },

                    drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                        SerializedProperty element = variablesProperty.GetArrayElementAtIndex(index);
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

                groupLists.Add(list);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (groupsProperty.arraySize == 0)
            {
                if (GUILayout.Button("Add Group"))
                {
                    AddNewGroup();
                }
            }

            for (int i = 0; i < groupsProperty.arraySize; i++)
            {
                SerializedProperty groupProperty = groupsProperty.GetArrayElementAtIndex(i);
                SerializedProperty groupNameProperty = groupProperty.FindPropertyRelative("groupName");

                EditorGUILayout.BeginHorizontal();
                groupFoldouts[i] = EditorGUILayout.Foldout(groupFoldouts[i], groupNameProperty.stringValue, true);
                groupNameProperty.stringValue = EditorGUILayout.TextField(groupNameProperty.stringValue);
                EditorGUILayout.EndHorizontal();

                if (groupFoldouts[i])
                {
                    groupLists[i].DoLayoutList();
                }
            }

            if (GUILayout.Button("Add Group"))
            {
                AddNewGroup();
            }

            if (GUILayout.Button("Open Extended View"))
            {
                ExtendedVariableLibraryWindow.ShowWindow((VariableLibrary)target);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void AddNewGroup()
        {
            int newIndex = groupsProperty.arraySize;
            groupsProperty.InsertArrayElementAtIndex(newIndex);
            groupsProperty.GetArrayElementAtIndex(newIndex).FindPropertyRelative("groupName").stringValue = "New Group";
            serializedObject.ApplyModifiedProperties();
            InitializeGroupLists();
        }
    }

    public class ExtendedVariableLibraryWindow : EditorWindow
    {
        private VariableLibrary library;
        private Vector2 scrollPosition;
        private SerializedObject serializedObject;
        private SerializedProperty groupsProperty;
        private List<ReorderableList> groupLists = new List<ReorderableList>();
        private List<bool> groupFoldouts = new List<bool>();
        private bool showDescriptions = false;

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
            groupsProperty = serializedObject.FindProperty("groups");

            groupLists.Clear();
            groupFoldouts.Clear();

            for (int i = 0; i < groupsProperty.arraySize; i++)
            {
                SerializedProperty groupProperty = groupsProperty.GetArrayElementAtIndex(i);
                SerializedProperty variablesProperty = groupProperty.FindPropertyRelative("variables");

                groupFoldouts.Add(true);

                ReorderableList list = new ReorderableList(serializedObject, variablesProperty, true, true, true, true)
                {
                    drawHeaderCallback = (Rect rect) => {
                        EditorGUI.LabelField(rect, "Variables");
                    },

                    drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                        SerializedProperty element = variablesProperty.GetArrayElementAtIndex(index);
                        SerializedProperty variableNameProp = element.FindPropertyRelative("variableName");
                        SerializedProperty initialValueProp = element.FindPropertyRelative("initialValue");
                        SerializedProperty editorDescriptionProp = element.FindPropertyRelative("editorDescription");

                        rect.y += 2;
                        float halfWidth = rect.width / 2 - 10;

                        EditorGUI.PropertyField(
                            new Rect(rect.x, rect.y, halfWidth, EditorGUIUtility.singleLineHeight),
                            variableNameProp, GUIContent.none);

                        EditorGUI.PropertyField(
                            new Rect(rect.x + halfWidth + 10, rect.y, halfWidth, EditorGUIUtility.singleLineHeight),
                            initialValueProp, GUIContent.none);

                        if (showDescriptions)
                        {
                            rect.y += EditorGUIUtility.singleLineHeight + 2;
                            EditorGUI.PropertyField(
                                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                                editorDescriptionProp, new GUIContent("Description"));
                        }
                    }
                };

                groupLists.Add(list);
            }
        }

        private void OnGUI()
        {
            serializedObject.Update();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            showDescriptions = EditorGUILayout.Toggle("Show Descriptions", showDescriptions);

            for (int i = 0; i < groupsProperty.arraySize; i++)
            {
                groupLists[i].DoLayoutList();
            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("Save Changes"))
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(library);
            }
        }
    }
}
