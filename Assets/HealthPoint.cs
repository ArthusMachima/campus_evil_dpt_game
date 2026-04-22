using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPoint : MonoBehaviour, IDamagable
{
    [SerializeField] int HPMax=100;
    [SerializeField] int HP;

    public void TakeDamage(int dmg)
    {
        HP-=dmg;
    }

    private void Start()
    {
        HP = HPMax;
    }

    private void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
