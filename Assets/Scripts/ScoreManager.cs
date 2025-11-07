using UnityEngine;
using TMPro;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public TMP_Text scoreText;

    public static int lives = 3;
    public GameObject[] heartImages;

    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;

    public GameObject winPanel;
    public TMP_Text winScoreText;

    public static int totalInvaders = 0;
    public static int invadersKilled = 0;

    public TMP_Text timerText;
    public float timeLimit = 60f;
    private float currentTime;


    void Start()
    {
        score = 0;
        lives = 3;
        currentTime = timeLimit;

        UpdateScoreText();
        UpdateLivesUI();
        UpdateTimerUI();

        Time.timeScale = 1f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        totalInvaders = FindObjectsOfType<Collider2D>()
                         .Count(c => c.CompareTag("Invader10") ||
                                      c.CompareTag("Invader25") ||
                                      c.CompareTag("Invader50") ||
                                      c.CompareTag("Invader100"));
        invadersKilled = 0;
    }

    void Update()
    {
        if (Time.timeScale > 0 && lives > 0 && invadersKilled < totalInvaders)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                GameOver();
            }

            UpdateTimerUI();
        }
    }

    public static void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;

        if (pointsToAdd > 0)
        {
            invadersKilled++;
            ScoreManager manager = FindObjectOfType<ScoreManager>();
            if (invadersKilled >= totalInvaders)
            {
                manager?.GameWin();
            }
        }

        FindObjectOfType<ScoreManager>()?.UpdateScoreText();
    }

    public static void LoseLife()
    {
        lives--;
        ScoreManager manager = FindObjectOfType<ScoreManager>();

        if (manager != null)
        {
            manager.UpdateLivesUI();

            if (lives <= 0)
            {
                manager.GameOver();
            }
        }
    }

    // NUEVA FUNCIÓN ESTÁTICA para ser llamada por player.cs
    public static void TriggerGameOverByContact()
    {
        ScoreManager manager = FindObjectOfType<ScoreManager>();
        manager?.GameOver();
    }


    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i] != null)
            {
                heartImages[i].SetActive(i < lives);
            }
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int seconds = Mathf.FloorToInt(currentTime);
            timerText.text = "Tiempo: " + seconds.ToString();
        }
    }


    void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            finalScoreText.text = "Puntuación Final: " + score.ToString();
            Time.timeScale = 0f;
        }
    }

    void GameWin()
    {
        winPanel.SetActive(true);
        winScoreText.text = "Puntuación Final: " + score.ToString();
        Time.timeScale = 0f;
    }
}