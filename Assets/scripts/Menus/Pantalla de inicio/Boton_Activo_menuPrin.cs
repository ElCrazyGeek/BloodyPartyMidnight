using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Boton_Activo_menuPrin : MonoBehaviour
{
    [Header("Pulso de boton")]
    public float escalaMax = 1.02f;
    public float escalaMin = 0.98f;
    public float duracionPulso = 1.2f;

    [Header("Pulso de luz")]
    public Image imagenBoton;
    public Color colorMin = new Color(0.5f, 0f, 0f, 1f);
    public Color colorMax = new Color(1f, 0.1f, 0.1f, 1f);

    void Start()
    {
        transform.DOScale(escalaMax, duracionPulso)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        imagenBoton.DOColor(colorMax, duracionPulso)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .From(colorMin);

            
    }

    
}