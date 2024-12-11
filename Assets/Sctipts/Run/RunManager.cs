using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance;

    public TextMeshProUGUI tmp;
    public float timeScoreRate = 10f;

    public float obstacleSpeed = 2.0f;
    public float increaseRate = 0.1f;

    private float currentScore = 0f; // ���� ����
    private bool isGameRunning = true; // ������ ���� ������ ����

    [SerializeField]
    private GameObject gameEndPanel;

    [SerializeField]
    private GameObject gameStartPanel;

    [SerializeField]
    private TextMeshProUGUI gameOverScore;

    [SerializeField]
    private TextMeshProUGUI finalScore;

    [SerializeField]
    private TextMeshProUGUI readySet;

    private void Awake()
    {
        // Singleton ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(ReadySet());
    }

    public void EndGame()
    {
        // ���� ���� ó��
        SoundManager.instance.PlayBgm(false);
        SoundManager.instance.Playsfx(SoundManager.Sfx.end);

        isGameRunning = false;
        gameOverScore.SetText(tmp.text);
        finalScore.SetText("Coin: "+CalScore().ToString());
        SaveScoreToMoneyManager();

        Invoke("ShowEndGamePanel", 2f);
    }

    public int CalScore()
    {
        int score;
        score = GetFinalScore() / 10;
        if (score > 30) { score = 30; }
        return score;
    }

    void ShowEndGamePanel()
    {
        gameEndPanel.SetActive(true);
    }

    private void UpdateScoreUI()
    {
        // ���� UI ������Ʈ
        if (tmp != null)
        {
            tmp.text = "Score: " + Mathf.FloorToInt(currentScore).ToString();
        }
    }

    public int GetFinalScore()
    {
        // ���� ������ ��ȯ
        return Mathf.FloorToInt(currentScore);
    }

    IEnumerator ReadySet()
    {
        gameStartPanel.SetActive(true);
        readySet.SetText("Ready");
        yield return new WaitForSeconds(2f);
        readySet.SetText("GO!");
        yield return new WaitForSeconds(2f);
        gameStartPanel.SetActive(false);
      
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(ScoreCalculationRoutine());
    }

    IEnumerator ScoreCalculationRoutine()
    {
        while (isGameRunning)
        {
            obstacleSpeed += increaseRate * Time.deltaTime;
            currentScore += timeScoreRate * Time.deltaTime;
            UpdateScoreUI();
            yield return null; // �� ������ ����
        }
    }

    void SaveScoreToMoneyManager()
    {
        int addedScore = CalScore();
        DataManager.instance.money += addedScore;
    }

    public void OnOKButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Room");
    }

}