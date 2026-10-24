using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultSceneUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text titleText;
    public TMP_Text messageText;
    public TMP_Text buttonText;
    public Button actionButton;

    void Start()
    {
        LoadFromData();
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(OnActionButtonPressed);
    }

    void LoadFromData()
    {
        if (ResultData.instance == null)
        {
            SetUI("Level Clear", "Great job!", "Next Level");
            return;
        }

        SetUI(
            ResultData.instance.titleText,
            ResultData.instance.messageText,
            ResultData.instance.buttonText
        );
    }

    void SetUI(string title, string message, string button)
    {
        titleText.text   = title;
        messageText.text = message;
        buttonText.text  = button;
    }

    void OnActionButtonPressed()
    {
        if (ResultData.instance != null && ResultData.instance.isGameOver)
            GameManager.instance.ResetGame();

        LevelManager.instance.isProcessing = false;
        LevelManager.instance.LoadLevel(GameManager.instance.currentLevelIndex);
        SceneManager.LoadScene("GameScene");
    }
}