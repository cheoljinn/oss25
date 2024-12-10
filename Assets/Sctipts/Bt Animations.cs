using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtAnimations : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void GoPlay()
    {
        PerformAction("goPlay", 20, () =>
        {
            DataManager.instance.sociality += 5;
            DataManager.instance.affection += 1;
        });
    }
        public void Feed()
    {
        PerformAction("feed", 10, () =>
        {
            DataManager.instance.health += 10;
            DataManager.instance.affection += 1;
        });
    }
    public void Read()
    {
        PerformAction("read", 20, () =>
        {
            DataManager.instance.intelligence+= 5;
            DataManager.instance.affection += 1;
        });
    }
    public void Exercise()
    {
        PerformAction("exercise", 20, () =>
        {
            DataManager.instance.willpower += 5;
            DataManager.instance.affection += 1;

        });
    }
    public void Shower()
    {
        PerformAction("shower", 20, () =>
        {
            DataManager.instance.cleanliness+= 10;
            DataManager.instance.affection += 1;
        });
    }
    public void Sleep()
    {
        animator.SetTrigger("sleep");
    }
    public void Handling()
    {
        PerformAction("handling", 0, () =>
        {
            DataManager.instance.affection += 1;
        });
    }

    private void PerformAction(string actionName, int cost, System.Action onSuccess)
    {
        if (!DataManager.instance.CanPerformAction(actionName))
        {
            Debug.LogWarning($"�̹� ���� {actionName} ��ư�� �������ϴ�!");
            return;
        }

        if (DataManager.instance.DeductMoney(cost))
        {
            onSuccess?.Invoke();
            DataManager.instance.SetActionPerformed(actionName);

            // ���� �ִϸ��̼� Ʈ���� ó��
            if (animator != null)
            {
                animator.SetTrigger(actionName);
            }

            Debug.Log($"{actionName} �ൿ �Ϸ�. ���� ��: {DataManager.instance.money}");
        }
    }
}
