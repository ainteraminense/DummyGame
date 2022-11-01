using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 1f;
    public Player player;
    public Enemy enemy;
    public Vector3 target;

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

        StartCoroutine(DestroyFireball()); // destroy fireball after 4 seconds
    }
    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject); // if hit player end game
    }
}
