using UnityEngine;
using UnityEngine.UI;

public class Botones : MonoBehaviour
{
    //1. Declaración de variables
    private SceneManager gameManager;
    //Referencias a los botones (public para poder asignarlas en la Inspector window)
    public Button botonJugar, botonPersonajes, botonCreditos, botonSalir;
    // Start is called before the first frame update
    void Start()
    {
        //2. Busco y asocio mi script de GameManager
        gameManager = FindFirstObjectByType<SceneManager>();
        
        //3. Acciones de cada botón
        
        //Botón inicio (si existe)
        if (botonJugar)
        {
            //Le añado la acción a ejecutar (cambiar a la escena de Inicio)
            botonJugar.GetComponent<Button>().onClick.AddListener(() => gameManager.cambiarEscena("Escena1_Cementerio"));
        }
                
        //Botón opciones (si existe)
        if (botonPersonajes)
        {
            //Le añado la acción a ejecutar (cambiar a la escena de Opciones)
            botonPersonajes.GetComponent<Button>().onClick.AddListener(() => gameManager.cambiarEscena("Escena_Personajes"));
        }
        
        //Botón creditos (si existe)
        if (botonCreditos)
        {
            //Le añado la acción a ejecutar (cambiar a la escena de Creditos)
            botonCreditos.GetComponent<Button>().onClick.AddListener(() => gameManager.cambiarEscena("Escena_Creditos"));
        }
        
        //Botón salir (si existe)
        if (botonSalir)
        {
            //Le añado la acción a ejecutar (salir del juego)
            //Este botón no funcionará en el Editor de Unity, pero sí al hacer la Build
            botonSalir.GetComponent<Button>().onClick.AddListener(() => Application.Quit());
        }
    }

}