using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyMaxHP;
    public float enemyCurrentHP = 0;

    private CapsuleCollider enemyCollider;


    [SerializeField] Slider HpBar;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    [SerializeField] GameObject target;
    private float targetDelay = 0.5f; 
    void Start()
    {
        InitEnemyHP();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider>();
        // 목표물 받기(플레이어)
        target = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        HpBar.value = enemyCurrentHP / enemyMaxHP;
        findPlayer();
    }

    private void InitEnemyHP()
    {
        enemyCurrentHP = enemyMaxHP; 
    }
    private void findPlayer()
    {
        if(target != null)
        {
            float maxDelay = 0.5f;
            targetDelay += Time.deltaTime;
            if (targetDelay < maxDelay)
            {
                return;
            }

            agent.destination = target.transform.position;
            transform.LookAt(target.transform.position);

            bool isInRange = Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance;
            if (isInRange)
            {
                animator.SetTrigger("Attack");
                Debug.Log("공격");
            }
            else
            {
                animator.SetFloat("Speed", agent.velocity.magnitude);
                Debug.Log("이동");
            }

            targetDelay = 0;
        }
        
    }
    public void TakeHit(int damage)
    {
        enemyCurrentHP -= damage;
        Debug.Log($"{gameObject.name} HP : {enemyCurrentHP}.");
        if (enemyCurrentHP <= 0)
        {
            animator.SetTrigger("Dead");
            
            StartCoroutine(Die());
            return;
        }
    }
    IEnumerator Die()
    {
        agent.speed = 0;
        animator.SetTrigger("Dead");
        enemyCollider.enabled = false;
        Debug.Log("죽음");
        yield return new WaitForSeconds(3f);
        // Destroy(gameObject);
        ObjectPoolingManager.ReturnObjectToPool(gameObject);
    }




}
