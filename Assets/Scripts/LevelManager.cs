
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2.0f;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    // 跳转到主游戏战斗场景
    public void LoadGame()
    {
        if (scoreKeeper == null)
        {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();
        }

        if (scoreKeeper != null)
        {
            scoreKeeper.ResetScore();
        }

        SceneManager.LoadScene("Game");
    }

    // 跳转到主菜单场景
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // 新增：跳转到玩法说明场景（Instructions）
    public void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    // 跳转到游戏结束结算场景
    public void LoadGameOver()
    {
        if (scoreKeeper == null)
        {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();
        }

        if (scoreKeeper != null)
        {
            scoreKeeper.SaveHighScoreIfNeeded();
        }

        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    // 退出游戏
    public void QuitGame()
    {
        Debug.Log("Quitting Game....");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string SceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneName);
    }
}
