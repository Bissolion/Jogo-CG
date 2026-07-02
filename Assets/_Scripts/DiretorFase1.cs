using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class DiretorFase1 : MonoBehaviour
{
    [Header("UI da Transição")]
    public CanvasGroup telaPretaCG;
    public float velocidadeFade = 1.5f;

    [Header("Controle da Câmera")]
    public CinemachineSplineDolly dollyApresentacao;
    [Tooltip("Tempo que a câmera fica parada assistindo a paisagem após o Fade")]
    public float tempoParada = 3.0f;
    [Tooltip("Velocidade do carrinho no trilho")]
    public float velocidadeCamera = 1.0f;

    private bool iniciarCamera = false;

    void Start()
    {
        // 1. Segurança Máxima: Garante que o motor nativo da Unity está desligado.
        // Nós somos o chefe agora.
        if (dollyApresentacao != null)
        {
            var motor = dollyApresentacao.AutomaticDolly;
            motor.Enabled = false;
            dollyApresentacao.AutomaticDolly = motor;
        }

        // 2. Segurança Máxima: Força a tela a começar 100% preta
        if (telaPretaCG != null)
        {
            telaPretaCG.gameObject.SetActive(true);
            telaPretaCG.alpha = 1f;
        }

        // 3. Dá o play na sequência do diretor
        StartCoroutine(SequenciaDeAbertura());
    }

    private IEnumerator SequenciaDeAbertura()
    {
        // Pausa de meio segundo para a Unity terminar de carregar os gráficos
        yield return new WaitForSeconds(0.5f);

        // ATO 1: O Fade In (clareia a tela)
        if (telaPretaCG != null)
        {
            while (telaPretaCG.alpha > 0f)
            {
                telaPretaCG.alpha -= Time.deltaTime * velocidadeFade;
                yield return null; // Espera o próximo frame
            }
            telaPretaCG.gameObject.SetActive(false); // Desliga a tela preta para liberar os botões/cliques
        }

        // ATO 2: A Pausa Dramática (3 segundos admirando a paisagem)
        yield return new WaitForSeconds(tempoParada);

        // ATO 3: Ação! (Manda o Update começar a mover a câmera)
        iniciarCamera = true;
    }

    void Update()
    {
        // Se recebeu o sinal verde do Ato 3, move a câmera um pouco todo frame
        if (iniciarCamera && dollyApresentacao != null)
        {
            dollyApresentacao.CameraPosition += velocidadeCamera * Time.deltaTime;
        }
    }
}