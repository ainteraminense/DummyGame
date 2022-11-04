using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //REFERENCE
    [SerializeField] private GameObject prefab;
    private GameObject fireBall;
    private Vector3 newTarget;
    private Player player;
    private Enemy enemy;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
    }
    public GameObject fireball { get => fireBall; set => fireBall = value; }

    private void Update()
    {
        // get the angle between player y coordinate minus enemy y cordinate and enemy x cordinate
        float angle = Mathf.Atan2(newTarget.y - enemy.transform.position.y, enemy.transform.position.x) * Mathf.Rad2Deg;
        newTarget = player.transform.position; // update the new position every frame

        if (!fireball && angle < 20) // shoot one fire ball at a time and if player is within angle
        {
            fireball = Instantiate(prefab, new Vector3(transform.position.x - transform.lossyScale.x / 2, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        }    
    }
}
