using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroSceneUI : MonoBehaviour
{
    [Header("Screens")]
    public GameObject mainPanel;    // your existing "Panel"
    public GameObject powerPopup;   // "power" object, converted to UI popup
    public GameObject enemyPopup;   // "enemy" object, converted to UI popup

    [Header("Buttons")]
    public Button playButton;
    public Button powerOkayButton;
    public Button enemyOkayButton;

    void Start()
    {
        // Only the main intro panel is visible at start
        mainPanel.SetActive(true);
        powerPopup.SetActive(false);
        enemyPopup.SetActive(false);

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(OnPlayPressed);

        powerOkayButton.onClick.RemoveAllListeners();
        powerOkayButton.onClick.AddListener(OnPowerOkay);

        enemyOkayButton.onClick.RemoveAllListeners();
        enemyOkayButton.onClick.AddListener(OnEnemyOkay);
    }

    void OnPlayPressed()
    {
        mainPanel.SetActive(false);
        powerPopup.SetActive(true);
    }

    void OnPowerOkay()
    {
        powerPopup.SetActive(false);
        enemyPopup.SetActive(true);
    }

    void OnEnemyOkay()
    {
        if (GameManager.instance != null)
            GameManager.instance.ResetGame();

        SceneManager.LoadScene("GameScene");
    }
}