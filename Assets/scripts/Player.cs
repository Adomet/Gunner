using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{

    public string playerName = "Player";

    public GameController GC;

    public Gun Gun;
    public GunTypes MyGunType;
    public GameObject Body;
    public TrailRenderer TR;

    public int PlayerPoint = 1;
    public int KillCount = 0;
    public int pUpCount = 0;

    public bool IsAI = true;

    public GameObject PowerUpPrefab;

    public Vector3 SpPos = new Vector3(0, 1, 0);

    public float SpSpeed = 1f;

    public Color MyColor;

    public AIMovement AIMove;
    public PlayerMovement PlayerMove;


    public ObjectPooler ObjectPooler;

    public bool ShootButtonPressed = false;
    public float Health = 100f;

    //UI
    public Image HealthBarImage;

    public TextMeshProUGUI P_Ammo;

    public void AddHealth(float addition)
    {

        Health = Mathf.Clamp(Health + addition, 0f, 10f);
        if (Health == 0f)
            GameOver();
        CalHealtBarLevel();

    }

    public void CalHealtBarLevel()
    {
        // BoostBar fill amount = (boostbarvalue / 10 )/ 3 
        if(!IsAI)
        HealthBarImage.fillAmount = Health / 30f;
    }

    public void UpdateAmmo()
    {
        P_Ammo.SetText(Gun.currentBulletAmount.ToString());
    }

    public void AddPointToPlayer(int addition)
    {
        PlayerPoint += addition;

        KillCount++;

        if(KillCount / 2!=0 && KillCount%2 == 0)
        {
            Gun.UpgradeGun();
        }
    }


    public void ShootButtonPress()
    {
        ShootButtonPressed = true;
    }
    public void ShootButtonRelease()
    {
        ShootButtonPressed = false;
    }

    // Start is called before the first frame update
    void Start()
    {


        if (IsAI)
        {
            AIMove = GetComponent<AIMovement>();
            playerName += Random.Range(0, 21);
        }
        else
        {
            PlayerMove = GetComponent<PlayerMovement>();
        }


    }

    private void OnEnable()
    {

        GC = GameObject.FindObjectOfType<GameController>();

        ObjectPooler = FindObjectOfType<ObjectPooler>();

        //Change indivicual color

        MyColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); ;


        Body.GetComponent<Renderer>().material.color = MyColor;

        TR.startColor = MyColor;
        MyColor.a = 0;

        TR.endColor = MyColor;
        if(!IsAI)
        UpdateAmmo();
    }


    // Update is called once per frame
    void Update()
    {
        if (ShootButtonPressed)
            ShootGun();
    }

    public void ShootGun()
    {
        Gun.Shoot();
    }


    public void GameOver()
    {


        if (IsAI)
        {
            //  Debug.Log("AI Game Over");
            ObjectPooler.DestroyToPool(gameObject);
        }

        else
        {
            gameObject.SetActive(false);
            GC.PlayerGameOver();
        }


        // Pooler.DestroyToPool(pl.gameObject);


        Gun.GunMesh.gameObject.SetActive(false);
        Destroy(Gun.GunMesh.gameObject);



        GameObject SpCube1 = ObjectPooler.SpawnFromPool("PUpCube(Clone)", PowerUpPrefab, (transform.position + SpPos), Random.rotation);
        Vector3 force = -1 * transform.forward * SpSpeed * 1.5f * Random.Range(0.5f, 1.2f);
        SpCube1.GetComponent<Rigidbody>().velocity = force;


        GameObject SpCube3 = ObjectPooler.SpawnFromPool("PUpCube(Clone)", PowerUpPrefab, (transform.position + SpPos), Random.rotation);
        Vector3 force3 = -1 * (transform.forward + transform.right) * SpSpeed * Random.Range(0.5f, 1.2f);
        SpCube3.GetComponent<Rigidbody>().velocity = force3;

        GameObject SpCube2 = ObjectPooler.SpawnFromPool("PUpCube(Clone)", PowerUpPrefab, (transform.position + SpPos), Random.rotation);
        Vector3 force2 = -1 * (transform.forward - transform.right) * SpSpeed * Random.Range(0.5f, 1.2f);
        SpCube2.GetComponent<Rigidbody>().velocity = force2;



        SpCube1.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        SpCube2.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        SpCube3.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);



      
    }
}
