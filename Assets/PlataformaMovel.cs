using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [SerializeField] private float alturaMaxima = 5f;
    [SerializeField] private float velocidade = 2f;   
    private Vector3 pontoInicial;
    private Vector3 pontoFinal;
    private bool indoParaOFinal = true;

    void Start()
    {
        pontoInicial = transform.position;

        pontoFinal = pontoInicial + new Vector3(0, alturaMaxima, 0);
    }

    void Update()
    {
        Vector3 destinoAtual = indoParaOFinal ? pontoFinal : pontoInicial;

        transform.position = Vector3.MoveTowards(transform.position, destinoAtual, velocidade * Time.deltaTime);

        if (Vector3.Distance(transform.position, destinoAtual) < 0.01f)
        {
            indoParaOFinal = !indoParaOFinal;
        }
    }
}
