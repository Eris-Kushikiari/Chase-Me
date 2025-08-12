using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;

    public void ContinueButton()
    {
        StartCoroutine(DelayLoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator DelayLoadScene(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
