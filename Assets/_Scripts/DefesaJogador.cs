using UnityEngine;
using UnityEngine.InputSystem; // <-- Essa linha faz a mágica do novo sistema

public class DefesaJogador : MonoBehaviour
{
    private SistemaVida minhaVida;

    void Start()
    {
        // Puxa o script de vida que está no seu personagem
        minhaVida = GetComponent<SistemaVida>();
    }

    void Update()
    {
        // Verifica se o mouse está conectado e se o botão direito está sendo segurado
        if (Mouse.current != null && Mouse.current.rightButton.isPressed)
        {
            minhaVida.defendendo = true;
        }
        else
        {
            minhaVida.defendendo = false;
        }
    }
}