using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MecanicaFase1 : MonoBehaviour
{
    private int bolasRecebidas = 0;
    private SistemaVida sistemaVida;

    [Header("Transição Dramática de Morte")]
    public CanvasGroup telaPretaCG;

    [Tooltip("Quão rápido a tela vai escurecer. Menor = mais dramático e lento.")]
    public float velocidadeFadeMorte = 0.8f;

    [Tooltip("Tempo que o jogador fica no breu total antes da Fase 2 carregar.")]
    public float tempoEscuridao = 1.5f;

    void Start()
    {
        sistemaVida = GetComponent<SistemaVida>();
    }

    public void ReceberMagiaFase1()
    {
        bolasRecebidas++;

        if (sistemaVida == null) sistemaVida = GetComponent<SistemaVida>();

        if (bolasRecebidas == 1)
        {
            PlayerController controle = GetComponent<PlayerController>();
            if (controle != null)
            {
                controle.escudoDesativadoPelaFase1 = true;
            }

            if (sistemaVida != null) sistemaVida.defendendo = false;

            Debug.Log("Escudo desativado!");
        }
        else if (bolasRecebidas >= 2)
        {
            if (sistemaVida != null) sistemaVida.TomarDano(999);

            StartCoroutine(IrParaFase2());
        }
    }

    private IEnumerator IrParaFase2()
    {
        if (sistemaVida != null)
        {
            yield return new WaitForSeconds(sistemaVida.tempoEsperaMorte);
        }

        if (telaPretaCG != null)
        {
            telaPretaCG.gameObject.SetActive(true);
            telaPretaCG.blocksRaycasts = true; 

            telaPretaCG.alpha = 0f;

            while (telaPretaCG.alpha < 1f)
            {
                telaPretaCG.alpha += Time.deltaTime * velocidadeFadeMorte;
                yield return null;
            }
        }

        yield return new WaitForSeconds(tempoEscuridao);

        SceneManager.LoadScene("Fase2");
    }
}