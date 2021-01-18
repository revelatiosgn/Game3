using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(TestInventory))]
public class TestInventoryEditor : Editor
{
    TestInventory inventory;
    ReorderableList inventoryList;

    void OnEnable()
    {
        inventory = (TestInventory) target;
        inventoryList = new ReorderableList(serializedObject, serializedObject.FindProperty("itemSlots"), true, true, true, true);

        inventoryList.drawElementCallback = DrawElementCallback;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        inventoryList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = inventoryList.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.PropertyField(
    	    new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("statement"), GUIContent.none);

        if (GUI.Button(new Rect(rect.x += 220, rect.y, 100, height: EditorGUIUtility.singleLineHeight), "test"))
        {
        }
    }
}
