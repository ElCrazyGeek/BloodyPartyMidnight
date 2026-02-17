using Unity.Loading;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pausar();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReiniciarJuego();
        }
    }

    void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Pausar()
    {
		if(Time.timeScale == 1){   
			 Time.timeScale = 0; 	
		} else if(Time.timeScale == 0) {   
			 Time.timeScale = 1;  			}
    }
}
