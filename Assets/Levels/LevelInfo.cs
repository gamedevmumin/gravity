using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class LevelData
{
  public int collectedStars;
  public int starsToCollect;
  public bool isLocked;
}


[CreateAssetMenu]
public class LevelInfo : ScriptableObject,  ISerializationCallbackReceiver
{
  [SerializeField] private LevelData levelData;
  public int CollectedStars
  {
    get => levelData.collectedStars;
    set => levelData.collectedStars = value;
  }

  public int StarsToCollect
  {
    get => levelData.starsToCollect;
    set => levelData.starsToCollect = value;
  }
  
  public void OnBeforeSerialize()
  {
    
  }

  public void OnAfterDeserialize()
  {
    levelData.collectedStars = 0;
  }
}
