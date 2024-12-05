using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject[] background;

    public float baseSpeed=2.0f;
    public float resetPosition = -6.0f;
    public float resetOffset = 24.0f;
    public float increaseRate=0.1f;
    public float currentSpeed;
    public bool isIncreasing;

    private void Start()
    {
        currentSpeed = baseSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if (isIncreasing)
        {
            currentSpeed += increaseRate * Time.deltaTime;
        }

        foreach (GameObject block in background) {

            block.transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);

            if (block.transform.localPosition.x <= resetPosition)
            {
                block.transform.localPosition += new Vector3(resetOffset, 0, 0);
            }
        }
    }
}
