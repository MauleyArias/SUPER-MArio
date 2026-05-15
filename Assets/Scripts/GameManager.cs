using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }
    public int score { get; private set; }
    public float timeRemaining { get; private set; }

    private const float LevelTime = 400f;
    private bool timerRunning;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        NewGame();
    }

    private void Update()
    {
        if (!timerRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            Player player = FindObjectOfType<Player>();
            if (player != null) player.Death();
        }
    }

    public void StartTimer() => timerRunning = true;
    public void StopTimer() => timerRunning = false;

    public void NewGame()
    {
        lives = 3;
        coins = 0;
        score = 0;

        LoadLevel(1, 1);
    }

    public void GameOver()
    {
        NewGame();
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        timeRemaining = LevelTime;
        timerRunning = true;

        AudioManager.Instance?.PlayLevelMusic();

        string sceneName = $"{world}-{stage}";
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        CancelInvoke(nameof(ResetLevel));
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if (lives > 0) {
            LoadLevel(world, stage);
        } else {
            GameOver();
        }
    }

    public void AddCoin()
    {
        coins++;
        AddScore(100);
        AudioManager.Instance?.PlayCoin();

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void AddLife()
    {
        lives++;
    }

}
