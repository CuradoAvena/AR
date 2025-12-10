using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleColor : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // FUNCIÓN PÚBLICA LLAMADA POR EL TOUCHMANAGER
    public void SetNewRandomColors()
    {
        if (ps == null) return;

        // 1. Acceder al módulo principal
        var main = ps.main;

        // 2. Obtener la estructura del gradiente existente (es crucial obtenerla y luego reasignarla)
        ParticleSystem.MinMaxGradient gradient = main.startColor;

        // 3. Crear dos colores aleatorios diferentes (para colorMin y colorMax)
        Color colorA = new Color(Random.value, Random.value, Random.value);
        Color colorB = new Color(Random.value, Random.value, Random.value);

        // 4. Modificar los valores colorMin y colorMax del gradiente existente
        // Esto funciona porque el modo ya está seteado a "Random Between Two Colors" en el Inspector.
        gradient.colorMin = colorA;
        gradient.colorMax = colorB;

        // 5. Asignar la estructura modificada de vuelta al Start Color
        main.startColor = gradient;

        // Opcional: Reiniciar la reproducción para que los cambios se vean inmediatamente.
        ps.Stop();
        ps.Play();
    }


}
