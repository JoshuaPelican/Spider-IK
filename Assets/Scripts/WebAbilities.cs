using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAbilities : MonoBehaviour
{
    private SpringJoint2D spring;
    private LineRenderer lineRend;
    public float webClimbSpeed;
    private Camera mainCam;

    public GameObject webAnchor;
    public LayerMask attachableLayers;
    private GameObject newWebAnchor;

    public bool attached;

    public float shootDelay;
    private float currentTimer;
    public GameObject webProjectile;

    private void Start()
    {
        mainCam = Camera.main;
        spring = GetComponent<SpringJoint2D>();
        lineRend = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRend.SetPosition(0, transform.position);

        Vector3 mousePos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector2 rayDir = (mousePos - transform.position).normalized;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, 20f, attachableLayers);

            if (hit.collider)
            {
                if (attached)
                {
                    Destroy(newWebAnchor);
                    attached = false;
                }

                spring.enabled = true;

                newWebAnchor = Instantiate(webAnchor, hit.point,Quaternion.FromToRotation(Vector3.up, hit.normal));
                spring.connectedBody = newWebAnchor.GetComponent<Rigidbody2D>();

                spring.distance = Mathf.Clamp(Vector2.Distance(newWebAnchor.transform.position, transform.position) - (Vector2.Distance(newWebAnchor.transform.position, transform.position)/2f), .25f, 100);

                attached = true;

                lineRend.positionCount = 2;

                lineRend.SetPosition(1, newWebAnchor.transform.position);
            }
        }

        currentTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentTimer >= shootDelay)
            {
                currentTimer = 0;
                Instantiate(webProjectile, transform.position, Quaternion.FromToRotation(Vector3.up, rayDir));
            }
        }

        if (attached)
        {
            float vert = Input.GetAxis("Vertical");

            if(vert != 0)
            {
                spring.distance += webClimbSpeed * Time.deltaTime * -vert;
                spring.distance = Mathf.Clamp(spring.distance, .25f, 50);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                attached = false;
                lineRend.positionCount = 1;

                Destroy(spring.connectedBody.gameObject);

                spring.connectedBody = null;
                spring.enabled = false;
            }
        }
    }
}
