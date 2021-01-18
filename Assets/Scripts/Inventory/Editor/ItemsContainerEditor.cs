using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

using ARPG.Items;

namespace ARPG.Inventory
{
    [CustomEditor(typeof(ItemsContainer))]
    public class ItemsContainerEditor : Editor
    {
        ItemsContainer container;
        ReorderableList containerList;

        void OnEnable()
        {
            container = (ItemsContainer) target;
            containerList = new ReorderableList(serializedObject, serializedObject.FindProperty("itemSlots"), true, true, true, true);

            containerList.drawElementCallback = DrawElementCallback;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            containerList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = containerList.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("item"), GUIContent.none);

            if (GUI.Button(new Rect(rect.x += 220, rect.y, 100, height: EditorGUIUtility.singleLineHeight), "Equip"))
            {
                container.ItemSlots[index].item.OnUse(container.gameObject);
                serializedObject.ApplyModifiedProperties();

                EditorUtility.SetDirty(target);
            }
        }
    }
}
