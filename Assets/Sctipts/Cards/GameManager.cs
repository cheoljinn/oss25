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

        totalMatches = allCards.Count/2;//이부분을 추가해야 오류가 안남
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
        int timeSec = Mathf.CeilToInt(currentTime); //올림함수
        timeoutText.SetText(timeSec.ToString());
    }

    IEnumerator FlipAllCardsRoutine() //시작 전 카드 보여주는 시간
    {
        isFlipping = true;
        yield return new WaitForSeconds(0.5f);
        FlipAllCards();
        yield return new WaitForSeconds(2.5f);
        FlipAllCards();
        yield return new WaitForSeconds(0.5f);
        isFlipping=false;

        //다 보여준 다음에 초시계 시작
        yield return StartCoroutine("CountDownTimerRoutine");
    }

    IEnumerator CountDownTimerRoutine()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            //소숫점 계산
            timeoutSlider.value = currentTime / timeLimit;
            SetCurrentTimeText() ;
            yield return null;
        }

        //시간이 다 끝남
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

            if(matchesFound == totalMatches)
            {
                StopCoroutine("CountDownTimerRoutine"); // 타이머 종료
                //currentScore += 3;
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
        //if (isGameOver) return; // 이미 게임 종료 상태라면 반환
        isGameOver = true;      // 게임 종료 상태로 설정

        StopCoroutine("CountDownTimerRoutine"); // 타이머 코루틴 중지
        if (success)
        {
            gameOverText.SetText("Clear!\nBonus +3!");
            gameOverScore.SetText("Score: 30");
        }
        else
        {
            gameOverText.SetText("Game Over");
            gameOverScore.SetText("Score:"+currentScore);

        }

        Invoke("ShowGameOverPanel", 2f);
    }


    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);

    }


    public void OK()
    {
        SceneManager.LoadScene(""); //씬 불러오기
    }
}
