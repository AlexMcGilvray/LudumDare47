using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour
{
    public GameObject uiGameObject;

    public AudioClip gameMusic;

    //public AudioClip gameOverMusic;

    public float pointsPerSecond = 10;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            if (!_isAlive)
            {
                _ui.ShowGameOver();
            }
        }
    }

    public void AddScore(int points)
    {
        _score += points;
    }

    void Start()
    {
        _ui = uiGameObject.GetComponent<UIBehavior>();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = gameMusic;
        _audioSource.volume = 0.7f;
        _audioSource.Play();
    }

    void UpdateUI()
    {
        _ui.UpdateScore((int)_score);
    }

    private float _score = 0;

    private UIBehavior _ui;

    void Update()
    {
        if (IsAlive)
        {
            _score += pointsPerSecond * Time.deltaTime;
            UpdateUI();
        }

    }

    private bool _isAlive = true;

    private AudioSource _audioSource;

}
