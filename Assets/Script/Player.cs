using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    private new Rigidbody rigidbody;
    private float direction;
    private float strength = 7f;
    private bool isGrounded = true;
    private float superPowerRemaining = 0;
    private CapsuleCollider capsuleCollider;

    public float superPowerIncrease = 5f;

    public float SuperPowerRemaining { get => superPowerRemaining; set => superPowerRemaining = value; }

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
        direction = Input.GetAxis("Horizontal");
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
}
