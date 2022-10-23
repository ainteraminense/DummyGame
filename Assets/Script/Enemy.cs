using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private GameObject fireBall;

    public GameObject FireBall { get => fireBall; set => fireBall = value; }

    private void Update()
    {
        if (!FireBall)
        {
            FireBall = Instantiate(prefab, new Vector3(transform.position.x - transform.lossyScale.x / 2, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        }    
    }
}
