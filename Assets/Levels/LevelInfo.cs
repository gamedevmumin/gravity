using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class LevelInfo : ScriptableObject
{
  public int collectedStars;
  public int starsToCollect;
}
