using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIntroManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] float speed;

    [SerializeField] int assignedAction;
    [SerializeField] int randomAction;


    private void RandomPose()
    {
        if (assignedAction < 7)
        {
            randomAction = Random.RandomRange(1, 6);
            animator.SetInteger("ranAction", randomAction);
        }
    }
    IEnumerator RandomAction()
    {
        if (assignedAction < 7)
        {
            randomAction = Random.RandomRange(1, 6);
            animator.SetInteger("ranAction", randomAction);
        }
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(RandomAction());
    }
    void Start()
    {
        // StartCoroutine(RandomAction());
        RandomPose();
        animator.SetFloat("Speed", speed);
        
    }

    void Update()
    {
        StartCoroutine(RandomAction());
        animator.SetFloat("Speed", speed);
    }
}
