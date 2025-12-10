using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] faces;
    public Sprite back;
    public int faceIndex;
    public GameObject particleEffectPrefab;

    public bool isMatched = false;

    // Cambiamos a la referencia del nuevo manager
    private SimpleMemoramaManager gameManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Falta el SpriteRenderer en el prefab Card.");
            return;
        }
        spriteRenderer.sprite = back;

        // Obtener el nuevo manager de la escena
        gameManager = FindObjectOfType<SimpleMemoramaManager>();
    }

    // Inicializa la carta 
    public void InitializeCard(int index, Sprite[] faceSprites, Sprite backSprite)
    {
        // ... (el código de inicialización se mantiene igual)
        if (faceSprites.Length == 0)
        {
            Debug.LogError("El array de caras está vacío.");
            return;
        }

        faceIndex = Mathf.Clamp(index, 0, faceSprites.Length - 1);
        faces = faceSprites;
        back = backSprite;
        spriteRenderer.sprite = back;
    }

    // Se llama cuando la carta es clickeada
    private void OnMouseDown()
    {
        if (isMatched || gameManager == null)
            return;

        // Notifica al nuevo manager
        gameManager.CardFlipped(this);
    }

    // Muestra la cara de la carta
    public void ShowFace()
    {
        // Asegúrate de que faceIndex esté dentro del rango del array faces
        if (faceIndex >= 0 && faceIndex < faces.Length)
        {
            spriteRenderer.sprite = faces[faceIndex];
        }
        else
        {
            Debug.LogError("El índice faceIndex está fuera de los límites del array faces.");
        }
    }

    // Muestra el reverso de la carta
    public void ShowBack()
    {
        spriteRenderer.sprite = back;
    }

    // Marca la carta como emparejada y muestra un efecto de partículas
    public void SetMatched()
    {
        isMatched = true;
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject, 0.5f); // Destruye la carta después de un breve delay
    }

    // Reinicia la carta a su estado inicial (con el reverso visible)
    public void ResetCard()
    {
        isMatched = false; // Reinicia el estado de emparejada
        spriteRenderer.sprite = back; // Muestra el reverso
    }

    // Para reiniciar la carta con un nuevo índice aleatorio
    public void ResetCardContent()
    {
        isMatched = false; // Reinicia el estado de "emparejada"
        ShowBack(); // Muestra el reverso de la carta (oculta la cara)

        // Asignamos un nuevo índice aleatorio para la cara de la carta
        if (faces.Length > 0) // Asegúrate de que el array faces no esté vacío
        {
            int newIndex = Random.Range(0, faces.Length); // Obtén un índice aleatorio de las caras
            faceIndex = newIndex;

            // Actualiza la imagen de la carta con la nueva cara asignada
            spriteRenderer.sprite = faces[faceIndex];
        }
        else
        {
            Debug.LogError("El array faces está vacío.");
        }
    }
}



