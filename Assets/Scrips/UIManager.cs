using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI puntuacion;
    public TextMeshProUGUI vidas;
    public GameObject GameOver;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        puntuacion.text = GameManager.instance.puntuacion.ToString();
        vidas.text = GameManager.instance.vidas.ToString();
        if(GameManager.instance.puntuacion == 15)
        {
            GameManager.instance.puntuacion -= 15;
            GameManager.instance.vidas += 1;
        }

        if (GameManager.instance.vidas == -1)
        {
            GameOver.SetActive(true);
        }
    }
}
