using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Vidas : MonoBehaviour
{
    public Text VidasText;
    public Text Tesoro;
    public Animator animator;

    public CharacterController2D charCon;
    public MovimientoJugador movJug;
    public CambioEscena cambioEsc;
    // Start is called before the first frame update
    void Start()
    {
        VidasText.text = "Vidas: " + PlayerPrefs.GetInt("Cantidad Vidas").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Cantidad Vidas") <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        charCon.enabled = false;
        movJug.enabled = false;

        animator.Play("Jugador_Muerte");

        Tesoro.enabled = true;
        Tesoro.text = "Perdiste...";

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Menu");
        cambioEsc.Reiniciar_Datos();
    }
}
