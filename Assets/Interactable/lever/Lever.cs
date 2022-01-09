using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

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

    // Update is called once per frame
    private void Update()
    {
        if (!_isInRange || !Input.GetKeyDown(KeyCode.E)) return;
        animator.SetTrigger(Pulled);
        interactableInfo.IsActive = !interactableInfo.IsActive;
        _stateListener.ReactOnStateChange(interactableInfo.IsActive);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }

    public override void LoadState(bool isActive)
    {
        interactableInfo.IsActive = isActive;
        animator.SetTrigger(Pulled);
    }

    public override bool CheckID(string id)
    {
        return id == interactableInfo.Id;
    }
    
    public override InteractableInfo GetInfo()
    {
        return interactableInfo;
    }
}
