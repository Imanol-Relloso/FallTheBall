using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField] 
    private BallSpeed ballSpeed;

    [SerializeField] 
    private DifficultyLevel[] difficultyLevels;

    private DifficultyLevel currentDifficulty;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        ScoreManager.Instance.OnScoreChanged.AddListener(OnScoreChanged);
        
        ChangeDifficulty(difficultyLevels[0]);
    }

    private void OnScoreChanged(float score)
    {
        DifficultyLevel highestDifficulty = difficultyLevels[0];
            
        foreach (var t in difficultyLevels)
        {
            if (t.Score <= score)
                highestDifficulty = t;
            else
                continue;
        }
        
        TryChangeDifficulty(highestDifficulty);
        
    }

    private void TryChangeDifficulty(DifficultyLevel difficultyLevel)
    {
        if (difficultyLevel == currentDifficulty)
            return;

        ChangeDifficulty(difficultyLevel);
    }

    private void ChangeDifficulty(DifficultyLevel difficultyLevel)
    {
        Debug.Log("difficulty changed: " + difficultyLevel.name);
        
        currentDifficulty = difficultyLevel;

        GridManager.Instance.SetGridXZ(difficultyLevel.GridFloor);
 
        ballSpeed.SetMinSpeed(difficultyLevel.minSpeed);
        ballSpeed.SetMaxSpeed(difficultyLevel.maxSpeed);
        
        ScoreManager.Instance.SetSumScore(difficultyLevel.SumScore);
        
        //cambio la camara si es necesario
    }
}
