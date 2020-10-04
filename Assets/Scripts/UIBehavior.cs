using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour
{
    public Text scoreText;

    public RawImage gameOverImage;

    // Start is called before the first frame update
    void Start()
    {
        gameOverImage.enabled = false;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ShowGameOver()
    {
        gameOverImage.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
