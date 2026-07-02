using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [Header("Status do Batedor (Ultima Lux)")]
    public int health = 3;

    [Header("Configuração de Morte")]
    public float tempoEsperaMorte = 1.5f; 

    private Animator anim;
    private bool isDead = false; 
    void Start()
    {
        
        anim = GetComponent<Animator>();

        
    }


    public void TakeDamage(int damageAmount = 1)
    {
        
        if (isDead) return;

        
        health -= damageAmount;

        Debug.Log("<color=#800080>BATEDOR SANGROU! Vida: " + health + "</color>");

        if (health <= 0)
        {
            Die();
        }
    }

    public event Action OnEnemyDeath;

    private void Die()
    {
        isDead = true; 
        Debug.Log("BATEDOR DE ULTIMA LUX DERROTADO!");

        OnEnemyDeath?.Invoke();

        
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; 
            rb.isKinematic = true; 
        }

        if (anim != null)
        {
            anim.SetTrigger("Morrer");
        }

        
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
        Destroy(gameObject, tempoEsperaMorte);
    }
}