using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SoundManager : MonoBehaviour
{
    public List<AudioSource> audioSources;
    public Slider volumeSlider;
    //private float savedVolume = 0.3f; // Значение громкости по умолчанию

    void Start()
    {
        DataScenes.savedVolume = 0.25f;

        LoadVolume(); // Загружаем сохраненное значение громкости

        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    void LoadVolume()
    {
        if (PlayerPrefs.HasKey("Volume"))
            DataScenes.savedVolume = PlayerPrefs.GetFloat("Volume");
        else
            DataScenes.savedVolume = YandexGame.savesData.savedVolume;

        volumeSlider.value = DataScenes.savedVolume; // Устанавливаем значение слайдера
        ChangeVolume(); // Применяем сохраненное значение громкости
    }

    void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", DataScenes.savedVolume);
        PlayerPrefs.Save();

        YandexGame.savesData.savedVolume = DataScenes.savedVolume;
        YandexGame.SaveProgress();
    }

    void ChangeVolume()
    {
        DataScenes.savedVolume = volumeSlider.value;
        foreach (AudioSource source in audioSources)
        {
            source.volume = DataScenes.savedVolume;
        }
        SaveVolume(); // Сохраняем значение громкости
    }
}