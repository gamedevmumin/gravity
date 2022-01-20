using System.Collections;
using UnityEngine;

/**
 * class responsible for freezing game for given time
 */
public class ScreeneFreezer : MonoBehaviour
{
    private bool isFrozen = false;
    private float duration;
    private bool shouldFreeze = false;

    /**
     * sets shouldFreeze to true and duration for given in param
     * @param duration
     */
    public void Freeze(float duration)
    {
        if (shouldFreeze != true)
        {
            shouldFreeze = true;
            this.duration = duration;
        }
    }

    void Update()
    {
        if(shouldFreeze && !isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }

    /**
     * sets Time.timeScale to 0 for time of field duration to imitate freezing of screen
     */
    private IEnumerator DoFreeze()
    {
        isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        isFrozen = false;
        shouldFreeze = false;
    }
}
