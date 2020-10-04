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
        if (_richochetCollisionResponseDelaySeconds <= 0)
        {
            if (_state == CatState.Ricochet &&
                other.gameObject.GetComponent<PlayerBehavior>() == null)
            {
                _ricochetDirection = -_ricochetDirection;
                _ricochetDirection.x += Random.value / 2.0f;
                _ricochetDirection.z += Random.value / 2.0f;
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
}