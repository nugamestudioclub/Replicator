using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;

namespace Time
{
    [CustomEditor(typeof(GameOperation))]
    public class GameOperationEditor : Editor
    {
        SerializedProperty library;
        SerializedProperty operations;
        ReorderableList operationsList;
        Dictionary<string, int> variableNameToIndexMap;
        string[] variableNames;

        private void OnEnable()
        {
            library = serializedObject.FindProperty("library");
            operations = serializedObject.FindProperty("operations");
            
            operationsList = new ReorderableList(serializedObject, operations, true, true, true, true)
            {
                drawHeaderCallback = rect =>
                {
                    EditorGUI.LabelField(rect, "Operations: [input] [operation] [output]");
                    
                },
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var element = operations.GetArrayElementAtIndex(index);
                    rect.y += 2;
                    float width = rect.width;
     
                    SerializedProperty op = element.FindPropertyRelative("op");
                    SerializedProperty inputVar = element.FindPropertyRelative("inputVar");
                    SerializedProperty outputVar = element.FindPropertyRelative("outputVar");

                    // Draw operation enum popup
                    EditorGUI.PropertyField(
                        new Rect(rect.x, rect.y, width * 0.3f, EditorGUIUtility.singleLineHeight),
                        op, GUIContent.none);

                    // Draw input variable dropdown
                    int inputIndex = variableNameToIndexMap.TryGetValue(inputVar.stringValue, out inputIndex) ? inputIndex : 0;
                    inputIndex = EditorGUI.Popup(
                        new Rect(rect.x + width * 0.3f, rect.y, width * 0.35f, EditorGUIUtility.singleLineHeight),
                        inputIndex, variableNames);
                    inputVar.stringValue = variableNames[inputIndex];

                    // Draw output variable dropdown
                    int outputIndex = variableNameToIndexMap.TryGetValue(outputVar.stringValue, out outputIndex) ? outputIndex : 0;
                    outputIndex = EditorGUI.Popup(
                        new Rect(rect.x + width * 0.65f, rect.y, width * 0.35f, EditorGUIUtility.singleLineHeight),
                        outputIndex, variableNames);
                    outputVar.stringValue = variableNames[outputIndex];
                },
                onAddCallback = list =>
                {
                    int index = list.serializedProperty.arraySize;
                    list.serializedProperty.arraySize++;
                    list.index = index;
                    SerializedProperty newOperation = list.serializedProperty.GetArrayElementAtIndex(index);

                    // Initialize the new operation's properties
                    newOperation.FindPropertyRelative("op").enumValueIndex = 0; // Default to the first enum value
                    newOperation.FindPropertyRelative("inputVar").stringValue = "";
                    newOperation.FindPropertyRelative("outputVar").stringValue = "";
                },
                onRemoveCallback = list =>
                {
                    if (EditorUtility.DisplayDialog("Warning",
                        "Are you sure you want to delete this operation?", "Yes", "No"))
                    {
                        ReorderableList.defaultBehaviours.DoRemoveButton(list);
                    }
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(library);

            VariableLibrary lib = library.objectReferenceValue as VariableLibrary;
            if (lib != null && lib.GetMappings() != null)
            {
                UpdateVariableNames(lib);
                operationsList.DoLayoutList();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void UpdateVariableNames(VariableLibrary library)
        {
            variableNameToIndexMap = new Dictionary<string, int>();
            variableNames = new string[library.GetMappings().Length];
            int i = 0;
            foreach (var kvp in library.GetMappings())
            {
                variableNames[i] = kvp.Key;
                variableNameToIndexMap[kvp.Key] = i;
                i++;
            }
        }
    }
}
