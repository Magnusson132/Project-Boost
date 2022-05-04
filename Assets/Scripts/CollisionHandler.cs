using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{   
     void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("Good job! You made it!");
                break;
            case "Fuel":
                Debug.Log("You picked up fuel");
                break;
            default:
                Debug.Log("Sorry, You crashed!");
                ReloadLevel();
                break;
            }    
    }
    void ReloadLevel()
    {
        int currrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currrentSceneIndex);
    }
}
