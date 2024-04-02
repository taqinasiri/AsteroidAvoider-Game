using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private ScoreSystem scoreSystem;

    public void EndGame()
    {
        asteroidSpawner.enabled = false;
        int finalScore = scoreSystem.EndTimer();
        gameOverText.text = $"Your Score: {finalScore}";
        gameOverDisplay.SetActive(true);
    }

    public void PlayAgain() => SceneManager.LoadScene("Game");

    public void ReturnToMenu() => SceneManager.LoadScene("Menu");
}