using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (this.gameObject.tag == "ButtomEdge")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 3f);

            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0, -3f);

            }
        }
    }
}
