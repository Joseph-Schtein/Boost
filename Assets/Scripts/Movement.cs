using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [SerializeField] float thrustForce = 1f;
    [SerializeField] float rotationForce = 1f;
    [SerializeField] AudioClip RocketEngine;
    
    [SerializeField] ParticleSystem RocketThrustParticle;
    [SerializeField] ParticleSystem LeftThrustPartical;
    [SerializeField] ParticleSystem RightThrustPartical;


    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }


    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (!RocketThrustParticle.isPlaying)
            RocketThrustParticle.Play();


        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(RocketEngine);
    }

    void StopThrusting()
    {
        audioSource.Stop();
        RocketThrustParticle.Stop();
    }

    void ProcessRotation()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            rotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rotateRight();
        }
        else
        {
            stopRotation();
        }
    }

    private void rotateLeft()
    {
        ApplyRotation(rotationForce);
        if (!LeftThrustPartical.isPlaying)
            LeftThrustPartical.Play();
    }
    private void rotateRight()
    {
        ApplyRotation(-rotationForce);
        if (!RightThrustPartical.isPlaying)
            RightThrustPartical.Play();
    }

    void stopRotation()
    {
        LeftThrustPartical.Stop();
        RightThrustPartical.Stop();
    }

    

    private void ApplyRotation(float direction)
    {
        rb.freezeRotation = true;  // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * direction * Time.deltaTime);
        rb.freezeRotation = false;  // Unfreezing rotation so the physics system can take over
    }
}
