using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Webable : MonoBehaviour
{
    public float duration;
    public GameObject webbedSprite;

    private GameObject newWebbedSprite;
    public bool webbed;

    public void BecomeWebbed()
    {
        if (!webbed)
        {
            webbed = true;

            newWebbedSprite = Instantiate(webbedSprite, transform);

            GetComponent<EnemyAI>().enabled = false;

            GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            StartCoroutine("Unweb");
        }
    }

    private IEnumerator Unweb()
    {
        yield return new WaitForSeconds(duration);

        webbed = false;
        Destroy(newWebbedSprite);
        GetComponent<EnemyAI>().enabled = true;
    }
}
