

using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score;
    int highScore;

    static ScoreKeeper instance;
    string filePath;

    void Awake()
    {
        ManageSingleton();
        filePath = Path.Combine(Application.persistentDataPath, "HighScore.xml");
        LoadHighScore();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() => score;

    public void ModifyScore(int value)
    {
        score += value;
        score = Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log("Current Score: " + score);
    }

    public void ResetScore() => score = 0;

    public int GetHighScore() => highScore;

    public void SaveHighScoreIfNeeded()
    {
        if (score > highScore)
        {
            highScore = score;
            HighScoreData data = new HighScoreData { highScore = highScore };

            XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
            Debug.Log("High Score Saved: " + highScore);
        }
    }

    void LoadHighScore()
    {
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                HighScoreData data = (HighScoreData)serializer.Deserialize(stream);
                highScore = data.highScore;
            }
            Debug.Log("High Score Loaded: " + highScore);
        }
        else
        {
            highScore = 0;
        }
    }
}
/*public class ScoreKeeper : MonoBehaviour
{
    int score ;

    static ScoreKeeper instance;

    void Awake()
    {

        ManageSingleton();
    }
    void ManageSingleton()
    {
        //int instanceCount = FindObjectsOfType(GetType()).Length;
        // if (instanceCount > 1)
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    public int GetScore()
    { return score; }
    public void ModifyScore(int value)
    {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(score);
    }
    public void ResetScore()
    { score = 0; }
}*/