using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyLevel", menuName = "GameSource/Difficulty/DifficultyLevel")]
public class DifficultyLevel : ScriptableObject
{
    public float Score;

    public int GridFloor;
    public float GridSeparationY;
    
    [Range(0,1)]
    public float SafeBlockProbability;

    public int supersonicGrid;
    
    public float minSpeed;
    public float maxSpeed;
    
    public float SumScore;
}
