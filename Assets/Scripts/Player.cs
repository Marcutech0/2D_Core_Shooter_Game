using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Player speed
    public float moveSpeed;

    // Rigid Body access
    public Rigidbody2D rigidBody;

    // Player movement
    private Vector2 movementInput;
    // For Shells
    public GameObject ShellPrefab;
    public Transform ShellSpawnPoint;
    public Transform ShellSpawnPoint2;
    public float ShellSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Get the reference to the Rigidbody2D component
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // PLAY/LAUNCH
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Shell = Instantiate(ShellPrefab, ShellSpawnPoint.position, Quaternion.identity);
            GameObject Shell2 = Instantiate(ShellPrefab, ShellSpawnPoint2.position, Quaternion.identity);
            Rigidbody2D rb = Shell.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.up * ShellSpeed; // Shoots the shell only in the upward direction
            Rigidbody2D rb2 = Shell2.GetComponent<Rigidbody2D>();
            rb2.velocity = Vector2.up * ShellSpeed; // Shoots the shell only in the upward direction
        }
    }

    // Fixed for physics update
    private void FixedUpdate()
    {
        // Player movement
        rigidBody.velocity = movementInput * moveSpeed;
    }

    // Input keybinds
    private void OnMove(InputValue inputValue)
    {
        // Get the movement input
        movementInput = inputValue.Get<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Speed"))
        {
            // Move the collided object off-screen
            collision.transform.position = new Vector2(999, 999);
        }
    }
}
