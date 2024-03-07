using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public static class DataScenes
{
    public static int credit_count = PlayerPrefs.GetInt("credit_count", 0);
    public static int credit_per_second = PlayerPrefs.GetInt("credit_per_second", 0);

    public static int credit_count_total = 0;

    public static int default_bonus_per_click = 1;

    public static int default_multiplier = 2;

    public static float savedVolume = PlayerPrefs.GetInt("Volume", 0);

    public static bool bMultiplier = false;

    public static bool bSDKEnable = false;

    public static int max_credit_count = 0;

    public static void Save(string data)
    {
        switch (data)
        {
            case "credit_count":
            {
                if (!bSDKEnable)
                {
                    PlayerPrefs.SetInt("credit_count", credit_count);
                    break;
                }

                YandexGame.savesData.credit_count = credit_count;
                YandexGame.SaveProgress();

                break;
            }
            case "credit_per_second":
            {
                if (!bSDKEnable)
                {
                    PlayerPrefs.SetInt("credit_per_second", credit_per_second);
                    break;
                }

                YandexGame.savesData.credit_per_second = credit_per_second;
                YandexGame.SaveProgress();

                break;
            }
            case "credit_count_total":
            {
                if (!bSDKEnable)
                {
                    PlayerPrefs.SetInt("credit_count_total", credit_count_total);
                    break;
                }

                YandexGame.savesData.credit_count_total = credit_count_total;
                YandexGame.SaveProgress();

                break;
            }
            default: break;
        }
    }

    public static void ChangeCredit(bool bFarm, int cost = 0, int reward = 0)
    {        
        if(credit_count >= cost) {
            credit_count -= cost;
            credit_per_second += reward;
        }

        if(!bFarm)
        {
            if (!bMultiplier)
            {
                credit_count += default_bonus_per_click;
            }
            else
            {
                credit_count += default_bonus_per_click * default_multiplier;
            }
        }
        else 
        {
            if(!bMultiplier)
            {
                credit_count += credit_per_second;
            }
            else
            {
                credit_count += credit_per_second * default_multiplier;
            }
        }

        if(credit_count > max_credit_count) {
            max_credit_count = credit_count;
            credit_count_total = max_credit_count;
        }

        //Debug.Log("credit_count: " + credit_count);
        //Debug.Log("credit_count_total 2222222: " + credit_count_total);

        DataScenes.Save("credit_count");
        DataScenes.Save("credit_count_total");
        DataScenes.Save("credit_per_second");
    }
}
