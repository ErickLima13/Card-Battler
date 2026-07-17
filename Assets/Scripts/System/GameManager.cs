using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private float _transitionTime;

    [SerializeField] private TextMeshProUGUI _winLoseDisplay;

    public bool IsGameActive { get; private set; }


    private void Start()
    {
        IsGameActive = true;
    }

    private void PlayerWin()
    {
        IsGameActive = false;
        _winLoseDisplay.text = "You defeat the boss!";
        StartCoroutine(RestartGame());
    }

    private void PlayerLose()
    {
        IsGameActive = false;
        _winLoseDisplay.text = "Game Over!";
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene("Game");
    }

    private void OnEnable()
    {
        PlayerEvents.OnPlayerDeath += PlayerLose;
        BossEvents.OnBossDeath += PlayerWin;
    }

    private void OnDisable()
    {
        PlayerEvents.OnPlayerDeath -= PlayerLose;
        BossEvents.OnBossDeath -= PlayerWin;
    }

    public void SetMessageGame(string message)
    {
        _winLoseDisplay.text = message;
        StartCoroutine(CleanUpText());
    }

    private IEnumerator CleanUpText()
    {
        yield return new WaitForSeconds(1f);
        _winLoseDisplay.text = "";
    }
}
