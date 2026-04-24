using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PickUpHandItems : MonoBehaviour
{
    public AudioSource gunFire;
    public AudioSource gunEmpty;
    private InputManager input;
    public Animator handGunAnim;

    public GameObject handGun;
    public bool canFire = true;

    [Header("Ammo")]
    public int currentAmmo = 12;
    public int pocketAmmo;
    public int maxAmmo = 12;

    [Header("Ammo Duisplay details")]
    public TextMeshProUGUI ammoDetails;
    public TextMeshProUGUI pocketAmmoText;

    [Header("Tool Avaibility")]
    public bool isGun = false;
    public bool isIpad = false;

    [Header("Display Stat weapon or tablet")]
    public GameObject gunDetails;

    [Header("BulletPrefabs")]
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public int poolSize = 50;

    [Header("Melee Special")]
    public int MaxMeleeCharges=3;
    [SerializeField] int MeleeCharges;
    [SerializeField] GameObject MeleePrefab;
    [SerializeField] Image[] MeleeChargesUI;
    [SerializeField] Transform MeleeChargesUIParent;
    
    public Queue<GameObject> bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    private void Start()
    {
        MeleeCharges = MaxMeleeCharges;
        MeleeChargesUI = MeleeChargesUIParent.GetComponentsInChildren<Image>();

        handGun.SetActive(false);
        gunDetails.SetActive(false);
        input = GetComponent<InputManager>();
        ammoDetails.text = currentAmmo + "/" + maxAmmo;
        pocketAmmoText.text = pocketAmmo.ToString();


        
    }

    private void Update()
    {
        if (isGun)
        {
            handGun.SetActive(true);
            gunDetails?.SetActive(true);
            HandGun();
        }
        
    }

    void HandGun()
    {
        if (input.OnFoot.Fire.triggered && canFire && isGun)
        {
            HandgunCurrentAmmo();
        }

        if (input.OnFoot.Reload.triggered && isGun)
        {
            HandGunReload();
        }

        if (input.OnFoot.Special.triggered && !MeleePrefab.activeInHierarchy)
        {
            UseMeleeAbility();
        }
    }

    IEnumerator FiringGun()
    {
        gunFire.Play();
        handGunAnim.Play("HandgunRecoil");
        yield return new WaitForSeconds(0.5f); 
        handGunAnim.Play("HandgunIdle");
        canFire = true; 
    }

    void HandgunCurrentAmmo()
    {
        if (currentAmmo != 0)
        {
            currentAmmo--;
            ammoDetails.text = currentAmmo + "/" + maxAmmo;
            canFire = false;
            GetBullet(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            StartCoroutine(FiringGun());
            
        }
        else
        {
            Debug.Log("Amoo is empty");
            gunEmpty.Play();
        }
    }

    void HandGunReload()
    {
        if (currentAmmo < maxAmmo && pocketAmmo > 0)
        {
            int reloadVal = maxAmmo - currentAmmo;

            // limit reloadVal to what we actually have
            reloadVal = Mathf.Min(reloadVal, pocketAmmo);

            currentAmmo += reloadVal;
            pocketAmmo -= reloadVal;

            ammoDetails.text = currentAmmo + "/" + maxAmmo;
            pocketAmmoText.text = pocketAmmo.ToString();
        }
        else
        {
            Debug.Log("Ammo is full or pocket empty.");
        }
    }


    GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bulletToFire = bulletPool.Dequeue();
        bulletToFire.transform.position = position;
        bulletToFire.transform.rotation = rotation;
        bulletToFire.SetActive(true);
        bulletPool.Enqueue(bulletToFire);
        return bulletToFire;
    }

    //Equip the gun
    public void EquipGun()
    {
        isGun = true;
        Debug.Log("Player now has a gun!");
    }

    public void MagPickup()
    {
        int collect = Random.Range(10, 15);
        pocketAmmo += collect;
        pocketAmmoText.text = pocketAmmo.ToString();
    }

    public void UseMeleeAbility()
    {
        if (MeleeCharges>0)
        {
            MeleeCharges--;
            UpdateMeleeChargesCountUI();
            MeleePrefab.SetActive(true);
            LeanTween.cancel(MeleePrefab);
            LeanTween.delayedCall(4, () =>
            {
                MeleePrefab.SetActive(false);
            });
        }
    }

    public void UpdateMeleeChargesCountUI()
    {
        if (MeleeCharges > 3) MeleeCharges = 3;
        
        foreach (var charges in MeleeChargesUI) charges.gameObject.SetActive(false);
        for (int i = 0; i < MeleeCharges; i++) MeleeChargesUI[i].gameObject.SetActive(true);
    }
}
