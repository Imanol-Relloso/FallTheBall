using System;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        if (PlayerPrefs.GetInt("PrimeraVez", 0) == 0)
        {
            PlayerPrefs.SetInt("VibrationEnabled", 1);

            PlayerPrefs.SetInt("PrimeraVez", 1);
        }
    }

    public void LightVibration()
    {
        if(PlayerPrefs.GetInt("VibrationEnabled") == 1)
            HapticFeedback.LightFeedback();
    }
    public void MediumVibration()
    {
        if(PlayerPrefs.GetInt("VibrationEnabled") == 1)
            HapticFeedback.MediumFeedback();
    }

    public void HeavyVibration()
    {
        if(PlayerPrefs.GetInt("VibrationEnabled") == 1)
            HapticFeedback.HeavyFeedback();
    }

    public void SetVibration(bool enabled)
    {
        PlayerPrefs.SetInt("VibrationEnabled", enabled ? 1 : 0);
    }
}
