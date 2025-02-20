using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI startText;

    [Space(5f)]
    [SerializeField] private RectTransform gameOverUI;
    [SerializeField] private TextMeshProUGUI endScoreText;
    [SerializeField] private float lerpSpeed = 3f;

    private CanvasGroup canvasGroup;
    private float alpha = 0f;
    private float targetAlpha = 0f;
    private int currentScore = 0;
    private int highScore = 0;
    private string key = "HighScore";

    public TextMeshProUGUI StartText => startText;


    private void Start()
    {
        if (scoreText == null) Debug.LogError("점수 텍스트가 없습니다.");
        if (startText == null) Debug.LogError("시작 텍스트가 없습니다.");

        highScore = PlayerPrefs.GetInt(key, 0);

        startText.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(true);
        canvasGroup = gameOverUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
    }

    private void Update()
    {
        alpha = Mathf.Lerp(alpha, targetAlpha, lerpSpeed * Time.deltaTime);
        canvasGroup.alpha = alpha;
    }

    public void SetRestart()
    {
        SaveScore();
        targetAlpha = 1f;
        canvasGroup.interactable = true;
    }

    public void UpdateScore(int score)
    {
        currentScore = score;
        scoreText.text = score.ToString();
    }

    public void SaveScore()
    {
        // 최고 점수 갱신
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(key, highScore);
            PlayerPrefs.Save();
        }

        endScoreText.text = $"<color=#fff2>현재 점수:</color>  {currentScore}\n<color=#fff2>최고 점수:</color>  {highScore}";
    }
}
