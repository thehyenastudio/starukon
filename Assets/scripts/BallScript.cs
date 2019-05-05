using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public AudioSource audioSource;
    public float speed = 100f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
        if (collision.gameObject.tag == "Player")
        {
            if (GameManager.Instance.controlBall = false)
            {
                float x = (transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;
                GetComponent<Rigidbody2D>().velocity = new Vector2(x, 1).normalized * speed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dieCol")
        {
            Destroy(gameObject);
            GameManager.PlayerDie();
        }
    }
}
