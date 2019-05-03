using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartObj : MonoBehaviour
{
    public Vector3 startPoint;
    public Transform EndPoint;
    public float timeMove = 0.0f;

    private void Start()
    {
        startPoint = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(startPoint.x, EndPoint.position.x, timeMove), startPoint.y, startPoint.z);
        timeMove += 0.5f * Time.deltaTime;

        if (transform.position.x >= EndPoint.position.x)
        {
            if (gameObject.GetComponent<EnemyScript>() != null) gameObject.GetComponent<EnemyScript>().enabled = true;
            else gameObject.GetComponent<PlayerScript>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(gameObject.GetComponent<StartObj>());
        }
    }
}
