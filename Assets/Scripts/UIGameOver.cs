//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class UIGameOver : MonoBehaviour
//{
//    [SerializeField] TextMeshProUGUI scoreText;
//    ScoreKeeper scoreKeeper;
//    [SerializeField] TextMeshProUGUI highScoreText;

//    void Awake()
//    {
//        scoreKeeper = FindObjectOfType<ScoreKeeper>();
//    }

//    void Start()
//    {
//        scoreText.text =  "You Scored:\n" + scoreKeeper.GetScore();
//        highScoreText.text = "High Score:\n" + scoreKeeper.GetHighScore();
//    }



//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;      // 本次得分数字组件
    [SerializeField] TextMeshProUGUI highScoreText;  // 历史最高分数字组件

    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        // 如果 Awake 时未找到，再次尝试获取一次
        if (scoreKeeper == null)
        {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();
        }

        if (scoreKeeper != null)
        {
            // 赋值纯得分数值（避免与 UI 场景原有的 YOU SCORED 标题文字冲突）
            if (scoreText != null)
            {
                //scoreText.text = scoreKeeper.GetScore().ToString();
                scoreText.text = "YOU SCORED: " + scoreKeeper.GetScore();
            }

            // 赋值最高分数值
            if (highScoreText != null)
            {
                //highScoreText.text = scoreKeeper.GetHighScore().ToString();
                highScoreText.text = "HIGH SCORE: " + scoreKeeper.GetHighScore();
            }
        }
        else
        {
            Debug.LogWarning("ScoreKeeper not found in GameOver scene.");
        }
    }
}