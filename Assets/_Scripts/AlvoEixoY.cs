using UnityEngine;

public class AlvoEixoY : MonoBehaviour
{
    [Tooltip("Arraste o Sir Winston aqui")]
    public Transform jogador;

    [Header("Eixo X (Centro da Tela)")]
    public float posicaoXFixa = 102.4f;

    [Header("Enquadramento Vertical (O Truque)")]
    [Tooltip("Valores negativos abaixam a câmera. Valores positivos sobem a câmera.")]
    public float compensacaoY = -3f;

    [Header("Limites do Eixo Y (Chão e Teto)")]
    public float limiteInferior = -10f;
    public float limiteSuperior = 20f;

    void LateUpdate()
    {
        if (jogador != null)
        {
            // 1. Pega a altura do jogador e aplica o seu desvio de enquadramento
            float alvoY = jogador.position.y + compensacaoY;

            // 2. AGORA SIM nós travamos nos limites da masmorra
            alvoY = Mathf.Clamp(alvoY, limiteInferior, limiteSuperior);

            transform.position = new Vector3(posicaoXFixa, alvoY, 0f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 pontoBaixo = new Vector3(posicaoXFixa, limiteInferior, 0f);
        Vector3 pontoAlto = new Vector3(posicaoXFixa, limiteSuperior, 0f);

        Gizmos.DrawLine(pontoBaixo, pontoAlto);
        Gizmos.DrawSphere(pontoBaixo, 0.3f);
        Gizmos.DrawSphere(pontoAlto, 0.3f);
    }
}