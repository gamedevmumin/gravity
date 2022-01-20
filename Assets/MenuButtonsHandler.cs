using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsHandler : MonoBehaviour
{
    public void HandleStartClick()
    {
        GameFlowManager.Instance.HandleStartClick();
    }

    public void HandleSettingsClick()
    {
        GameFlowManager.Instance.LoadSettingsMenu();
    }

    public void HandleExitClick()
    {
        GameFlowManager.Instance.ExitGame();
    }
}
