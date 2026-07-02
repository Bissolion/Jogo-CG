using UnityEngine;
using UnityEngine.SceneManagement;  

public class MenuManager : MonoBehaviour
{
    public void IniciarJogo()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
}
