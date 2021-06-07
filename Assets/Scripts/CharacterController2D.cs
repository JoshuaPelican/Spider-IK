using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float movementSmoothing;

    public bool isGrounded = true;

    private Rigidbody2D rig;
    private Vector3 velocity;
    private bool facingRight = true;
    private WebAbilities web;

    public Transform groundCheck;
    public float groundedRadius;
    public LayerMask groundLayer;

    public Transform spawnPoint;

    private Animator anim;

    public Transform[] targets;
    public float yOffset;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        web = GetComponent<WebAbilities>();
    }

    private void FixedUpdate()
    {
        if(Physics2D.OverlapCircle(groundCheck.position, groundedRadius, groundLayer) && !web.attached)
        {
            isGrounded = true;
            rig.gravityScale = 0;
        }
        else
        {
            isGrounded = false;
            rig.gravityScale = 1.5f;
        }

        if (isGrounded)
        {
            rig.velocity = rig.velocity - (rig.velocity.y * Vector2.up);

            float yTotal = 0;
            Vector3 dirTotal = Vector3.up;

            for (int i = 0; i < targets.Length; i++)
            {
                dirTotal += (targets[i].position - transform.position).normalized;
                yTotal += targets[i].position.y;
            }

            float yAvg = yTotal / targets.Length;
            Vector3 avgDir = (dirTotal / targets.Length).normalized;

            Vector3 currentPos = transform.position;
            currentPos.y = yAvg + yOffset;

            transform.position = Vector3.Lerp(transform.position, currentPos, .5f);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.transform.up, avgDir), .05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("deathPlane"))
        {
            transform.position = spawnPoint.position;
        }
        if (collision.CompareTag("Respawn"))
        {
            spawnPoint = collision.transform;
        }
    }

    public void Move(float moveAmount)
    {
        Vector3 targetVelocity = new Vector2(moveAmount * moveSpeed, rig.velocity.y);

        rig.velocity = Vector3.SmoothDamp(rig.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (moveAmount > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveAmount < 0 && facingRight)
        {
            Flip();
        }

        anim.SetFloat("Horizontal", Mathf.Abs(moveAmount));
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 currentScale = transform.localScale;
        currentScale.y *= -1;
        transform.localScale = currentScale;
    }
}
