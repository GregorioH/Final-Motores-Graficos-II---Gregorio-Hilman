using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Posicion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("Posicion X") + 0.5f, PlayerPrefs.GetFloat("Posicion Y"));
    }

    public void GuardarPosicion()
    {
        PlayerPrefs.SetFloat("Posicion X", transform.position.x);
        PlayerPrefs.SetFloat("Posicion Y", transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            GuardarPosicion();
            SceneManager.LoadScene("Batalla");
        }
    }
}
