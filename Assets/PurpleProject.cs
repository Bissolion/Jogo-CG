using UnityEngine;

public class PurpleProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;

    void Start()
    {
        // Faz ela voar para frente (considerando o forward da Maga)
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, 3f); // Mata a magia após 3 segundos
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Identificamos a cena atual
            string cenaAtual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            // 2. Se for Fase 1, usamos a lógica exclusiva dela
            if (cenaAtual == "Fase1")
            {
                MecanicaFase1 fase1 = other.GetComponent<MecanicaFase1>();
                if (fase1 != null)
                {
                    fase1.ReceberMagiaFase1();
                }
            }
            // 3. Se NÃO for Fase 1 (ex: Fase 3), usamos o SistemaVida padrão
            else
            {
                SistemaVida vidaPlayer = other.GetComponent<SistemaVida>();
                if (vidaPlayer != null)
                {
                    vidaPlayer.TomarDano(damage);
                }
            }

            // Destrói a magia após processar o dano correto
            Destroy(gameObject);
        }
    }
}

