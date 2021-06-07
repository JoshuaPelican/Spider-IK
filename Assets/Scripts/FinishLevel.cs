using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public GameObject finishLevelUI;
    public Scene nextScene;
    public float delay = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine("LoadNewLevel");
        }
    }

    private IEnumerator LoadNewLevel()
    {
        finishLevelUI.SetActive(true);

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(nextScene.handle);
    }

}
