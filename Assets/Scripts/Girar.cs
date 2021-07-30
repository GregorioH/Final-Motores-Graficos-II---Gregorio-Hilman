using UnityEngine;

public class Girar : MonoBehaviour
{
    public float velocidadRotacion;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, velocidadRotacion);
    }
}
