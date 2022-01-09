using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    [SerializeField] private List<LevelSaveData> levels;
    public bool hasGameBeenStarted;
    public int currentLevel;
    public List<LevelSaveData> Levels => levels;
}
