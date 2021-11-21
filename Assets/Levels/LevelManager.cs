using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private GameEvent updateUI;
    public void OnStarCollected()
    {
        levelInfo.collectedStars++;
        updateUI.Raise();
    }
    

}
