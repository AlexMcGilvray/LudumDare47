using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float PlayerSpeed = 10.0f;

    bool wDown;
    bool sDown;
    bool aDown;
    bool dDown;

    void Start()
    {

    }

    void Update()
    {
        wDown = Input.GetKey(KeyCode.W);
        sDown = Input.GetKey(KeyCode.S);
        aDown = Input.GetKey(KeyCode.A);
        dDown = Input.GetKey(KeyCode.D);

        if (Input.anyKey) //keyboard has priority over gamepad
        {
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

    void OnTriggerEnter(Collider other)
    {


        var isCat = other.gameObject.GetComponent<CatBehavior>() != null ? true : false;
        if (isCat)
        {
            Destroy(gameObject);
        }
        else
        {
        }
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("enter " + collision.gameObject.name);
    //     gameObject.transform.position = _lastPosition;

    // }
}
