using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class Enemy : MonoBehaviour
{
    public static event Action EnemyKilledEvent;
    public static event Action<Transform> OnDeathEvent;

    private Transform player;

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
    private bool blockMovement = false;
    public int nbHit = 0;

    //Loot part
    private GameObject loot;
    private GameObject portal;
    private Transform text;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        loot = transform.Find("Emerald").gameObject;
        portal = transform.Find("Portal blue").gameObject;
        text = transform.Find("Canvas").Find("Text");
    }

    void Update()
    {
        if (dead)
        {
            return;
        }

        if (hp <= 0 && !dead)
        {
            dead = true;
            OnDeath();
        }

        if (nbHit > 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Strike_1"))
        {
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, detectionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                ManagerPlayerCollision();
                break;
            }
        }

        if (loot != null && loot.activeSelf)
        {
            if (loot.transform.localScale.x < 0.1f)
            {
                Destroy(loot.gameObject);
            }
        }

        if (portal != null && portal.activeSelf)
        {
            if (loot == null)
            {
                DestroyAll();
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

    void MoveToPlayer()
    {
        if (blockMovement)
            FreeBody();
        Vector3 pos = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
        animator.SetBool("walking", true);
        LookAt();

    }

    void AttackPlayer()
    {
        nbHit = 0;
        int randomValue = Random.Range(0, 2);
        animator.SetBool("attacking1", (randomValue == 0));
        animator.SetBool("attacking2", (randomValue == 1));
    }

    void addScore()
    {
        NetworkingScore.SendScoreEvent(200);
    }

    void OnDeath()
    {
        EnemyKilledEvent.Invoke();
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
            animator.Play("Dead_1");
        else
            animator.Play("Dead_2");
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        LootHandler();
    }

    void LootHandler()
    {
        //manage loot
        loot.SetActive(true);
        loot.GetComponent<Animator>().SetBool("isEnemyDead", true);
        loot.GetComponent<Grabbable>().enabled = true;
        loot.GetComponent<HandGrabInteractable>().enabled = true;
        //manage portal
        portal.SetActive(true);
        portal.GetComponent<Animator>().SetBool("isEnemyDead", true);
        text.gameObject.SetActive(true);
    }

    public int GetDamage()
    {
        if (nbHit == 0)
        {
            nbHit++;
            return damage;
        }
        return 0;
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

    private void DestroyAll()
    {
        Destroy(portal);
        Destroy(text);
        addScore();
        StartCoroutine(DestroyAfterAnimation());
    }

    private void ManagerPlayerCollision()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > minDistance)
            MoveToPlayer();
        else
        {
            blockMovement = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            animator.SetBool("walking", false);

            AttackPlayer();
        }
    }

}
