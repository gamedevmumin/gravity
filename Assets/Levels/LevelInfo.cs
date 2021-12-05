using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class LevelInfo : ScriptableObject,  ISerializationCallbackReceiver
{
  public int collectedStars;
  public int starsToCollect;


  public void OnBeforeSerialize()
  {
    
  }

  public void OnAfterDeserialize()
  {
    collectedStars = 0;
  }
}
