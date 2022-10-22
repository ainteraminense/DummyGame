using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 1f;
    private void Update()
    {
        transform.position += Vector3.left*speed*Time.deltaTime; // move the ball
        StartCoroutine(DestroyFireball()); // destroy fireball after 4 seconds
    }
    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject); // if hit player end game
    }
}
