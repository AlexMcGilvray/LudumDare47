using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            var pos = gameObject.transform.position;
            pos.x = player.transform.position.x;
            pos.z = player.transform.position.z;
            gameObject.transform.position = pos;
        }
    }

    private Camera _camera;
}
