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
