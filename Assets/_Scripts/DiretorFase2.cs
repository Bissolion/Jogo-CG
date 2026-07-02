using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DiretorFase2 : MonoBehaviour
{
    [Header("Configurações de Transição")]
    public CanvasGroup telaPretaCG;
    public float velocidadeFade = 1.0f;
    public float tempoEscuridao = 1.0f;

    // Esta função pública será chamada pela Arqueira antes de morrer completamente
    public void IniciarTransicaoParaFase3()
    {
        StartCoroutine(FluxoTransicao());
    }

    private IEnumerator FluxoTransicao()
    {
        if (telaPretaCG != null)
        {
            telaPretaCG.gameObject.SetActive(true);
            telaPretaCG.blocksRaycasts = true;
            telaPretaCG.alpha = 0f;

            // Transição fluida: Escurece a tela totalmente
            while (telaPretaCG.alpha < 1f)
            {
                telaPretaCG.alpha += Time.deltaTime * velocidadeFade;
                yield return null;
            }
        }

        // Pausa dramática no breu
        yield return new WaitForSeconds(tempoEscuridao);

        Debug.Log("Diretor autorizou o carregamento. Indo para a Fase 3!");
        SceneManager.LoadScene("Fase3");
    }
}