using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SwitchButton : StateOwner
{
    [SerializeField] SwitchButton pairedButton;
    [SerializeField] private InteractableInfo interactableInfo;

    [SerializeField] private EnableOnTrigger enableOnTrgger;
    
    private bool _isInRange; 
    [SerializeField] private Animator animator;

    private static readonly int Pressed = Animator.StringToHash("pressed");

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
        Press();
        pairedButton.Unpress();
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

    public void Press()
    {
        animator.SetBool(Pressed, true);
        interactableInfo.IsActive = true;
        _stateListener.ReactOnStateChange(interactableInfo.IsActive);
        enableOnTrgger.Disable();
    }

    public void Unpress()
    {
        animator.SetBool(Pressed, false);
        interactableInfo.IsActive = false;
        enableOnTrgger.Enable();
    }

    public override void LoadState(bool isActive)
    {
        interactableInfo.IsActive = isActive;
        animator.SetBool(Pressed, isActive);
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
