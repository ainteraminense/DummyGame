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
    public GameObject FireBall { get => fireBall; set => fireBall = value; }

    private void Update()
    {

        float angle = Mathf.Atan2(newTarget.y - enemy.transform.position.y, enemy.transform.position.x) * Mathf.Rad2Deg;
        newTarget = player.transform.position;
        Debug.Log(angle);

        if (!FireBall && angle < 20) //put angle code here
        {
            FireBall = Instantiate(prefab, new Vector3(transform.position.x - transform.lossyScale.x / 2, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        }    
    }
}
