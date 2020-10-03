using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRingBehavior : MonoBehaviour
{
    public float rotationSpeed = 3.0f;
    public GameObject catTemplateObject;
    public float catRingBuffer = 0.5f;
    public float catReleaseTimerBaseTarget = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // want to make sure we have a circle for our ring and not an ellipses. This is in case I accidentally
        // change the scale of the X/Z axis non-uniformly
        Debug.Assert(Mathf.Approximately(gameObject.transform.localScale.x, gameObject.transform.localScale.z));

        _parentCylinder = gameObject.GetComponentInParent<CapsuleCollider>();

        var radius = _parentCylinder.radius * gameObject.transform.localScale.x;
        var circumference = radius * Mathf.PI * 2;
        var catHitboxLength = catTemplateObject.GetComponent<BoxCollider>().size.z + catRingBuffer;
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

            _ringCats.Add(cat);
        }

        _catReleaseTimer = catReleaseTimerBaseTarget;
    }

    private void UpdateCatRingLayout()
    {
        var catHitboxLength = catTemplateObject.GetComponent<BoxCollider>().size.z + catRingBuffer;
        var numRingCats = _ringCats.Count;
        var newCircumference = numRingCats * catHitboxLength;
        var radius = newCircumference / (Mathf.PI * 2);
        float angleStepSize = (Mathf.PI * 2) / numRingCats;
          for (int i = 0; i < numRingCats; ++i)
        {
            float currentAngle = i * angleStepSize;
            Debug.Assert(currentAngle < Mathf.PI * 2);
            Vector3 spawnPosition;
            spawnPosition.z = Mathf.Sin(currentAngle) * radius;
            spawnPosition.x = Mathf.Cos(currentAngle) * radius;
            spawnPosition.y = 0;
            Quaternion orientation = Quaternion.Euler(0,currentAngle * -Mathf.Rad2Deg,0);

            _ringCats[i].transform.SetPositionAndRotation(spawnPosition,orientation);
        }
    }

    void Update()
    {
        var rotation = Vector3.zero;
        rotation.y = rotationSpeed * Time.deltaTime;
        gameObject.transform.Rotate(rotation, Space.Self);

        _catReleaseTimer -= Time.deltaTime;

        if (_catReleaseTimer <= 0)
        {
            int randomCatIdx = Random.Range(0,_ringCats.Count - 1);
            var cat = _ringCats[randomCatIdx];
            cat.transform.SetParent(null);
            _ringCats.RemoveAt(randomCatIdx);
            _catReleaseTimer = catReleaseTimerBaseTarget;
            UpdateCatRingLayout();
        }
    }

    public List<GameObject> Cats => _ringCats;

    private CapsuleCollider _parentCylinder;

    private List<GameObject> _ringCats = new List<GameObject>();

    private float _catReleaseTimer;
}