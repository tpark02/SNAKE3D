using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject startPanel;
    public GameObject gameOverPanel;

    public Button startButton;
    public Button restartButton;
    public Button exitButton1, exitButton2;

    void Start()
    {
        instance = this;
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        startButton.onClick.AddListener(() => { 
            startPanel.SetActive(false);
            Snake.instance.StartGame();
        });
        restartButton.onClick.AddListener(() => { 
            gameOverPanel.SetActive(false);
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
}
