using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float thrustForce = 1f;
    [SerializeField] float rotationForce = 1f;
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
        PrpcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            if(!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
        
    }

    void PrpcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationForce);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationForce);
        }
    }

    private void ApplyRotation(float direction)
    {
        rb.freezeRotation = true;  // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * direction * Time.deltaTime);
        rb.freezeRotation = false;  // Unfreezing rotation so the physics system can take over
    }
}
