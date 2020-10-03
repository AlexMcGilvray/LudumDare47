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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        wDown = Input.GetKey(KeyCode.W);
        sDown = Input.GetKey(KeyCode.S);
        aDown = Input.GetKey(KeyCode.A);
        dDown = Input.GetKey(KeyCode.D);

        if (Input.anyKey)
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

        //gameObject.transform.Rotate(0,0.2f,0,Space.Self);
    }
}
