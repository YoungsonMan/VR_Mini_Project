using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{

    [SerializeField] GameObject bPrefab;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] float bulletSpeed;

    [SerializeField] ParticleSystem vfxFire;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }


    }

    public void Fire()
    {
        GameObject bullet = ObjectPoolingManager.SpawnObject(bPrefab, muzzlePoint.position, muzzlePoint.rotation);
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(bullet.transform.forward * bulletSpeed ,  ForceMode.Impulse);
        // rigid.velocity = bullet.transform.forward * bulletSpeed * Time.deltaTime;
        vfxFire.Play();
    }
}
