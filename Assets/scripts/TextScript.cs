using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScript : MonoBehaviour
{ 
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * 400f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "endText")
        {
            if (gameObject.name == "gameover(Clone)")
            {
                if (!GameManager.Instance.newScore)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(2);
                }
            }
            else
            {
                GameManager.Instance.playerSpeed = 1f;
                GameManager.Instance.controlBall = false;
                GameManager.Instance.ready = false;
                Destroy(gameObject);
            }
        }
    }
}
