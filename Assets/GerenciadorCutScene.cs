using System.Collections;
using UnityEngine;
using Unity.Cinemachine; // Novo Namespace da versão 3

public class GerenciadorCutscene : MonoBehaviour
{
    public PlayerController jogador;
    public CinemachineCamera vcamApresentacao; // Arraste sua câmera aqui
    public float tempoDeApresentacao = 5f;

    void Start()
    {
        StartCoroutine(TocarCutsceneInicial());
    }

    public IEnumerator TocarCutsceneInicial()
    {
        jogador.emCutscene = true;
        vcamApresentacao.Priority = 20;

        // Na versão nova, o componente se chama CinemachineSplineDolly
        var dolly = vcamApresentacao.GetComponent<CinemachineSplineDolly>();

        if (dolly != null)
        {
            float tempoPassado = 0f;
            while (tempoPassado < tempoDeApresentacao)
            {
                tempoPassado += Time.deltaTime;
                // O valor vai de 0 a 1 (0% a 100% do trilho)
                dolly.CameraPosition = Mathf.Lerp(0f, 1f, tempoPassado / tempoDeApresentacao);
                yield return null;
            }
        }

        vcamApresentacao.Priority = 0;
        yield return new WaitForSeconds(1.5f);
        jogador.emCutscene = false;
    }
}