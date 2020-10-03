using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRingBehavior : MonoBehaviour
{

    public float rotationSpeed = 3.0f;
    public GameObject catTemplateObject;

    // Start is called before the first frame update
    void Start()
    {
        _parentCylinder = gameObject.GetComponentInParent<CapsuleCollider>();
        var radius = _parentCylinder.radius;

        // want to make sure we have a circle for our ring and not an ellipses. This is in case I accidentally
        // change the scale of the X/Z axis non-uniformly
        Debug.Assert(Mathf.Approximately(gameObject.transform.localScale.x,gameObject.transform.localScale.z));

        Vector3 spawnPosition;
        spawnPosition.y = .1f;
        spawnPosition.x = radius * gameObject.transform.localScale.x;
        spawnPosition.z = 0;

        var cat = Instantiate(catTemplateObject,spawnPosition,Quaternion.identity);

        cat.transform.SetParent(this.transform);

        _cats.Add(cat);
    }

    // Update is called once per frame
    void Update()
    {
       var rotation = Vector3.zero;
       rotation.y = rotationSpeed * Time.deltaTime;
       gameObject.transform.Rotate(rotation,Space.Self);
    }

    private CapsuleCollider _parentCylinder;
    private List<GameObject> _cats = new List<GameObject>();
}