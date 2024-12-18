using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BtAnimations : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public GameObject canvas;
    private Animator animator;
    private int maxStat = 50;

    [SerializeField]
    private Slider[] statSlider; // 0: health, 1: intelligence, 2: willpower, 3: cleanliness, 4: sociality, 5: affection

    private void Start()
    {
        animator = GetComponent<Animator>();

        // 초기 스탯 설정
        DataManager.instance.health = 20;
        DataManager.instance.cleanliness = 20;
        DataManager.instance.intelligence = 0;
        DataManager.instance.willpower = 0;
        DataManager.instance.sociality = 0;
        DataManager.instance.affection = 0;

        

        UpdateAllSliders(); // Initialize sliders with current values
    }

    public void GoPlay()
    {
        PerformAction("goPlay", 0, () =>
        {
            SoundManager.instance.Playsfx(SoundManager.Sfx.button);

            DataManager.instance.sociality = Mathf.Min(DataManager.instance.sociality + 5, maxStat);
            DataManager.instance.affection = Mathf.Min(DataManager.instance.affection + 1, maxStat);
            UpdateAllSliders();
        });
    }

    public void Feed()
    {
        PerformAction("feed", 10, () =>
        {
            SoundManager.instance.Playsfx(SoundManager.Sfx.button);

            DataManager.instance.health = Mathf.Min(DataManager.instance.health + 10, maxStat);
            DataManager.instance.affection = Mathf.Min(DataManager.instance.affection + 1, maxStat);
            UpdateAllSliders();
        });
    }

    public void Read()
    {
        PerformAction("read", 20, () =>
        {
            SoundManager.instance.Playsfx(SoundManager.Sfx.button);

            DataManager.instance.intelligence = Mathf.Min(DataManager.instance.intelligence + 5, maxStat);
            DataManager.instance.affection = Mathf.Min(DataManager.instance.affection + 1, maxStat);
            UpdateAllSliders();
        });
    }

    public void Exercise()
    {
        PerformAction("exercise", 20, () =>
        {
            SoundManager.instance.Playsfx(SoundManager.Sfx.button);

            DataManager.instance.willpower = Mathf.Min(DataManager.instance.willpower + 5, maxStat);
            DataManager.instance.affection = Mathf.Min(DataManager.instance.affection + 1, maxStat);
            UpdateAllSliders();
        });
    }

    public void Shower()
    {
        PerformAction("shower", 20, () =>
        {
            SoundManager.instance.Playsfx(SoundManager.Sfx.button);

            DataManager.instance.cleanliness = Mathf.Min(DataManager.instance.cleanliness + 10, maxStat);
            DataManager.instance.affection = Mathf.Min(DataManager.instance.affection + 1, maxStat);
            UpdateAllSliders();
        });
    }

    public void Sleep()
    {
        SoundManager.instance.Playsfx(SoundManager.Sfx.button);

        animator.SetTrigger("sleep");
    }

    public void Handling()
    {
        PerformAction("handling", 0, () =>
        {
            SoundManager.instance.Playsfx(SoundManager.Sfx.button);

            DataManager.instance.affection = Mathf.Min(DataManager.instance.affection + 1, maxStat);
            UpdateAllSliders();
        });
    }

    private void PerformAction(string actionName, int cost, System.Action onSuccess)
    {
        if (!DataManager.instance.CanPerformAction(actionName))
        {
            SoundManager.instance.Playsfx(SoundManager.Sfx.die);

            Debug.LogWarning($"이미 오늘 {actionName} 버튼을 눌렀습니다!");
            return;
        }

        if (DataManager.instance.DeductMoney(cost))
        {
            onSuccess?.Invoke();
            DataManager.instance.SetActionPerformed(actionName);

            // 공통 애니메이션 트리거 처리
            if (animator != null)
            {
                animator.SetTrigger(actionName);
            }

            Debug.Log($"{actionName} 행동 완료. 남은 돈: {DataManager.instance.money}");
        }
        UpdateMoneyUI();
    }

    private void UpdateAllSliders()
    {
        if (statSlider.Length >= 6)
        {
            statSlider[0].value = DataManager.instance.health / (float)maxStat;
            statSlider[1].value = DataManager.instance.intelligence / (float)maxStat;
            statSlider[2].value = DataManager.instance.willpower / (float)maxStat;
            statSlider[3].value = DataManager.instance.cleanliness / (float)maxStat;
            statSlider[4].value = DataManager.instance.sociality / (float)maxStat;
            statSlider[5].value = DataManager.instance.affection / (float)maxStat;
        }
    }

    public void UpdateMoneyUI()
    {
        moneyText.text = DataManager.instance.money.ToString();
    }

    public void ActivePlay()
    {
        canvas.SetActive(true);
    }
}