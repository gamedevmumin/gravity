using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * class representing manager that manages general flow of the game
 */
public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; set; }

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

    /**
     * makes object singleton - if there is already an instance of it
     * it is destroyed
     */
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
    
    /**
     * initializes menu - loads save file and sets
     * UI options accordingly
     */
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

    /**
     * sets _hasMenuJustLoaded to true to refresh man menu options if entered from other scene
     */
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
    }

    /**
     * handles click of start button - it sets hasGameBeenStarted to true and
     * loads first scene
     */
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

    /**
     * increases currentLevel and saves it to file
     */
    public void IncreaseLevel()
    {
        saveData.currentLevel++;
        var json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath +"/save.json", json);
    }

    /**
     * loads current level
     */
    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(sceneNames[saveData.currentLevel]);
    }

    /**
     * loads main menu
     */
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /**
     * loads settings menu
     */
    public void LoadSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    /**
     * exits game
     */
    public void ExitGame()
    {
        Application.Quit();
    }
}
