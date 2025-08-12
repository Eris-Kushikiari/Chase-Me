using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    private Enemy enemyScript;
    [SerializeField] private AudioSource waterAudio;
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private AudioSource pickUpAudio;
    [SerializeField] private AudioSource winAudio;
    public AudioClip waterSplash;
    public AudioClip engine;
    public AudioClip pickUpSound;
    public AudioClip winSound;
    private float speed = 5;
    private float rotationSpeed = 100f;

    //Player outside boundary
    private float xRange = 23;
    private float zRange = 23;

    //Score
    private int score;
    public TextMeshProUGUI scoreText;
    public GameObject winText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();
        score = 0;

        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 2)
        {
            engineAudio = sources[0];
            waterAudio = sources[1];
        }
        else
        {
            Debug.LogError("Player is missing one or both AudioSources!");
        }

        if (!enemyScript.isGameOver)
        {
            if (!engineAudio.isPlaying)
            {
                engineAudio.clip = engine;
                engineAudio.loop = true;
                engineAudio.Play();
            }

            if (!waterAudio.isPlaying)
            {
                waterAudio.clip = waterSplash;
                waterAudio.loop = true;
                waterAudio.Play();
            }
        }
        else
        {
            engineAudio.Stop();
            waterAudio.Stop();
        }


        SetScoreText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        PlayerBoundary();
    }

    void PlayerMovement()
    {
        if (GameManager.Instance.hasWon)
            return;

        float horizontalInput = 0f;

        //  PC controls
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        //  Mobile controls
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.position.x < Screen.width / 2)
                {
                    horizontalInput = -1f; // Left side turn left
                }
                else
                {
                    horizontalInput = 1f; // Right side turn right
                }
            }
        }

        // Rotate player
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // Always move forward
        Vector3 forwardMovement = transform.forward * speed;
        playerRb.linearVelocity = new Vector3(forwardMovement.x, playerRb.linearVelocity.y, forwardMovement.z);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
            score = score + 1;
            SetScoreText();
            pickUpAudio.PlayOneShot(pickUpSound);
        }

    }

    public void SetScoreText()
    {
        scoreText.text = "Anchor: " + score.ToString();

        if (score >= 10)
        {
            winText.SetActive(true);
            GameManager.Instance.WinGame();
            winAudio.PlayOneShot(winSound);
        }
    }



    void PlayerBoundary()
    {
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.z > xRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -xRange);
        }
    }

    public int GetScore()
    {
        return score;
    }

}
