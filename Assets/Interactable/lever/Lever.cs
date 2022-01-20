using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * class that handles lever logic
 */
public class Lever : StateOwner
{
    [SerializeField] private InteractableInfo interactableInfo;

    private bool _isInRange;

    [SerializeField] private Animator animator;

    private static readonly int Pulled = Animator.StringToHash("pulled");

    private IStateListener _stateListener;
    // Start is called before the first frame update
    private void Start()
    {
        _stateListener = GetComponent<IStateListener>();
    }

    /**
     * if E button is pressed and player is in range it calls ReactOnStateChange
     * function of connected state listener
     */
    private void Update()
    {
        if (!_isInRange || !Input.GetKeyDown(KeyCode.E)) return;
        AudioManager.Instance.PlaySound("Activate");
        animator.SetTrigger(Pulled);
        interactableInfo.IsActive = !interactableInfo.IsActive;
        _stateListener.ReactOnStateChange(interactableInfo.IsActive);
    }

    /**
     * if player entered trigger sets _isInRange to true
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }
    
    /**
     * if player exited trigger sets _isInRange to false
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }

    /**
     * sets IsActive field of interactableInfo and sets trigger of animator
     * @param isActive - value to set interactableInfo.IsActive to
     */
    public override void LoadState(bool isActive)
    {
        interactableInfo.IsActive = isActive;
        animator.SetTrigger(Pulled);
    }

    /**
     * checks if id of lever is equal to given id
     * @param id
     */
    public override bool CheckID(string id)
    {
        return id == interactableInfo.Id;
    }
    
    /**
     * returns interactableInfo
     */
    public override InteractableInfo GetInfo()
    {
        return interactableInfo;
    }
}
