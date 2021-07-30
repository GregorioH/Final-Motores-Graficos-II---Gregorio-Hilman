using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoJugador : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float velMovimiento = 40f;

    float movHorizontal = 0f;

    public bool salto = false;
    public bool agachar = false;
    public bool victoria = false;

    // Update is called once per frame
    void Update()
    {
        movHorizontal = Input.GetAxisRaw("Horizontal") * velMovimiento;

        animator.SetFloat("Velocidad", Mathf.Abs(movHorizontal));

        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
            animator.SetBool("Saltando", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            agachar = true;
            animator.Play("Jugador_Agachado");
            animator.SetBool("Agachado", true);
        } 
        else if (Input.GetButtonUp("Crouch"))
        {
            agachar = false;
            animator.SetBool("Agachado", false);
        }

        if (victoria == true)
        {
            animator.Play("Jugador_Victoria");
        }
    }

    private void FixedUpdate()
    {
        controller.Move(movHorizontal * Time.fixedDeltaTime, agachar, salto);
        salto = false;
    }

    public void TocaPiso()
    {
        animator.SetBool("Saltando", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            SceneManager.LoadScene("Batalla");
        }

        if (collision.gameObject.tag == "Reiniciar")
        {
            PlayerPrefs.SetInt("Cantidad Vidas", PlayerPrefs.GetInt("Cantidad Vidas") - 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
