using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;

    [SerializeField]
    GameObject obstaclePrefab;
    [SerializeField]
    GameObject tallObstaclePrefab;
    [SerializeField]
    GameObject goalPrefab;

    [SerializeField] float tallObstacleChance = 0.2f    ;

    [SerializeField]
    GameObject coinPrefab;

    [SerializeField]
    GameObject coinDecreasePrefab;

    [SerializeField]
    Transform goalSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = FindObjectOfType<GroundSpawner>();
        groundSpawner.tilesSpawned++;
        if (groundSpawner.tilesSpawned == 37)
        {
            groundSpawner.SpawnTile(false);
        }
        if (groundSpawner.tilesSpawned == 38)
        {
            groundSpawner.SpawnTile(false);
        }
        if (groundSpawner.tilesSpawned == 39)
        {
            groundSpawner.SpawnTile(false);
            SpawnGoal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (groundSpawner.tilesSpawned < 37)
        {
            groundSpawner.SpawnTile(true);
        }
        //groundSpawner.SpawnTile(true);
        Destroy(gameObject, 4);
    }


    public void SpawnGoal()
    {
        Instantiate(goalPrefab, goalSpawnPoint.position, Quaternion.identity, transform);
    }


    public void SpawnObstacle()
    {
        // Elegir qué obstáculo spawnear
        GameObject obstacleToSpawn = obstaclePrefab;
        float random = Random.Range(0f, 1f);
        if (random < tallObstacleChance)
        {
            obstacleToSpawn = tallObstaclePrefab;
        }

        // Elegir un punto aleatorio donde spawnear el obstaculo
        int obstacleSpawnIndex = Random.Range(2, 5);

        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;


        

        // Spawnear el obstáculo en posición
        Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);



        
    }

    public void SpawnCoins()
    {
        int coinsToSpawn = 13;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            if (Random.Range(0f, 1f) > 0.5f)
            {
                GameObject temp = Instantiate(coinPrefab, transform);
                temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
            }

            else
            {
                GameObject temp = Instantiate(coinDecreasePrefab, transform);
                temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
            }
            //GameObject temp = Instantiate(coinPrefab, transform);
            //temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3( Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                                     Random.Range(collider.bounds.min.y, collider.bounds.max.y),
                                     Random.Range(collider.bounds.min.z, collider.bounds.max.z));

        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}
