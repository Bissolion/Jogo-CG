using UnityEngine;

public class TrocaCamera : MonoBehaviour
{
    [Tooltip("Arraste a Câmera que segue o jogador (3D) aqui")]
    public GameObject cameraPrincipal;

    [Tooltip("Arraste a Câmera Ortográfica da masmorra (2D) aqui")]
    public GameObject cameraMasmorra;

    // Quando o jogador ENTRA no cubo invisível
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se quem entrou tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            cameraPrincipal.SetActive(false); // Desliga a câmera normal
            cameraMasmorra.SetActive(true);   // Liga a câmera da masmorra
        }
    }

    // Quando o jogador SAI do cubo invisível
    private void OnTriggerExit(Collider other)
    {
        // Verifica se quem saiu tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            cameraPrincipal.SetActive(true);  // Liga a câmera normal de volta
            cameraMasmorra.SetActive(false);  // Desliga a câmera da masmorra
        }
    }
}