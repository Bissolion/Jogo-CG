using UnityEngine;
using System.Collections;

public class SwordHitbox : MonoBehaviour
{
    public Collider hitCollider;
    private bool hasHitThisSwing = false;

    void Start()
    {
        if (hitCollider == null)
        {
            hitCollider = GetComponent<Collider>();
        }
        hitCollider.enabled = false;
    }

    void Update()
    {
        if (hitCollider.enabled == false)
        {
            hasHitThisSwing = false;
        }
        else if (hitCollider.enabled == true && !hasHitThisSwing)
        {
            VerificarAcertoManual();
        }
    }

    void VerificarAcertoManual()
    {
        BoxCollider box = hitCollider as BoxCollider;
        if (box == null) return;

        Vector3 center = box.transform.TransformPoint(box.center);
        Vector3 halfExtents = Vector3.Scale(box.size, box.transform.lossyScale) * 0.5f;

        Collider[] acertados = Physics.OverlapBox(center, halfExtents, box.transform.rotation);

        foreach (Collider other in acertados)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyController enemy = other.GetComponentInParent<EnemyController>();
                DarkMageController mage = other.GetComponentInParent<DarkMageController>();

                if (enemy != null)
                {
                    enemy.TakeDamage();
                    hasHitThisSwing = true;
                    StartCoroutine(ApplyHitStop());
                    break;
                }
                else if (mage != null)
                {
                    mage.TakeDamage(1);
                    hasHitThisSwing = true;
                    StartCoroutine(ApplyHitStop());
                    Debug.Log("Espadada conectou na Maga via Matemática Perfeita!");
                    break;
                }
            }
        }
    }

    IEnumerator ApplyHitStop()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.05f);
        Time.timeScale = 1f;
    }
}