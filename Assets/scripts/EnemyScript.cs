using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject bullet;
    private float speed = 30f;
    private float TimeGun;
    private float controlTimeGun = 2f;

    private float timeBonus;
    private float controlTimeBonus = 50f;
    private bool getBonus = true;

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        Fire();

        timeBonus += Time.deltaTime;
        if (timeBonus >= controlTimeBonus)
        {
            getBonus = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "col")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            speed *= (-1);
        }
        else if (collision.gameObject.tag == "ball" && getBonus)
        {
            ScoreManager.Instance.SetScore(250);
            getBonus = false;
            timeBonus = 0;
        }
    }

    private void Fire()
    {
        TimeGun += Time.deltaTime;
        if (TimeGun >= controlTimeGun / (GameManager.Instance.speed / 4) && GameManager.Instance.ready == false)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), Quaternion.identity);
            TimeGun = 0;
        }
    }
}
