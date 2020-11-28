using UnityEngine;

public class BallScript : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    public Sprite[] spritesPlayer;
    private Rigidbody2D rig;
    private float speed = 100f;
    private bool control = false;

    private float TimeUnduBig = 0f;
    public float controlTimeUnduBig = 20f;
    public bool big = false;

    private void Start()
    {
        speed = GameManager.Instance.ballSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = Vector2.up * speed;
    }

    private void Update()
    {
        rig.velocity = rig.velocity.normalized * speed;

        speed = GameManager.Instance.ballSpeed;
        if (control)
        {
            transform.position = new Vector3(GameManager.Instance.playerObj.transform.position.x, transform.position.y, transform.position.z);
        }
        TimeUnduBig += Time.deltaTime;
        if (TimeUnduBig >= controlTimeUnduBig && big)
        {
            SetSprite();
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
            GameManager.Instance.ballObj.Remove(gameObject);
            GameManager.Instance.ballObj.Remove(null);
            if (GameManager.Instance.ballObj.Count <= 1)
            {
                GameManager.Instance.isBall = true;
                UIManager.Instance.ballImage.SetActive(true);
            }
            GameManager.Instance.ballCount--;
            UIManager.Instance.ChangeBallCount(GameManager.Instance.ballCount.ToString());
            if (GameManager.Instance.ballCount < 0) GameManager.PlayerDie();
        }
    }

    public void SetSprite()
    {
        if (spriteRenderer.sprite != spritesPlayer[1])
        {
            spriteRenderer.sprite = spritesPlayer[1];
            GetComponent<Rigidbody2D>().gravityScale *= 2.5f;
            GetComponent<CircleCollider2D>().radius = 0.18f;
            big = true;
        }
        else
        {
            spriteRenderer.sprite = spritesPlayer[0];
            GetComponent<Rigidbody2D>().gravityScale /= 2.5f;
            GetComponent<CircleCollider2D>().radius = 0.04f;
            big = false;
        }
        TimeUnduBig = 0f;
    }
}
