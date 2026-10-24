using UnityEngine;

public class ResultData : MonoBehaviour
{
    public static ResultData instance;

    public string titleText;
    public string messageText;
    public string buttonText;
    public bool isGameOver;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}