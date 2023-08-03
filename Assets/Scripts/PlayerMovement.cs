using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (transform.position.y < -5)
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;

        gameOverPanel.SetActive(true);

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
        }

    }
}
