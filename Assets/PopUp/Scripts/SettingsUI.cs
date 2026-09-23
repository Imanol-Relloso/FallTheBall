using UnityEngine;
using DG.Tweening;

public class SettingsUI : UIWindow
{
    
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
            canvasGroup.GetComponent<RectTransform>().DOMoveX(540, 0);
            canvasRectTransform.gameObject.SetActive(true);
        }
        else
        {
            canvasRectTransform.gameObject.SetActive(true);
                
            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOMoveX(540, showDuration).SetEase(showEase); 
        }
        
    }

    public override void Hide(bool instant = false)
    {
        if(instant)
        {
            canvasGroup.GetComponent<RectTransform>().DOMoveX(2000, 0);
            canvasRectTransform.gameObject.SetActive(false);
        }
        else
        {
            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOMoveX(2000, hideDuration).SetEase(hideEase).OnComplete(
                ()=> canvasRectTransform.gameObject.SetActive(false)); 
        }
    }
    
    public void Home()
    {
        Hide();
        
        //Show mainmenu
        Debug.Log("Main menu");
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
