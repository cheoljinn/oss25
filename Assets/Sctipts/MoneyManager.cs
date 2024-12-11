using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance; // 싱글톤 인스턴스
    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
        }
        else
        {
            Destroy(gameObject); // 중복된 오브젝트 제거
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
