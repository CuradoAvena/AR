using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class TouchManager : MonoBehaviour
{
    // --------------------------------------------------------------------
    // CAMPOS PÚBLICOS PARA CONEXIÓN EN EL INSPECTOR
    // --------------------------------------------------------------------

    [Header("Mecánica A: Cargar Minijuego")]
    public string nombreEscenaMinijuego = "Escena_Minijuego";

    [Header("Mecánica B: Cargar Recetas (NUEVA)")]
    // ¡NUEVO CAMPO! El alumno debe escribir el nombre de la escena aquí.
    public string nombreEscenaRecetas = "Escena_Recetas";

    [Header("Mecánica C: Control de Luz Aleatorio")] // NUEVO CAMPO
    public LightRandom discoLightController;
    public ParticleSystem vfxAmbientePrincipal;
    public Animator dancerAnimator;     // Personaje que baila (Baila 1)
    public Animator fighter1Animator;   // Personaje que pelea (Pelea 1)
    public Animator fighter2Animator;   // Personaje que pelea (Pelea 2)
    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main;
       
    }

    void Update()
    {
        // ... (Lógica de Clic y Toque, la cual llama a HandleInteraction)
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) HandleInteraction(Input.mousePosition);
#endif
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) HandleInteraction(Input.GetTouch(0).position);
    }

    void HandleInteraction(Vector3 screenPosition)
    {
        Ray rayo = arCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit))
        {
            string tagTocado = hit.collider.CompareTag(hit.collider.tag) ? hit.collider.tag : "";

            // --- MECÁNICA A: Cargar Minijuego (Tag: Mecanica_A) ---
            if (tagTocado == "Mecanica_A")
            {
                SceneManager.LoadScene(nombreEscenaMinijuego);
            }

            // --- MECÁNICA B: Cargar Recetas (Tag: Mecanica_B) ---
            // ¡ESTE ES EL CAMBIO! Ahora carga la nueva escena de recetas.
            else if (tagTocado == "Mecanica_B")
            {
                Debug.Log("TOCADO: Mecánica B. Cargando Módulo de Recetas Multi-Target.");
                SceneManager.LoadScene(nombreEscenaRecetas);
            }

            // --- MECÁNICA C: Activar Efecto (Tag: Mecanica_C) ---
            else if (tagTocado == "Mecanica_C")
            {
                Debug.Log("TOCADO: Mecánica C. Activando efectos múltiples.");

                AudioSource[] audios = hit.collider.GetComponents<AudioSource>();
                foreach (AudioSource audio in audios)
                {
                    // Esto reproducirá todos los clips asignados al objeto
                    audio.Play();
                }

                // 3. ¡NUEVA LÓGICA! Controla el Sistema de Partículas Central
                if (vfxAmbientePrincipal != null)
                {
                    // Detenemos el sistema primero si ya estaba corriendo para que se reinicie
                    vfxAmbientePrincipal.Stop();
                    vfxAmbientePrincipal.Play();
                }
            }

            if (discoLightController != null)
                {
                    if (discoLightController.IsDiscoActive())
                    {
                        discoLightController.StopDisco(); // Detener luz
                    }
                    else
                    {
                        discoLightController.StartDisco(); // Iniciar luz
                    }
                }
            }
        // 1. BAILARÍN: Modo Toggle (Prende/Apaga el baile)
        if (dancerAnimator != null)
        {
            bool isDancing = dancerAnimator.GetBool("IsDancing");
            dancerAnimator.SetBool("IsDancing", !isDancing);
        }

        // 2. PELEADOR 1: Dispara el Trigger de la animación de ataque
        if (fighter1Animator != null)
        {
            fighter1Animator.SetTrigger("StartFight");
        }

        // 3. PELEADOR 2: Dispara el Trigger de la animación de ataque
        if (fighter2Animator != null)
        {
            fighter2Animator.SetTrigger("StartFight");
        }


    }
    }
