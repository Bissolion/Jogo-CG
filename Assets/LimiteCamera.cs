using UnityEngine;

public class LimiteCamera : MonoBehaviour
{
    [Tooltip("Até onde a câmera pode ir para a ESQUERDA?")]
    public float limiteEsquerdo = -10f;

    [Tooltip("Até onde a câmera pode ir para a DIREITA?")]
    public float limiteDireito = 10f;

    void LateUpdate()
    {
        Vector3 posicaoAtual = transform.position;

        posicaoAtual.x = Mathf.Clamp(posicaoAtual.x, limiteEsquerdo, limiteDireito);

        transform.position = posicaoAtual;
    }
}