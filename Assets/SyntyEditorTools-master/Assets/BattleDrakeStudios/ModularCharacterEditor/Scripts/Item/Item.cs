using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;
using bizazin;

namespace BattleDrakeStudios.ModularCharacters {
    [CreateAssetMenu(fileName = "NewItem", menuName = "Items/Base")]
    public class Item : ScriptableObject {
        
        [SerializeField] private string itemName;
       
        
        [JsonIgnore] 
        [IgnoreDataMember] [SerializeField] private Sprite _icon;
        public Equipment equipment;
        public string ItemName { get { return itemName; } set { itemName = value; } }
        
        [JsonIgnore] 
        [IgnoreDataMember] public Sprite Icon { get { return _icon; } set { _icon = value; } }
        
        public virtual void Use()
        {
            Debug.Log("Using " + itemName);
            EventsManager.OnItemPickedUp?.Invoke(equipment);
        }

        public void RemoveFromInventory()
        {
            Inventory.Instance.RemoveItem(equipment);
        }
    }
    

}

