using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    [SerializeField] float rcsThrust = 200f;  // SerializeField value can be modified from inspector
    [SerializeField] float mainThrust = 200f;
    Rigidbody rigidBody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();

        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                print("ok");// remove later
                break;
            case "Fuel":
                // do nothing
                print("Fuel");// remove later
                break;
            default:
                print("dead");
                // kill player
                break;
        }
    }
    private void Thrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;  // to adjust the degree
        if (Input.GetKey(KeyCode.Space))  // keyCode is a enum of key buttons
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);  // relati
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                rigidBody.freezeRotation = true; // resume rotation
            }

        }
        else
        {
            audioSource.Stop();
            rigidBody.freezeRotation = false; // used to take control of rotation
        }
    }
    private void Rotate()
    {

        
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationThisFrame);
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        
    }


}
