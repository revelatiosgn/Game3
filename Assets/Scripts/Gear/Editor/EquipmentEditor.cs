using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

using ARPG.Items;
using ARPG.Inventory;
using ARPG.Gear;

[CustomEditor(typeof(Equipment))]
public class EquipmentsEditor : Editor
{
    Equipment equipment;
    ReorderableList equipmentList;
    SerializedProperty equipmentSlots;

    void OnEnable()
    {
        equipment = (Equipment) target;
        equipmentSlots = serializedObject.FindProperty("equipmentSlots");
        equipmentList = new ReorderableList(serializedObject, equipmentSlots, true, true, true, true);

        equipmentList.drawHeaderCallback = DrawHeaderCallback;
        equipmentList.onAddDropdownCallback = OnAddDropdownCallback;
        equipmentList.drawElementCallback = DrawElementCallback;
        equipmentList.elementHeightCallback = ElementHeightCallback;

        equipment.onEquip.AddListener(OnEquip);
        equipment.onUnequip.AddListener(OnUnequip);
    }

    void OnDisable()
    {
        equipment.onEquip.RemoveListener(OnEquip);
        equipment.onUnequip.RemoveListener(OnUnequip);
    }

    void OnEquip(Item item)
    {
        EditorUtility.SetDirty(target);
    }

    void OnUnequip(Item item)
    {
        EditorUtility.SetDirty(target);
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        serializedObject.Update();
        equipmentList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();


        if (EditorGUI.EndChangeCheck())
        {
        }
    }

    private void DrawHeaderCallback(Rect rect)
    {
        EditorGUI.LabelField(rect, "Equipments");
    }

    void OnAddDropdownCallback(Rect buttonRect, ReorderableList list)
    {
        TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(typeof(EquipmentSlot));
        GenericMenu menu = new GenericMenu();
        foreach (Type type in types)
        {
            menu.AddItem(new GUIContent(type.Name), false, AddItem, type);
        }
        menu.ShowAsContext();
    }

    void AddItem(object type)
    {
        int lastIndex = equipmentList.serializedProperty.arraySize;
        equipmentList.serializedProperty.InsertArrayElementAtIndex(lastIndex);

        var element = equipmentList.serializedProperty.GetArrayElementAtIndex(lastIndex);
        element.managedReferenceValue = Activator.CreateInstance((Type)type);

        serializedObject.ApplyModifiedProperties();
    }

    void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        EquipmentSlot equipmentSlot = equipment.EquipmentSlots[index];

        SerializedProperty element = equipmentList.serializedProperty.GetArrayElementAtIndex(index);
        EditorGUI.PropertyField(position:
            new Rect(rect.x += 10, rect.y, Screen.width * .8f, height: EditorGUIUtility.singleLineHeight), property:
            element, label: new GUIContent(equipmentSlot.GetSlotType().ToString()), includeChildren: true);

        if (equipmentSlot.item && !isDefaultItem(element))
        {
            float height = EditorGUI.GetPropertyHeight(equipmentList.serializedProperty.GetArrayElementAtIndex(index), true);
            GUI.Label(new Rect(rect.x + 10, rect.y + height + 5, Screen.width * .8f, height: EditorGUIUtility.singleLineHeight), equipmentSlot.item.title);
            if (GUI.Button(new Rect(rect.x + 10, rect.y + height + 5 + EditorGUIUtility.singleLineHeight, 100, height: EditorGUIUtility.singleLineHeight), "Unequip"))
            {
                equipmentSlot.item.OnUse(equipment.gameObject);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }

    float ElementHeightCallback(int index)
    {
        SerializedProperty element = equipmentList.serializedProperty.GetArrayElementAtIndex(index);
        float propertyHeight = EditorGUI.GetPropertyHeight(equipmentList.serializedProperty.GetArrayElementAtIndex(index), true);
        float spacing = EditorGUIUtility.singleLineHeight / 2;

        EquipmentSlot equipmentSlot = equipment.EquipmentSlots[index];
        if (equipmentSlot.item && !isDefaultItem(element))
        {
            propertyHeight += EditorGUIUtility.singleLineHeight * 2.0f;
        }

        return propertyHeight + spacing;
    }

    bool isDefaultItem(SerializedProperty element)
    {
        SerializedProperty prop = element.FindPropertyRelative("defaultItem");
        if (prop != null && prop.objectReferenceValue == element.FindPropertyRelative("item").objectReferenceValue)
            return true;

        return false;
    }
}