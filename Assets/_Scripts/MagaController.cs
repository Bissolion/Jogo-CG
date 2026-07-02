using UnityEngine;

public class DarkMageController : MonoBehaviour
{
    [Header("Status da Maga")]
    public int health = 3;
    public float tempoEsperaMorte = 1.5f;

    [Header("Ataque à Distância")]
    public Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackRange = 8f;
    public float cooldownAtaque = 1.5f;

    private Animator anim;
    private bool isDead = false;
    private float nextFireTime = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            LookAtPlayer();

            if (Time.time >= nextFireTime)
            {
                CastSpell();
                nextFireTime = Time.time + cooldownAtaque;
            }
        }
    }

    void LookAtPlayer()
    {
        if (player.position.x > transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    void CastSpell()
    {
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void TakeDamage(int damageAmount = 1)
    {
        if (isDead) return;

        health -= damageAmount;
        Debug.Log("<color=#800080>MAGA SOMBRIA FERIDA! Vida: " + health + "</color>");

        if (health <= 0) Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("MAGA SOMBRIA DERROTADA!");

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true; 
        }

        if (anim != null) anim.SetTrigger("Morrer");

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Destroy(gameObject, tempoEsperaMorte);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void SpawnFireball()
    {
        if (projectilePrefab != null && firePoint != null && player != null)
        {
            Vector3 direcao = (player.position - firePoint.position).normalized;

            direcao.y = 0;

            Quaternion rotacaoCerta = Quaternion.LookRotation(direcao);

            Instantiate(projectilePrefab, firePoint.position, rotacaoCerta);
        }
    }
}