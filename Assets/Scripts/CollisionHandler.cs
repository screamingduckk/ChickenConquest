using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;

    private void OnCollisionEnter(Collision other)
    {

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Careful!");
                break;
            case "Finish":
                Debug.Log("Level complete!");
                StartSuccessSequence();
                break;
            default:
                Debug.Log("You crashed!");
                StartCrashSequence();
                break;
        }

    }
    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), loadDelay);
    }

    void StartSuccessSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), loadDelay);
    }

    void ReloadLevel()
    {
        //todo add particle fx
        //todo add crash sfx
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);

    }
}
