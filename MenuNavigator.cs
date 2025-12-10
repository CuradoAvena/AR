using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigator : MonoBehaviour
{

    public void CargarEscena(string nombreDeEscena)
    {
        
        SceneManager.LoadScene(nombreDeEscena);
    }

    public void SalirDeLaApp()
    {
        Application.Quit();

#if UNITY_EDITOR
        Debug.Log("¡Has presionado SALIR! (Esto no funciona en el editor)");
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
