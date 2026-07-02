using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SistemaVida : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vidaAtual;

    [Tooltip("Marca se o personagem está se defendendo")]
    public bool defendendo = false;

    [Header("Interface HUD")]
    public Slider barraDeVida;
    public float velocidadeDaBarra = 10f;

    [Header("UI de Game Over (Apenas Fase 3)")]
    public GameObject painelLore;

    [Header("Transição Fade (Fase 3)")]
    public CanvasGroup telaPretaCG;
    public float velocidadeFade = 1.0f;
    public float tempoEscuridao = 1.0f;

    [Header("Efeito de Dano (Flash)")]
    public Material materialFlash;
    public float tempoDoFlash = 0.15f;

    [Header("Configurações de Morte")]
    public float tempoEsperaMorte = 1.5f;
    private Animator anim;
    private bool estaMorto = false;

    private Renderer[] renderizadores;
    private Dictionary<Renderer, Material[]> materiaisOriginais = new Dictionary<Renderer, Material[]>();

    void Start()
    {
        vidaAtual = vidaMaxima;
        anim = GetComponent<Animator>();

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }

        renderizadores = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderizadores)
        {
            materiaisOriginais.Add(rend, rend.materials);
        }
    }

    void Update()
    {
        if (barraDeVida != null && barraDeVida.value != vidaAtual)
        {
            barraDeVida.value = Mathf.Lerp(barraDeVida.value, vidaAtual, velocidadeDaBarra * Time.deltaTime);
        }
    }

    public void TomarDano(int quantidadeDano)
    {
        if (estaMorto || vidaAtual <= 0) return;

        bool fase1Ativa = (SceneManager.GetActiveScene().name == "Fase1");
        bool bloqueioPermitido = defendendo && !fase1Ativa;

        if (bloqueioPermitido)
        {
            Debug.Log("Defendeu!");
            return;
        }

        vidaAtual -= quantidadeDano;

        if (vidaAtual <= 0)
        {
            Morrer();
        }
        else
        {
            StartCoroutine(PiscarDano());
        }
    }

    private IEnumerator PiscarDano()
    {
        foreach (Renderer rend in renderizadores)
        {
            if (rend != null)
            {
                Material[] flashMats = new Material[rend.materials.Length];
                for (int i = 0; i < flashMats.Length; i++)
                {
                    flashMats[i] = materialFlash;
                }
                rend.materials = flashMats;
            }
        }

        yield return new WaitForSeconds(tempoDoFlash);

        foreach (Renderer rend in renderizadores)
        {
            if (rend != null && materiaisOriginais.ContainsKey(rend))
            {
                rend.materials = materiaisOriginais[rend];
            }
        }
    }

    private void Morrer()
    {
        if (estaMorto) return;
        estaMorto = true;

        Debug.Log(gameObject.name + " morreu!");

        if (anim != null)
        {
            anim.SetBool("IsBlocking", false);
            anim.SetTrigger("Morrer");
        }

        if (gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name != "Fase1")
            {
                StartCoroutine(ProcessarMorteDoJogador());
            }
        }
    }

    private IEnumerator ProcessarMorteDoJogador()
    {
        yield return new WaitForSeconds(tempoEsperaMorte);

        string nomeFase = SceneManager.GetActiveScene().name;
        if (nomeFase == "Fase1")
        {
            SceneManager.LoadScene("Fase2");
        }
        else if (nomeFase == "Fase2")
        {
            SceneManager.LoadScene("Fase2");
        }
        else if (nomeFase == "Fase3")
        {
            if (barraDeVida != null)
            {
                barraDeVida.gameObject.SetActive(false);
            }

            if (telaPretaCG != null)
            {
                telaPretaCG.gameObject.SetActive(true);
                telaPretaCG.blocksRaycasts = true;
                telaPretaCG.alpha = 0f;

                while (telaPretaCG.alpha < 1f)
                {
                    telaPretaCG.alpha += Time.unscaledDeltaTime * velocidadeFade;
                    yield return null;
                }
            }

            yield return new WaitForSecondsRealtime(tempoEscuridao);

            if (painelLore != null)
            {
                painelLore.SetActive(true);
            }
            else
            {
                Debug.LogWarning("O Painel Lore não foi associado no Inspetor!");
            }
        }
    }
}