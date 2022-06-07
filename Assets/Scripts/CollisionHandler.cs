using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{   
    
    [SerializeField] float levelLoadDelay = 2f; // Creates a box in unity for delay betweeen levels
    [SerializeField] AudioClip success; // Box in unity for putting in audioclip for success
    [SerializeField] AudioClip crash; // Box in unity for putting in audioclip for crash

    [SerializeField] ParticleSystem successParticles; // box in unity for putting particles at copter
    [SerializeField] ParticleSystem crashParticles; // box in unity for putting particles at copter


    AudioSource audioSource;

    bool isTransitioning = false; // The moment in between crashes or finnishing levels etc.

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }
    

    
     void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) {return;} // makes the soundFX only trigger once at crash etc
                                        // the return makes sure none of the other later codes in the switch statement
                                        // below gets triggered aka " if it's true then don't do any of this stuff "
                                        // If its not in "isTransitioning" do all the other stuff below as normal.
        
        switch (other.gameObject.tag)
        {
            case "Friendly":            // If it hits a friendly object, nothing really happens except for in debug
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("Good job! You made it!");
                StartsuccessSequence(); // If it hits end pad or " finish " the successSequence starts
                break;
            case "Fuel":
                Debug.Log("You picked up fuel");
                break;
            default:
                Debug.Log("Sorry, You crashed!");
                StartCrashSequence(); // If it crashes the StartCrashSequence starts which is further down in "void StartCrashSequence"
                break;
            }    
    }
    
    void StartsuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play(successParticles);
        GetComponent<Movement>().enabled = false;
            if (isTransitioning)

        Invoke("LoadNextLevel", levelLoadDelay);
    
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play(crashParticles);
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
