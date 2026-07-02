using System.Collections;
using UnityEngine;
using Unity.Cinemachine; 

public class GerenciadorCutscene : MonoBehaviour
{
    public PlayerController jogador;
    public CinemachineCamera vcamApresentacao; 
    public float tempoDeApresentacao = 5f;

    void Start()
    {
        StartCoroutine(TocarCutsceneInicial());
    }

    public IEnumerator TocarCutsceneInicial()
    {
        jogador.emCutscene = true;
        vcamApresentacao.Priority = 20;

        var dolly = vcamApresentacao.GetComponent<CinemachineSplineDolly>();

        if (dolly != null)
        {
            float tempoPassado = 0f;
            while (tempoPassado < tempoDeApresentacao)
            {
                tempoPassado += Time.deltaTime;
                dolly.CameraPosition = Mathf.Lerp(0f, 1f, tempoPassado / tempoDeApresentacao);
                yield return null;
            }
        }

        vcamApresentacao.Priority = 0;
        yield return new WaitForSeconds(1.5f);
        jogador.emCutscene = false;
    }
}