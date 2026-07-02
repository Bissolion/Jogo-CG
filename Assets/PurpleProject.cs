using UnityEngine;

public class PurpleProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;

    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, 3f); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string cenaAtual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            if (cenaAtual == "Fase1")
            {
                MecanicaFase1 fase1 = other.GetComponent<MecanicaFase1>();
                if (fase1 != null)
                {
                    fase1.ReceberMagiaFase1();
                }
            }
            else
            {
                SistemaVida vidaPlayer = other.GetComponent<SistemaVida>();
                if (vidaPlayer != null)
                {
                    vidaPlayer.TomarDano(damage);
                }
            }

            Destroy(gameObject);
        }
    }
}

