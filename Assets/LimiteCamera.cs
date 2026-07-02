using UnityEngine;

public class LimiteCamera : MonoBehaviour
{
    [Tooltip("Até onde a câmera pode ir para a ESQUERDA?")]
    public float limiteEsquerdo = -10f;

    [Tooltip("Até onde a câmera pode ir para a DIREITA?")]
    public float limiteDireito = 10f;

    // Usamos LateUpdate para garantir que a câmera seja limitada 
    // logo APÓS o jogador já ter terminado de se mover neste frame.
    void LateUpdate()
    {
        Vector3 posicaoAtual = transform.position;

        // Trava o valor de X
        posicaoAtual.x = Mathf.Clamp(posicaoAtual.x, limiteEsquerdo, limiteDireito);

        // Devolve a posição corrigida para a câmera
        transform.position = posicaoAtual;
    }
}