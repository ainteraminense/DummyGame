﻿using UnityEngine;

public class Player : MonoBehaviour
{
    //VARIABLES    
    private float direction;
    private bool isGrounded = true;
    private float superPowerRemaining = 0;
    private float SuperPowerRemaining { get => superPowerRemaining; set => superPowerRemaining = value; }

    public float strength = 7f;
    public float superPowerIncrease = 5f;

    //REFERENCES
    [SerializeField] private LayerMask playerLayer;
    private new Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private Vector3 velocity = Vector3.zero;
    //private CharacterController characterController;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    //private void Start()
    //{
    //    characterController = GetComponent<CharacterController>();
    //}
    private void Update()
    {
        IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (SuperPowerRemaining > 0)
            {
                strength += superPowerIncrease; // use the superpower amount
                rigidbody.AddForce(Vector3.up * strength, ForceMode.Impulse);
                strength -= superPowerIncrease; // after using return to the normal jump
                SuperPowerRemaining--; // spend the amount of one superpower 'coin'
                return;
            }
            // make the player jump when space is pressed
            rigidbody.AddForce(Vector3.up*strength, ForceMode.Impulse);
        }
        direction = Input.GetAxis("Horizontal");
        //if (direction < 0)
        //{
        transform.forward = new Vector3(0, 0, direction);
        FindObjectOfType<Camera>().transform.position = Vector3.SmoothDamp(FindObjectOfType<Camera>().transform.position, transform.position + new Vector3 (0, 0, -10f), ref velocity, 0.3f);
        //}
        //else if (direction > 0)
        //{
        //    transform.Rotate(Vector3.right * Time.deltaTime);
        //}
        //Vector3 moveX = new Vector3(direction, 0, 0);
        
        //characterController.Move(moveX);
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime;       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fall") || other.CompareTag("Fireball" ))
        {
            FindObjectOfType<GameManager>().GameOver(); // if player fall, end the game
            //FindObjectOfType<Camera>().gameObject.transform.parent = null;
            //gameObject.SetActive(false);
        }
        else if (other.CompareTag("Coin"))
        {
            FindObjectOfType<GameManager>().IncreaseScore();
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("superJump"))
        {
            Destroy(other.gameObject); // when get the coins
            SuperPowerRemaining++; // when the player gets the object he earns count of one sumperPower (super jump) 
        }
    }

    /// <summary>
    /// check if player is grounded
    /// </summary>
    private void IsGrounded()
    {
        // check if the layer that the raycast reach is other than of the player
        if(Physics.Raycast(capsuleCollider.bounds.center, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f, playerLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void Move()
    {

    }
}
