using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EndingManager : MonoBehaviour
{
    public GameObject[] endingDecorations; // �� ���� ��ǰ ������Ʈ �迭
    public TextMeshProUGUI tamagotchiDialogue; // �ٸ���ġ ��� ��¿� �ؽ�Ʈ

    // ������ ��� ����Ʈ
    private string[][] endingDialogues = new string[][]
    {
        new string[] { "����...�ñ��� �ٸ���ġ?","���� � �͵� �̷� �� �־�!", "��� �����̿���. ������!" },
        new string[] { "��� �Ǿ��ٴ� ������.", "�ƽ����� ����� ���׿�.", "�ູ�߾��. �����մϴ�." },
        new string[] { "���Ͼ���~!", "���� ������ �����ϰ� ���� �ſ���.", "�¼����� ���������� ������ �ʰھ�!" },
        new string[] { "128��e980.", "�� ��ǥ�� �ּ��� ������!", "�������� �Ǿ� ��� �� Ž���ϰھ�!" },
        new string[] { "�� ģ���� �̷��� ���ƿ�.", "��ŵ� ���� ģ���ϽǷ���?", "���� ������ ������ ƴ�� ���� �ſ���." },
        new string[] { "�� ��簡 �Ǿ����!", "� �÷õ� �� ���� �� ����!", "�Բ� ������ ������. ���� �Ͼ��!" }
    };

    private int currentDialogueIndex = 0; // ���� ��� �ε���
    private int endingIndex; // ������ ���� �ε���
    private bool isIntroDialogue = true; // �ƾ� ��簡 ���� ������ Ȯ��

    void Start()
    {
        endingIndex = DetermineEnding(); // ���� ����
        ActivateEnding(endingIndex); // ���� ����
        ShowIntroDialogue(); // �ƾ� ��� ����
    }

    int DetermineEnding()
    {
        // ���� �� ��������
        int affection = DataManager.instance.affection;
        int health = DataManager.instance.health;
        int cleanliness = DataManager.instance.cleanliness;
        int intelligence = DataManager.instance.intelligence;
        int sociality = DataManager.instance.sociality;
        int willpower = DataManager.instance.willpower;

        // ��� ������ 45 �̻��̸� Ư�� ����
        if (affection >= 45 && health >= 45 && cleanliness >= 45 &&
            intelligence >= 45 && sociality >= 45 && willpower >= 45)
        {
            return 0; // Ư�� ����
        }

        // �������� ü�� Ȯ��
        if (affection < 40 || health < 40)
        {
            return 1; // ����� ��ġ
        }

        // ���� ���� ���� (40 �̻��� ���� ����)
        int careerEnding = DetermineCareer(cleanliness, intelligence, sociality, willpower);
        if (careerEnding != -1)
        {
            return careerEnding + 2; // 2������ ���� ����
        }

        return 1; // �⺻ �������� ��ü
    }

    int DetermineCareer(int cleanliness, int intelligence, int sociality, int willpower)
    {
        // 40�� �Ѵ� ���� Ȯ��
        List<int> highStats = new List<int>();
        if (cleanliness >= 40) highStats.Add(0); // û�ᵵ
        if (intelligence >= 40) highStats.Add(1); // ����
        if (sociality >= 40) highStats.Add(2); // ��ȸ��
        if (willpower >= 40) highStats.Add(3); // ������

        // ���� ���� ó��
        if (highStats.Count > 1)
        {
            return ResolveTie(highStats); // ��ġ�� ��� ó��
        }
        else if (highStats.Count == 1)
        {
            return highStats[0]; // ���� ����
        }

        return -1; // �ش� ����
    }

    int ResolveTie(List<int> highStats)
    {
        int randomIndex = Random.Range(0, highStats.Count);
        return highStats[randomIndex];
    }

    void ActivateEnding(int index)
    {
        // ��� ��ǰ ��Ȱ��ȭ
        foreach (var decoration in endingDecorations)
        {
            decoration.SetActive(false);
        }

        // �ش� ���� ��ǰ Ȱ��ȭ
        if (DataManager.instance.selectedEgg == 2 && index == 0)
        {
            endingDecorations[6].SetActive(true);
        }
        else if (DataManager.instance.selectedEgg == 3 && index == 0)
        {
            endingDecorations[7].SetActive(true);
        }
        else
        {
            endingDecorations[index].SetActive(true);
        }
    }

    void ShowIntroDialogue()
    {
        // �ƾ� ��� ���
        switch (currentDialogueIndex)
        {
            case 0:
                tamagotchiDialogue.text = "�׵��� �淯�ּż� �����մϴ�.";
                currentDialogueIndex++;
                break;
            case 1:
                tamagotchiDialogue.text = "��� ���п� ����...";
                currentDialogueIndex++;
                break;
            case 2:
                string[] endingNames = { "�ñ���", "�����", "û�ҿ�", "�ڻ�", "�ν�", "���" };
                tamagotchiDialogue.text = $"{endingNames[endingIndex]} ��ġ�� �Ǿ����!";
                currentDialogueIndex++;
                break;
            default:
                isIntroDialogue = false; // �ƾ� ��� ����
                currentDialogueIndex = 0; // ���� ���� �ε��� �ʱ�ȭ
                ShowNextDialogue(); // ���� ��� ����
                break;
        }
    }

    public void ShowNextDialogue()
    {
        if (isIntroDialogue)
        {
            ShowIntroDialogue(); // �ƾ� ��� ����
            return;
        }

        // ���� ������ ��� ����Ʈ ��������
        string[] dialogues = endingDialogues[endingIndex];

        // ���� ��� ���
        if (currentDialogueIndex < dialogues.Length)
        {
            tamagotchiDialogue.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            DataManager.instance.ResetGame();
            DataManager.instance.SaveData();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
    }

    void Update()
    {
        // ��ġ �Է� ����
        if (Input.GetMouseButtonDown(0)) // ���콺 Ŭ�� �Ǵ� ȭ�� ��ġ
        {
            ShowNextDialogue(); // ���� ��� ǥ��
        }
    }
}
