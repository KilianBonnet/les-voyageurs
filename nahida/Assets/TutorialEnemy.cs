using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    public static event Action TutorialComplete;

    private Transform player;

    private Rigidbody rb;
    public float detectionRange = 20f;
    public float minDistance = 1.735f;
    private Animator animator;

    //Battle Stats
    [SerializeField] int hp = 10;
    private bool dead = false;
    private bool blockMovement = false;
    public int nbHit = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hp <= 0 && !dead)
        {
            dead = true;
            OnDeath();
        }

        if (nbHit > 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Strike_1"))
        {
            return;
        }

        if (!dead)
        {
            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, detectionRange);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    ManagerPlayerCollision();
                    break;
                }
            }
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    void LookAt()
    {
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);
    }

    void FreeBody()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }

    void AttackPlayer()
    {
        nbHit = 0;
        int randomValue = UnityEngine.Random.Range(0, 2);
        animator.SetBool("attacking1", (randomValue == 0));
        animator.SetBool("attacking2", (randomValue == 1));
    }

    void OnDeath()
    {
        TutorialComplete.Invoke();
        int randomValue = UnityEngine.Random.Range(0, 2);
        if (randomValue == 0)
            animator.Play("Dead_1");
        else
            animator.Play("Dead_2");
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        StartCoroutine(DestroyAfterAnimation());
    }


    public void OnAttackAnimationComplete()
    {
        nbHit = 0;
    }

    //If skeleton get hit by the player's sword then it takes damages
    public void TakeDamage(int playerDamage)
    {
        hp -= playerDamage;
    }

    private void ManagerPlayerCollision()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > minDistance)
            LookAt();
        else
        {
            blockMovement = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            animator.SetBool("walking", false);

            AttackPlayer();
        }
    }
}
