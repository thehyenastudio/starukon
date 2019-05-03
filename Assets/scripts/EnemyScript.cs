using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 30f;

    public GameObject bullet;
    private float TimeGun;
    public float controlTimeGun = 2f;

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        Fire();
    }

    void Fire()
    {
        TimeGun += Time.deltaTime;
        if (TimeGun >= controlTimeGun)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), Quaternion.identity);
            TimeGun = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "col")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            speed *= (-1);
        }
        else if (collision.gameObject.tag == "ball")
        {
            GameManager.Instance.SetScore(200);
        }
    }
}
