﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Moving,
    Dashing
}

public class PlayerBehavior : MonoBehaviour
{
    public float PlayerSpeed = 10.0f;

    public float DashSpeed = 50.0f;

    public float DashTime = 0.5f;

    public float DashCooldownTime = 1.5f;

    public GameObject gameManager;

    public PlayerState State => _state;

    public AudioClip DashSound;
    public AudioClip OofSound;

    private AudioSource _audioSource;

    void Start()
    {
        _gameManager = gameManager.GetComponent<GameManagerBehavior>();
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    void SetState(PlayerState state)
    {
        _state = state;
    }

    void UpdateMovement()
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
        else
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

    }



    void Update()
    {
        bool AnyGamepadButtonDown()
        {
            return Input.GetButtonDown("Jump") ||
            Input.GetButtonDown("Fire1") ||
            Input.GetButtonDown("Fire2") || 
            Input.GetButtonDown("Fire3");

        }
        switch (_state)
        {
            case PlayerState.Moving:
                if (_dashCooldownTimer > 0)
                {
                    _dashCooldownTimer -= Time.deltaTime;
                }
                if ((AnyGamepadButtonDown() || Input.GetKeyDown(KeyCode.Space)) && _dashCooldownTimer <= 0)
                {
                    Vector3 lookAtRotation = Vector3.zero;
                    lookAtRotation.x = Mathf.Sin(Mathf.Deg2Rad * gameObject.transform.rotation.eulerAngles.y);
                    lookAtRotation.z = Mathf.Cos(Mathf.Deg2Rad * gameObject.transform.rotation.eulerAngles.y);
                    _dashDirection = lookAtRotation;
                    _dashDirection.Normalize();
                    _dashTimer = DashTime;
                    _dashCooldownTimer = DashCooldownTime;
                    _audioSource.PlayOneShot(DashSound);
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

    private void Kill()
    {
        _gameManager.IsAlive = false;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        var isCat = other.gameObject.GetComponent<CatBehavior>() != null ? true : false;

        switch (_state)
        {
            case PlayerState.Moving:
                if (isCat)
                {
                    Kill();
                }
                break;

            case PlayerState.Dashing:
                if (isCat)
                {
                    var cat = other.gameObject.GetComponent<CatBehavior>();
                    if (cat.State == CatState.Ricochet)
                    {
                        // TODO make a score table or something..
                        _gameManager.AddScore(50);
                        other.gameObject.GetComponent<CatBehavior>().OnHitByPlayer();
                    }
                    else if (cat.State == CatState.Ring)
                    {
                        Kill();
                    }

                }
                else
                {
                    other.GetComponent<Rigidbody>()?.AddForce(_dashDirection * DashHitSpeed, ForceMode.Impulse);
                }
                break;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        var isCat = collision.gameObject.GetComponent<CatBehavior>() != null ? true : false;
        if (!isCat && _state == PlayerState.Dashing)
        {
            _audioSource.PlayOneShot(OofSound);
            collision.gameObject.GetComponent<Rigidbody>()?.AddForce(_dashDirection * DashHitSpeed);
        }
    }

    public float DashHitSpeed = 25.0f;
    private Vector3 _dashDirection;
    private float _dashTimer;
    private float _dashCooldownTimer;

    private GameManagerBehavior _gameManager;

    private PlayerState _state;
    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("enter " + collision.gameObject.name);
    //     gameObject.transform.position = _lastPosition;

    // }
}
