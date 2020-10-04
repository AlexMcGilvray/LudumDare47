using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatState
{
    Ring,
    Ricochet,
    Leaving
}

public class CatBehavior : MonoBehaviour
{
    public float ricochetSpeed = 3.0f;

    public float ricochetSpeedIncreaseOnBounce = 1.0f;

    public float richochetCollisionResponseDelaySeconds = 2.0f;

    public int bouncesBeforeLeaving = 5;

    public int bounceRandomRange = 3;

    public float ringReleaseForceMultiplier = 20.0f;

    public void ChangeToRicochetMode()
    {
        SetState(CatState.Ricochet);
        _ricochetDirection = -gameObject.transform.right;
        _richochetCollisionResponseDelaySeconds = richochetCollisionResponseDelaySeconds;

        _bouncesLeft = bouncesBeforeLeaving + (int)(Random.value * bounceRandomRange);
    }
    public void SetState(CatState state)
    {
        _state = state;
    }
    void Start()
    {
        _ricochetSpeed = ricochetSpeed;
        _collider = gameObject.GetComponent<BoxCollider>();
    }

    void Update()
    {
        void updateTransform()
        {
            gameObject.transform.position += _ricochetDirection * _ricochetSpeed * Time.deltaTime;
            float directionAngle = Mathf.Atan2(-_ricochetDirection.x, -_ricochetDirection.z);
            gameObject.transform.rotation = Quaternion.Euler(0, directionAngle * Mathf.Rad2Deg, 0);
        }

        switch (_state)
        {
            case CatState.Ring:
                break;
            case CatState.Ricochet:
                _richochetCollisionResponseDelaySeconds -= Time.deltaTime;
                updateTransform();
                break;
            case CatState.Leaving:
                updateTransform();
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        void onTriggerEnterRingState()
        {
            if (other.gameObject.GetComponent<PlayerBehavior>() == null &&
                    other.gameObject.GetComponent<CatBehavior>() == null)
            {
                Vector3 direction = other.gameObject.transform.position - gameObject.transform.position;
                direction.Normalize();
                other.GetComponent<Rigidbody>()?.AddForce(
                    direction * _ricochetSpeed * ringReleaseForceMultiplier, ForceMode.Impulse);
            }
        }

        void onTriggerEnterRicochetState()
        {
            if (_richochetCollisionResponseDelaySeconds <= 0)
            {
                if (other.gameObject.GetComponent<PlayerBehavior>() == null)
                {
                    other.GetComponent<Rigidbody>()?.AddForce(
                        _ricochetDirection * _ricochetSpeed, ForceMode.Impulse);

                    _ricochetDirection = -_ricochetDirection;
                    _ricochetDirection.x += Random.value;
                    _ricochetDirection.z += Random.value;
                    _ricochetSpeed += ricochetSpeedIncreaseOnBounce;

                    _bouncesLeft--;
                    if (_bouncesLeft <= 0)
                    {
                        _ricochetSpeed = _ricochetSpeed * 2.0f;
                        SetState(CatState.Leaving);
                    }
                    //Debug.Log("Cat collided with " + other.gameObject.name + " in OnTriggerEnter");
                }
            }
            else
            {
                if (other.gameObject.GetComponent<PlayerBehavior>() == null &&
                    other.gameObject.GetComponent<CatBehavior>() == null)
                {
                    other.GetComponent<Rigidbody>()?.AddForce(
                        _ricochetDirection * _ricochetSpeed * ringReleaseForceMultiplier, ForceMode.Impulse);

                    _ricochetDirection.x += Random.value / 2.0f;
                    _ricochetDirection.z += Random.value / 2.0f;
                }
            }
        }

        switch (_state)
        {
            case CatState.Ricochet:
                onTriggerEnterRicochetState();
                break;
            case CatState.Ring:
                onTriggerEnterRingState();
                break;
        }

    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (_state == CatState.Ricochet &&
    //         collision.gameObject.GetComponent<PlayerBehavior>() == null)
    //     {
    //         _ricochetDirection = -_ricochetDirection;
    //         Debug.Log("Cat collided with " + collision.gameObject.name + " in OnCollisionEnter");
    //     }

    // }

    // when this hits zero the cat begins to leave the area

    private float _ricochetSpeed;
    private int _bouncesLeft;

    private CatState _state = CatState.Ring;

    private Vector3 _ricochetDirection;

    private float _richochetCollisionResponseDelaySeconds;

    private BoxCollider _collider;
}