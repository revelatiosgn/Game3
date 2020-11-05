using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    List<TestItem> items;
    ReorderableList reorderableList;

    void OnEnable()
    {
        items = ((Test) target).items;
        reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("items"), true, true, true, true);

// l   ist.drawHeaderCallback = DrawHeader;
        reorderableList.onAddDropdownCallback += OnAddDropdownCallback;
        reorderableList.drawElementCallback = DrawElementCallback;
        reorderableList.elementHeightCallback = ElementHeightCallback;
        

        TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(typeof(TestItem));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    void OnAddDropdownCallback(Rect buttonRect, ReorderableList list)
    {
        TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(typeof(TestItem));
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
        int index = reorderableList.serializedProperty.arraySize;
        reorderableList.serializedProperty.arraySize++;
        reorderableList.index = index;
        SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
        Type t = (Type) type;
        element.managedReferenceValue = Activator.CreateInstance(t);
        serializedObject.ApplyModifiedProperties();
    }

    void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
        List<TestItem> items = ((Test) target).items;

        if (index < reorderableList.serializedProperty.arraySize)
        {
            EditorGUI.PropertyField(position:
                new Rect(rect.x += 10, rect.y, Screen.width * .8f, height: EditorGUIUtility.singleLineHeight), property:
                element, label: new GUIContent(items[index].GetType().ToString()), includeChildren: true);
        }
    }

    float ElementHeightCallback(int index)
    {
        float propertyHeight = EditorGUI.GetPropertyHeight(reorderableList.serializedProperty.GetArrayElementAtIndex(index), true);
        float spacing = EditorGUIUtility.singleLineHeight / 2;

        return propertyHeight + spacing;
    }
}
