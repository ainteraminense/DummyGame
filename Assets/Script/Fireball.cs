﻿using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //VARIABLES
    public float speed = 1f;
    public float weaponRange = 50f;

    private Player player;
    private Vector3 target;
    private GameObject explosion;

    //REFERENCES
    [SerializeField] private GameObject prefab;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        target = player.transform.position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (target == transform.position && explosion == null)
        {
            transform.localScale = new Vector3(0, 0, 0); //Make fireball disappear after reach final point
            explosion = Instantiate(prefab, target, Quaternion.identity); //Instantiant explosion prefab
            StartCoroutine(DestroyFireball()); // destroy fireball 
        }

    }
    /// <summary>
    /// Destroy the explosion prefab and fireball prefab
    /// </summary>
    /// <returns> 1 second</returns>
    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(1f);
        Destroy(explosion.gameObject); // destroy explosion prefab
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject); // if hit player destroy firebal
    }
}
