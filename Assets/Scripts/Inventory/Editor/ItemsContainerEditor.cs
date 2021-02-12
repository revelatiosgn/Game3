using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

using ARPG.Gear;
using ARPG.Items;

namespace ARPG.Inventory
{
    [CustomEditor(typeof(ItemsContainer))]
    public class ItemsContainerEditor : Editor
    {
        ItemsContainer container;
        ReorderableList containerList;
        int focusedElementIndex = -1;

        void OnEnable()
        {
            container = (ItemsContainer) target;
            containerList = new ReorderableList(serializedObject, serializedObject.FindProperty("itemSlots"), true, true, true, true);

            containerList.drawElementCallback = DrawElementCallback;
            containerList.onAddCallback = OnAddCallback;
            containerList.onRemoveCallback = OnRemoveCallback;
        }

        public override void OnInspectorGUI()
        {
            focusedElementIndex = -1;

            serializedObject.Update();
            containerList.DoLayoutList();

            StackEqualItems();

            serializedObject.ApplyModifiedProperties();
        }

        void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (isFocused)
                focusedElementIndex = index;

            SerializedProperty element = containerList.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("item"), GUIContent.none);

            EditorGUI.PropertyField(
                new Rect(rect.x + 220, rect.y, 50, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("count"), GUIContent.none);

            Equipment equipment = container.GetComponent<Equipment>();
            if (equipment != null)
            {
                Item item = container.ItemSlots[index].item;
                if (item as EquipmentItem)
                {
                    if (GUI.Button(new Rect(rect.x += 320, rect.y, 100, height: EditorGUIUtility.singleLineHeight), equipment.IsEquipped(item) ? "Unequip" : "Equip"))
                    {
                        item.OnUse(container.gameObject);
                        serializedObject.ApplyModifiedProperties();

                        EditorUtility.SetDirty(target);
                    }
                }
            }
        }

        void OnAddCallback(ReorderableList list)
        {
            containerList.serializedProperty.arraySize++;
            SerializedProperty newElement = containerList.serializedProperty.GetArrayElementAtIndex(containerList.serializedProperty.arraySize - 1);
            newElement.FindPropertyRelative("item").objectReferenceValue = null;
            newElement.FindPropertyRelative("count").intValue = 1;
        }

        void OnRemoveCallback(ReorderableList list)
        {
            if (0 <= focusedElementIndex && focusedElementIndex < containerList.serializedProperty.arraySize)
            {
                Item item = container.ItemSlots[focusedElementIndex].item;
                Equipment equipment = container.GetComponent<Equipment>();
                if (item as EquipmentItem && equipment != null && equipment.IsEquipped(item))
                {
                    item.OnUse(container.gameObject);
                    containerList.serializedProperty.DeleteArrayElementAtIndex(focusedElementIndex);
                }
            }
        }

        void StackEqualItems()
        {
            for (int i = 0; i < containerList.serializedProperty.arraySize; i++)
            {
                SerializedProperty elementi = containerList.serializedProperty.GetArrayElementAtIndex(i);
                for (int j = i + 1; j < containerList.serializedProperty.arraySize; j++)
                {
                    SerializedProperty elementj = containerList.serializedProperty.GetArrayElementAtIndex(j);
                    if (elementi.FindPropertyRelative("item").objectReferenceValue == elementj.FindPropertyRelative("item").objectReferenceValue)
                    {
                        Debug.Log("Stacked Items: " + i + "-" + j);
                        elementi.FindPropertyRelative("count").intValue += elementj.FindPropertyRelative("count").intValue;
                        containerList.serializedProperty.DeleteArrayElementAtIndex(j);
                        j--;
                    }
                }
                
            }
        }
    }
}
