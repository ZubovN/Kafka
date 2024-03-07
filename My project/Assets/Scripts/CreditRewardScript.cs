using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using YG;
using System;

public class CreditRewardScript : MonoBehaviour
{
    [SerializeField] private Button rewardButton;
    public static Action OnButtonSound;

    private void Start()
    {
        rewardButton.onClick.AddListener(delegate { ExampleOpenRewardAd(1); });
    }
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }
    void ExampleOpenRewardAd(int id)
    {
        //OnButtonSound();
        YandexGame.RewVideoShow(id);
    }
    void Rewarded(int id)
    {
        switch (id)
        {
            case 1:
            {
                DataScenes.bMultiplier = true;
                break;
            }
            default:
                break;
        }
    }
}
