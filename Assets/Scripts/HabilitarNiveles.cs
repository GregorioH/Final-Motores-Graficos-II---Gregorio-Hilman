using UnityEngine;
using UnityEngine.UI;

public class HabilitarNiveles : MonoBehaviour
{
    [SerializeField]
    private Button nivel1;
    [SerializeField]
    private Button nivel2;
    [SerializeField]
    private Button nivel3;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Nivel desbloqueado") == 1)
        {
            nivel1.interactable = true;
            nivel2.interactable = false;
            nivel3.interactable = false;
        } 
        else if (PlayerPrefs.GetInt("Nivel desbloqueado") == 2)
        {
            nivel1.interactable = false;
            nivel2.interactable = true;
            nivel3.interactable = false;
        } 
        else if (PlayerPrefs.GetInt("Nivel desbloqueado") == 3)
        {
            nivel1.interactable = false;
            nivel2.interactable = false;
            nivel3.interactable = true;
        }
    }
}
