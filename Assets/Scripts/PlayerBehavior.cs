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

        if (wDown)
        {
            Vector3 pos = Vector3.zero;
            pos.z = PlayerSpeed * Time.deltaTime;
            gameObject.transform.position += pos;
        }
        if (sDown)
        {
            Vector3 pos = Vector3.zero;
            pos.z = -PlayerSpeed * Time.deltaTime;
            gameObject.transform.position += pos;
        }
        if (aDown)
        {
            Vector3 pos = Vector3.zero;
            pos.x = -PlayerSpeed * Time.deltaTime;
            gameObject.transform.position += pos;
        }
        if (dDown)
        {
            Vector3 pos = Vector3.zero;
            pos.x = PlayerSpeed * Time.deltaTime;
            gameObject.transform.position += pos;
        }
    }
}
