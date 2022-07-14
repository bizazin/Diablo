using TMPro;
using UnityEngine;

public class ItemsCountView : MonoBehaviour
{
    [SerializeField] private TMP_Text countView;

    private void Update()
    {
        countView.text = $"{KeyManager.GetPrefsValue(KeyManager.ItemsCount)} / {Inventory.Instance.Space}";
    }
}
