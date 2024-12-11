
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartUI : MonoBehaviour
{
    public GameObject[] hearts;

    public void UpdateHearts(int currentHP)
    {
        if (currentHP < 0)
        {
            Debug.Log("불가능한 HP");
            return;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHP)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
