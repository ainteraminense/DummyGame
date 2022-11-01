using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //VARIABLES
    public float speed = 1f;

    private Player player;
    private Enemy enemy;
    private Vector3 target;
    private GameObject explosion;
    //REFERENCES
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
        target = player.transform.position;
    }
    private void Update()
    {
        float dir = enemy.transform.position.y - target.y;
        if (dir > -1f && dir < 1f) //Enemy just shoot if player is not in higher place
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (target == transform.position && explosion == null)
        {
            transform.localScale = new Vector3(0, 0, 0);
            explosion = Instantiate(prefab, target, Quaternion.identity);
            //StartCoroutine(Explode());
            StartCoroutine(DestroyFireball()); // destroy fireball after 4 seconds
        }

    }
    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(1f);
        Destroy(explosion.gameObject);
        Destroy(this.gameObject);
    }
    //IEnumerator Explode()
    //{

    //}

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject); // if hit player end game
    }
}
