using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletModel : MonoBehaviour
{
    [SerializeField] int curBullet;

    public int CurBullet { get { return curBullet; } set { curBullet = value; OnCurBulletChanged?.Invoke(curBullet); } } 
    public UnityAction<int> OnCurBulletChanged;


    [SerializeField] int maxBullet;
    public int MaxBullet { get { return maxBullet; } set { maxBullet = value; OnMaxBulletChanged?.Invoke(maxBullet); } }
    public UnityAction<int> OnMaxBulletChanged;


}
