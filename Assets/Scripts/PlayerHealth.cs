using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public Image healthBar;
    public AudioSource source;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;
        source.Play();

        if(currentHealth <= 0)
        {
            //Death
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
