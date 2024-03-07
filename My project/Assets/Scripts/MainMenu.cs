using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] int credit_count;
    //[SerializeField] int credit_per_second;
    public Text moneyText;
    public Text moneyPerSecondText;

    public Text upgradeText_1, upgradeText_2, upgradeText_3, giftText;
    public Image upgradeImage_1, upgradeImage_2, upgradeImage_3, giftImage;

    public Button buttonUpgrade_1, buttonUpgrade_2, buttonUpgrade_3;
    public Button buttonGift;
    public Button rewardButton;
    
    enum BonusUpgrade
    {
        Bonus_1 = 1,
        Bonus_2 = 10,
        Bonus_3 = 100
    }

    enum UpgradeButton
    {   
        Cost1 = 150,
        Cost2 = 1500,
        Cost3 = 15000
    }


    const int GIFT_COST = 2000000;

    public AudioSource audioSource;

    public GameObject effect;
    public GameObject button;

    private void Start()
    {
        // RESET DATA TEST
        // PlayerPrefs.SetInt("credit_count", 0);
        // PlayerPrefs.SetInt("credit_per_second", 0);
        // PlayerPrefs.SetInt("credit_count_total", 0);

        // YandexGame.savesData.credit_count = 0;
        // YandexGame.savesData.credit_per_second = 0;
        // YandexGame.savesData.credit_count_total = 0;

        if (YandexGame.SDKEnabled)
        {
            DataScenes.bSDKEnable = true;

            DataScenes.credit_count = YandexGame.savesData.credit_count;
            DataScenes.credit_per_second = YandexGame.savesData.credit_per_second;
            //DataScenes.savedVolume = YandexGame.savesData.savedVolume;

            StartCoroutine(IdleScore());
        }

        audioSource = GetComponent<AudioSource>();

        buttonUpgrade_1 = GameObject.Find("ButtonUpgrade_1").GetComponent<Button>();
        buttonUpgrade_2 = GameObject.Find("ButtonUpgrade_2").GetComponent<Button>();
        buttonUpgrade_3 = GameObject.Find("ButtonUpgrade_3").GetComponent<Button>();
        buttonGift = GameObject.Find("ButtonGift").GetComponent<Button>();

        StartCoroutine(IdleFarm());
    }

    public void UpgradeButton_1()
    {
        DataScenes.ChangeCredit(false, (int) UpgradeButton.Cost1, (int) BonusUpgrade.Bonus_1);
    }

    public void UpgradeButton_2()
    {
        DataScenes.ChangeCredit(false, (int) UpgradeButton.Cost2, (int) BonusUpgrade.Bonus_2);
    }

    public void UpgradeButton_3()
    {
        DataScenes.ChangeCredit(false, (int) UpgradeButton.Cost3, (int) BonusUpgrade.Bonus_3);
    }

    public void PlayerButtonClick() 
    {
        // делаемс инкремент значения
        //DataScenes.credit_count += DataScenes.default_bonus_per_click;
        DataScenes.ChangeCredit(false);

        DataScenes.Save("credit_count");
        //PlayerPrefs.SetInt("money", DataScenes.credit_count);

        audioSource.Play();

        Instantiate(effect, button.GetComponent<RectTransform>().position.normalized, Quaternion.identity);
    }

    public void GameOverButton()
    {
        // добавить проверки сброса прогресса и сохранения в облако
        if(DataScenes.credit_count >= GIFT_COST) {
            PlayerPrefs.SetInt("credit_count", 0);
            PlayerPrefs.SetInt("credit_per_second", 0);
            //PlayerPrefs.SetInt("credit_count_total", 0);

            YandexGame.savesData.credit_count = 0;
            YandexGame.savesData.credit_per_second = 0;
            //YandexGame.savesData.credit_count_total = 0;

            SceneManager.LoadScene(1);
        }

    }

    public void RewardButton()
    {
        // логика показа рекламы и multiplier кредитов
    }

    IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1); // 1 sec delay
        //DataScenes.credit_count = DataScenes.credit_count + DataScenes.credit_per_second;
        //PlayerPrefs.SetInt("credit_count", DataScenes.credit_count);
        
        DataScenes.ChangeCredit(true);
        StartCoroutine(IdleFarm());

        //Debug.Log(DataScenes.credit_count);
    }

    IEnumerator IdleScore()
    {
        //Debug.Log("IdleScore");

        yield return new WaitForSeconds(5);

        YandexGame.NewLeaderboardScores("Score", YandexGame.savesData.credit_count_total);

        StartCoroutine(IdleScore());
    }

    // Update is called once per frame
    void Update()
    {
        ChangeButtonColor(buttonUpgrade_1, upgradeText_1, upgradeImage_1, (int) UpgradeButton.Cost1, (int) BonusUpgrade.Bonus_1, DataScenes.credit_count);
        ChangeButtonColor(buttonUpgrade_2, upgradeText_2, upgradeImage_2, (int) UpgradeButton.Cost2, (int) BonusUpgrade.Bonus_2, DataScenes.credit_count);
        ChangeButtonColor(buttonUpgrade_3, upgradeText_3, upgradeImage_3, (int) UpgradeButton.Cost3, (int) BonusUpgrade.Bonus_3, DataScenes.credit_count);

        ChangeButtonColor(buttonGift, giftText, giftImage, GIFT_COST, 0, DataScenes.credit_count);

        moneyText.text = DataScenes.credit_count.ToString();

        if(DataScenes.bMultiplier)
        {
            moneyPerSecondText.text = "Кредиты/сек " + (DataScenes.credit_per_second * DataScenes.default_multiplier).ToString();
        }
        else
        {
            moneyPerSecondText.text = "Кредиты/сек " + DataScenes.credit_per_second.ToString();
        }
    }

    private void ChangeButtonColor(Button button, Text text, Image img, int cost, int bonus, int credit)
    {
        if(button != null) {
            bool bUpgradeButton = false;
            bUpgradeButton = (credit < cost) ? false : true;

            button.interactable = bUpgradeButton;

            text.color = new Color(text.color.r, text.color.g, text.color.b, bUpgradeButton ? 1 : 0.5f);
            img.color = new Color(img.color.r, img.color.g, img.color.b, bUpgradeButton ? 1 : 0.5f);

            //text.text = "Путешествие" + cost.ToString() + "Кредиты/сек " + bonus.ToString();
        }
    }
}
