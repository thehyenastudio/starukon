using System.Linq;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public int id;
    public GameObject bonusObj;
    private int bonus;
    private bool sendDMG = false;

    private void Awake()
    {
        GameManager.Instance.enemys.Add(gameObject);
        gameObject.GetComponent<EnemyBulletScript>().id = GameManager.Instance.enemys.Count - 1;
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
            StartDie(true);
        }

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y <= -265.05f)
            {
                transform.position.Set(transform.position.x, -270.05f, transform.position.z);
                if (!sendDMG && GameManager.Instance.ready == false) GameManager.Instance.GetDamage(Random.Range(0.01f, 0.05f));
                sendDMG = true;
                StartCoroutine(collision.gameObject.GetComponent<PlayerScript>().GetDmg());
            }
            else
            {
                StartDie(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dieCol")
        {
            StartDie(true);
        }
    }

    public void StartDie(bool now)
    {
        bool dlt = false;
        int index = 0;
        while (!dlt && index < GameManager.Instance.enemys.Count)
        {
            if (GameManager.Instance.enemys[index].GetComponent<EnemyBulletScript>().id == gameObject.GetComponent<EnemyBulletScript>().id)
            {
                GameManager.Instance.enemys.RemoveAt(index);
                GameManager.Instance.enemys.RemoveAll(item => item != null);
                dlt = true;
            }
            else index++;
        }
        if (!now) Destroy(gameObject, 0.2f);
        else Destroy(gameObject);
    }
}
