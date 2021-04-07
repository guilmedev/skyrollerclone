using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private bool gameOver;


    public bool GameOver { get => gameOver ;
        private set
        {
            gameOver = value;
        }
    }


    [Header("HUD")]
    [SerializeField] GameObject ControllerSlider;
    [SerializeField] GameObject TrackSlider;
    [SerializeField] GameObject PlayButton;

    [Header("Panels")]
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject WinPanel;


    [Header("Controllers")]
    [SerializeField] DistanceController distanceController;

    [Header("Obstacles")]
    [SerializeField] GameObject[] obstaclesPrefab;
    [SerializeField] Transform obstacleInitPos;
    [SerializeField] GameObject obstaclesParent;
    [SerializeField] int maxObstacles;


    private PlayerController playerController;


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        distanceController = GetComponent<DistanceController>();
        playerController = FindObjectOfType<PlayerController>();

        GenerateRandomObstacles();

    }

    private GameObject GetRandomObstacle( GameObject[] array )
    {
        return array [ Random.Range(0, array.Length) ] ;
    }

    private void GenerateRandomObstacles()
    {

        System.Random rnd = new System.Random();

        for (int i = 1; i < maxObstacles; i++)
        {
            

            GameObject newObstacle;
            if (i == 1)
            {
                newObstacle = Instantiate( obstaclesPrefab[ 0 ], obstacleInitPos.position, Quaternion.identity);
            }else
                newObstacle = Instantiate( obstaclesPrefab[ rnd.Next( 0 , obstaclesPrefab.Length ) ] , obstacleInitPos.position , Quaternion.identity);

                newObstacle.transform.SetParent( obstaclesParent.transform );

                newObstacle.transform.position = 
                    new Vector3(
                        obstacleInitPos.position.x, 
                        obstacleInitPos.position.y, 
                        ( obstacleInitPos.position.z * i ) - Random.Range( 2f , 5f ) );            
        }
    }


    public void RestartGame()
    {
        //Reload Level
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void InitGame()
    {
        if (PlayButton)
                PlayButton.SetActive(false);

        if (ControllerSlider)
            ControllerSlider.SetActive(true);

        if (TrackSlider)
            TrackSlider.SetActive(true);

        playerController.InitGame();

        distanceController.enabled = true;
    }

    private void DisableGameHUD()
    {
        if (ControllerSlider)
            ControllerSlider.SetActive(false);

        if (TrackSlider)
            TrackSlider.SetActive(false);
    }


    public void SetGameOver()
    {
        if (GameOverPanel)
            GameOverPanel.SetActive(true);

        this.gameOver = true;

        DisableGameHUD();
    }

    public void SetGameWin()
    {
        if (WinPanel)
            WinPanel.SetActive(true);

        this.gameOver = true;

        DisableGameHUD();

    }


}
