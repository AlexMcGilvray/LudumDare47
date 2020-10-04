using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
    Moving,
    Dashing
}

public class PlayerBehavior : MonoBehaviour
{
    public float PlayerSpeed = 10.0f;

    public float DashSpeed = 50.0f;

    public float DashTime = 0.5f;

    public GameObject gameManager;

    void Start()
    {
        _gameManager = gameManager.GetComponent<GameManagerBehavior>();
    }

    void SetState(PlayerState state)
    {
        _state = state;
    }

    void UpdateMovement()
    {

        // movement
        if (Input.anyKey) //keyboard has priority over gamepad
        {
            bool wDown;
            bool sDown;
            bool aDown;
            bool dDown;

            wDown = Input.GetKey(KeyCode.W);
            sDown = Input.GetKey(KeyCode.S);
            aDown = Input.GetKey(KeyCode.A);
            dDown = Input.GetKey(KeyCode.D);

            Vector3 posDelta = Vector3.zero;

            if (wDown)
            {
                posDelta.z = PlayerSpeed * Time.deltaTime;
            }
            if (sDown)
            {
                posDelta.z = -PlayerSpeed * Time.deltaTime;
            }
            if (aDown)
            {
                posDelta.x = -PlayerSpeed * Time.deltaTime;
            }
            if (dDown)
            {
                posDelta.x = PlayerSpeed * Time.deltaTime;
            }

            gameObject.transform.position += posDelta;

            posDelta.Normalize();
            Vector3 lookAtRotation = Vector3.zero;
            lookAtRotation.y = Mathf.Rad2Deg * Mathf.Atan2(posDelta.x, posDelta.z);
            gameObject.transform.SetPositionAndRotation(
                gameObject.transform.position, Quaternion.Euler(lookAtRotation));
        }
        else
        {

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
            {
                Vector3 posDelta = Vector3.zero;
                posDelta.x = horizontal * PlayerSpeed * Time.deltaTime;
                posDelta.z = vertical * PlayerSpeed * Time.deltaTime;

                gameObject.transform.position += posDelta;

                posDelta.Normalize();
                Vector3 lookAtRotation = Vector3.zero;
                lookAtRotation.y = Mathf.Rad2Deg * Mathf.Atan2(posDelta.x, posDelta.z);
                gameObject.transform.SetPositionAndRotation(
                    gameObject.transform.position, Quaternion.Euler(lookAtRotation));
            }
        }
    }
    void Update()
    {
        switch (_state)
        {
            case PlayerState.Moving:
                if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
                {
                    Vector3 lookAtRotation = Vector3.zero;
                    lookAtRotation.x = Mathf.Sin(Mathf.Deg2Rad * gameObject.transform.rotation.eulerAngles.y);
                    lookAtRotation.z = Mathf.Cos(Mathf.Deg2Rad * gameObject.transform.rotation.eulerAngles.y);
                    _dashDirection = lookAtRotation;
                    _dashDirection.Normalize();
                    _dashTimer = DashTime;
                    SetState(PlayerState.Dashing);
                }
                else
                {
                    UpdateMovement();
                }

                break;

            case PlayerState.Dashing:
                Vector3 posDelta = Vector3.zero;
                posDelta.x = _dashDirection.x * DashSpeed * Time.deltaTime;
                posDelta.z = _dashDirection.z * DashSpeed * Time.deltaTime;
                gameObject.transform.position += posDelta;
                _dashTimer -= Time.deltaTime;
                if (_dashTimer <= 0)
                {
                    SetState(PlayerState.Moving);
                }
                break;
        }


    }

    void OnTriggerEnter(Collider other)
    {


        var isCat = other.gameObject.GetComponent<CatBehavior>() != null ? true : false;
        if (isCat)
        {
            _gameManager.IsAlive = false;
            Destroy(gameObject);
        }
        else
        {
        }
    }

    private Vector3 _dashDirection;
    private float _dashTimer;

    private GameManagerBehavior _gameManager;

    private PlayerState _state;
    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("enter " + collision.gameObject.name);
    //     gameObject.transform.position = _lastPosition;

    // }
}
