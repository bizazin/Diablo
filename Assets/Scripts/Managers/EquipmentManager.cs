using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class EquipmentManageR : MonoBehaviour
{
    #region Singleton

    public static EquipmentManageR Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private SkinnedMeshRenderer _targetMesh;

    private Equipment[] _currentEquipment;
    private Inventory _inventory;
    SkinnedMeshRenderer[] _currentMeshes;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        _inventory = Inventory.Instance;
        int numberSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numberSlots];
       _currentMeshes = new SkinnedMeshRenderer[numberSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipSlot;
        Equipment oldItem = null;
        if (_currentEquipment[slotIndex] != null)
        {
            oldItem = _currentEquipment[slotIndex];
            _inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
            onEquipmentChanged(newItem, oldItem);

        _currentEquipment[slotIndex] = newItem;

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.Mesh);
        newMesh.transform.parent = _targetMesh.transform;
        newMesh.bones = _targetMesh.bones;
        newMesh.rootBone = _targetMesh.rootBone;
        _currentMeshes[slotIndex] = newMesh;
    }
    
    
    // public void newJson()
    // {
    //     string path = Application.streamingAssetsPath+"/test.json";
    //     string fileData = File.ReadAllText(path);
    //     List<string> outjson =JsonConvert.DeserializeObject<List<string>>(fileData);
    // }

  
    
    private void Unequip(int slotIndex)
    {
        if (_currentEquipment[slotIndex] != null)
        {
            if (_currentMeshes[slotIndex] != null)
                Destroy(_currentMeshes[slotIndex].gameObject);

            Equipment oldItem = _currentEquipment[slotIndex];
            _inventory.Add(oldItem);
            _currentEquipment[slotIndex] = null;
            if (onEquipmentChanged != null)
                onEquipmentChanged(null, oldItem);
        }
    }

    private void UnequipAll()
    {
        for (int i = 0; i < _currentEquipment.Length; i++)
            Unequip(i);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

    public void SaveToFile(string[] saveEquipToJson)
    {
        string path = Application.streamingAssetsPath+"/Equipment.json";
        string str = JsonConvert.SerializeObject(saveEquipToJson);
        File.WriteAllText(path,str);
    }

    private string[] SaveEquipToJson()
    {
        var jsonEquip = new string[6];
        for (int i = 0; i < _currentEquipment.Length; i++)
        {
            if (_currentEquipment[i]!=null)
                jsonEquip[i] = _currentEquipment[i].Name;
        }
        return jsonEquip;
    }

    private void OnDisable()
    {
        SaveToFile(SaveEquipToJson());
    }
}
