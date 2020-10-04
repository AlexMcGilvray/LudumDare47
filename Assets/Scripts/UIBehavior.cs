using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIBehavior : MonoBehaviour
{
    public Text scoreText;

    public RawImage gameOverImage;

    public Button restartButton;

    void Start()
    {
        gameOverImage.enabled = false;
        restartButton.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ShowGameOver()
    {
        gameOverImage.enabled = true;
        restartButton.gameObject.SetActive(true);

        restartButton.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        
    }
}
