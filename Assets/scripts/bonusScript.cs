using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dieCol")
        {
            Destroy(gameObject);
        }
    }
}
