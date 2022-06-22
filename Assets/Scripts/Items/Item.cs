using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bizazin
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [SerializeField] private Sprite _icon;
        [SerializeField] private bool _isDefaultItem;
        public string ItemName { get { return _itemName; } set { _itemName = value; } }
        public Sprite Icon { get { return _icon; } set { _icon = value; } }
        public bool IsDefaultItem { get { return _isDefaultItem; } set { _isDefaultItem = value; } }
    }
}