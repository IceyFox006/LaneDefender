using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    [SerializeField] private GameObject _playerObject;
    [SerializeField] private AudioClip _deathSE;
    private int currentPlayerHP;

    [Header("UI")]
    [SerializeField] private ScoreTrackerSO _scoreTrackerSO;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _playerHPText;
    [SerializeField] private Image _playerHPBar;
    [SerializeField] private GameObject _gameOverCanvas;

    public static GameController Instance { get => instance; set => instance = value; }
    public GameObject PlayerObject { get => _playerObject; set => _playerObject = value; }
    public ScoreTrackerSO ScoreTrackerSO { get => _scoreTrackerSO; set => _scoreTrackerSO = value; }
    public GameObject GameOverCanvas { get => _gameOverCanvas; set => _gameOverCanvas = value; }
    public AudioClip DeathSE { get => _deathSE; set => _deathSE = value; }
    public int CurrentPlayerHP { get => currentPlayerHP; set => currentPlayerHP = value; }

    private void Awake()
    {
        instance = this;
        currentPlayerHP = _playerObject.GetComponent<EntityController>().EntityType.MaxHP;
        _scoreTrackerSO.Scores.Add(0);
        UpdateScore(0);
        _highScoreText.text = _scoreTrackerSO.HighScore.ToString();
    }
    public void UpdateScore(int updateAmount)
    {
        _scoreTrackerSO.Scores[_scoreTrackerSO.Scores.Count - 1] += updateAmount;
        _currentScoreText.text = _scoreTrackerSO.Scores[_scoreTrackerSO.Scores.Count - 1].ToString();
        foreach (int score in _scoreTrackerSO.Scores)
        {
            if (score > _scoreTrackerSO.HighScore)
            {
                _scoreTrackerSO.HighScore = score;
                _highScoreText.text = _scoreTrackerSO.HighScore.ToString();
                break;
            }
        }
    }
    public void UpdatePlayerHPUI(int totalPlayerHP)
    {
        Debug.Log("UI updated");
        _playerHPText.text = currentPlayerHP.ToString() + "/" + totalPlayerHP.ToString();
        _playerHPBar.fillAmount = (float)currentPlayerHP / totalPlayerHP;
    }
    public void ResetScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
