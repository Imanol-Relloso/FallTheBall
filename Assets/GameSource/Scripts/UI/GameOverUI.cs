using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameOverUI : UIWindow
{
    [Header("Move transition")] 
    [SerializeField]
    private float showPosition = -1170f;
    [SerializeField]
    private float hidePosition = 1170f;    
    [SerializeField]
    private Ease moveHideEase = Ease.Linear;    
    [SerializeField]
    private float moveHideDuration = 0.2f;
    
    [Header("Score")]
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI recordText;
    
    public override void Show(bool instant = false)
    {
        if (instant)
        {
            canvasGroup.GetComponent<RectTransform>().DOAnchorPosY(showPosition, 0);
            canvasRectTransform.gameObject.SetActive(true);
        }
        else
        {
            canvasGroup.GetComponent<RectTransform>().DOAnchorPosY(showPosition, 0);
            canvasRectTransform.gameObject.SetActive(true);
                
            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOScale(Vector3.one, showDuration).SetEase(showEase); 
        }

        _isShowing = true;
    }
    
    public override void Hide(bool instant = false)
    {
        RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

        if (instant)
        {
            canvasGroup.GetComponent<RectTransform>().DOAnchorPosY(hidePosition, 0);
            canvasRectTransform.gameObject.SetActive(false);
            
            rectTran.DOScale(Vector3.zero, 0); 
        }
        else
        {
            rectTran.DOScale(Vector3.zero, hideDuration).SetEase(hideEase).OnComplete(
                ()=> canvasRectTransform.gameObject.SetActive(false)); 
        }

        _isShowing = false;
    }

    public void DirHide()
    {
        RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();
        
        rectTran.DOAnchorPosY(hidePosition, moveHideDuration).SetEase(moveHideEase).OnComplete(
            () =>
            {
                canvasRectTransform.gameObject.SetActive(false);
                rectTran.DOScale(Vector3.zero, 0); 
            });
        
        _isShowing = false;
    }


    #region Bttns

    public void PlayAgain()
    {
        //Hide();
        
        //GameManager.Instance.PlayAgain();
    }

    public void MainMenu()
    {
        UIManager.Instance.ShowWindow("MainMenu");
        DirHide();
    }

    public void ShowScore()
    {
        //scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void ShowRecord()
    {
        //recordText.text = PlayerPrefs.GetInt("Record", 0).ToString();
    }

    #endregion
    
    #region Test

    [Button("Test Show")]
    private void ShowTest()
    {
        Show();
    }
    [Button("Test Hide")]

    private void HideTest()
    {
        Hide();
    }

    #endregion
}
