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
            if (gameObject.name == "gameover(Clone)") SceneManager.LoadScene(0);
            else
            {
                GameManager.Instance.ready = false;
                Destroy(gameObject);
            }
        }
    }
}
