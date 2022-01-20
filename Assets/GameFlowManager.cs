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
    [SerializeField] private SoundSettings soundSettings;
    [SerializeField] private string[] sceneNames;
    private bool _hasMenuJustLoaded;
    
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

    public void Update()
    {
        if (_hasMenuJustLoaded)
        {
            _hasMenuJustLoaded = false;
            Initialize();
        }
    }
    
    public void Initialize()
    {
        var json = File.ReadAllText(Application.persistentDataPath + "/save.json");
        saveData = JsonUtility.FromJson<SaveData>(json);
        menuStartOption = GameObject.Find("MenuStartOption").GetComponent<TextMeshProUGUI>();
        menuStartOption.text = saveData.hasGameBeenStarted ? "Kontynuuj" : "Rozpocznij";
        if (saveData.currentLevel == sceneNames.Length)
        {
            menuStartOption.text = "Rozpocznij od nowa";
            saveData.Levels = new List<LevelSaveData>();
            saveData.currentLevel = 0;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            _hasMenuJustLoaded = true;
        }
    }
    
    void Start()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
        //var json = JsonUtility.ToJson(saveData);
        //File.WriteAllText(Application.persistentDataPath +"/save.json", json);
    }

    public void HandleStartClick()
    {
        if (!saveData.hasGameBeenStarted)
        {
            saveData.hasGameBeenStarted = true;
            var json = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.persistentDataPath +"/save.json", json);
            SceneManager.LoadScene("first");
        }
        else
        {
            SceneManager.LoadScene(sceneNames[saveData.currentLevel]);
        }
    }

    public void IncreaseLevel()
    {
        saveData.currentLevel++;
        var json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath +"/save.json", json);
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(sceneNames[saveData.currentLevel]);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
