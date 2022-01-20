using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsHandler : MonoBehaviour
{
    /**
     * handles start button click in menu
     */
    public void HandleStartClick()
    {
        GameFlowManager.Instance.HandleStartClick();
    }

    /**
     * handles settings button click in menu
     */
    public void HandleSettingsClick()
    {
        GameFlowManager.Instance.LoadSettingsMenu();
    }

    /**
     * handles exit button click in menu
     */
    public void HandleExitClick()
    {
        GameFlowManager.Instance.ExitGame();
    }
}
