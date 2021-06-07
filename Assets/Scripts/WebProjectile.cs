using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebProjectile : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Web Enemy
            collision.gameObject.GetComponent<Webable>().BecomeWebbed();
        }

        Destroy(gameObject);
    }
}
