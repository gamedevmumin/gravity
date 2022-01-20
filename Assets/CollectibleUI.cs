using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * class representing UI that displays stars
 */
public class CollectibleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI starsAmountText;
    [SerializeField] private TextMeshProUGUI maxStarsText;
    
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private Animator textAnimator;
    private static readonly int Collected = Animator.StringToHash("Collected");

    private void Start()
    {
        UpdateUI();
    }
    
    /**
     * updates UI to match current amount of stars collected by player
     */
    public void UpdateUI()
    {
        starsAmountText.text = $"{levelInfo.CollectedStars}";
        maxStarsText.text = $"/{levelInfo.StarsToCollect}";
        textAnimator.SetTrigger(Collected);
    }
}
