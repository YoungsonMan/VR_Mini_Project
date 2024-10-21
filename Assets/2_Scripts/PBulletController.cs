using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBulletController : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    // [SerializeField] float bulletSpeed;
    [SerializeField] float destroyTime = 3f;

    private Coroutine returnToPoolTimer;
    public int damage;



    Enemy enemy;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnEnable()
    {
        returnToPoolTimer = StartCoroutine(ReturnToPoolAfterTime());
    }
    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < destroyTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolingManager.ReturnObjectToPool(gameObject);
    }
    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            Debug.Log(collision.gameObject.name);
            ObjectPoolingManager.ReturnObjectToPool(gameObject);
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            enemy = collision.collider.gameObject.GetComponent<Enemy>();
            if (enemy != null && enemy.enemyCurrentHP > 0)
            {
                enemy.TakeHit(damage);
                //Debug.Log($"enemyHP : {enemy.enemyCurrentHP}");
            }
        }
        
    }


}
