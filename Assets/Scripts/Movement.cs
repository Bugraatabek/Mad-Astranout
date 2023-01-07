using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThruster;
    [SerializeField] ParticleSystem rightThruster;
    [SerializeField] ParticleSystem leftThruster;

   
    Rigidbody rb;
    AudioSource audiosource;

    

    
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
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
            StopMainAudioAndParticles();
        }
    }
     void ProcessRotation()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            StartThrustingLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartThrustingRight();
        }
        else
        {
            StopParticleEffects();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        mainThruster.Play();
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(mainEngine);
            mainThruster.Play();
        }
    }

    void StopMainAudioAndParticles()
    {
        audiosource.Stop();
        mainThruster.Stop();
    }
    void StartThrustingRight()
    {
        ApplyRotation(-rotationSpeed);

        if (!leftThruster.isPlaying)
        {
            leftThruster.Play();
        }
    }

    void StartThrustingLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rightThruster.isPlaying)
        {
            rightThruster.Play();
        }
    }
    void StopParticleEffects()
    {
        leftThruster.Stop();
        rightThruster.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
