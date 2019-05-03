using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float speed;
    public float speedMax = 1f;

    private void Start()
    {
        speed = Random.Range(0.1f, speedMax);
    }
    void Update()
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x - speed, transform.position.y, transform.position.z), transform.rotation);
    }
}
