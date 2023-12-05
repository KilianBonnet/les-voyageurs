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
    public float minDistance = 1.735f;
    private Animator animator;

    //Battle Stats
    [SerializeField] int damage = 2;
    [SerializeField] int hp = 10;
    private bool dead = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(hp <= 0 && !dead)
        {
            dead = true;
            OnDeath();
        }

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position,detectionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {       
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance > minDistance)
                    MoveToPlayer();
                else
                {
                    rb.velocity = Vector3.zero;
                    animator.SetBool("walking", false);
                    
                    AttackPlayer();
                }
                break;
            }
        }  
    }

    void LookAt()
    {
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);
    }
    
    void MoveToPlayer()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
        animator.SetBool("walking", true);
        LookAt();
    }

    void AttackPlayer()
    {
        int randomValue = Random.Range(0, 2);
        animator.SetBool("attacking1", (randomValue == 0));
        animator.SetBool("attacking2", (randomValue == 1));
    }

    //Pour modifier le score
    void addScore()
    {
      //NetworkingScore.SendScoreEvent(200);
    }

    void OnDeath()
    {
        addScore();
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
            animator.Play("Dead_1");
        else
            animator.Play("Dead_2");
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        Destroy(gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }

    //If skeleton get hit by the player's sword then it takes damages
    public void TakeDamage(int playerDamage)
    {
        hp -= playerDamage;
    }
    
}
