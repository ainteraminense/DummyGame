using UnityEngine;

public class Player : MonoBehaviour
{
    //VARIABLES    
    private float direction;
    private bool isGrounded = true;
    private float superPowerRemaining = 0;
    private float SuperPowerRemaining { get => superPowerRemaining; set => superPowerRemaining = value; }

    public float strength = 7f;
    public float superPowerIncrease = 5f;
    public Vector3 offset = new Vector3(0, 0, -5f);

    //REFERENCES
    [SerializeField] private LayerMask playerLayer;
    private Animator anim;
    private new Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

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

        if (Input.GetAxis("Horizontal") != 0)
        {
            Walk();
            MoveCamera();
        }
        else
        {
            Idle();
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fall") || other.CompareTag("Fireball" ))
        {
            //FindObjectOfType<GameManager>().GameOver(); // if player fall, end the game
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

    private void Idle()
    {
        anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }
    private void Walk()
    {
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
        direction = Input.GetAxis("Horizontal");
        transform.forward = new Vector3(0, 0, direction);
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime;
    }
    private void MoveCamera()
    {
        //It is better to use camera reference instead
        FindObjectOfType<Camera>().transform.position = Vector3.SmoothDamp(FindObjectOfType<Camera>().transform.position, transform.position + offset, ref velocity, 0.3f);
    }
}
