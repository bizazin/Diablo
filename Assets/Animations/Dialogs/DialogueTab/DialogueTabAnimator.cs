using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTabAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject questsContainer;
    [SerializeField] private Button button;
    private string isOpen = "IsOpen";
    private bool dialoguesState = true;

    private void Start()
    {
        button.onClick.AddListener(SwitchUIQuestsState);
        animator.GetComponent<Animator>();
    }
    private void SwitchUIQuestsState()
    {
        dialoguesState = !dialoguesState;
        animator.SetBool(isOpen,dialoguesState);
        StartCoroutine(OffButton());
    }
    private IEnumerator OffButton()
    {
        button.interactable = false;
        yield return new WaitForSeconds(.4f);
        button.interactable = true;
    }
    
    
  

    
    
}
