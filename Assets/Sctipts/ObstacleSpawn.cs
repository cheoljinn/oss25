using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Transform spawnPoint;
    public Transform birdSpawnPoint;

    private Transform point;
    public float spawnInterval = 8.0f;
    public float randomInterval = 1.0f;
    private float nextSpawnTime;

    public float baseSpeed = 2.0f;
    public float increaseRate = 0.1f;
    public float currentSpeed;

    void Start()
    {
        nextSpawnTime = Time.time + GetRandomInterval();
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        currentSpeed += increaseRate * Time.deltaTime;

        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + GetRandomInterval();
        }
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);

        if (randomIndex == 0)
        {
            point = birdSpawnPoint;
        }
        else
        {
            point = spawnPoint;
        }

        GameObject obstacle = Instantiate(obstaclePrefabs[randomIndex], point.position, Quaternion.identity);

        float currentSpeed = GameManager.Instance.obstacleSpeed;
        Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();
        if (obstacleScript != null)
        {
            obstacleScript.SetSpeed(currentSpeed);

        }
    }

    private float GetRandomInterval()
    {
        return spawnInterval+Random.Range(-randomInterval, randomInterval);
    }
}
