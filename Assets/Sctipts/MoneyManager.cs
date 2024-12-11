using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance; // �̱��� �ν��Ͻ�
    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� ������Ʈ ����
        }
    }

    public void UpdateMoneyUI()
    {
        if (moneyText != null && DataManager.instance != null)
        {
            moneyText.text = DataManager.instance.money.ToString();
        }
    }
}
