using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Seleccion de Nivel");
    }

    public void Reiniciar_Datos()
    {
        Cursor.visible = true;

        PlayerPrefs.SetFloat("Posicion X", -1.5f);
        PlayerPrefs.SetFloat("Posicion Y", 0.22f);

        PlayerPrefs.SetInt("Cantidad Vidas", 5);

        PlayerPrefs.SetInt("Vida", 150);
        PlayerPrefs.SetInt("Ataque", 40);
        PlayerPrefs.SetInt("Defensa", 25);
        PlayerPrefs.SetInt("DefensaPreCalculo", PlayerPrefs.GetInt("Defensa"));

        PlayerPrefs.SetInt("VidaE", 250);
        PlayerPrefs.SetInt("AtaqueE", 35);
        PlayerPrefs.SetInt("DefensaE", 15);

        PlayerPrefs.SetString("Enemigo", "Vivo");

        PlayerPrefs.SetInt("Nivel desbloqueado", 1);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Nivel1()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void Nivel2()
    {
        SceneManager.LoadScene("Nivel 2");
    }

    public void Nivel3()
    {
        SceneManager.LoadScene("Nivel 3");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
