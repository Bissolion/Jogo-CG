using UnityEngine;

public class CameraCatedralFollow : MonoBehaviour
{
    [Header("Alvo")]
    [Tooltip("Arraste o Sir Winston (Player) para cá")]
    public Transform jogador;

    [Header("Configurações de Movimento")]
    [Tooltip("Quão rápido a câmera alcança o jogador (maior = mais rápido)")]
    public float suavidade = 5f;

    [Header("Limites da Catedral (Eixo Y)")]
    [Tooltip("Ponto mais baixo que a câmera pode descer (Chão)")]
    public float limiteInferior;
    [Tooltip("Ponto mais alto que a câmera pode subir (Teto)")]
    public float limiteSuperior;

    // A câmera não vai para os lados nem para frente/trás, só para cima/baixo
    private float posicaoXFixa;
    private float posicaoZFixa;

    void Start()
    {
        // Grava o X (centro da catedral) e o Z (distância) para eles nunca mudarem
        posicaoXFixa = transform.position.x;
        posicaoZFixa = transform.position.z;
    }

    // Usamos LateUpdate para câmeras para evitar tremedeiras (jittering)
    // Ele garante que a câmera só se mova DEPOIS que o jogador já se moveu no Update
    void LateUpdate()
    {
        if (jogador == null) return;

        // O PULO DO GATO MATEMÁTICO: Mathf.Clamp
        // Ele pega o Y do jogador. Se o jogador passar do limite, ele trava a câmera no limite.
        float alvoY = Mathf.Clamp(jogador.position.y, limiteInferior, limiteSuperior);

        // Monta a posição exata para onde a câmera deve ir neste frame
        Vector3 posicaoDestino = new Vector3(posicaoXFixa, alvoY, posicaoZFixa);

        // Vector3.Lerp faz o movimento ser fluido e macio, em vez de um corte seco
        transform.position = Vector3.Lerp(transform.position, posicaoDestino, suavidade * Time.deltaTime);
    }
}