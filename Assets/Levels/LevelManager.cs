using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private GameEvent updateUI;
    [SerializeField] private GameObject levelFinishedMenu;

    public void SetStars(int amount)
    {
        Debug.Log("Amount"+amount);
        levelInfo.CollectedStars=amount;
        updateUI.Raise();
    }
    
    public void OnStarCollected()
    {
        levelInfo.CollectedStars++;
        updateUI.Raise();
    }

    public void FinishLevel()
    {
        levelFinishedMenu.SetActive(true);
        GameObject.FindWithTag("Player").SetActive(false);
    }
}
