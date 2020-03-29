using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    //Make Scriptable Objects
    // public enum GunType  {Pistol,SMG,Ak,AUG,M4};

    public Player MyPlayer;
    public ObjectPooler Pooler;
    public GameObject BulletPrefab;

    public GameObject GunMesh;
    public GunTypes[] GunTypes;
    private GunTypes MyGunType;
    private int Magcount;
    private float reloadtime;
    public int currentBulletAmount;
    private float damage;
    private float recoiltime;
    private float tmprecoiltime;
    private float gunRange;
    public int GunTypeindex = 0;
    public bool GotMag = true;
    public float bulletSpeed;
    public Animator animator;
    public AudioSource AudioSource;


    private void OnEnable()
    {
        MyGunType = GunTypes[GunTypeindex];

        AudioSource = GetComponent<AudioSource>();
        GetGun();

        animator = GetComponent<Animator>();
  


        Pooler = FindObjectOfType<ObjectPooler>();
        MyPlayer = transform.root.GetComponent<Player>();
        tmprecoiltime = recoiltime;
        currentBulletAmount = Magcount;
        GunMesh = Instantiate(MyGunType.Gun,transform.position, transform.rotation,transform);

    }

    public void GetGun()
    {
        Magcount = MyGunType.Magcount;
        reloadtime = MyGunType.reloadtime;
        currentBulletAmount = MyGunType.Magcount;
        damage = MyGunType.damage;
        recoiltime = MyGunType.recoiltime;
        tmprecoiltime = MyGunType.tmprecoiltime;
        gunRange = MyGunType.gunRange;
        bulletSpeed = MyGunType.bulletSpeed;
        if (!MyPlayer.IsAI)
        {
            AudioSource.clip = MyGunType.GunSound;
        }
    }

    public void UpgradeGun ()
    {
        
        if (GunTypeindex < GunTypes.Length-1)
        {
            GunTypeindex++;
            MyGunType = GunTypes[GunTypeindex];
            if(GunMesh!=null)
            GunMesh.SetActive(false);
            GunMesh = Instantiate(MyGunType.Gun, transform.position, transform.rotation, transform);
            GetGun();
        }
    }

    public void ShootBullet(float diroffset)
    {
        Quaternion Shootdir = Quaternion.LookRotation(transform.forward);
        Shootdir = Quaternion.Euler(Shootdir.eulerAngles.x, Shootdir.eulerAngles.y + diroffset, Shootdir.eulerAngles.z);
        GameObject Bulletgo = Pooler.SpawnFromPool("Bullet(Clone)", BulletPrefab, transform.position+(transform.forward*1),Shootdir);
        Bullet bullet = Bulletgo.GetComponent<Bullet>();
        Color clr = MyPlayer.MyColor;
        clr.a = 1f;
        bullet.GetComponent<Renderer>().material.color = clr;
        bullet.MyPlayer = MyPlayer;
        bullet.range = gunRange;
        bullet.Pooler = Pooler;
        bullet.bulletdamage = damage;
        bullet.speed = bulletSpeed;
    }

    public void Shoot ()
    {
        if (tmprecoiltime <= 0f)
        {
           
            if (currentBulletAmount != 0)
            {
                if (animator != null)
                    animator.SetTrigger("Shoot");
                if (!MyPlayer.IsAI)
                {
                AudioSource.pitch = Random.Range(1.0f, 1.5f);
                AudioSource.Play();
                    MyPlayer.UpdateAmmo();
                }

                currentBulletAmount -= 1;
                if(!MyGunType.MultiShot)
                {
                ShootBullet(0);

                }
                else
                {
                    ShootBullet(10f);
                    ShootBullet(0f);
                    ShootBullet(-10f);
                }

            }
            else
            {
                if(GotMag)
                Reload();
            }
        tmprecoiltime = recoiltime;
        }
    }
    public void Update()
    {
    
        if (tmprecoiltime > 0)
            tmprecoiltime -= Time.deltaTime;
    }
    public void Reload()
    {
        if (!MyPlayer.IsAI)
        {
            MyPlayer.GetComponent<AudioSource>().Play();
            MyPlayer.UpdateAmmo();
        }

        StartCoroutine(ReloadAnim(reloadtime));
    }

    IEnumerator ReloadAnim(float reloadtime)
    {
        yield return new WaitForSeconds(reloadtime);
        currentBulletAmount = Magcount;
    }
}
