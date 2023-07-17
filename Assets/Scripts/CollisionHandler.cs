using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    
    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("Respawn");
                break;

            case "Finish":
                LoadNextLevel();
                break;

            case "Obstacle":
                Debug.Log("Obstacle");
                ReloadLevel();
                break;

            default:
                Debug.Log("unknowen");
                ReloadLevel();
                break;
        }
    }
    void ReloadLevel()
    {
        //SceneManager.LoadScene(0);
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNumber);
    }

    void LoadNextLevel()
    {
        if (LoadFirstLevel())
        {
            int sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(sceneNumber);
        }

        else
        {
            SceneManager.LoadScene(0);
        }
    }

    bool LoadFirstLevel()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
        return sceneNumber <= SceneManager.sceneCount;
            
    }
}
