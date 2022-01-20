using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * class representing data to save
 */
[System.Serializable]
public class SaveData
{
    [SerializeField] private List<LevelSaveData> levels;
    public bool hasGameBeenStarted;
    public int currentLevel;
    public List<LevelSaveData> Levels
    {
        get { return levels; }
        set { levels = value; }
    }
}
