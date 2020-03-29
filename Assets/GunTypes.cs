using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gun",menuName ="Gun")]
public class GunTypes : ScriptableObject
{
    public string GunName;
    public int   Magcount;
    public float reloadtime;
    public int   currentBulletAmount;
    public float damage;
    public float recoiltime;
    public float tmprecoiltime;
    public float gunRange;
    public float bulletSpeed;
    public bool MultiShot;
    public GameObject Gun;
    public AudioClip GunSound;
}
