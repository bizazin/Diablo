using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bizazin
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite icon;
        [SerializeField] private bool isDefaultItem;
        public string Name { get { return name; } set { name = value; } }
        public Sprite Icon { get { return icon; } set { icon = value; } }
        public bool IsDefaultItem { get { return isDefaultItem; } set { isDefaultItem = value; } }
    }
}