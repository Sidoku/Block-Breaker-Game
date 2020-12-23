using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    //config params
    [SerializeField] Paddle paddle1;

    //state
    Vector2 paddleToBallVector;
    bool hasStarted = false;
    [SerializeField] float xPush = 3f;
    [SerializeField] float yPush = 300f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //cached component refrences
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallPosition();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(xPush,yPush);
            hasStarted = true;
        }
    }

    private void LockBallPosition()
    {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float randomAngle = Random.Range(-randomFactor, randomFactor);
        myRigidBody2D.velocity = Quaternion.Euler(0, 0, randomAngle) * myRigidBody2D.velocity;

        if (hasStarted)
        {
            AudioClip clip=ballSounds[UnityEngine.Random.Range(0,ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
        }
    }
}