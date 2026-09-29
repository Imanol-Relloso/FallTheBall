using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainMenuUI : UIWindow
{
    [Header("Move positions")]
    [SerializeField]
    private float showPositionX = 540f;
    [SerializeField]
    private float showPositionY = 2000f;

    [SerializeField]
    private float hidePositionX = 2000f;
    [SerializeField]
    private float hidePositionY = 0f;

    [Header("Record")]
    [SerializeField]
    private TextMeshProUGUI recordText;


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
            canvasGroup.GetComponent<RectTransform>().DOMove(new Vector3(showPositionX, showPositionY, 0), 0);
            canvasRectTransform.gameObject.SetActive(true);
        }
        else
        {
            DirShow(UIManager.Instance.GetActiveWindowID());
        }

        _isShowing = true;
    }

    public override void Hide(bool instant = false)
    {
        if (instant)
        {
            Debug.LogWarning("Use DirHide method to hide the window with animation");

            canvasRectTransform.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Use DirHide method to hide the window with animation");
        }

        _isShowing = false;
    }

    public void DirShow(string windowId)
    {
        switch (windowId)
        {
            case "Settings":
                {
                    canvasRectTransform.gameObject.SetActive(true);

                    RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

                    rectTran.DOMoveX(showPositionX, showDuration).SetEase(showEase);
                    break;
                }

            case "Credits":
                {
                    canvasRectTransform.gameObject.SetActive(true);

                    RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

                    rectTran.DOMoveY(-showPositionY, showDuration).SetEase(showEase);
                    break;
                }

            case "Game":
                {
                    canvasRectTransform.gameObject.SetActive(true);

                    RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

                    rectTran.DOMoveY(showPositionY, showDuration).SetEase(showEase);
                    break;
                }

            default:
                Debug.LogError("Window not found:" + windowId);
                break;
        }

        _isShowing = true;
    }
    public void DirHide(string windowId)
    {
        RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

        switch (windowId)
        {
            case "Settings":
                rectTran.DOMoveX(hidePositionX, hideDuration).SetEase(hideEase).OnComplete(
                            () => canvasRectTransform.gameObject.SetActive(false));
                break;
            case "Credits":
                rectTran.DOMoveY(-hidePositionY, hideDuration).SetEase(hideEase).OnComplete(
                        () => canvasRectTransform.gameObject.SetActive(false));
                break;
            case "Game":
                rectTran.DOMoveY(hidePositionY, hideDuration).SetEase(hideEase).OnComplete(
                        () => canvasRectTransform.gameObject.SetActive(false));
                break;
            default:
                Debug.LogError("Window not found");
                break;
        }

        _isShowing = false;
    }

    #region Bttns

    public void Play()
    {
        UIManager.Instance.ShowWindow("Game");
        DirHide("Game");
    }

    public void Settings()
    {
        UIManager.Instance.ShowWindow("Settings");
        DirHide("Settings");
    }

    public void Credits()
    {
        UIManager.Instance.ShowWindow("Credits");
        DirHide("Credits");
    }

    public void UpdateRecord()
    {
        //recordText.text = PlayerPrefs.GetInt("Record", 0).ToString();
    }

    #endregion
}
