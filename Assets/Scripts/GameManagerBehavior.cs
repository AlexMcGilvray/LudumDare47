using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour
{
    public GameObject uiGameObject;

    public float pointsPerSecond = 10;

    //public GameObject catRing;

    // Start is called before the first frame update
    void Start()
    {
        _ui = uiGameObject.GetComponent<UIBehavior>();
       // _catRing = catRing.GetComponent<CatRingBehavior>();
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

    // Update is called once per frame
    void Update()
    {     
        _score += pointsPerSecond * Time.deltaTime;
        UpdateUI();
        //player.GetComponentInChildren<MeshCollider>().o
        //Physics.OverlapBox()
        
        //Physics.
    //     foreach(var cat in _catRing.Cats)
    //     {
            
    //     }
     }

    //private CatRingBehavior _catRing;
}
