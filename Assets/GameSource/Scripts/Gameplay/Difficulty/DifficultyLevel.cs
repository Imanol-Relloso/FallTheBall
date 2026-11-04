using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyLevel", menuName = "GameSource/Difficulty/DifficultyLevel")]
public class DifficultyLevel : ScriptableObject
{
    public float Score;

    public int GridFloor;
    
    public float minSpeed;
    public float maxSpeed;
    
    public float SumScore;
}
