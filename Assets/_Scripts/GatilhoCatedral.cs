using UnityEngine;
using Unity.Cinemachine;

public class GatilhoCatedral : MonoBehaviour
{
    [Header("Câmera da Masmorra")]
    public CinemachineCamera vCamCatedral;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vCamCatedral.Priority = 20;
            Debug.Log("Entrou na Catedral");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vCamCatedral.Priority = 0;
            Debug.Log("Saiu da Catedral");
        }
    }
}