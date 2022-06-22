using UnityEngine;

namespace BattleDrakeStudios.ModularCharacters {
    [CreateAssetMenu(fileName = "NewItem", menuName = "Items/Base")]
    public class Item : ScriptableObject 
    {
        [SerializeField] private string _itemName;
        [SerializeField] private Sprite _icon;
        public Equipment equipment;
        public string ItemName { get { return _itemName; } set { _itemName = value; } }
        public Sprite Icon { get { return _icon; } set { _icon = value; } }

        
        public virtual void Use()
        {
            Debug.Log("Using " + _itemName);
            EventsManager.OnItemPickedUp?.Invoke(this);
        }

        public void RemoveFromInventory()
        {
//            Inventory.Instance.Remove(this);
        }
    }
}

