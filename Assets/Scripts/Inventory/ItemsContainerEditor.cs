﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

using ARPG.Items;
using ARPG.Inventory;

[CustomEditor(typeof(ItemsContainer))]
public class ItemsContainerEditor : Editor
{
    ReorderableList reorderableList;

    void OnEnable()
    {
        reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("itemSlots"), true, true, true, true);

        reorderableList.drawHeaderCallback = DrawHeaderCallback;
        reorderableList.onAddDropdownCallback = OnAddDropdownCallback;
        reorderableList.drawElementCallback = DrawElementCallback;
        reorderableList.elementHeightCallback = ElementHeightCallback;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawHeaderCallback(Rect rect)
    {
        EditorGUI.LabelField(rect, "Items");
    }

    void OnAddDropdownCallback(Rect buttonRect, ReorderableList list)
    {
        TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(typeof(Item));
        GenericMenu menu = new GenericMenu();
        foreach (Type type in types)
        {
            if (type.IsSealed)
                menu.AddItem(new GUIContent(type.Name), false, AddItem, type);
        }
        menu.ShowAsContext();
    }

    void AddItem(object type)
    {
        int lastIndex = reorderableList.serializedProperty.arraySize;
        reorderableList.serializedProperty.InsertArrayElementAtIndex(lastIndex);

        var element = reorderableList.serializedProperty.GetArrayElementAtIndex(lastIndex);
        element.managedReferenceValue = new ItemSlot();
        element.FindPropertyRelative("item").managedReferenceValue = Activator.CreateInstance((Type)type);

        serializedObject.ApplyModifiedProperties();
    }

    void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.PropertyField(position:
            new Rect(rect.x += 10, rect.y, Screen.width * .8f, height: EditorGUIUtility.singleLineHeight), property:
            element, label: new GUIContent("Item Slot"), includeChildren: true);
    }

    float ElementHeightCallback(int index)
    {
        float propertyHeight = EditorGUI.GetPropertyHeight(reorderableList.serializedProperty.GetArrayElementAtIndex(index), true);
        float spacing = EditorGUIUtility.singleLineHeight / 2;

        return propertyHeight + spacing;
    }
}