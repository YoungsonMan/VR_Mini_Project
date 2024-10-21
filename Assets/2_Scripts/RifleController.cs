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
    // 스코프를 따로 구현해야되나 or 버튼같은걸 눌러서 그냥 카메라로 스코프 모드를 만들어야하나
    // 그냥 눈만 갖다 대도 스코프 되는게 신기할거같긴한데...

    // 일단은 버튼누르면 줌되서 스코프 (카메라로 떙겨서) 보이는게 좀더 쉬운거 같으니 그쪽으로...해보기??


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
