using UnityEngine;
using TMPro;
using System.Collections;

public class EfeitoTextoGameOver : MonoBehaviour
{
    public float tempoPorLetra = 0.05f;
    private TextMeshProUGUI textoMesh;

    void Awake()
    {
        textoMesh = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        if (textoMesh != null)
        {
            StartCoroutine(MostrarTextoAosPoucos());
        }
    }

    IEnumerator MostrarTextoAosPoucos()
    {
        textoMesh.ForceMeshUpdate();
        int totalCaracteres = textoMesh.textInfo.characterCount;
        textoMesh.maxVisibleCharacters = 0;

        for (int i = 0; i <= totalCaracteres; i++)
        {
            textoMesh.maxVisibleCharacters = i;

            yield return new WaitForSecondsRealtime(tempoPorLetra);
        }
    }
}