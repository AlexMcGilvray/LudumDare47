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
