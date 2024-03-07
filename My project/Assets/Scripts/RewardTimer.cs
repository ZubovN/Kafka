using UnityEngine;
using UnityEngine.UI;

public class RewardTimer : MonoBehaviour
{
    public Button buttonReward;
    public Text textReward;
    public Image imgReward;
    public float timerDuration = 30f; // Длительность таймера в секундах
    private float timer;
    private bool buttonActive = true;

    void Start()
    {
        buttonReward.onClick.AddListener(StartTimer);
    }

    void Update()
    {
        if (buttonActive)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                DataScenes.bMultiplier = false;

                ChangeButtonColor(buttonReward, textReward, imgReward, false, true);
            }
        }
    }

    void StartTimer()
    {
        ChangeButtonColor(buttonReward, textReward, imgReward, true, false);
        timer = timerDuration;
    }

    private void ChangeButtonColor(Button button, Text text, Image img, bool active, bool interactable)
    {
        if(button != null) {
            Debug.Log("ButtonActive: " + buttonActive);

            buttonActive = active;
            button.interactable = interactable;

            text.color = new Color(text.color.r, text.color.g, text.color.b, buttonActive ? 0.5f : 1);
            img.color = new Color(img.color.r, img.color.g, img.color.b, buttonActive ? 0.5f : 1);
        }
    }
}