using UnityEngine;

public class TutorialInteraction : MonoBehaviour
{
    public GameObject painelTutorial; // Arraste um painel UI (Canvas) aqui

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            painelTutorial.SetActive(true); // Mostra a dica
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            painelTutorial.SetActive(false); // Esconde a dica
        }
    }
}