using UnityEngine;

public class FlechaInimiga : MonoBehaviour
{
    public float velocidade = 15f;
    public int dano = 10;

    
    private float fixedZPosition;

    void Start()
    {
        
        fixedZPosition = transform.position.z;

        
        Destroy(gameObject, 5f);
    }

    void Update()
    {

        transform.Translate(Vector3.left * velocidade * Time.deltaTime, Space.World);

        
        Vector3 currentPos = transform.position;
        currentPos.z = fixedZPosition;
        transform.position = currentPos;
    }

    void OnTriggerEnter(Collider other)
    {
        
        bool atingiuInimigo = other.CompareTag("Enemy") || other.transform.root.CompareTag("Enemy") ||
                                other.CompareTag("Light") || other.transform.root.CompareTag("Light");

        if (other.CompareTag("Player"))
        {
            SistemaVida vidaWinston = other.GetComponent<SistemaVida>();

            if (vidaWinston != null)
            {
                vidaWinston.TomarDano(dano);
            }

            Debug.Log("Sir Winston tomou uma flechada!");
            Destroy(gameObject); 
        }
        
        else if (!atingiuInimigo)
        {
            Destroy(gameObject);
        }
    }
}