using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Player speed
    public float moveSpeed;

    //Rigid Body access
    public Rigidbody2D rigidBody;

    //Player movement
    private Vector2 movementInput;

    //player animations
    public Animator anim;

    //coin collecting
    public int CoinCount;

    //player healt
    public int HealthPoints;

    //for Shells
    public GameObject ShellPrefab;
    public Transform ShellSpawnPoints;
    public float ShellSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //para sa player to pre kimi lang
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // PLAY/LAUNCH
    void Update()
    {
        anim.SetFloat("Horizontal", movementInput.x);
        anim.SetFloat("Vertical", movementInput.y);
        anim.SetFloat("Speed", movementInput.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Shell = Instantiate(ShellPrefab, ShellSpawnPoints);
            Rigidbody2D rb = Shell.GetComponent<Rigidbody2D>();
            rb.velocity = transform.up * ShellSpeed;
        }
    }
    //Fixed for physx kimi lang pre
    private void FixedUpdate()
    {
        //player  movement
        rigidBody.velocity = movementInput * moveSpeed;
    }

    //input keybinds
    private void OnMove(InputValue inputValue)
    {
        // When A is pressed
        movementInput = inputValue.Get<Vector2>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Speed"))
        {
            Transform col = collision.transform;
            col.transform.position = new Vector2(999, 999);

        }
        if (collision.CompareTag("COIN_PREFAB"))
        {
            CoinCount++;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Health"))
        {
            Destroy(collision.gameObject);
        }
    }
}