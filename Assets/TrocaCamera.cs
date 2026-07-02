using UnityEngine;

public class TrocaCamera : MonoBehaviour
{
    [Tooltip("Arraste a Câmera que segue o jogador (3D) aqui")]
    public GameObject cameraPrincipal;

    [Tooltip("Arraste a Câmera Ortográfica da masmorra (2D) aqui")]
    public GameObject cameraMasmorra;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraPrincipal.SetActive(false); 
            cameraMasmorra.SetActive(true);
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraPrincipal.SetActive(true);  
            cameraMasmorra.SetActive(false);  
        }
    }
}