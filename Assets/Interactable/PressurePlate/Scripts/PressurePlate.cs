using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * class representing PressurePlate
 */
public class PressurePlate : StateOwner
{
    [SerializeField] private InteractableInfo interactableInfo;
    [SerializeField] private Animator animator;

    [SerializeField] private Activable activable;
    private static readonly int Pressed = Animator.StringToHash("Pressed");

    private int objectsOnPlate = 0;
    
    /**
     * increases number of objects on plate and if there is exactly one
     * activates connected actieable
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsOnPlate++;
        if (objectsOnPlate != 1) return;
        AudioManager.Instance.PlaySound("Activate");
        activable.Activate();
        animator.SetBool(Pressed, true);
        interactableInfo.IsActive = true;
    }
    
    /**
     * decreases number of objects on plate and if there is exactly zero
     * deactivates connected activable
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        objectsOnPlate--;
        if (objectsOnPlate != 0) return;
        activable.Deactivate();
        animator.SetBool(Pressed, false);
        interactableInfo.IsActive = false;
    }
    
    /**
     * sets interactableInfo.IsActive
     * @param isActive
     */
    public override void LoadState(bool isActive)
    {
        interactableInfo.IsActive = isActive;
    }

    /**
     * checks if id of lever is equal to given id
     * @param id
     */
    public override bool CheckID(string id)
    {
        return interactableInfo.Id == id;
    }

    public override InteractableInfo GetInfo()
    {
        return interactableInfo;
    }
}
