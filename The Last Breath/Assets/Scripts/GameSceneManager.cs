using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    private SceneManager gameManager;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    public void cambiarEscena(string nombreEscena)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscena);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Escena_Inicio");
    }
}
