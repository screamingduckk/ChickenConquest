using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource sound;
    float tuneThrust = 650f;
    float tuneRotation = 100f;
    // Start is called before the first frame update
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
            Debug.Log("Space is down - lifting!");
            rb.AddRelativeForce(Vector3.up * tuneThrust * Time.deltaTime);
            if (!sound.isPlaying)
            {
                sound.Play();
            }
        }
        else
        {
            sound.Pause();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(tuneRotation);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-tuneRotation);
        }
    }
    private void ApplyRotation(float rotateBy)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateBy * Time.deltaTime);
        rb.freezeRotation = false;
    }
}