using UnityEngine;

public class DivineLight : MonoBehaviour
{
    [Tooltip("Dano exato aplicado a cada pulso")]
    public int danoPorTick = 10;
    
    [Tooltip("Tempo em segundos entre cada pulso de dano")]
    public float intervaloDoTick = 1f;

    
    private float tempoProximoDano = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (Time.time >= tempoProximoDano)
            {
                SistemaVida sistemaVida = other.GetComponent<SistemaVida>();
                
                if (sistemaVida != null)
                {
                    
                    sistemaVida.TomarDano(danoPorTick);
                    
                    
                    tempoProximoDano = Time.time + intervaloDoTick;
                }
            }
        }
    }
}