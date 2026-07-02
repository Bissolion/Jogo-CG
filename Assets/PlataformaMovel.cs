using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [SerializeField] private float alturaMaxima = 5f; // O quanto ela vai subir
    [SerializeField] private float velocidade = 2f;   // Velocidade do movimento

    private Vector3 pontoInicial;
    private Vector3 pontoFinal;
    private bool indoParaOFinal = true;

    void Start()
    {
        // Salva a posição de onde a plataforma começou no cenário
        pontoInicial = transform.position;

        // Define o ponto mais alto com base na altura máxima configurada
        pontoFinal = pontoInicial + new Vector3(0, alturaMaxima, 0);
    }

    void Update()
    {
        // Define qual é o objetivo atual da plataforma
        Vector3 destinoAtual = indoParaOFinal ? pontoFinal : pontoInicial;

        // Move a plataforma em direção ao destino de forma suave por frame
        transform.position = Vector3.MoveTowards(transform.position, destinoAtual, velocidade * Time.deltaTime);

        // Se ela chegou muito perto do destino, inverte a direção
        if (Vector3.Distance(transform.position, destinoAtual) < 0.01f)
        {
            indoParaOFinal = !indoParaOFinal;
        }
    }
}
