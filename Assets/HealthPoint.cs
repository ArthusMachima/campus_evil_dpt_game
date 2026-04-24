using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPoint : MonoBehaviour, IDamagable
{
    [SerializeField] int HPMax = 100;
    [SerializeField] Material DMGMaterial;
    [SerializeField] float DMGFlashDuration = 0.1f;
    [SerializeField] int HP;
    [SerializeField] Transform HPBar;

    private Renderer[] renderers;
    private Material[] originalMaterials;

    private void Start()
    {
        HP = HPMax;
        renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
            originalMaterials[i] = renderers[i].material;
    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        if (DMGMaterial != null && renderers.Length > 0)
            StartCoroutine(FlashDMGMaterial());
    }

    private IEnumerator FlashDMGMaterial()
    {
        foreach (Renderer r in renderers)
            r.material = DMGMaterial;

        yield return new WaitForSeconds(DMGFlashDuration);

        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material = originalMaterials[i];
    }

    private void Update()
    {
        if (HPBar != null)
            HPBar.localScale = new((float)HP / HPMax, 1, 1);

        if (HP <= 0)
        {
            Destroy(gameObject);
            Destroy(HPBar.parent.gameObject);
        }
    }
}