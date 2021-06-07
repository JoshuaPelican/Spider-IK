using UnityEngine;

public class TargetGroundIK : MonoBehaviour
{
    public Transform targetPoint;
    public TargetGroundIK otherLeg;
    public float xOffset;
    public float moveThreshold = 1f;

    public LayerMask groundLayers;
    private Vector3 targetHitPoint;
    private bool moving;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayers);

        if (hit.collider && !moving)
        {
            transform.position = Vector3.Lerp(transform.position, hit.point, .175f);
        }

        RaycastHit2D[] targetHit = Physics2D.RaycastAll(targetPoint.position, Vector2.down, 2.5f, groundLayers);

        if (targetHit.Length > 0)
        {
            targetHitPoint = targetHit[0].point;

            float dist = Vector2.Distance(targetHitPoint, transform.position);

            if (dist > moveThreshold && !otherLeg.moving)
            {
                moving = true;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPoint.position - (Vector3.up * .9f) + (Vector3.right * xOffset * transform.root.GetChild(5).localScale.y), 1f);
        }

        if (moving)
        {
            transform.position = Vector3.Lerp(transform.position, targetHitPoint + (Vector3.up * .5f), .135f * Time.deltaTime * 200);

            if (transform.position.x > targetHitPoint.x - .25f && transform.position.x < targetHitPoint.x + .25f)
            {
                moving = false;
            }
        }
    }
}
