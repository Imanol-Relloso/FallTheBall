using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsUI : UIWindow
{
    [Header("Move positions")]
    [SerializeField]
    private float showPositionX = 540f;
    [SerializeField]
    private float hidePositionX = 2000f;
    
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider sfxVolumeSlider;

    void Start()
    {
        Initialize();
    }
    
    public override void Initialize()
    {
        if(hideOnStart)
            Hide(true);
        
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }
    
    public override void Show(bool instant = false)
    {
        if (instant)
        {
            canvasGroup.GetComponent<RectTransform>().DOAnchorPosX(showPositionX, 0);
            canvasRectTransform.gameObject.SetActive(true);
        }
        else
        {
            canvasRectTransform.gameObject.SetActive(true);
                
            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOAnchorPosX(showPositionX, showDuration).SetEase(showEase); 
        }

        _isShowing = true;
    }

    public override void Hide(bool instant = false)
    {
        if(instant)
        {
            canvasGroup.GetComponent<RectTransform>().DOAnchorPosX(hidePositionX, 0);
            canvasRectTransform.gameObject.SetActive(false);
        }
        else
        {
            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOAnchorPosX(hidePositionX, hideDuration).SetEase(hideEase).OnComplete(
                ()=> canvasRectTransform.gameObject.SetActive(false)); 
        }

        _isShowing = false;
    }
    
    public void Home()
    {
        UIManager.Instance.ShowWindow("MainMenu");

        Hide();
    }

    public void ChangeSFXVolume(float volume)
    {
        AudioManager.Instance.UpdateAudioMixerGroupVolume(volume, AudioType.SFX);
    }
    
    public void ChangeMusicVolume(float volume)
    {
        AudioManager.Instance.UpdateAudioMixerGroupVolume(volume, AudioType.Music);
    }

    public void ChangeVibration(bool active)
    {
        Debug.Log("Vibration = " + active);
    }
}
