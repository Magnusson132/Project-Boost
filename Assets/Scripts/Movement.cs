using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{// PARAMETERS // for tuning, typically set in the editor
    [SerializeField] float mainThrust = 100f; // option to change thrust speed in unity
    [SerializeField] float rotationThrust = 1f; // option to change rotation speed in unity
    [SerializeField] AudioClip mainEngine; // Makes a option to put a audiofile in movementsript in unity
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
 
  // CACHE // e.g. references for readability or speed  
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
        ProceessRotation();
    }

    void ProcessThrust() // code for going straigth
    {
        if (Input.GetKey(KeyCode.Space))
        {    
            StartThrusting();
        }
        else
            StopThrusting();
    }
    void ProceessRotation() // code for rotating left or right
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }



    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);

        }
        mainEngineParticles.Play();

    }
        private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }


    private void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying) ;
        {
            leftThrusterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
        else
        {
            rightThrusterParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        // rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        // rb.freezeRotation = false; // unfreezing rotation
    }
}