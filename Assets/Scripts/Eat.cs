using System.Collections;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public Animator anim;
    public AudioSource source;

    public bool eating;
    public float eatDuration;

    public GameObject bloodParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.isTrigger && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.gameObject.GetComponent<Webable>().webbed && !eating)
            {
                StartCoroutine("EatEnemy", collision);
            }
        }
    }

    private IEnumerator EatEnemy(Collider2D collision)
    {
        //Eat bug;
        eating = true;
        anim.SetBool("Eating", true);
        source.Play();

        yield return new WaitForSeconds(.25f);
        Instantiate(bloodParticles, collision.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.25f);
        Instantiate(bloodParticles, collision.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.25f);
        Instantiate(bloodParticles, collision.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.25f);
        Instantiate(bloodParticles, collision.transform.position, Quaternion.identity);

        anim.SetBool("Eating", false);
        eating = false;
        Destroy(collision.gameObject);
    }
}
