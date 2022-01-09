using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using Collectibles.Star.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController.Scripts.PlayerController playerControllerPrefab;
    [SerializeField] private PlayerController.Scripts.PlayerController playerController;

    [SerializeField] private Checkpoint currentCheckpoint;

    [SerializeField] private float timeToSpawn;

    [SerializeField] private CinemachineVirtualCamera currentCamera;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Transform starsParent;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private List<Room.Room> rooms;
    [SerializeField] private List<StateOwner> stateOwners;
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private ScreeneFreezer screenFreezer;
    
    private bool loaded;
    private void Update()
    {
        if (loaded) return;
        LoadGame();
        loaded = true;
    }
    
    private void LoadGame()
    {
        var levels = GameFlowManager.Instance.SaveData.Levels;
        var levelSaveData = levels.Find(level => level.sceneName == SceneManager.GetActiveScene().name);
        if (levelSaveData != null)
        {
            for (int i = 0; i < starsParent.childCount; i++)
            {
                Destroy(starsParent.GetChild(i).gameObject);
            }
            levelManager.SetStars(levelSaveData.levelData.collectedStars);
            foreach (var starLocation in levelSaveData.starsLocations)
            {
                var star = Instantiate(starPrefab, starLocation, Quaternion.identity).transform;
                star.parent = starsParent;
                star.localPosition = starLocation;
                star.GetComponent<Collectible>().Initialize(screenFreezer);
            }

            foreach (var roomInfo in levelSaveData.roomInfos)
            {
                var foundRoom = rooms.Find(room => room.RoomInfo.Id == roomInfo.Id);
                if (foundRoom)
                {
                    foundRoom.ChangeRoomGravity(roomInfo.Gravity);
                }
            }

            foreach (var interactableInfo in levelSaveData.interactableInfos)
            {
                var foundInteractable = stateOwners.Find(owner => owner.CheckID(interactableInfo.Id));
                foundInteractable.LoadState(interactableInfo.IsActive);
            }

            playerController =
                Instantiate(playerControllerPrefab, levelSaveData.checkpointLocation, Quaternion.identity);
        }
        else
        {
            playerController =
                Instantiate(playerControllerPrefab, currentCheckpoint.transform.position, Quaternion.identity);
        }
    }

    public void SaveGame()
    {
        var saveData = GameFlowManager.Instance.SaveData;
        var levelSaveData = new LevelSaveData();
        Debug.Log(levelInfo.CollectedStars);
        levelSaveData.levelData = new LevelData {collectedStars = levelInfo.CollectedStars};
        
        for (var i = 0; i < starsParent.childCount; i++)
        { 
            levelSaveData.starsLocations.Add(starsParent.GetChild(i).localPosition);
        }

        levelSaveData.checkpointLocation = currentCheckpoint.transform.position;

        foreach (var stateOwner in stateOwners)
        {
            levelSaveData.interactableInfos.Add(stateOwner.GetInfo());
        }

        foreach (var room in rooms)
        {
            levelSaveData.roomInfos.Add(room.RoomInfo);
        }
        
        var item = saveData.Levels.Find(level => level.sceneName == SceneManager.GetActiveScene().name);
        if (item != null)
        {
            levelSaveData.sceneName = SceneManager.GetActiveScene().name;
            var indexOfLevel = saveData.Levels.IndexOf(item);
            saveData.Levels[indexOfLevel] = levelSaveData;
        }
        else
        {
            Debug.Log("no item found");
            levelSaveData.sceneName = SceneManager.GetActiveScene().name;
            Debug.Log(levelSaveData.sceneName);
            saveData.Levels.Add(levelSaveData);
        }
        var json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath +"/save.json", json);
    }
    
    public void OnCheckpointEntered(Checkpoint checkpoint)
    {
        if (currentCheckpoint == checkpoint) return;
        currentCheckpoint = checkpoint;
        SaveGame();
    }

    public void OnPlayerDeath()
    {
        StartCoroutine(DelayBeforeRespawn());
    }

    private IEnumerator DelayBeforeRespawn()
    {
        yield return new WaitForSeconds(timeToSpawn);
        playerController =
            Instantiate(playerControllerPrefab, currentCheckpoint.transform.position, Quaternion.identity);
    }

    public void SwitchRoom(CinemachineVirtualCamera roomCamera)
    {
        StartCoroutine(SwitchRoomDelay(roomCamera));
    }
    
    public void SwitchCamera(CinemachineVirtualCamera roomCamera)
    {
        currentCamera.Priority = 0;
        currentCamera = roomCamera;
        currentCamera.Priority = 1;
        var cameraController = currentCamera.GetComponent<CameraController>();
        if (cameraController)
        {
            cameraController.FollowPlayer(playerController);
        }
    }
    private IEnumerator SwitchRoomDelay(CinemachineVirtualCamera roomCamera)
    {
        SwitchCamera(roomCamera);
        playerController.SetPaused(true);
        yield return new WaitForSeconds(0f);
        playerController.SetPaused(false);
    }
    
}
