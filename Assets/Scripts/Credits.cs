using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{
    public float delayBeforeReturn = 5f;

    void Start()
    {
        StartCoroutine(ReturnToMainMenu());
    }

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(delayBeforeReturn);

        SceneManager.LoadScene(0);
    }
}
