using UnityEngine;
using TMPro;

// This class manages the player's score in a game.
public class ScoreManager : MonoBehaviour
{
    // #region Variables
    #region Variables
    // A static instance of the ScoreManager for global access (singleton pattern).
    public static ScoreManager instance;

    // Reference to the TextMeshProUGUI component to display the score on the UI.
    public TextMeshProUGUI scoreText;

    // Internal variable to keep track of the score.
    private int score = 0;
    #endregion

    // #region Unity Lifecycle Methods
    #region Unity Lifecycle Methods
    // This method is called when the script instance is being loaded.
    void Awake()
    {
        // Ensure only one instance of ScoreManager exists.
        if (instance == null)
            instance = this;
    }

    // This method is called just before any of the Update methods are called for the first time.
    void Start()
    {
        // Initialize the score text on the UI.
        UpdateScoreText();
    }
    #endregion

    // #region Score Management Methods
    #region Score Management Methods
    // Public method to add points to the current score.
    public void AddScore(int points)
    {
        // Increase the score by the given points.
        score += points;

        // Update the displayed score text.
        UpdateScoreText();
    }

    // Private method to update the score text on the UI.
    private void UpdateScoreText()
    {
        // Modify the scoreText UI element to display the current score.
        scoreText.text = "Score: " + score;
    }

    // Internal method to reset the score to zero and update the UI.
    internal void ResetScore()
    {
        // Set the score to zero.
        score = 0;

        // Update the displayed score text.
        UpdateScoreText();
    }
    #endregion
}
