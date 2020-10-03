using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRingBehavior : MonoBehaviour
{

    private CapsuleCollider parentCylinder;

    public GameObject catTemplateObject;

    // Start is called before the first frame update
    void Start()
    {
        parentCylinder = gameObject.GetComponentInParent<CapsuleCollider>();

        var radius = parentCylinder.radius;

        // want to make sure we have a circle for our ring and not an ellipses. This is in case I accidentally
        // change the scale of the X/Z axis non-uniformly
        Debug.Assert(Mathf.Approximately(gameObject.transform.localScale.x,gameObject.transform.localScale.z));

        Vector3 spawnPosition;
        spawnPosition.y = .5f;
        spawnPosition.x = radius * gameObject.transform.localScale.x;
        spawnPosition.z = 0;

        Instantiate(catTemplateObject,spawnPosition,Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
