using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public bool isGame2d;

    int score;

    public static GameManager inst;

    [SerializeField]
    TMP_Text scoreText;

    [SerializeField] PlayerMovement playerMovement;
    public GameObject levelCompletedPanel;

    private PlayerMovement player;

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        // Incrementar la velocidad del jugador
        //playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }

    public void IncrementSpeed()
    {
        if (playerMovement.speed < 10)
        {
            playerMovement.speed += playerMovement.speedIncreasePerPoint;

        }


        //playerMovement.speed += playerMovement.speedIncreasePerPoint;

        // Check if jumpForce is less than 650 before increasing it.
        if (playerMovement.jumpForce < 650f)
        {
            playerMovement.jumpForce += 30f;
            // Ensure jumpForce doesn't exceed 650.
            if (playerMovement.jumpForce > 650f)
                playerMovement.jumpForce = 650f;
        }
    }

    public void DecreaseSpeed()
    {
        playerMovement.speed -= playerMovement.speedIncreasePerPoint;
        
        // Check if jumpForce is greater than zero before decreasing it.
        if (playerMovement.jumpForce > 0f)
        {
            playerMovement.jumpForce -= 20f;
            // Ensure jumpForce doesn't go below zero.
            if (playerMovement.jumpForce < 0f)
                playerMovement.jumpForce = 0f;
        }
    }

    void Awake()
    {
        inst = this;
        player = FindObjectOfType<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        playerMovement.Restart();
    }

    public void LevelComplete()
    {
        levelCompletedPanel.SetActive(true);
    }
}
