using UnityEngine;
using UnityEngine.UI;

// Main GameManager class
public class GameManager : MonoBehaviour
{
    // #region Variables
    #region Variables
    public static GameManager instance = null;

    // UI elements for game state
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject globalVolume;

    // Game control buttons
    public Button startButton;
    public Button restartButton;
    public Button exitButton1, exitButton2;
    #endregion

    // #region Unity Lifecycle Methods
    #region Unity Lifecycle Methods
    void Start()
    {
        // Singleton instance setup
        instance = this;

        // Set initial UI states
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        // Button listeners
        startButton.onClick.AddListener(() => {
            startPanel.SetActive(false);
            globalVolume.SetActive(false);
            Snake.instance.StartGame();
        });

        restartButton.onClick.AddListener(() => {
            gameOverPanel.SetActive(false);
            globalVolume.SetActive(false);
            ScoreManager.instance.ResetScore();
            Tail.instance.ResetTail();
            Snake.instance.StartGame();
        });

        exitButton1.onClick.AddListener(() => {
            Application.Quit();
        });

        exitButton2.onClick.AddListener(() => {
            Application.Quit();
        });
    }
    #endregion
}
