using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyMaxHP;
    public float enemyCurrentHP = 0;

    [SerializeField] Slider HpBar;

    void Start()
    {
        InitEnemyHP();
    }
    void Update()
    {
        HpBar.value = enemyCurrentHP / enemyMaxHP;
    }

    private void InitEnemyHP()
    {
        enemyCurrentHP = enemyMaxHP; 
    }
    private void findPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    public void TakeHit(int damage)
    {
        enemyCurrentHP -= damage;
        Debug.Log($"{gameObject.name} HP : {enemyCurrentHP}.");
        if (enemyCurrentHP <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);

    }



}
