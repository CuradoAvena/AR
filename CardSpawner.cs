using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab de la carta (con script Card.cs)
    public Sprite[] faces;        // Array de imágenes para las caras (4 sprites mínimos)
    public Sprite back;           // Sprite para el reverso
    public Transform cardContainer; // Contenedor UNICO para las cartas

    // Hemos eliminado la dependencia de GameControl1.

    private void Start()
    {
        // Requerimos 4 imágenes para 4 pares = 8 cartas
        if (faces.Length < 4)
        {
            Debug.LogError("Se necesitan al menos 4 imágenes (sprites) para jugar.");
            return;
        }

        SpawnCardsFixed(4); // Lanza el spawn de 4 pares (8 cartas)
    }

    public void SpawnCardsFixed(int numPairs)
    {
        int cardCount = numPairs * 2;
        int groupSize = 2; // Pares de 2

        if (cardContainer == null)
        {
            Debug.LogError("¡El slot 'Card Container' está vacío! Arrastra el objeto que contendrá las cartas.");
            return;
        }

        // Configuramos la grilla para 2 filas y 4 columnas
        int rows = 2;
        int columns = numPairs; // 4 columnas

        // 1. Generar índices y barajear
        List<int> cardIndices = GenerateCardIndices(cardCount, groupSize);
        ShuffleList(cardIndices);

        float xOffset = 2.5f; // Espaciado horizontal
        float yOffset = 3.0f; // Espaciado vertical

        float startX = -(columns - 1) * xOffset / 2;
        float startY = (rows - 1) * yOffset / 2;

        for (int i = 0; i < cardCount; i++)
        {
            // Instanciar la carta en el contenedor
            GameObject card = Instantiate(cardPrefab, cardContainer);
            int x = i % columns;
            int y = i / columns;

            // Asignar posición en 3D (IMPORTANTE: Esto NO es UI, es World Space)
            card.transform.localPosition = new Vector3(startX + x * xOffset, startY - y * yOffset, 0f);

            // 2. Inicializar el script de la carta
            Card cardScript = card.GetComponent<Card>();
            if (cardScript != null)
            {
                // Solo usamos las primeras 'numPairs' caras de la lista 'faces'
                cardScript.InitializeCard(cardIndices[i], faces, back);
            }
        }
    }

    private List<int> GenerateCardIndices(int cardCount, int groupSize)
    {
        List<int> indices = new List<int>();
        int groupCount = cardCount / groupSize;
        for (int i = 0; i < groupCount; i++)
        {
            for (int j = 0; j < groupSize; j++)
            {
                indices.Add(i);
            }
        }
        return indices;
    }

    private void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
