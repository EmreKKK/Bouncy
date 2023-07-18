using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public AudioSource audio;

    private bool isJumping = false;
    private Rigidbody2D rb;

    private bool alreadyScaledBigger = false;
    private bool alreadyScaledSmaller = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void Update()
    {
        Movement();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.CompareTag("Pump"))
        {
            if (!alreadyScaledBigger && collision.gameObject.CompareTag("Pump"))
            {
                ScaleObjectBigger(gameObject.transform);
                alreadyScaledBigger = true;
            }
        }
        if (collision.gameObject.CompareTag("Vacuum"))
        {
            if (!alreadyScaledSmaller && collision.gameObject.CompareTag("Vacuum"))
            {
                ScaleObjectSmaller(gameObject.transform);
                alreadyScaledSmaller = true;
            }
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(0);
        }
    }

    void ScaleObjectBigger(Transform objTransform)
    {
        // Mevcut boyutu iki katýna çýkarýn
        Vector3 newScale = objTransform.localScale * 1.5f;

        // Nesnenin boyutlarýný güncelleyin
        objTransform.localScale = newScale;
    }
    void ScaleObjectSmaller(Transform objTransform)
    {
        // Mevcut boyutu iki katýna çýkarýn
        Vector3 newScale = objTransform.localScale * 0.75f;

        // Nesnenin boyutlarýný güncelleyin
        objTransform.localScale = newScale;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ring"))
        {
            audio.Play();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("RingOFF"))
        {
            collision.gameObject.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            audio.Play();
            Destroy(collision.gameObject);
        }

    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal * speed, rb.velocity.y);
        rb.velocity = movement;

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }


}
