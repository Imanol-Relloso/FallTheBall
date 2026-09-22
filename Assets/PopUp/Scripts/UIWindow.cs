using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    [SerializeField]
    private RectTransform canvasRectTransform;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private bool hideOnStart;
    
    [Header("Animation Settings")]
    [SerializeField]
    protected float showDuration = 0.5f;    
    [SerializeField]
    protected float hideDuration = 0.5f; 
    
    [SerializeField]
    protected Ease showEase = Ease.OutBack;    
    [SerializeField]
    protected Ease hideEase = Ease.InBack;
    
    
    public CanvasGroup CanvasGroup => canvasGroup;
    
    void Start()
    {
        Initialize();
    }
    
    public virtual void Initialize()
    {
        if(hideOnStart)
            Hide();
    }
    
    public virtual void Show(bool instant = false)
    {
        if(instant)
            canvasRectTransform.gameObject.SetActive(true);
        else
        {
            canvasRectTransform.gameObject.SetActive(true);
                
            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOScale(Vector3.one, showDuration).SetEase(showEase); 
        }
        
    }

    public virtual void Hide(bool instant = false)
    {
        if(instant)
            canvasRectTransform.gameObject.SetActive(false);
        else
        {
            RectTransform rectTran = canvasGroup.GetComponent<RectTransform>();

            rectTran.DOScale(Vector3.zero, hideDuration).SetEase(hideEase).OnComplete(
                ()=> canvasRectTransform.gameObject.SetActive(false)); 
        }
    }
    
    #region Test

    [Button]
    private void ShowTest()
    {
        Show();
    }    
    [Button]
    private void HideTest()
    {
        Hide();
    }

    #endregion
}
