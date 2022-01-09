using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PressurePlate : StateOwner
{
    [SerializeField] private InteractableInfo interactableInfo;
    [SerializeField] private Animator animator;

    [SerializeField] private Activable activable;
    private static readonly int Pressed = Animator.StringToHash("Pressed");

    private int objectsOnPlate = 0;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsOnPlate++;
        if (objectsOnPlate != 1) return;
        activable.Activate();
        animator.SetBool(Pressed, true);
        interactableInfo.IsActive = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        objectsOnPlate--;
        if (objectsOnPlate != 0) return;
        activable.Deactivate();
        animator.SetBool(Pressed, false);
        interactableInfo.IsActive = false;
    }
    
    public override void LoadState(bool isActive)
    {
        interactableInfo.IsActive = isActive;
    }

    public override bool CheckID(string id)
    {
        return interactableInfo.Id == id;
    }

    public override InteractableInfo GetInfo()
    {
        return interactableInfo;
    }
}
