using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueScript : MonoBehaviour
{
public TextMeshProUGUI dialogos;
    
    public string [] lineas;

    public float textvel = 0.2f;

    int index; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogos.text = string.Empty;
        startDia();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SiguienteLinea();
        }
        else
        {
            StopAllCoroutines();
            dialogos.text = lineas[index]; 
        }
    }

    public void startDia()
    {
        index = 0;
        
        StartCoroutine(EscribirLinea());
    }

    IEnumerator EscribirLinea(){
    foreach (char letras in lineas[index].ToCharArray())
        {
            dialogos.text += letras; 
            yield return new WaitForSeconds(textvel); 
        }
    }

    public void SiguienteLinea()
    {
        if(index <  lineas.Length - 1)
        {
            index++;
            dialogos.text = string.Empty;
            StartCoroutine(EscribirLinea());
        }
        else
        {
            gameObject.SetActive(false); 
        }

    }
}
