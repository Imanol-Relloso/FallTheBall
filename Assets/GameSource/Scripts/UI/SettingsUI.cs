using UnityEngine;
using DG.Tweening;

public class SettingsUI : UIWindow
{
    [Header("Move positions")]
    [SerializeField]
    private float showPositionX = 540f;
    [SerializeField]
    private float hidePositionX = 2000f;

    void Start()
    {
        Initialize();
    }
    
    public override void Initialize()
    {
        if(hideOnStart)
            Hide(true);
    }
    
    public override void Show(bool instant = false)
    {
        if (instant)
        {
            canvasGroup.GetComponent<RectTransform>().DOMoveX(showPositionX, 0);
            canvasRectTransform.gameObject.SetActive(true);
        }
        else
        {
            canvasRectTransform.gameObject.SetActive(true);
                
            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOMoveX(showPositionX, showDuration).SetEase(showEase); 
        }

        _isShowing = true;
    }

    public override void Hide(bool instant = false)
    {
        if(instant)
        {
            canvasGroup.GetComponent<RectTransform>().DOMoveX(hidePositionX, 0);
            canvasRectTransform.gameObject.SetActive(false);
        }
        else
        {
            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOMoveX(hidePositionX, hideDuration).SetEase(hideEase).OnComplete(
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
        Debug.Log("SFX volume: " + volume);
    }
    
    public void ChangeMusicVolume(float volume)
    {
        Debug.Log("Music volume = " + volume);
    }

    public void ChangeVibration(bool active)
    {
        Debug.Log("Vibration = " + active);
    }
}
