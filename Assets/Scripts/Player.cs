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



    //coin collecting
    public int CoinCount;

    //player healt
    public int HealthPoints;

    //for Shells
    public GameObject ShellPrefab;
    public Transform ShellSpawnPoints;
    public Transform ShellSpawnPoints2;
    public float ShellSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //para sa player to pre kimi lang
        rigidBody = GetComponent<Rigidbody2D>();

    }

    // PLAY/LAUNCH
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Shell = Instantiate(ShellPrefab, ShellSpawnPoints.position,Quaternion.identity);
            GameObject Shell2 = Instantiate(ShellPrefab, ShellSpawnPoints2.position, Quaternion.identity);
            Rigidbody2D rb = Shell.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = Shell2.GetComponent<Rigidbody2D>();
            rb.velocity = transform.up * ShellSpeed;
            rb2.velocity = transform.up * ShellSpeed;
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