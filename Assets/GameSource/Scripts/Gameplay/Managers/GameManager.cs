using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
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

    public void StartGame()
    {
        Debug.Log("Start Game");
        
        //Como no esta el juego terminado muestra la UI de derrota
        GameOver();
    }
    public void PlayAgain()
    {
        Debug.Log("Play Again");
        
        //Como no esta el juego terminado muestra la UI de derrota
        GameOver();
    }

    public void GameOver()
    {
        UIManager.Instance.ShowWindow("GameOver");
    }
}
