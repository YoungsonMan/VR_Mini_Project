using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PistolController : MonoBehaviour
{

    [SerializeField] GameObject bPrefab;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] float bulletSpeed;

    [Header("SFX Effects")]
    [SerializeField] ParticleSystem vfxFire;
    [SerializeField] ParticleSystem vfxShellOut;

    [Header("SFX Sounds")]
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip clickSound;


    void Start()
    {

    }

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space))
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
        
        // �ѱ� ����Ʈȿ�� ����
        vfxFire.Play();
        vfxShellOut.Play();
        
        // �ѱ�߻� ȿ����
        SoundManager.Instance.PlaySFXPistolFire(fireSound);
    }
    public void ClickSound()
    {
        SoundManager.Instance.PlaySFXPistolClick(clickSound);
    }
 
}
