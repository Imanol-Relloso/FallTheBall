using DG.Tweening;
using UnityEngine;

public class CreditsUI : UIWindow
{
    [Header("Move positions")]
    [SerializeField]
    private float showPositionY = 1170f;
    [SerializeField]
    private float hidePositionY = 3170f;
    
    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        if (hideOnStart)
            Hide(true);
    }

    public override void Show(bool instant = false)
    {
        if (instant)
        {
            canvasGroup.GetComponent<RectTransform>().DOAnchorPosY(showPositionY, 0);
            canvasRectTransform.gameObject.SetActive(true);
        }
        else
        {
            canvasRectTransform.gameObject.SetActive(true);

            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOAnchorPosY(showPositionY, showDuration).SetEase(showEase);
        }

        _isShowing = true;
    }

    public override void Hide(bool instant = false)
    {
        if (instant)
        {
            canvasGroup.GetComponent<RectTransform>().DOAnchorPosY(hidePositionY, 0);
            canvasRectTransform.gameObject.SetActive(false);
        }
        else
        {
            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOAnchorPosY(hidePositionY, hideDuration).SetEase(hideEase).OnComplete(
                () => canvasRectTransform.gameObject.SetActive(false));
        }

        _isShowing = false;
    }

    public void Home()
    {
        UIManager.Instance.ShowWindow("MainMenu");

        Hide();
    }
}
