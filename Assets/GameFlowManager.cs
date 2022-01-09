using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; set; }

    private enum State { MainMenu, Game }
    [SerializeField] private SaveData saveData;
    [SerializeField] private TextMeshProUGUI menuStartOption;

    public SaveData SaveData
    {
        get => saveData;
        set => saveData = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        menuStartOption.text = saveData.hasGameBeenStarted ? "Continue" : "Start";
        var json = File.ReadAllText(Application.persistentDataPath + "/save.json");
        saveData = JsonUtility.FromJson<SaveData>(json);
        //var json = JsonUtility.ToJson(saveData);
        //File.WriteAllText(Application.persistentDataPath +"/save.json", json);
    }

    public void HandleStartClick()
    {
       // if (!saveData.hasGameBeenStarted)
        {
            saveData.hasGameBeenStarted = true;
            //save game
            SceneManager.LoadScene("first");
        }
    }
}
