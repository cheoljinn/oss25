using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float obstacleSpeed = 2.0f;
    public float increaseRate = 0.1f;

    private void Awake()
    {
        // Singleton ¼³Á¤
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        obstacleSpeed += increaseRate * Time.deltaTime;
    }
}

