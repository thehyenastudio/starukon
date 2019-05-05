using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;
    public float speed = 40f;

    public GameObject ball;

    void Start()
    {
        animator = GetComponent<Animator>();
        Instantiate(ball, new Vector3(transform.position.x, transform.position.y + 0.04f, 5), Quaternion.identity);
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed * Input.GetAxis("Horizontal") * GameManager.Instance.playerSpeed;

        if (transform.position.y <= -290.05f)
        {
            transform.position.Set(transform.position.x, -340.05f, transform.position.z);
            StartCoroutine(UnDmg());
        }
    }

    public void StartDie()
    {
        animator.SetBool("die", true);
        Destroy(gameObject, 0.7f);
    }

    public IEnumerator GetDmg()
    {
        for (float time = 0.0f; time <= 1; time += Time.deltaTime)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(-270.05f, -340.05f, time), transform.position.z);
            yield return null;
        }
    }

    public IEnumerator UnDmg()
    {
        for (float time = 0.0f; time <= 1; time += Time.deltaTime)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(-340.05f, -270.05f, time), transform.position.z);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bonus")
        {
            Destroy(collision.GetComponent<Rigidbody2D>());
            collision.GetComponent<Animator>().SetBool("open", true);
            var bonus = Random.Range(0, 7);
            GameManager.Instance.GetBonus(bonus);
            Destroy(collision.gameObject, 1f);
        }
    }
}
