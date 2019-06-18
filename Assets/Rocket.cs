using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Rocket : MonoBehaviour
{


    enum State { alive, dying, transcending};
    State state = State.alive;

    [SerializeField] float rcsThrust = 200f;  // SerializeField value can be modified from inspector
    [SerializeField] float mainThrust = 200f;
    [SerializeField] float brakeControl = 50f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;



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
        if (state == State.alive)
        {
            RespondToThrust();

            RespondToRotate();
            Brake();
        }
        
              
    }

   

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.alive) { return; }  // avoid keep generating sound when dying
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                // print("ok");// remove later
                break;
            //case "Fuel":
            // do nothing
            // print("Fuel");// remove later
            // break;
            case "Finish":
                StartFinishSequence();

                break;
            default:
                StartDeathSequence();
                break;
        }
    }

   

    private void StartFinishSequence()
    {
        print("Next Level");
        state = State.transcending;
        audioSource.Stop();  // stop thrust sound when new sound is to appear
        audioSource.PlayOneShot(successSound);  // playoneshot for just play one short time
        successParticles.Play();

        Invoke("LoadNextLevel", 1f);
    }
    private void StartDeathSequence()
    {
        print("dead");
        state = State.dying;
        audioSource.Stop();
        audioSource.PlayOneShot(explosionSound);
        explosionParticles.Play();
        // kill player
        Invoke("LoadInitialScene", 1f);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadInitialScene()
    {
        SceneManager.LoadScene(0);

    }

    private void RespondToThrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;  // to adjust the degree
        if (Input.GetKey(KeyCode.W))  // keyCode is a enum of key buttons
        {
            ApplyThrust(thrustThisFrame);

        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
            //rigidBody.freezeRotation = false; // used to take control of rotation
        }
    }

    private void ApplyThrust(float thrustThisFrame)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);  // relati
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            // rigidBody.freezeRotation = true; // resume rotation
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotate()
    {

        
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        rigidBody.freezeRotation = true; // resume rotation
        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationThisFrame);
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; // used to take control of rotation
    }
    private void Brake()
    {
        float brakeThisFrame = brakeControl * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
        {

            rigidBody.AddRelativeForce(-Vector3.up * brakeThisFrame); ;

        }
       
    }


}
