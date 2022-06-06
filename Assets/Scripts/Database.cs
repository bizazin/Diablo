using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DPUtils.Systems.ItemSystem.Scriptable_Objects.Items.Resources
{
    [CreateAssetMenu(menuName = "Inventory System/Database")]
    public class Database : ScriptableObject
    {
        [SerializeField] private List<Equipment> equipment;
        [SerializeField] private List<Equipment> heads;

        public List<Equipment> Equipment => equipment;

        [ContextMenu("Set heads")]
        //example
        public void GetHead()
        {
            foreach (var i in equipment)
            {
                if (i.EquipSlot == EquipmentSlot.Head)
                {
                    heads.Add(i);
                }
            }
        }

        [ContextMenu("Set IDs")]
        public void SetItemIDs()
        {
            equipment = new List<Equipment>();

            var foundItems = UnityEngine.Resources.LoadAll<Equipment>("Equipment").OrderBy(i => i.ID).ToList();

            var hasIDInRange = foundItems.Where(i => i.ID != -1 && i.ID < foundItems.Count).OrderBy(i => i.ID).ToList();
            var hasIDNotInRange = foundItems.Where(i => i.ID != -1 && i.ID >= foundItems.Count).OrderBy(i => i.ID)
                .ToList();
            var noID = foundItems.Where(i => i.ID == -1).ToList();

            var index = 0;
            for (int i = 0; i < foundItems.Count; i++)
            {
                Debug.Log($"Checking ID {i}");
                var itemToAdd = hasIDInRange.Find(d => d.ID == i);

                if (itemToAdd != null)
                {
                    Debug.Log($"Found item {itemToAdd} which has an id of {itemToAdd.ID}");
                    equipment.Add(itemToAdd);
                }
                else if (index < noID.Count)
                {
                    noID[index].ID = i;
                    Debug.Log($"Setting item {noID[index]} which has an invalid id to the id of {i}");
                    itemToAdd = noID[index];
                    index++;
                    equipment.Add(itemToAdd);
                }
            }
        }
    }
}