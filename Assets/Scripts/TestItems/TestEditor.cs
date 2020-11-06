using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    List<TestItemSlot> itemSlots;
    ReorderableList reorderableList;

    void OnEnable()
    {

        itemSlots = ((Test) target).itemSlots;
        reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("itemSlots"), true, true, true, true);

// l   ist.drawHeaderCallback = DrawHeader;
        reorderableList.onAddDropdownCallback = OnAddDropdownCallback;
        reorderableList.drawElementCallback = DrawElementCallback;
        reorderableList.elementHeightCallback = ElementHeightCallback;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        reorderableList.DoLayoutList();

        // Debug.Log(itemSlots.Count + " " + reorderableList.serializedProperty.arraySize);

        // for (int i = 0; i < itemSlots.Count; i++)
        // {
        //     var el = reorderableList.serializedProperty.GetArrayElementAtIndex(i);
        //     Debug.Log(el.FindPropertyRelative("item").managedReferenceFullTypename);
        // }

        // if (itemSlots.Count > reorderableList.serializedProperty.arraySize)
        // {
            // int lastIndex = reorderableList.serializedProperty.arraySize;
            // reorderableList.serializedProperty.InsertArrayElementAtIndex(lastIndex);
        // }

        serializedObject.ApplyModifiedProperties();
    }

    void OnAddDropdownCallback(Rect buttonRect, ReorderableList list)
    {
        TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(typeof(TestItem));
        GenericMenu menu = new GenericMenu();
        foreach (Type type in types)
        {
            menu.AddItem(new GUIContent(type.Name), false, AddItem, type);
        }
        menu.ShowAsContext();
    }

    void AddItem(object type)
    {
        int lastIndex = reorderableList.serializedProperty.arraySize;
        reorderableList.serializedProperty.InsertArrayElementAtIndex(lastIndex);
        var element = reorderableList.serializedProperty.GetArrayElementAtIndex(lastIndex);
        element.managedReferenceValue = new TestItemSlot();

        Type t = (Type) type;
        element.FindPropertyRelative("item").managedReferenceValue = Activator.CreateInstance(t);

        Debug.Log(element.managedReferenceFullTypename);

        // element.FindPropertyRelative("item").managedReferenceValue = new TestPotion();

        

        // TestItemSlot itemSlot = new TestItemSlot();
        // itemSlot.item = new TestPotion();
        // itemSlots.Add(itemSlot);
        // Debug.Log(t.Name + " " + itemSlots.Count);


        serializedObject.ApplyModifiedProperties();
    }

    void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.PropertyField(position:
            new Rect(rect.x += 10, rect.y, Screen.width * .8f, height: EditorGUIUtility.singleLineHeight), property:
            element, label: new GUIContent("ItemSlot"), includeChildren: true);
    }

    float ElementHeightCallback(int index)
    {
        float propertyHeight = EditorGUI.GetPropertyHeight(reorderableList.serializedProperty.GetArrayElementAtIndex(index), true);
        float spacing = EditorGUIUtility.singleLineHeight / 2;

        return propertyHeight + spacing;
    }
}
