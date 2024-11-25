using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject[] background;

    public float speed = 2.0f;
    public float resetPosition = -6.0f;
    public float resetOffset = 24.0f;

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject block in background) {

            block.transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (block.transform.localPosition.x <= resetPosition)
            {
                block.transform.localPosition += new Vector3(resetOffset, 0, 0);
            }
        }
    }
}
