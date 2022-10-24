using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private int score;
    private Rigidbody playerRigidbody;
    private GameObject superJump;
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
        playerRigidbody = player.GetComponent<Rigidbody>();
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
        //score = 0;

        //playerRigidbody.AddForce(Vector3.zero); // stop moving player and situate him at initial position
        //player.transform.position = new Vector3(0, 1.11f, 0);
        player.enabled = true;
        //player.SuperPowerRemaining = 0;

        //if (superJump != null)
        //{
        //    Destroy(superJump.gameObject); // destroy any remaining super jump from previous round
        //}
        //superJump = Instantiate(prefab, new Vector3(2.5f, 1.95f, 0f), Quaternion.identity) as GameObject; // situate super jump at its initial position

        //if (true)
        //{
        //    //Destroy fireball instances
        //}
    }

    public void IncreaseScore()
    {
        score++;
        text.text = score.ToString();
    }
}
