using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GerenciadorMenu : MonoBehaviour
{
    [Header("Botões do Menu")]
    public Button botaoIniciar;

    [Header("Componentes de Transição")]
    public CanvasGroup menuPrincipalCG; 
    public GameObject painelLore;
    public CanvasGroup painelLoreCG;
    public TextMeshProUGUI textoLoreComponente;
    public CanvasGroup telaPretaFadeCG;

    [Header("Configurações do Texto e Tempo")]
    [TextArea(5, 10)]
    public string historiaDoJogo = "Em uma catedral esquecida pelo tempo, as tochas remanescentes testemunham o despertar de Sir Winston...";
    public float velocidadeDigitacao = 0.05f;
    public float velocidadeFade = 1.5f;

    void Start()
    {
        painelLore.SetActive(false);
        painelLoreCG.alpha = 0f;
        telaPretaFadeCG.alpha = 0f;

        if (botaoIniciar != null)
        {
            botaoIniciar.onClick.AddListener(IniciarSequenciaDeJogo);
        }
    }

    public void IniciarSequenciaDeJogo()
    {
        botaoIniciar.interactable = false;
        StartCoroutine(FluxoCinematografico());
    }

    private IEnumerator FluxoCinematografico()
    {
        while (menuPrincipalCG.alpha > 0f)
        {
            menuPrincipalCG.alpha -= Time.deltaTime * velocidadeFade;
            yield return null;
        }

        menuPrincipalCG.gameObject.SetActive(false);

        painelLore.SetActive(true);
        textoLoreComponente.text = "";
        painelLoreCG.alpha = 1f; 

        foreach (char letra in historiaDoJogo.ToCharArray())
        {
            textoLoreComponente.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }

        yield return new WaitForSeconds(3.0f);

        telaPretaFadeCG.gameObject.SetActive(true);
        telaPretaFadeCG.alpha = 0f;
        telaPretaFadeCG.blocksRaycasts = true;

        while (telaPretaFadeCG.alpha < 1f)
        {
            telaPretaFadeCG.alpha += Time.deltaTime * velocidadeFade;
            yield return null;
        }

        SceneManager.LoadScene("Fase1");
    }

}