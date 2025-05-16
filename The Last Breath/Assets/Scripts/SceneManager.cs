using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour
{
    private SceneManager gameManager;
    //public static SceneManager instance = null;
    void Awake()
    {
       /* if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        */
        DontDestroyOnLoad(gameObject);

    }

    public void cambiarEscena(string nombreEscena)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscena);
    }
}
