using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    float turnSpeed = 90f;

    [SerializeField]
    GameObject coin;

    [SerializeField]
    Transform pescaditoMesh;

    [SerializeField]
    Collider collider_3D;
    [SerializeField]
    Collider collider_2D;

    [SerializeField]
    GameObject hitParticle;


    public bool isDecreasing;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(coin);
            return;
        }


        // Revisar si la moneda ha colisionado con el jugador

        if (other.gameObject.name != "Player")
        {
            return;
        }


        if (other.gameObject.name == "Player")
        {
            if (isDecreasing)
            {
                GameManager.inst.DecreaseSpeed();
            }
            else
            {
                GameManager.inst.IncrementSpeed();
            }


            // Agregar puntaje  
            GameManager.inst.IncrementScore();

            Explode();
            // Destruir esta moneda
            Destroy(coin);
        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pescaditoMesh.Rotate(0, 0, turnSpeed * Time.deltaTime); 

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

    void Explode()
    {
        GameObject hit = Instantiate(hitParticle, transform.position, Quaternion.identity);
        hit.GetComponent<ParticleSystem>().Play();
    }
}
