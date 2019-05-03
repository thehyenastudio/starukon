using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cloud" || collision.gameObject.tag == "bubble")
        {
            if (collision.gameObject.tag == "bubble")
            {
                collision.GetComponent<Bubble>().StartDie();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
