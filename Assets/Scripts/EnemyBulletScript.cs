using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public int id;
    public GameObject bonusObj;
    private int bonus;
    private bool sendDMG = false;
    public int type = 0;

    public GameObject explosiv;

    private void Start()
    {
        GameManager.Instance.enemys.Add(gameObject);
        id = GameManager.Instance.enemys.Count - 1;
        bonus = Random.Range(0, 100);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball" && type == 0)
        {
            ScoreManager.Instance.SetScore(100);
            if (bonus <= 25)
            {
                Instantiate(bonusObj, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            StartDie(false);
        }

        if (collision.gameObject.tag == "Player")
        {
#if UNITY_STANDALONE || UNITY_WEBGL
            if (collision.gameObject.transform.position.y <= -265.05f)
#endif
#if UNITY_ANDROID
            if (collision.gameObject.transform.position.y <= -195.05f)
#endif
            {
#if UNITY_STANDALONE || UNITY_WEBGL
                transform.position.Set(transform.position.x, -270.05f, transform.position.z);
#endif
#if UNITY_ANDROID
                transform.position.Set(transform.position.x, -200.05f, transform.position.z);
#endif
                if (!sendDMG && GameManager.Instance.ready == false)
                {
                    switch (type)
                    {
                        case 0:
                            GameManager.Instance.GetDamage(Random.Range(0.01f, 0.05f));
                            break;
                        case 1:
                            GameManager.Instance.GetDamage(Random.Range(0.2f, 0.25f));
                            break;
                        case 2:
                            GameManager.Instance.GetDamage(Random.Range(0.2f, 0.5f));
                            break;
                        case 3:
                            GameManager.Instance.GetDamage(Random.Range(0.1f, 0.3f));
                            break;
                    }
                }
                sendDMG = true;
                StartCoroutine(collision.gameObject.GetComponent<PlayerScript>().GetDmg());
                StartDie(false);
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
#if UNITY_STANDALONE || UNITY_WEBGL
        while (!dlt && index < GameManager.Instance.enemys.Count)
#endif
#if UNITY_ANDROID
        while (!dlt && index < GameManager.Instance.enemys.Count + 1)
#endif
        {
            if (GameManager.Instance.enemys[index].GetComponent<EnemyBulletScript>().id == id)
            {
                GameManager.Instance.enemys.RemoveAt(index);
                dlt = true;
            }
            else index++;
        }

        if (!now)
        {
            Instantiate(explosiv, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.2f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
