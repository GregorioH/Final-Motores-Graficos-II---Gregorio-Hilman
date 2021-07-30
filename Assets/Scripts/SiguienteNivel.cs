using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SiguienteNivel : MonoBehaviour
{
    public Text Tesoro;
    public CharacterController2D charCon;
    public MovimientoJugador movJug;
    public CambioEscena CambioEsc;
    // Start is called before the first frame update
    void Start()
    {
        Tesoro.enabled = false;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Victoria());
        }
    }

    IEnumerator Victoria()
    {
        movJug.velMovimiento = 0;
        movJug.victoria = true;

        yield return new WaitForEndOfFrame();

        Tesoro.enabled = true;
        charCon.enabled = false;
        movJug.enabled = false;

        if (SceneManager.GetActiveScene().name == "Nivel 1")
        {
            PlayerPrefs.SetInt("Nivel desbloqueado", 2);
        }

        if (SceneManager.GetActiveScene().name == "Nivel 2")
        {
            PlayerPrefs.SetInt("Nivel desbloqueado", 3);
        }

        yield return new WaitForSeconds(2);

        if (PlayerPrefs.GetString("Enemigo") == "Muerto")
        {
            CambioEsc.Reiniciar_Datos();

            SceneManager.LoadScene("Menu");
        } 
        else
        {
            Cursor.visible = true;
            SceneManager.LoadScene("Seleccion de nivel");
        }
    }
}
