using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource sound;
    float tuneThrust = 650f;
    float tuneRotation = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    [SerializeField] ParticleSystem mainBoosterParticles;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        sound.loop = true;
       

    }

    // Update is called once per frame
    void Update()
    {
        ProcessLift();
        ProcessRotation();
    }

    void ProcessLift()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartLift();
        }
        else
        {
            StopLift();
        }
    }

    void ProcessRotation()
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
            StopRotations();
        }
    }

    void StopRotations()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-tuneRotation);
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(tuneRotation);
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }

    void StartLift()
    {
        rb.AddRelativeForce(Vector3.up * tuneThrust * Time.deltaTime);
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }

        if (!sound.isPlaying)
        {
            sound.PlayOneShot(mainEngine);
        }
    }

    void StopLift()
    {
        sound.Pause();
        mainBoosterParticles.Stop();
    }
    void ApplyRotation(float rotateBy)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateBy * Time.deltaTime);
        rb.freezeRotation = false;
    }
}