using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private AudioSource playeraudioSource;
    private AudioSource cameraAudioSource;
    public float jumpForce = 500;
    public float gravityModifier = 1;
    public bool isOnTheGround = true;
    public bool gameOver;
    public ParticleSystem explosionParticleSystem;
    public ParticleSystem dirtParticleSystem;
    public AudioClip jumpClip;
    public AudioClip explosionClip;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playeraudioSource = GetComponent<AudioSource>();
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !gameOver)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, 
                ForceMode.Impulse);
            playerAnimator.SetTrigger("Jump_trig");
            isOnTheGround = false;
            dirtParticleSystem.Stop();
            playeraudioSource.PlayOneShot(jumpClip, 1f);
            
        }
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        if (!gameOver)
        {
            if (otherCollider.gameObject.CompareTag("Ground"))
            {
                isOnTheGround = true;
                dirtParticleSystem.Play();
            }

            else if (otherCollider.gameObject.CompareTag("Obstacle"))
            {
                int randomDeathType = Random.Range(1, 3);
                playerAnimator.SetBool("Death_b", true);
                playerAnimator.SetInteger("DeathType_int", randomDeathType);
                explosionParticleSystem.Play();
                dirtParticleSystem.Stop();
                cameraAudioSource.volume = 0.1f;
                playeraudioSource.PlayOneShot(explosionClip, 1f);
                gameOver = true;
            }
        }
        
    }
}
