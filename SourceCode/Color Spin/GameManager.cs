using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string introSceneName = "Intro_Scene";
    public string mainSceneName = "Main_Scene";
    public Text timerText;
    public Text scoreText; // ���� �ؽ�Ʈ �߰�
    public GameObject gameOverUI; // ���� ���� UI
    private Ball ball; // Ball ��ũ��Ʈ ����

    private float timer;
    public bool isGameStarted; // public���� ����
    private int score;

    void Start()
    {
        gameOverUI.SetActive(false);
        ball = FindObjectOfType<Ball>(); // Ball ��ü ã��
        if (ball == null)
        {
            Debug.LogError("Ball object not found in the scene!");
        }
        InitializeGame();
    }

    void Update()
    {
        if (isGameStarted)
        {
            timer += Time.deltaTime;
            UpdateTimer();
        }
        else
        {
            CheckForInput();
        }
    }

    void InitializeGame()
    {
        isGameStarted = false;
        timer = 0.0f; // Ÿ�̸� �ʱⰪ 0
        score = 0; // ���� �ʱⰪ 0
        UpdateTimer();
        UpdateScore(); // �ʱ� ���� ������Ʈ
        if (ball != null)
        {
            ball.ResetPosition(); // �� ��ġ �ʱ�ȭ
        }
    }

    void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        Debug.Log("StartGame called");
        isGameStarted = true;
        Time.timeScale = 1f; // ���� �ӵ� ����
        if (ball != null)
        {
            ball.StartMoving(); // �� �����̱� ����
        }
    }

    void UpdateTimer()
    {
        // ����ȭ�� ���� ���ڿ� ���� ��� ���� ����� �̿��� �ؽ�Ʈ ������Ʈ
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
    }

    void UpdateScore()
    {
        // ����ȭ�� ���� ���ڿ� ���� ��� ������ ���� ��� ���
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Debug.Log("Game over function called");
        SaveScoreAndTime();
        // ���� ���� UI Ȱ��ȭ
        gameOverUI.SetActive(true);
        isGameStarted = false;
        Time.timeScale = 0f; // ������ ����
    }

    public void Retry()
    {
        Debug.Log("Retry button clicked");
        Time.timeScale = 1f; // ���� �ӵ� ����
        SceneManager.LoadScene(mainSceneName); // ���� �� �ٽ� �ε�
    }

    public void Exit()
    {
        Debug.Log("Exit button clicked");
        Time.timeScale = 1f; // ���� �ӵ� ����
        SceneManager.LoadScene(introSceneName); // ��Ʈ�� ������ �̵�
    }

    void SaveScoreAndTime()
    {
        int[] topScores = new int[3];
        float[] topTimes = new float[3];

        // ���� ��� �ҷ�����
        for (int i = 0; i < 3; i++)
        {
            topScores[i] = PlayerPrefs.GetInt("TopScore" + i, 0);
            topTimes[i] = PlayerPrefs.GetFloat("TopTime" + i, 0f);
        }

        // ���ο� ��� �߰�
        int newScore = score;
        float newTime = timer;

        // ��� ����
        for (int i = 0; i < 3; i++)
        {
            if (newScore > topScores[i] || (newScore == topScores[i] && newTime < topTimes[i]))
            {
                // ���� �������� �з����� ����� ����
                int tempScore = topScores[i];
                float tempTime = topTimes[i];

                // ���ο� ��� ����
                topScores[i] = newScore;
                topTimes[i] = newTime;

                // �з��� ����� ���� ������
                newScore = tempScore;
                newTime = tempTime;
            }
        }

        // ���� 3�� ��� ����
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt("TopScore" + i, topScores[i]);
            PlayerPrefs.SetFloat("TopTime" + i, topTimes[i]);
        }

        PlayerPrefs.Save();
    }
}
