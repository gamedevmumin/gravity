using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * class representing movable structure
 */
public class MovableStructure : Activable
{
    [SerializeField] private Transform transformHidden;
    [SerializeField] private Transform transformShown;
    private float interpolationRatio = 1;
    [SerializeField] private float changingSpeed;
    private int pressurePlatePressed;
    [SerializeField] private int pressurePlateRequired;

    /**
     * changes position of movable structure using lerp depending on interpolationRatio
     */
    void ChangePosition()
    {
        var x = Mathf.Lerp(transformHidden.position.x, transformShown.position.x, interpolationRatio);
        var y = Mathf.Lerp(transformHidden.position.y, transformShown.position.y, interpolationRatio);
        transform.position = new Vector2(x, y);
    }
    
    private void Update()
    {
        if (IsHiding())
        {
            interpolationRatio -= changingSpeed * Time.deltaTime;
            if(interpolationRatio<0) interpolationRatio=0;
        }
        else
        {
            interpolationRatio += changingSpeed * Time.deltaTime;
            if (interpolationRatio > 1) interpolationRatio = 1;
        }

        ChangePosition();
    }

    /*
     * returns true if enough plates are pressed
     */
    private bool IsHiding()
    {
        return pressurePlatePressed >= pressurePlateRequired;
    }
    
    /**
     * increases number of plates pressed
     */
    public override void Activate()
    {
        pressurePlatePressed++;
    }

    /**
     * decreases number of plates pressed
     */
    public override void Deactivate()
    {
        pressurePlatePressed--;
    }
}
