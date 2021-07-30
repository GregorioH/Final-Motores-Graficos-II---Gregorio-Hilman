using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Combate : MonoBehaviour
{
    public CanvasGroup Acciones;

    public Animator animatorJugador;
    public Animator animatorEnemigo;

    public Text VidaTxt;
    public Text AtaqueTxt;
    public Text DefensaTxt;

    public Text VidaEnemigoTxt;
    public Text AtaqueEnemigoTxt;
    public Text DefensaEnemigoTxt;
    public Text Narrador;

    private int vida;
    private int ataque;
    private int defensa;
    private int defensaPreCalculo;

    private int vidaEnemigo;
    private int ataqueEnemigo;
    private int defensaEnemigo;
    void Start()
    {
        Cursor.visible = true;

        vida = PlayerPrefs.GetInt("Vida");
        ataque = PlayerPrefs.GetInt("Ataque");
        defensa = PlayerPrefs.GetInt("Defensa");
        defensaPreCalculo = PlayerPrefs.GetInt("DefensaPreCalculo");

        vidaEnemigo = PlayerPrefs.GetInt("VidaE");
        ataqueEnemigo = PlayerPrefs.GetInt("AtaqueE");
        defensaEnemigo = PlayerPrefs.GetInt("DefensaE");

        VidaTxt.text = "Vida: " + vida.ToString();
        AtaqueTxt.text = "Ataque: " + ataque.ToString();
        DefensaTxt.text = "Defensa: " + defensa.ToString();

        VidaEnemigoTxt.text = "Vida: " + vidaEnemigo.ToString();
        AtaqueEnemigoTxt.text = "Ataque: " + ataqueEnemigo.ToString();
        DefensaEnemigoTxt.text = "Defensa: " + defensaEnemigo.ToString();

        defensaPreCalculo = defensa;

        Narrador.text = "Tu Turno";
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {
            StartCoroutine(GameOver());
        }

        if (vidaEnemigo <= 0)
        {
            StartCoroutine(Victoria());
        }
    }

    public void Atacar()
    {
        Acciones.interactable = false;

        animatorJugador.Play("Jugador_Ataque");

        Narrador.text = "Has atacado, hiciste " + (ataque - defensaEnemigo) + " de daño";

        vidaEnemigo -= ataque - defensaEnemigo;
        VidaEnemigoTxt.text = "Vida: " + vidaEnemigo.ToString();

        StartCoroutine(Turnos());
    }

    public void Defender()
    {
        Acciones.interactable = false;

        animatorJugador.SetBool("Turno Enemigo", true);
        animatorJugador.Play("Jugador_Defensa");

        Narrador.text = "Te defendiste, defensa +50%";

        defensa += Mathf.RoundToInt(defensa * 0.50f);
        DefensaTxt.text = "Defensa: " + defensa.ToString();

        StartCoroutine(Turnos());
    }

    public void Huir()
    {
        StartCoroutine(Huida());
    }

    private void TurnoEnemigo()
    {
        switch(Random.Range(0, 9))
        {
            // Observar
            case 0:
                Narrador.text = "El enemigo te observa...";
                break;
            // Aumentar Ataque
            case 1: case 2:
                if (ataqueEnemigo < 45)
                {
                    animatorEnemigo.SetBool("Turno Jugador", false);
                    animatorEnemigo.Play("Enemigo_Aumento_Estadisticas");

                    Narrador.text = "El enemigo ha aumentado su ataque";
                    ataqueEnemigo += 5;
                    AtaqueEnemigoTxt.text = "Ataque: " + ataqueEnemigo.ToString();
                }
                // En caso de tener la ataque alto
                else
                {
                    animatorEnemigo.Play("Enemigo_Aumento_Estadisticas");

                    Narrador.text = "¡El enemigo trato de aumentar su ataque, pero fallo!";
                }
                break;
            // Aumentar Defensa
            case 3: case 4:
                if (defensaEnemigo < 25)
                {
                    animatorEnemigo.Play("Enemigo_Aumento_Estadisticas");

                    Narrador.text = "El enemigo ha aumentado su defensa";
                    defensaEnemigo += 5;
                    DefensaEnemigoTxt.text = "Defensa: " + defensaEnemigo.ToString();
                } 
                // En caso de tener la defensa alta
                else
                {
                    animatorEnemigo.Play("Enemigo_Aumento_Estadisticas");

                    Narrador.text = "¡El enemigo trato de aumentar su defensa, pero fallo!";
                }
                break;
            //Atacar
            default:
                if (defensa >= ataqueEnemigo)
                {
                    animatorEnemigo.Play("Enemigo_Ataque");

                    Narrador.text = "El enemigo te ataco, no recibiste daño";
                }
                else
                {
                    animatorEnemigo.Play("Enemigo_Ataque");

                    Narrador.text = "El enemigo te ataco, recibiste " + (ataqueEnemigo - defensa) + " de daño";
                    vida -= ataqueEnemigo - defensa;
                    VidaTxt.text = "Vida: " + vida.ToString();
                }
                break;
        }
    }

    IEnumerator Turnos()
    {
        yield return new WaitForSeconds(2);

        Narrador.text = "Turno Enemigo";

        yield return new WaitForSeconds(2);

        TurnoEnemigo();

        yield return new WaitForSeconds(2);

        animatorJugador.SetBool("Turno Enemigo", false);
        animatorEnemigo.SetBool("Turno Jugador", true);

        defensa = defensaPreCalculo;
        DefensaTxt.text = "Defensa: " + defensa.ToString();

        Narrador.text = "Tu Turno";

        Acciones.interactable = true;
    }

    IEnumerator GameOver()
    {
        Acciones.interactable = false;

        animatorJugador.Play("Jugador_Muerte");

        Narrador.text = "Game Over";

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Menu");
    }

    IEnumerator Huida()
    {
        Acciones.interactable = false;

        Narrador.text = "Has huido";

        PlayerPrefs.SetInt("Vida", vida);
        PlayerPrefs.SetInt("Ataque", ataque);
        PlayerPrefs.SetInt("Defensa", defensa);
        PlayerPrefs.SetInt("DefensaPreCalculo", defensaPreCalculo);

        PlayerPrefs.SetInt("VidaE", vidaEnemigo);
        PlayerPrefs.SetInt("AtaqueE", ataqueEnemigo);
        PlayerPrefs.SetInt("DefensaE", defensaEnemigo);

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Nivel 3");
    }

    IEnumerator Victoria()
    {
        Acciones.interactable = false;

        animatorJugador.Play("Jugador_Victoria");
        animatorEnemigo.Play("Enemigo_Muerte");

        PlayerPrefs.SetString("Enemigo", "Muerto");

        Narrador.text = "Has ganado";

        yield return new WaitForSeconds(2);

        Cursor.visible = false;

        SceneManager.LoadScene("Nivel 3");
    }
}
