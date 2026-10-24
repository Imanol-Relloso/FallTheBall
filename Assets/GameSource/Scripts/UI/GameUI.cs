using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameUI : UIWindow
{
    [Header("Move positions")]
    [SerializeField]
    private float showPositionY = 0;
    [SerializeField]
    private float hidePositionY = -3000;
    
    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI scoreText;
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
        
        UpdateScore(ScoreManager.Instance.GetGameScore());
        UpdateRecord(PlayerPrefs.GetFloat("Record", 0));

        ScoreManager.Instance.OnScoreChanged.AddListener(UpdateScore);
        ScoreManager.Instance.OnRecordChanged.AddListener(UpdateRecord);
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

    public void UpdateScore(float score)
    {
        scoreText.text = score.ToString("0");
    }
    public void UpdateRecord(float record)
    {
        recordText.text = record.ToString("0");
    }
}
