using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    PlayerMovement playerMovement;


    [SerializeField]
    float turnSpeed = 90f;

    [SerializeField]
    Transform goalMesh;

    [SerializeField] AudioSource endLevelSound;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerMovement.LevelComplete();
            GameManager.inst.LevelComplete();
            endLevelSound.Play();
        }
        // Matar al jugador

    }

    private void Update()
    {
        goalMesh.Rotate(0, turnSpeed, 0 * Time.deltaTime);
    }

}
