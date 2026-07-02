using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InimigoArqueiro : MonoBehaviour
{
    private Animator anim;

    [Header("Configurações de Combate")]
    public float tempoEntreAtaques = 3f;
    private float timerAtaque;

    [Header("Munição")]
    public GameObject flechaPrefab;
    public Transform pontoDeDisparo;

    [Header("Transição para Fase 3")]
    public CanvasGroup telaPretaCG;
    public float velocidadeFade = 1.0f;
    [Tooltip("Tempo extra de breu total após a tela escurecer inteira")]
    public float tempoEscuridao = 1.0f;
    public float tempoAguardarAntesDaTroca = 2.0f; // Tempo da animação de morte

    private EnemyController enemyController;


    [Header("Referências do Diretor")]
    public DiretorFase2 diretorFase2; // Arraste o objeto com o script do Diretor aqui
    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.OnEnemyDeath += IniciarTrocaDeFase;
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        timerAtaque = tempoEntreAtaques;
    }

    void Update()
    {
        timerAtaque -= Time.deltaTime;

        if (timerAtaque <= 0f)
        {
            Atacar();
            timerAtaque = tempoEntreAtaques;
        }
    }

    void Atacar()
    {
        anim.SetTrigger("Attack"); // Só inicia a animação
    }

    public void InstanciarFlecha()
    {
        // Clonando com a rotação perfeita
        Instantiate(flechaPrefab, pontoDeDisparo.position, transform.rotation);
    }

    void IniciarTrocaDeFase()
    {
        StartCoroutine(TrocarCenaAposTempo());
    }
    IEnumerator TrocarCenaAposTempo()
    {
        // Espera o corpo cair na animação
        yield return new WaitForSeconds(tempoAguardarAntesDaTroca);

        // Invoca o Diretor da Fase para fazer o serviço pesado
        if (diretorFase2 != null)
        {
            diretorFase2.IniciarTransicaoParaFase3();
        }
        else
        {
            // Fallback de segurança caso esqueça de arrastar no Inspector
            Debug.LogWarning("DiretorFase2 não foi arrastado no Inspector da Arqueira!");
            SceneManager.LoadScene("Fase3");
        }
    }

    void OnDestroy()
    {
        if (enemyController != null)
        {
            enemyController.OnEnemyDeath -= IniciarTrocaDeFase;
        }
    }

}