using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;

    public float cameraApproachSpeed = 1.0f;

    public float cameraStartYOffset = 500.0f;

    public float cameraZoomInTime = 2.0f;

    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();

        _cameraStartY = gameObject.transform.position.y;
        _cameraZoomCurrent = cameraZoomInTime;
    }

    void Update()
    {
        if (_cameraZoomCurrent > 0.0f)
        {
            _cameraZoomCurrent -= Time.deltaTime;
            float cameraY = Mathf.Lerp(
                _cameraStartY,
                _cameraStartY + cameraStartYOffset,
                _cameraZoomCurrent / cameraZoomInTime);
            var camPos = gameObject.transform.position;
            camPos.y = cameraY;
            gameObject.transform.position = camPos;
        }

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

    private float _cameraStartY;
    private float _cameraZoomCurrent;
}
