using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class TestWindow : EditorWindow
{
    [MenuItem("Window/Test Window")]
    public static void ShowWindow()
    {
        GetWindow<TestWindow>("Test Window");
    }

    TestItemContainer itemContainer = null;

    void OnGUI()
    {
        if (itemContainer != null)
            Draw();
    }

    void Draw()
    {
        GUILayout.Label("Items: " + itemContainer.items.Count.ToString());

        foreach (TestItem item in itemContainer.items)
        {
            GUILayout.Label("Item: " + item.GetType().ToString());
        }

        if (GUILayout.Button("Weapon"))
        {
            if (itemContainer)
            {
                TestItem item = new TestItem();
                itemContainer.items.Add(item);
            }
        }

        if (GUILayout.Button("Armor"))
        {
            if (itemContainer)
            {
                TestItem item = new TestArmorItem();
                itemContainer.items.Add(item);
            }
        }

        if (GUILayout.Button("Potion"))
        {
            if (itemContainer)
            {
                TestItem item = new TestPotionItem();
                itemContainer.items.Add(item);
            }
        }

        SerializedObject serializedObject = new SerializedObject(itemContainer);
        SerializedProperty serializedProperty = serializedObject.FindProperty("items");


        serializedObject.Update();

        if (GUILayout.Button("Remove"))
        {
            // if (itemContainer && itemContainer.items.Count > 0)
            // {
            //     itemContainer.items.RemoveAt(itemContainer.items.Count - 1);
            // }

            serializedProperty.InsertArrayElementAtIndex(serializedProperty.arraySize);
            Debug.Log(serializedProperty.arraySize);

        }

        
        
        // EditorGUILayout.PropertyField(serializedProperty, true);
        // serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Save"))
        {
            // SerializedObject serializedObject = new SerializedObject(itemContainer);
            // SerializedProperty serializedProperty = serializedObject.FindProperty("items");
            // serializedProperty.


            serializedObject.ApplyModifiedProperties();


            // ReorderableList reorderableList = new ReorderableList(serializedObject, serializedProperty);

            // var index = reorderableList.serializedProperty.arraySize;
            // reorderableList.serializedProperty.arraySize++;
            // reorderableList.index = index;
            // reorderableList.DoLayoutList();

            // Debug.Log(reorderableList.count);

            // serializedObject.Update();
            // serializedObject.ApplyModifiedProperties();





            // Undo.RecordObject(itemContainer, "Append Item");
            // SerializedObject so = new SerializedObject(itemContainer);
            // SerializedProperty sp = so.FindProperty("items");
            // Debug.Log(sp.isArray);
            // // Debug.Log(sp.GetArrayElementAtIndex(0));
            // sp.InsertArrayElementAtIndex(0);
            // Debug.Log(sp.arraySize);
            // so.Update();
            // so.ApplyModifiedPropertiesWithoutUndo();
            
            // // Debug.Log(sp);

            
            var currentScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(currentScene);
            // PlayerPrefs.Save();
        }

        
    }

    public void OnInspectorUpdate()
    {
        if (Selection.gameObjects.Length == 1)
            itemContainer = Selection.gameObjects[0].GetComponent<TestItemContainer>();
        else
            itemContainer = null;

        Repaint();
    }
}
