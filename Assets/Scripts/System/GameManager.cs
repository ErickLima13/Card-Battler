using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private float _transitionTime;

    [SerializeField] private TextMeshProUGUI _winLoseDisplay;

    private void PlayerWin()
    {
        _winLoseDisplay.text = "You defeat the boss!";
        StartCoroutine(RestartGame());
    }

    private void PlayerLose()
    {
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
}
