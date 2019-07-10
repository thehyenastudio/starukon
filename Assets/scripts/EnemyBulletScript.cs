using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public GameObject bonusObj;
    private int bonus;

    private void Start()
    {
        GameManager.Instance.enemys.Add(gameObject);
        bonus = Random.Range(0, 100);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            ScoreManager.Instance.SetScore(100);
            if (bonus <= 25)
            {
                Instantiate(bonusObj, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y <= -265.05f)
            {
                transform.position.Set(transform.position.x, -270.05f, transform.position.z);
                StartCoroutine(collision.gameObject.GetComponent<PlayerScript>().GetDmg());
            }
            else
            {
                StartDie();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dieCol")
        {
            Destroy(gameObject);
        }
    }

    public void StartDie()
    {
        Destroy(gameObject, 0.2f);
    }
}
