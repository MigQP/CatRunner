using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    public BoxCollider collider_3D;
    public BoxCollider collider_2D;



    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerMovement.Die();
        }
        // Matar al jugador
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.inst.isGame2d)
        {
            collider_3D.enabled = true;
            collider_2D.enabled = false;
        }
        else
        {
            collider_2D.enabled = true;
            collider_3D.enabled = false;
        }
    }
}
