using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rig;
    public Transform flipCheck;
    
    public float walkSpeed;
    public float runSpeed;
    public float damage;
    public LayerMask groundLayers;

    private float currentSpeed;
    private bool facingRight = true;

    private enum Mode { Patrol, Chase}
    private Mode currentMode;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        currentSpeed = walkSpeed;
        currentMode = Mode.Patrol;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(flipCheck.position, -flipCheck.transform.up, 1f, groundLayers);

        if (!hit.collider && currentMode == Mode.Patrol)
        {
            Flip();
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal), .2f);
        }

        RaycastHit2D hitWall = Physics2D.Raycast(flipCheck.position, Vector2.right * -transform.localScale.x, 1f, groundLayers);

        if (hitWall.collider)
        {
            if (hit.collider)
            {
                if(hitWall.collider != hit.collider)
                {
                    Flip();
                }
            }
        }

    }

    private void Update()
    {
        if (currentMode == Mode.Patrol)
        {
            currentSpeed = walkSpeed * Mathf.Sign(currentSpeed);
        }
        else if(currentMode == Mode.Chase)
        {
            currentSpeed = runSpeed * Mathf.Sign(currentSpeed);
        }

        Move(currentSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float xValue = collision.ClosestPoint(transform.position).x;

            if((xValue < transform.position.x && facingRight) || (xValue > transform.position.x && !facingRight))
            {
                Flip();
            }

            currentMode = Mode.Chase;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentMode = Mode.Patrol;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !GetComponent<Webable>().webbed)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    public void Move(float moveAmount)
    {
        Vector3 targetVelocity = new Vector2(moveAmount, rig.velocity.y);

        rig.velocity = targetVelocity;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        currentSpeed *= -1;

        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
