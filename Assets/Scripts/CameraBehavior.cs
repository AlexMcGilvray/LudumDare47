using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;

    public float cameraApproachSpeed = 3.0f;

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
            Vector2 playerTopDownPos;
            playerTopDownPos.x = player.transform.position.x;
            playerTopDownPos.y = player.transform.position.z;

            Vector2 cameraTopDownPos;
            cameraTopDownPos.x = gameObject.transform.position.x;
            cameraTopDownPos.y = gameObject.transform.position.z;

            Vector2 approachDirection = playerTopDownPos - cameraTopDownPos;
            approachDirection.Normalize();

            Vector2 approachVelocity = approachDirection * cameraApproachSpeed * Time.deltaTime;

            Vector3 cameraPosition = gameObject.transform.position;
            cameraPosition.x += approachVelocity.x;
            cameraPosition.z += approachVelocity.y;

            gameObject.transform.position = cameraPosition;
        }
    }

    private Camera _camera;
}
