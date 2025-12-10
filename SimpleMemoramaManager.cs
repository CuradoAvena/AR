using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMemoramaManager : MonoBehaviour
{
    private List<Card> cardsFlipped = new List<Card>();
    private bool isChecking = false;

    // Llamado por el script Card.cs cuando el usuario hace clic
    public void CardFlipped(Card card)
    {
        if (isChecking || card.isMatched || cardsFlipped.Contains(card))
            return;

        card.ShowFace();
        cardsFlipped.Add(card);

        if (cardsFlipped.Count == 2)
        {
            isChecking = true;
            StartCoroutine(CheckMatch(cardsFlipped[0], cardsFlipped[1]));
        }
    }

    // Corrutina para esperar antes de voltear de nuevo
    IEnumerator CheckMatch(Card card1, Card card2)
    {
        // Esperar 1 segundo para que el usuario vea la carta
        yield return new WaitForSeconds(1.0f);

        if (card1.faceIndex == card2.faceIndex)
        {
            Debug.Log("¡PAR ENCONTRADO! ID: " + card1.faceIndex);
            card1.SetMatched();
            card2.SetMatched();
            // Lógica de fin de juego (mostrar mensaje o volver a escena AR)
        }
        else
        {
            Debug.Log("No coinciden. Volteando de nuevo.");
            card1.ShowBack();
            card2.ShowBack();
        }

        cardsFlipped.Clear();
        isChecking = false;
    }
}
