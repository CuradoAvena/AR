using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRandom : MonoBehaviour
{
    private Light _discolight;
    [SerializeField] private float changeInterval = 0.2f; // Intervalo rápido para el efecto

    private void Awake()
    {
        _discolight = GetComponent<Light>();
        // Aseguramos que la luz esté apagada al inicio
        _discolight.color = Color.black;
        _discolight.intensity = 0f;
    }

    private void ChangeLightColor()
    {
        // Cambia el color
        _discolight.color = new Color(r: Random.value, g: Random.value, b: Random.value);
        // Asegura que la luz esté encendida
        _discolight.intensity = 1.5f;
    }

    // -----------------------------------------------------------
    // FUNCIONES PÚBLICAS LLAMADAS POR EL TOUCHMANAGER
    // -----------------------------------------------------------

    public void StartDisco()
    {
        // Cancela cualquier Invoke previo y lo reinicia.
        CancelInvoke(nameof(ChangeLightColor));
        InvokeRepeating(nameof(ChangeLightColor), time: 0f, repeatRate: changeInterval);
    }

    public void StopDisco()
    {
        // Detiene el ciclo de cambio de color.
        CancelInvoke(nameof(ChangeLightColor));
        // Apaga la luz para completar el efecto.
        _discolight.color = Color.black;
        _discolight.intensity = 0f;
    }

    public bool IsDiscoActive()
    {
        return IsInvoking(nameof(ChangeLightColor));
    }
}
