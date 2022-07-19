using UnityEngine;

public class InventoryWindow : MonoBehaviour, IWindow
{
    [SerializeField] private GameObject inventory;

    public void Close()
    {
        inventory.SetActive(false);
    }

    public void Open()
    {
        inventory.SetActive(true);
    }
}
