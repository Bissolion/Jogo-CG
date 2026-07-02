using UnityEngine;
using System.Collections;

public class FadeInFase2 : MonoBehaviour
{
    [Header("Configurações do Fade")]
    public CanvasGroup telaPretaCG;

    [Tooltip("Tempo extra no breu total antes de começar a clarear a visão")]
    public float tempoNoEscuro = 0.5f;

    [Tooltip("Velocidade com que a tela vai clarear")]
    public float velocidadeFade = 1.0f;

    void Start()
    {
        
        if (telaPretaCG != null)
        {
            telaPretaCG.alpha = 1f;
            telaPretaCG.blocksRaycasts = true;

            
            StartCoroutine(DespertarWinston());
        }
    }

    private IEnumerator DespertarWinston()
    {
        
        yield return new WaitForSeconds(tempoNoEscuro);

        while (telaPretaCG.alpha > 0f)
        {
            telaPretaCG.alpha -= Time.deltaTime * velocidadeFade;
            yield return null;
        }

        telaPretaCG.gameObject.SetActive(false);
    }
}