using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private JumpHandler _JumpHandler;

    public void OnPointerDown(PointerEventData eventData)
    {
        _JumpHandler.HandleJump();
    }
}
