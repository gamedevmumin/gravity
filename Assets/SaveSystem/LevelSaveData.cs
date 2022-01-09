using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSaveData
{
    [SerializeField] public string sceneName;
    [SerializeField] public List<Vector3> starsLocations = new List<Vector3>();
    [SerializeField] public List<InteractableInfo> interactableInfos = new List<InteractableInfo>();
    [SerializeField] public List<RoomInfo> roomInfos = new List<RoomInfo>();
    [SerializeField] public LevelData levelData;
    [SerializeField] public Vector3 checkpointLocation;
}
