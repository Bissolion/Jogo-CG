using UnityEngine;

public class TutorialInteraction : MonoBehaviour
{
    public GameObject painelTutorial; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            painelTutorial.SetActive(true); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            painelTutorial.SetActive(false); 
        }
    }
}