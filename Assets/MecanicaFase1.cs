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

        // Garante que o sistemaVida esteja carregado
        if (sistemaVida == null) sistemaVida = GetComponent<SistemaVida>();

        if (bolasRecebidas == 1)
        {
            PlayerController controle = GetComponent<PlayerController>();
            if (controle != null)
            {
                controle.escudoDesativadoPelaFase1 = true;
            }

            // Só tenta desativar se o sistemaVida não for nulo
            if (sistemaVida != null) sistemaVida.defendendo = false;

            Debug.Log("Escudo desativado!");
        }
        else if (bolasRecebidas >= 2)
        {
            if (sistemaVida != null) sistemaVida.TomarDano(999);

            // Inicia a sequência cinematográfica de morte
            StartCoroutine(IrParaFase2());
        }
    }

    private IEnumerator IrParaFase2()
    {
        // 1. Espera o corpo do Winston cair (tempo da animação de morte)
        if (sistemaVida != null)
        {
            yield return new WaitForSeconds(sistemaVida.tempoEsperaMorte);
        }

        // 2. Prepara a tela preta (que estava invisível desde o início da fase)
        if (telaPretaCG != null)
        {
            telaPretaCG.gameObject.SetActive(true);
            telaPretaCG.blocksRaycasts = true; // Trava os cliques do mouse

            // BLINDAGEM MATEMÁTICA: Garante que começa 100% transparente
            telaPretaCG.alpha = 0f;

            // 3. Fade OUT da fase (A tela preta ganha opacidade lentamente)
            while (telaPretaCG.alpha < 1f)
            {
                telaPretaCG.alpha += Time.deltaTime * velocidadeFadeMorte;
                yield return null;
            }
        }

        // 4. O peso da derrota: O jogador encara o abismo antes do corte
        yield return new WaitForSeconds(tempoEscuridao);

        // 5. Carrega a Fase 2, entregando Winston à Feiticeira
        SceneManager.LoadScene("Fase2");
    }
}