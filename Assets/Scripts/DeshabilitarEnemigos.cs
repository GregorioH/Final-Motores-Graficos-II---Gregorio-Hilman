using UnityEngine;

public class DeshabilitarEnemigos : MonoBehaviour
{
    public GameObject Enemigo;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("Enemigo") == "Muerto")
        {
            Destroy(Enemigo);
        }
    }
}
