using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private int score;
    //private Rigidbody playerRigidbody;
    //private GameObject superJump;
    bool gameHasEnded = false;

    public Player player;
    public Text text;
    public GameObject playButton;
    public GameObject gameOver;


    private void Awake()
    {
        Pause();
        player.enabled = false;
    }
    private void Start()
    {
        //playerRigidbody = player.GetComponent<Rigidbody>();
    }
    public void Play()
    {
        ResetSettings(); // game initial state

        playButton.SetActive(false); // hide play button and game over image
        gameOver.SetActive(false);

        Time.timeScale = 1f; // let update
    }

    public void Pause()
    {
        Time.timeScale = 0f; // halt any update

    }
    public void GameOver()
    {
        playButton.SetActive(true); // show play button and game over image
        gameOver.SetActive(true);
        gameHasEnded = true;
        Pause();
    }

    /// <summary>
    /// Set the initial state of the game
    /// </summary>
    public void ResetSettings()
    {
        if (gameHasEnded == true)
        {
            SceneManager.LoadScene(0);
        }
        player.enabled = true;
    }
    /// <summary>
    /// when player collect coin increase score
    /// </summary>
    public void IncreaseScore()
    {
        score++;
        text.text = score.ToString();
    }
}
