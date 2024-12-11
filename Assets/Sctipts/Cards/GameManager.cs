using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<Card> allCards;
    private Card flippedCard;
    private bool isFlipping = false;

    [SerializeField]
    private Slider timeoutSlider;
    private float timeLimit = 35f;
    private float currentTime;
    private int totalMatches = 9;
    private int matchesFound = 0;
    private int currentScore = 0;
    //private int maxScore = 30;

    [SerializeField]
    private TextMeshProUGUI timeoutText;

    [SerializeField]
    private GameObject gameOverPanel;
    private bool isGameOver = false;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private TextMeshProUGUI ScoreNum;

    [SerializeField]
    private TextMeshProUGUI gameOverScore;
   
 

    private void Awake()
    {
        if(instance == null){
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Board board = FindObjectOfType<Board>();
        allCards = board.GetCards();

        totalMatches = allCards.Count/2;//�̺κ��� �߰��ؾ� ������ �ȳ�
        currentTime = timeLimit;
        SetCurrentTimeText();
        SetScoreNumText();
        StartCoroutine("FlipAllCardsRoutine");

    }

    void SetScoreNumText()
    {
        
        ScoreNum.SetText(currentScore.ToString());
    }

    void SetCurrentTimeText()
    {
        int timeSec = Mathf.CeilToInt(currentTime); //�ø��Լ�
        timeoutText.SetText(timeSec.ToString());
    }

    IEnumerator FlipAllCardsRoutine() //���� �� ī�� �����ִ� �ð�
    {
        AudioManager.instance.Playsfx(AudioManager.Sfx.clear);
        isFlipping = true;
        yield return new WaitForSeconds(0.5f);
        FlipAllCards();
        AudioManager.instance.PlayBgm(true);

        yield return new WaitForSeconds(2.5f);
        FlipAllCards();
        yield return new WaitForSeconds(0.5f);
        isFlipping=false;

        //�� ������ ������ �ʽð� ����
        yield return StartCoroutine("CountDownTimerRoutine");
    }

    IEnumerator CountDownTimerRoutine()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            //�Ҽ��� ���
            timeoutSlider.value = currentTime / timeLimit;
            SetCurrentTimeText() ;
            yield return null;
        }

        //�ð��� �� ����
        isGameOver = true;
        GameOver(false);
    }

    void FlipAllCards()
    {
        foreach (Card card in allCards)
        {
            card.FlipCard();
        }
    }

    public void CardClicked(Card card)
    {
        if (isFlipping||isGameOver) { return; }
        card.FlipCard();
        AudioManager.instance.Playsfx(AudioManager.Sfx.click);

        if(flippedCard == null)
        {
            flippedCard = card;
        }
        else
        {
            //check match
            StartCoroutine(CheckMatchRoutine(flippedCard, card));
        }
    }

    IEnumerator CheckMatchRoutine(Card card1, Card card2)
    {
        isFlipping =true;
        if (card1.cardID == card2.cardID)
        {
            card1.SetMatched();
            card2.SetMatched();
            matchesFound++;
            currentScore += 3;
            SetScoreNumText();
            AudioManager.instance.Playsfx(AudioManager.Sfx.matching);

            if (matchesFound == totalMatches)
            {
                StopCoroutine("CountDownTimerRoutine"); // Ÿ�̸� ����
                currentScore += 3;
                GameOver(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.6f);

            card1.FlipCard();
            card2.FlipCard();
            yield return new WaitForSeconds(0.4f);
        }
        isFlipping = false;
        flippedCard = null;
    }


    void GameOver(bool success)
    {
        //if (isGameOver) return; // �̹� ���� ���� ���¶�� ��ȯ
        isGameOver = true;      // ���� ���� ���·� ����

        StopCoroutine("CountDownTimerRoutine"); // Ÿ�̸� �ڷ�ƾ ����
        AudioManager.instance.Playsfx(AudioManager.Sfx.clear);
        AudioManager.instance.PlayBgm(false);
        SaveScoreToMoneyManager();


        if (success)
        {
            gameOverText.SetText("Clear!\nBonus +3!");
            gameOverScore.SetText("Score: 30");
            SaveScoreToMoneyManager();

        }
        else
        {
            gameOverText.SetText("Game Over");
            gameOverScore.SetText("Score:"+currentScore);

        }

        Invoke("ShowGameOverPanel", 2f);
        AudioManager.instance.Playsfx(AudioManager.Sfx.ending);

    }

    void SaveScoreToMoneyManager()
    {
        if (MoneyManager.instance != null)
        {
            DataManager.instance.money += currentScore;
            MoneyManager.instance.UpdateMoneyUI();
        }
    }
    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);

    }
    public void OK()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Room");
    }

}
