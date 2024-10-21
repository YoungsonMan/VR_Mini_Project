using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeammateManager : MonoBehaviour
{
    [SerializeField] Slider HpBar;
    [SerializeField] float tmMaxHP;
    public float tmCurrentHP;

    Animator animator;

    

    void Start()
    {
        tmCurrentHP = tmMaxHP;
    }


    void Update()
    {
        HpBar.value = tmCurrentHP / tmMaxHP;
    }
    public void TakeHIt(int damage)
    {
        tmCurrentHP -= damage;
        if(tmCurrentHP <= 0)
        {
            StartCoroutine(Die());
            return;
        }
    }
    IEnumerator Die()
    {
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(3f);
    }
}
