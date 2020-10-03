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
        // want to make sure we have a circle for our ring and not an ellipses. This is in case I accidentally
        // change the scale of the X/Z axis non-uniformly
        Debug.Assert(Mathf.Approximately(gameObject.transform.localScale.x, gameObject.transform.localScale.z));

        _parentCylinder = gameObject.GetComponentInParent<CapsuleCollider>();

        var radius = _parentCylinder.radius * gameObject.transform.localScale.x;
        var circumference = radius * Mathf.PI * 2;
        var catHitboxLength = catTemplateObject.GetComponent<BoxCollider>().size.z;
        int numCatsToGenerate = (int)(circumference / catHitboxLength);
        float angleStepSize = (Mathf.PI * 2) / numCatsToGenerate;

        Debug.Log("circumference " + circumference);
        Debug.Log("catHitboxLength " + catHitboxLength);
        Debug.Log("numCatsToGenerate" + numCatsToGenerate);
        Debug.Log("angleStepSize " + angleStepSize);

        for (int i = 0; i < numCatsToGenerate; ++i)
        {
            float currentAngle = i * angleStepSize;
            Debug.Assert(currentAngle < Mathf.PI * 2);
            Vector3 spawnPosition;
            spawnPosition.z = Mathf.Sin(currentAngle) * radius;
            spawnPosition.x = Mathf.Cos(currentAngle) * radius;
            spawnPosition.y = 0;
          
            Quaternion orientation = Quaternion.Euler(0,currentAngle * -Mathf.Rad2Deg,0);
            var cat = Instantiate(catTemplateObject, spawnPosition, orientation);
            // var cat = Instantiate(catTemplateObject, spawnPosition, Quaternion.identity);
            cat.transform.SetParent(this.transform);

            _cats.Add(cat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = Vector3.zero;
        rotation.y = rotationSpeed * Time.deltaTime;
        gameObject.transform.Rotate(rotation, Space.Self);
    }

    public List<GameObject> Cats => _cats;

    private CapsuleCollider _parentCylinder;
    private List<GameObject> _cats = new List<GameObject>();
}