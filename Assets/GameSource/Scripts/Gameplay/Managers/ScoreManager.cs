using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [SerializeField]
    private BallSpeed ballSpeed;
    
    private float _score = 0;
    
    public UnityEvent<float> OnScoreChanged;
    public UnityEvent<float> OnRecordChanged;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RestartScore()
    {
        _score = 0;
        OnScoreChanged?.Invoke(_score);
    }

    public void UpdateScore(float score = 10)
    {
        if (ballSpeed.GetSupersonic())
            score *= 2;
        
        _score += score;
        
        OnScoreChanged?.Invoke(_score);
        
        if (_score > PlayerPrefs.GetFloat("Record"))
            UpdateRecord();
    }

    public void UpdateRecord()
    {
        PlayerPrefs.SetFloat("Record", _score);
        OnRecordChanged?.Invoke(_score);
        Debug.Log("Record: " + PlayerPrefs.GetFloat("Record"));
    }
    
    public float GetGameScore()
    {
        return _score;
    }
    
    [Button]
    public void ResetRecord()
    {
        PlayerPrefs.SetFloat("Record", 0);
        OnRecordChanged?.Invoke(_score);
    }
}
