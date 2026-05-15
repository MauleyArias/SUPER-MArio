using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public Text scoreText;
    public Text worldText;
    public Text timeText;
    public Text livesText;
    public Text coinsText;

    private void Update()
    {
        if (GameManager.Instance == null) return;

        scoreText.text = GameManager.Instance.score.ToString("D6");
        worldText.text = $"{GameManager.Instance.world}-{GameManager.Instance.stage}";
        timeText.text  = Mathf.CeilToInt(GameManager.Instance.timeRemaining).ToString("D3");
        livesText.text = $"x{GameManager.Instance.lives}";
        coinsText.text = $"x{GameManager.Instance.coins:D2}";
    }
}
