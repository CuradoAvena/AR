using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para manipular la imagen de la receta
using UnityEngine.SceneManagement; // Para el botón de regreso

public class RecipeTouchManager : MonoBehaviour
{
    [Header("Conexiones de UI")]
    public GameObject recipePanel; // El panel completo de pantalla (oculto)
    public Image recipeDisplayImage; // El componente Image dentro del panel

    private GameObject currentActiveTarget = null;

    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main;
        if (recipePanel != null)
            recipePanel.SetActive(false); // Ocultar el panel al inicio
    }

    void Update()
    {
        // Lógica de Raycasting (Se asume que la detección clic/toque ya está implementada)
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) HandleRecetaInteraction(Input.mousePosition);
#endif
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) HandleRecetaInteraction(Input.GetTouch(0).position);
    }

    void HandleRecetaInteraction(Vector3 screenPosition)
    {
        Ray rayo = arCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit))
        {
            InfoDisplay info = hit.collider.GetComponent<InfoDisplay>();

            if (info != null)
            {
                // 1. GUARDAR LA REFERENCIA AL OBJETO PADRE (el ImageTarget)
                // Usamos el padre porque al desactivarlo, se oculta el modelo 3D completo.
                currentActiveTarget = hit.collider.transform.parent.gameObject;

                // 2. OCULTAR MODELO: Desactivamos el objeto AR para que no atraviese el Canvas.
                currentActiveTarget.SetActive(false);

                // 3. MOSTRAR PANEL: Lógica de UI (Se mantiene igual)
                Sprite recipeSprite = info.recipeImage;
                if (recipeDisplayImage != null && recipePanel != null && recipeSprite != null)
                {
                    recipeDisplayImage.sprite = recipeSprite;
                    recipePanel.SetActive(true);
                }
            }
        }
    }

    // Función pública para el botón de cierre del panel de recetas
    public void CloseRecipePanel()
    {
        // 1. OCULTAR PANEL UI
        recipePanel.SetActive(false);

        // 2. MOSTRAR MODELO AR DE NUEVO (Activamos el ImageTarget que guardamos)
        if (currentActiveTarget != null)
        {
            currentActiveTarget.SetActive(true);
            currentActiveTarget = null; // Limpiar la referencia
        }
    }

    // Función pública para el botón de Regresar a la escena AR principal
    public void BackToARScene(string sceneName = "Escena_AR")
    {
        SceneManager.LoadScene(sceneName);
    }
}
