using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField]
    private CameraController cameraController;
    
    [SerializeField]
    private Transform ballTransform;
    private BallDeath ballDeath;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        if (ballTransform != null)
            ballDeath = ballTransform.GetComponent<BallDeath>();
    }

    public void StartGame()
    {
        ScoreManager.Instance.RestartScore();
        
        //Transicion de la camara
        cameraController.GameTransition();
        
        //Reinicia y empieza la generacion
        GridManager.Instance.ResetGrids();
        GridManager.Instance.StartGeneration();
        
        //Se habilita la bola
        ballTransform.gameObject.SetActive(true);
        
        //Resetea posicion de bola y descongelo el movimiento
        ballDeath.Reset();
        
    }
    public void PlayAgain()
    {
        ScoreManager.Instance.RestartScore();
        UIManager.Instance.ShowWindow("Game");
        
        //Resetea posicion de bola y descongelo el movimiento
        ballDeath.Reset();
        
        //Transicion de la camara
        cameraController.PlayAgainTransition();
        
        //Se resetean las grids
        GridManager.Instance.ResetGrids();

        //Empieza una nueva generacion
        GridManager.Instance.StartGeneration();
    }

    public void GameOver()
    {
        //Aparece el gameover
        UIManager.Instance.ShowWindow("GameOver");
        UIManager.Instance.HideWindow("Game");
        
        //Ball death
        ballDeath.Death();
    }

    public void ResetForMenu()
    {
        cameraController.ResetCamera();
    }
}
