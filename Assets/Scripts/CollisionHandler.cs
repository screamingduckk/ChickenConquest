using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    AudioSource sound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
  
    bool isTransitioning = false;
    bool collisonDisable = false;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKey(KeyCode.C))
        {
            collisonDisable = !collisonDisable;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (isTransitioning || collisonDisable) { return; }

         switch (other.gameObject.tag)
            {
              case "Friendly":
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
        isTransitioning = true;
        sound.Stop();
        sound.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), loadDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        sound.Stop();
        sound.PlayOneShot(successSound);
        successParticles.Play();
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
