using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionManager : MonoBehaviour
{
    // Asigna estos paneles desde el Inspector de Unity
    public GameObject paso1Panel;
    public GameObject paso2Panel;
    public GameObject paso3Panel;

    // **NUEVA VARIABLE:** Asigna aquí el GameObject de tu botón 'Ir a Menú'
    public GameObject backToMenuButton;

    private int currentStep = 1;

    void Start()
    {
        // Asegura que solo el Paso 1 esté visible y el botón de menú OCULTO al inicio
        ShowStep(1);
    }

    public void GoToNextStep()
    {
        // ... (el código de GoToNextStep se mantiene igual)
        currentStep++;

        if (currentStep > 3)
        {
            currentStep = 3; // Nos aseguramos de no pasar de 3
            return;
        }

        ShowStep(currentStep);
    }

    private void ShowStep(int step)
    {
        // Desactiva todos los paneles
        paso1Panel.SetActive(false);
        paso2Panel.SetActive(false);
        paso3Panel.SetActive(false);

        // **NUEVO:** Desactiva el botón de menú por defecto
        backToMenuButton.SetActive(false);

        // Activa el panel correspondiente
        switch (step)
        {
            case 1:
                paso1Panel.SetActive(true);
                break;
            case 2:
                paso2Panel.SetActive(true);
                break;
            case 3:
                paso3Panel.SetActive(true);
                // **NUEVO:** ¡Activa el botón al llegar al Paso 3!
                backToMenuButton.SetActive(true);
                break;
        }
    }

    // **NUEVO MÉTODO:** Esta función es la que llama el botón
    public void BackToMainMenu()
    {

        SceneManager.LoadScene("Menu"); 

    }
}
