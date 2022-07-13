using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour, IWindow
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        throw new System.NotImplementedException();
    }
}
