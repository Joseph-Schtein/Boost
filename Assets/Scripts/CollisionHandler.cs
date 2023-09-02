using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    
    [SerializeField] float Delaye = 1f;
    [SerializeField] AudioClip Cruch;
    [SerializeField] AudioClip Success;

    [SerializeField] ParticleSystem CruchParticle;
    [SerializeField] ParticleSystem SuccessParticle;

    bool cancelColision;
    bool isTransitioning;
    AudioSource StatusAudio;

    void Start()
    {
        cancelColision = false;
        isTransitioning = false;
        StatusAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.C))
        {
            cancelColision = !cancelColision;
        }

        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (!isTransitioning && !cancelColision)
        {
            switch (other.gameObject.tag)
            {
                case "Respawn":
                    Debug.Log("Respawn");
                    break;

                case "Finish":
                    StartSuccessSequence();
                    break;

                default:
                    Debug.Log("Obstecal or Ground");
                    StartCrashSequence();
                    break;
            }
        }
    }

    public void StartSuccessSequence()
    {
        isTransitioning = true;
        StatusAudio.Stop();
        SuccessParticle.Play();
        StatusAudio.PlayOneShot(Success);
        GetComponent<Movement>().enabled = false;// We want to forbide the rocket to fly
                                                 // After a success, so we change the movment to false
        Invoke("LoadNextLevel", Delaye);
        
    }


    public void StartCrashSequence()
    {
        isTransitioning = true;
        StatusAudio.Stop();
        CruchParticle.Play();
        StatusAudio.PlayOneShot(Cruch);
        GetComponent<Movement>().enabled = false;  // We want to forbide the rocket to fly
                                                   // After a fail, so we change the movment to false
        Invoke("ReloadLevel", Delaye);       
    }

    void ReloadLevel()
    {     
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
            StatusAudio.Stop();
        }
        
    }

    bool LoadFirstLevel()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
        return sceneNumber <= SceneManager.sceneCount;
            
    }
}
