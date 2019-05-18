using UnityEngine;

public class BallScript : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    public Sprite[] spritesPlayer;
    private float speed = 100f;
    private bool control = false;

    private void Start()
    {
        speed = GameManager.Instance.ballSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }

    private void Update()
    {
        if (control)
        {
            transform.position = new Vector3(GameManager.Instance.playerObj.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
        if (collision.gameObject.tag == "Player")
        {
            if (GameManager.Instance.controlBall == false)
            {
                float x = (transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;
                GetComponent<Rigidbody2D>().velocity = new Vector2(x, 1).normalized * speed;
            }
            else
            {
                control = true;
                GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            }
        }
        else
        {
            control = false;
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

    public void SetSprite()
    {
        if (spriteRenderer.sprite != spritesPlayer[1])
        {
            spriteRenderer.sprite = spritesPlayer[1];
            GetComponent<Rigidbody2D>().gravityScale *= 2.5f;
            GetComponent<CircleCollider2D>().radius = 0.18f;
        }
    }
}
