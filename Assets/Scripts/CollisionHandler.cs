using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{   
    
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }
    

    
     void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("Good job! You made it!");
                StartsuccessSequence();
                break;
            case "Fuel":
                Debug.Log("You picked up fuel");
                break;
            default:
                Debug.Log("Sorry, You crashed!");
                StartCrashSequence();
                break;
            }    
    }

    void StartsuccessSequence()
    {
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    
    }

    void StartCrashSequence()
    {
        // todo add SFX upon crash
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
             if(!audioSource.isPlaying)
           {
           audioSource.PlayOneShot(crash);
            
           }
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void LoadNextLevel()
    {
       int currrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       int nextSceneIndex = currrentSceneIndex + 1;
       if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
       {
          nextSceneIndex = 0;
       }
       SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        int currrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currrentSceneIndex);
    }
}
