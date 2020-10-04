using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour
{
    public GameObject uiGameObject;

    public float pointsPerSecond = 10;

    public bool IsAlive { get; set; } = true;

    void Start()
    {
        _ui = uiGameObject.GetComponent<UIBehavior>();
    }

    void AddToScore(int points)
    {
        _score += points;
        UpdateUI();
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
}
