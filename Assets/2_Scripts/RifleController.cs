using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RifleController : MonoBehaviour
{
    [SerializeField] Image scope;
    [SerializeField] BulletModel bulletModel;
    [SerializeField] int damage;


    [SerializeField] CinemachineFreeLook zoomedCam;
    [SerializeField] bool zoomedMode;
    // �������� ���� �����ؾߵǳ� or ��ư������ ������ �׳� ī�޶�� ������ ��带 �������ϳ�
    // �׳� ���� ���� �뵵 ������ �Ǵ°� �ű��ҰŰ����ѵ�...

    // �ϴ��� ��ư������ �ܵǼ� ������ (ī�޶�� ���ܼ�) ���̴°� ���� ����� ������ ��������...�غ���??


    [SerializeField] float reloadTime;



    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void Fire()
    {
        if (bulletModel.CurBullet <= 0) 
            return;
        if (bulletModel != null)
            return;

        bulletModel.CurBullet--;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo) )
        {
            Enemy enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
            enemy?.TakeHit(damage);
        }
    }
    Coroutine reloadRoutine;
    private void Reload()
    {
        if (bulletModel.CurBullet >= bulletModel.MaxBullet)
            return;
        if (reloadRoutine != null)
            return;

        reloadRoutine = StartCoroutine(ReloadRoutine());
    }
    IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(reloadTime);
        bulletModel.CurBullet = bulletModel.MaxBullet;
        reloadRoutine = null;
    }


    Coroutine scopeRoutine;
    IEnumerator ShowScopeRoutine()
    {
        while (true)
        {
            yield return null;
            scope.fillAmount += 5 * Time.deltaTime;

            if (scope.fillAmount >= 1f)
                yield break;
        }
    }
    IEnumerator HideScopeRoutine()
    {
        while (true)
        {
            yield return null;
            scope.fillAmount -= 5 * Time.deltaTime;

            if (scope.fillAmount <= 0f)
                yield break;
        }
    }

    private void ZoomedModeOn(bool zoomedMode)
    {
        this.zoomedMode = zoomedMode;
        if (zoomedMode)
        {
            zoomedCam.Priority = 20;
            Vector3 lookDir = Camera.main.transform.forward;
            lookDir.y = 0;


            if (scopeRoutine != null)
            {
                StopCoroutine(scopeRoutine);
            }
            scopeRoutine = StartCoroutine(ShowScopeRoutine());
        }
        else
        {
            zoomedCam.Priority = 5;

            if (scopeRoutine != null)
            {
                StopCoroutine(scopeRoutine);
            }
            scopeRoutine = StartCoroutine(HideScopeRoutine());
        }
    }

}
