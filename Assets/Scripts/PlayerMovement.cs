using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator characterAnim;

    public bool grounded;

    bool isAlive = true;

    public float speed = 5;

    [SerializeField]
    Rigidbody rb;

    float horizontalInput;

    public float horizontalMultiplier;

    public float speedIncreasePerPoint = 0.1f;

    public float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    public float minX = -4.0f;
    public float maxX = 4.0f;

    [SerializeField] GameObject gameOverPanel;

    [SerializeField] AudioManager audioManager;

    [SerializeField] AudioSource jumpSound;

    [SerializeField] AudioSource dieSound;
    private void FixedUpdate()
    {
        if (!isAlive)
            return;
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;

        // Calculate the new position including both forward and horizontal movements
        Vector3 clampledNewPosition = rb.position + forwardMove + horizontalMove;


        // Clamp the x-coordinate to stay within the defined range
        clampledNewPosition.x = Mathf.Clamp(clampledNewPosition.x, minX, maxX);

        rb.MovePosition(clampledNewPosition);

        // Check grounded
        float height = GetComponent<Collider>().bounds.size.y;
        // Check if the player has started falling after jumping
        if (!grounded && rb.velocity.y < 0)
        {
            characterAnim.SetBool("isJumping", false);
            characterAnim.SetBool("isFalling", true);
        }

        if (grounded && rb.velocity.y == 0)
        {
            characterAnim.SetBool("isFalling", false);
            characterAnim.SetBool("isGrounded", true);
        }

        grounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed <= 1.0)
        {
            Die();
        }




        horizontalInput = Input.GetAxis("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (transform.position.y < -5)
        {
            Die();
        }

        // Check if the player has landed and transition to idle animation
        if (characterAnim.GetBool("isGrounded") && grounded)
        {

            characterAnim.SetBool("isMoving", true); 
        }
    }

    public void Die()
    {
        isAlive = false;

        gameOverPanel.SetActive(true);

        audioManager.GameOverFade();

        dieSound.Play();

        // Reiniciar el juego
        //Invoke("Restart", 2);
    }

    public void LevelComplete ()
    {
        isAlive = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump()
    {
        // Check grounded
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f);

        if (isGrounded)
        {
            // Si grounded, brincar 
            rb.AddForce(Vector3.up * jumpForce);
            characterAnim.SetBool("isMoving", false); 
            characterAnim.SetBool("isJumping", true);
            jumpSound.Play();
        }



    }
}
