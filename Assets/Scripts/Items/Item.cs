using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
[JsonObject(MemberSerialization.OptIn)]
public class Item : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite icon;
    [SerializeField] private bool isNew = true;
    [JsonProperty][SerializeField] private ItemStats stats;

    public string Name { get { return name; } set { name = value; } }
    public Sprite Icon { get { return icon; } set { icon = value; } }
    public bool IsNew { get { return isNew; } set { isNew = value; } }
    public ItemStats Stats { get { return stats; } set { stats = value; } }
}