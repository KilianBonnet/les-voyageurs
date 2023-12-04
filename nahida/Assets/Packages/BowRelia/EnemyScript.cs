using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] Transform player;

    //speed 5 for walking ennemy and 7 for running ones
    [SerializeField] float speed = 5f;
    private Rigidbody rb;
    public float detectionRange = 20f;
    public float minDistance = 1f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position,detectionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                
                float distance = Vector3.Distance(transform.position, player.position);
                if(distance > minDistance)
                    moveToPlayer();
                lookAt();
                break;
            }
        }  
    }

    void lookAt()
    {
        transform.LookAt(player);
    }
    
    void moveToPlayer()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
    }



}
