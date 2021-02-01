using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

namespace ARPG.AI
{
    [CustomEditor(typeof(AIState), true)]
    public class AIStateEditor : Editor
    {
        AIState aiState;
        ReorderableList aiActionsList;
        ReorderableList aiTransitionsList;

        void OnEnable()
        {
            aiState = (AIState) target;
            aiActionsList = new ReorderableList(serializedObject, serializedObject.FindProperty("actions"), true, true, true, true);
            aiActionsList.drawHeaderCallback = (Rect rect) => { EditorGUI.LabelField(rect, "Actions"); };
            aiActionsList.drawElementCallback = DrawElementActionsCallback;
            aiActionsList.elementHeightCallback = ElementHeightActionsCallback;
            aiTransitionsList = new ReorderableList(serializedObject, serializedObject.FindProperty("transitions"), true, true, true, true);
            aiTransitionsList.drawHeaderCallback = (Rect rect) => { EditorGUI.LabelField(rect, "Transitions"); };
            aiTransitionsList.drawElementCallback = DrawElementTransitionsCallback;
            aiTransitionsList.elementHeightCallback = ElementHeightTransitionsCallback;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            aiActionsList.DoLayoutList();
            aiTransitionsList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        void DrawElementActionsCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            DrawElement(aiActionsList, rect, index, isActive, isFocused);
        }

        float ElementHeightActionsCallback(int index)
        {
            return ElementHeight(aiActionsList, index);
        }

        void DrawElementTransitionsCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            DrawElement(aiTransitionsList, rect, index, isActive, isFocused);
        }

        float ElementHeightTransitionsCallback(int index)
        {
            return ElementHeight(aiTransitionsList, index);
        }

        void DrawElement(ReorderableList list, Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(position:
                new Rect(rect.x += 10, rect.y, Screen.width * .8f, height: EditorGUIUtility.singleLineHeight), property:
                element, label: GUIContent.none, includeChildren: true);
        }

        float ElementHeight(ReorderableList list, int index)
        {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            float propertyHeight = EditorGUI.GetPropertyHeight(element, true);

            return propertyHeight;
        }
    }
}
