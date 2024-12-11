using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Obstacle : MonoBehaviour
{
    public float speed = 2.0f;
    public float increaseRate = 0.1f;

    public float destroyPosition = -6.0f;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    void Update()
    {

        transform.Translate(Vector3.left *speed * Time.deltaTime);

        if (transform.position.x <= destroyPosition)
        {
            Destroy(gameObject);
        }
    }
}
