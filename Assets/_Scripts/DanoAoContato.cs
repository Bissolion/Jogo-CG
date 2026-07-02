using System.Collections;
using UnityEngine;

public class AtaqueInimigo : MonoBehaviour
{
    [Tooltip("Quanto de dano o ataque causa?")]
    public int forcaDoDano = 20;

    [Tooltip("Qual a distância máxima para o inimigo conseguir bater?")]
    public float distanciaDeAtaque = 2f;

    [Tooltip("Quantos segundos ele espera entre um ataque e outro?")]
    public float tempoEntreAtaques = 2f;

    [Tooltip("Tempo de atraso para o dano bater com a animação da espada/soco descendo")]
    public float atrasoDoDano = 0.5f;

    private Transform jogador;
    private SistemaVida vidaJogador;
    private Animator anim;
    private float proximoAtaqueTempo = 0f;

    void Start()
    {
        // Acha o jogador automaticamente pela Tag e pega a vida dele
        GameObject objJogador = GameObject.FindGameObjectWithTag("Player");
        if (objJogador != null)
        {
            jogador = objJogador.transform;
            vidaJogador = objJogador.GetComponent<SistemaVida>();
        }

        // Pega o componente de animação do próprio inimigo
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (jogador == null) return; // Se o jogador morreu ou não existe, não faz nada

        // Calcula a distância matemática entre o inimigo e o jogador
        float distancia = Vector3.Distance(transform.position, jogador.position);

        // Se o jogador estiver na área de ataque E o tempo de recarga já passou
        if (distancia <= distanciaDeAtaque && Time.time >= proximoAtaqueTempo)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        // Trava o próximo ataque baseado no tempo de espera
        proximoAtaqueTempo = Time.time + tempoEntreAtaques;

        // Toca a animação de ataque
        if (anim != null)
        {
            anim.SetTrigger("Atacar"); // O nome do gatilho no Animator precisa ser exatamente esse!
        }

        // Chama a rotina que dá o dano com um pequeno atraso (sincronizando com a animação)
        StartCoroutine(DarDanoComAtraso());
    }

    IEnumerator DarDanoComAtraso()
    {
        // Espera a animação chegar no ponto em que o golpe realmente acerta
        yield return new WaitForSeconds(atrasoDoDano);

        // Confere de novo a distância para garantir que o jogador não fugiu no meio da animação
        if (jogador != null && Vector3.Distance(transform.position, jogador.position) <= distanciaDeAtaque + 0.5f)
        {
            if (vidaJogador != null)
            {
                vidaJogador.TomarDano(forcaDoDano);
            }
        }
    }
}