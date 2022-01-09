using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableStructure : Activable
{
    [SerializeField] private Transform transformHidden;
    [SerializeField] private Transform transformShown;
    private float interpolationRatio = 1;
    [SerializeField] private float changingSpeed;
    private int pressurePlatePressed;
    [SerializeField] private int pressurePlateRequired;

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
     * returns true if enough plates is pressed
     */
    private bool IsHiding()
    {
        return pressurePlatePressed >= pressurePlateRequired;
    }
    
    public override void Activate()
    {
        Debug.Log(pressurePlatePressed);
        pressurePlatePressed++;
    }

    public override void Deactivate()
    {
        Debug.Log(pressurePlatePressed);
        pressurePlatePressed--;
    }
}
