using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EndingManager : MonoBehaviour
{
    public GameObject[] endingDecorations; // 각 엔딩 소품 오브젝트 배열
    public TextMeshProUGUI tamagotchiDialogue; // 다마고치 대사 출력용 텍스트

    // 엔딩별 대사 리스트
    private string[][] endingDialogues = new string[][]
    {
        new string[] { "내가...궁극의 다마고치?","이제 어떤 것도 이룰 수 있어!", "당신 덕분이에요. 고마워요!" },
        new string[] { "어른이 되었다는 뜻이죠.", "아쉽지만 헤어질 때네요.", "행복했어요. 감사합니다." },
        new string[] { "쓱싹쓱싹~!", "제가 세상을 깨끗하게 만들 거예요.", "온세상이 빛날때까지 멈추지 않겠어!" },
        new string[] { "128√e980.", "내 목표는 최성록 교수님!", "연구생이 되어 모든 걸 탐구하겠어!" },
        new string[] { "저 친구가 이렇게 많아요.", "당신도 저와 친구하실래요?", "저랑 있으면 지루할 틈이 없을 거예요." },
        new string[] { "저 용사가 되었어요!", "어떤 시련도 날 막을 순 없어!", "함께 모험을 떠나요. 저만 믿어요!" }
    };

    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private int endingIndex; // 결정된 엔딩 인덱스
    private bool isIntroDialogue = true; // 컷씬 대사가 진행 중인지 확인

    void Start()
    {
        endingIndex = DetermineEnding(); // 엔딩 결정
        ActivateEnding(endingIndex); // 엔딩 설정
        ShowIntroDialogue(); // 컷씬 대사 시작
    }

    int DetermineEnding()
    {
        // 스탯 값 가져오기
        int affection = DataManager.instance.affection;
        int health = DataManager.instance.health;
        int cleanliness = DataManager.instance.cleanliness;
        int intelligence = DataManager.instance.intelligence;
        int sociality = DataManager.instance.sociality;
        int willpower = DataManager.instance.willpower;

        // 모든 스탯이 45 이상이면 특별 엔딩
        if (affection >= 45 && health >= 45 && cleanliness >= 45 &&
            intelligence >= 45 && sociality >= 45 && willpower >= 45)
        {
            return 0; // 특별 엔딩
        }

        // 애정도와 체력 확인
        if (affection < 40 || health < 40)
        {
            return 1; // 평범한 고치
        }

        // 직업 엔딩 결정 (40 이상인 스탯 기준)
        int careerEnding = DetermineCareer(cleanliness, intelligence, sociality, willpower);
        if (careerEnding != -1)
        {
            return careerEnding + 2; // 2번부터 직업 엔딩
        }

        return 1; // 기본 엔딩으로 대체
    }

    int DetermineCareer(int cleanliness, int intelligence, int sociality, int willpower)
    {
        // 40을 넘는 스탯 확인
        List<int> highStats = new List<int>();
        if (cleanliness >= 40) highStats.Add(0); // 청결도
        if (intelligence >= 40) highStats.Add(1); // 지능
        if (sociality >= 40) highStats.Add(2); // 사회성
        if (willpower >= 40) highStats.Add(3); // 의지력

        // 다중 스탯 처리
        if (highStats.Count > 1)
        {
            return ResolveTie(highStats); // 겹치는 경우 처리
        }
        else if (highStats.Count == 1)
        {
            return highStats[0]; // 단일 직업
        }

        return -1; // 해당 없음
    }

    int ResolveTie(List<int> highStats)
    {
        int randomIndex = Random.Range(0, highStats.Count);
        return highStats[randomIndex];
    }

    void ActivateEnding(int index)
    {
        // 모든 소품 비활성화
        foreach (var decoration in endingDecorations)
        {
            decoration.SetActive(false);
        }

        // 해당 엔딩 소품 활성화
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
        // 컷씬 대사 출력
        switch (currentDialogueIndex)
        {
            case 0:
                tamagotchiDialogue.text = "그동안 길러주셔서 감사합니다.";
                currentDialogueIndex++;
                break;
            case 1:
                tamagotchiDialogue.text = "당신 덕분에 저는...";
                currentDialogueIndex++;
                break;
            case 2:
                string[] endingNames = { "궁극의", "평범한", "청소왕", "박사", "인싸", "용사" };
                tamagotchiDialogue.text = $"{endingNames[endingIndex]} 고치가 되었어요!";
                currentDialogueIndex++;
                break;
            default:
                isIntroDialogue = false; // 컷씬 대사 종료
                currentDialogueIndex = 0; // 엔딩 대사로 인덱스 초기화
                ShowNextDialogue(); // 엔딩 대사 시작
                break;
        }
    }

    public void ShowNextDialogue()
    {
        if (isIntroDialogue)
        {
            ShowIntroDialogue(); // 컷씬 대사 진행
            return;
        }

        // 현재 엔딩의 대사 리스트 가져오기
        string[] dialogues = endingDialogues[endingIndex];

        // 현재 대사 출력
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
        // 터치 입력 감지
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 또는 화면 터치
        {
            ShowNextDialogue(); // 다음 대사 표시
        }
    }
}
