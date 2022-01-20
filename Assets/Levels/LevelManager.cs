using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private GameEvent updateUI;
    [SerializeField] private GameObject levelFinishedMenu;
    private bool isFinished;

    private void Update()
    {
        if (isFinished && Input.GetKeyDown(KeyCode.Space))
        {
            GameFlowManager.Instance.LoadCurrentLevel();
        }
        if(isFinished && Input.GetKeyDown(KeyCode.Escape))
        {
            GameFlowManager.Instance.LoadMenu();
        }
    }
    
    public void SetStars(int amount)
    {
        levelInfo.CollectedStars=amount;
        if (levelInfo.CollectedStars == levelInfo.StarsToCollect)
        {
            AudioManager.Instance.PlaySound("AllCollected");
        }
        updateUI.Raise();
    }
    
    public void OnStarCollected()
    {
        levelInfo.CollectedStars++;
        updateUI.Raise();
    }

    public void FinishLevel()
    {
        isFinished = true;
        levelFinishedMenu.SetActive(true);
        GameObject.FindWithTag("Player").SetActive(false);
    }
    
    public void LoadMenu()
    {
        GameFlowManager.Instance.LoadMenu();
    }

    public void ExitGame()
    {
        GameFlowManager.Instance.ExitGame();
    }
}
