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

    public float richochetCollisionResponseDelaySeconds = 2.0f;

    public void ChangeToRicochetMode()
    {
        SetState(CatState.Ricochet);
        _ricochetDirection = -gameObject.transform.right;
        _richochetCollisionResponseDelaySeconds = richochetCollisionResponseDelaySeconds;
    }
    public void SetState(CatState state)
    {
        _state = state;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case CatState.Ring:
                break;
            case CatState.Ricochet:
                gameObject.transform.position += _ricochetDirection * ricochetSpeed * Time.deltaTime;
                _richochetCollisionResponseDelaySeconds -= Time.deltaTime;
                float directionAngle = Mathf.Atan2(-_ricochetDirection.x, -_ricochetDirection.z);
                gameObject.transform.rotation = Quaternion.Euler(0,directionAngle * Mathf.Rad2Deg,0);
                break;
            case CatState.Leaving:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_richochetCollisionResponseDelaySeconds <= 0)
        {


            if (_state == CatState.Ricochet &&
                other.gameObject.GetComponent<CatBehavior>() != null)
            {
                _ricochetDirection = -_ricochetDirection;
                //Debug.Log("Cat collided with cat in OnTriggerEnter");

            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (_state == CatState.Ricochet &&
            collision.gameObject.GetComponent<CatBehavior>() != null)
        {
            _ricochetDirection = -_ricochetDirection;
            //Debug.Log("Cat collided with cat in OnCollisionEnter");
        }

    }

    private CatState _state = CatState.Ring;

    private Vector3 _ricochetDirection;

    private float _richochetCollisionResponseDelaySeconds;
}
