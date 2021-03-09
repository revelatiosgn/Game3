using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

using ARPG.Items;
using ARPG.Inventory;
using ARPG.Gear;
using ARPG.Events;

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
        // equipmentList.onAddDropdownCallback = OnAddDropdownCallback;
        equipmentList.onAddCallback = OnAddCallback;
        equipmentList.drawElementCallback = DrawElementCallback;
        equipmentList.elementHeightCallback = ElementHeightCallback;
        equipmentList.footerHeight = 100f;

        ((EquipmentItemEvent) serializedObject.FindProperty("onEquip").objectReferenceValue).onEventRaised += OnEquip;
        ((EquipmentItemEvent) serializedObject.FindProperty("onUnequip").objectReferenceValue).onEventRaised += OnUnequip;
    }

    void OnDisable()
    {
        ((EquipmentItemEvent) serializedObject.FindProperty("onEquip").objectReferenceValue).onEventRaised -= OnEquip;
        ((EquipmentItemEvent) serializedObject.FindProperty("onUnequip").objectReferenceValue).onEventRaised -= OnUnequip;
    }

    void OnEquip(EquipmentItem item, GameObject target)
    {
        if (equipment.gameObject == target)
            EditorUtility.SetDirty(this.target);
    }

    void OnUnequip(EquipmentItem item, GameObject target)
    {
        if (equipment.gameObject == target)
            EditorUtility.SetDirty(this.target);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        equipmentList.DoLayoutList();

        float height = equipmentList.GetHeight() - 90f;

        EditorGUI.LabelField(new Rect(20f, height, 100f, height: EditorGUIUtility.singleLineHeight), "On Equip");
        EditorGUI.PropertyField(position:
        new Rect(110f, height, 200f, height: EditorGUIUtility.singleLineHeight), property:
            serializedObject.FindProperty("onEquip"), GUIContent.none, includeChildren: true);

        height += 25f;
        EditorGUI.LabelField(new Rect(20f, height, 100f, height: EditorGUIUtility.singleLineHeight), "On Unequip");
        EditorGUI.PropertyField(position:
        new Rect(110f, height, 200f, height: EditorGUIUtility.singleLineHeight), property:
            serializedObject.FindProperty("onUnequip"), GUIContent.none, includeChildren: true);

        EditorGUI.BeginChangeCheck();

        // height += 25f;
        // EditorGUI.LabelField(new Rect(20f, height, 100f, height: EditorGUIUtility.singleLineHeight), "Skin");
        // EditorGUI.PropertyField(position:
        // new Rect(110f, height, 200f, height: EditorGUIUtility.singleLineHeight), property:
        //     serializedObject.FindProperty("skinMaterial"), GUIContent.none, includeChildren: true);


        // height += 25f;
        // EditorGUI.LabelField(new Rect(20f, height, 100f, height: EditorGUIUtility.singleLineHeight), "Hair");
        // EditorGUI.PropertyField(position:
        // new Rect(110f, height, 200f, height: EditorGUIUtility.singleLineHeight), property:
        //     serializedObject.FindProperty("hairMaterial"), GUIContent.none, includeChildren: true);

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            // equipment.UpdateMaterials();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void DrawHeaderCallback(Rect rect)
    {
        EditorGUI.LabelField(rect, "Equipments");
    }

    void OnAddCallback(ReorderableList list)
    {
        int lastIndex = equipmentList.serializedProperty.arraySize;
        equipmentList.serializedProperty.InsertArrayElementAtIndex(lastIndex);

        var element = equipmentList.serializedProperty.GetArrayElementAtIndex(lastIndex);
        element.managedReferenceValue = new EquipmentWeaponSlot();

        serializedObject.ApplyModifiedProperties();
    }

    // void OnAddDropdownCallback(Rect buttonRect, ReorderableList list)
    // {
    //     TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(typeof(EquipmentSlot));
    //     GenericMenu menu = new GenericMenu();
    //     foreach (Type type in types)
    //     {
    //         if (type.IsSealed)
    //             menu.AddItem(new GUIContent(type.Name), false, AddItem, type);
    //     }
    //     menu.ShowAsContext();
    // }

    void AddItem(object type)
    {
        int lastIndex = equipmentList.serializedProperty.arraySize;
        equipmentList.serializedProperty.InsertArrayElementAtIndex(lastIndex);

        var element = equipmentList.serializedProperty.GetArrayElementAtIndex(lastIndex);
        element.managedReferenceValue = Activator.CreateInstance((Type)type);

        serializedObject.ApplyModifiedProperties();
        // equipment.UpdateMaterials();
    }

    void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        EquipmentSlot equipmentSlot = equipment.EquipmentSlots[index];

        SerializedProperty element = equipmentList.serializedProperty.GetArrayElementAtIndex(index);
        EquipmentSlot.Type type = equipmentSlot.type;

        EditorGUI.BeginChangeCheck();

        string label = type.ToString();
        EditorGUI.PropertyField(position:
            new Rect(rect.x += 10, rect.y, Screen.width * .8f, height: EditorGUIUtility.singleLineHeight), property:
            element, label: new GUIContent(label), includeChildren: true);

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();

            if (type != equipmentSlot.type)
            {
                switch (equipmentSlot.type)
                {
                    case EquipmentSlot.Type.Weapon:
                        element.managedReferenceValue = new EquipmentWeaponSlot();
                        break;

                    case EquipmentSlot.Type.Armor:
                        element.managedReferenceValue = new EquipmentArmorSlot();
                        break;

                    case EquipmentSlot.Type.Shield:
                        element.managedReferenceValue = new EquipmentShieldSlot();
                        break;
                }
            }

            equipmentSlot.Update(equipment);
        }
    }

    float ElementHeightCallback(int index)
    {
        return EditorGUI.GetPropertyHeight(equipmentList.serializedProperty.GetArrayElementAtIndex(index), true);
    }
}