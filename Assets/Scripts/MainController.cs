using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController Instance { get; private set; }

    public AudioPlayer SoundManager { get; private set; }
    public UIManager UIManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

     
        SoundManager = GetComponentInChildren<AudioPlayer>(true);
        UIManager = GetComponentInChildren<UIManager>(true);

        if (SoundManager == null)
        {
            Debug.LogError("AudioPlayer not found under MainController.");
        }

        if (UIManager == null)
        {
            Debug.LogError("UIManager not found under MainController.");
        }
    }
}
