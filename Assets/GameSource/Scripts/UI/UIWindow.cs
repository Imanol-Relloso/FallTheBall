using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private string _id;
    protected bool _isShowing;

    [Header("UI Settings")]
    [SerializeField]
    protected RectTransform canvasRectTransform;
    [SerializeField]
    protected CanvasGroup canvasGroup;
    [SerializeField]
    protected bool hideOnStart;
    
    [Header("Animation Settings")]
    [SerializeField]
    protected float showDuration = 0.5f;    
    [SerializeField]
    protected float hideDuration = 0.5f; 
    
    [SerializeField]
    protected Ease showEase = Ease.OutBack;    
    [SerializeField]
    protected Ease hideEase = Ease.InBack;

    public string Id => _id;
    public bool IsShowing => _isShowing;

    void Start()
    {
        Initialize();
    }
    
    public virtual void Initialize()
    {
        if(hideOnStart)
            Hide(true);
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

        _isShowing = true;
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

        _isShowing = false;
    }
}
