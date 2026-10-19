using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainmenuscript : MonoBehaviour
{  
    public Image controlsscreen;
    public Image settingsscreen;
    public GameObject backbutton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controlsscreen.enabled = false;
        settingsscreen.enabled = false;
        backbutton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startgame(){

    }

    public void controls(){
        controlsscreen.enabled = true;
        backbutton.SetActive(true);
    }

    public void settings(){
        settingsscreen.enabled = true;
        backbutton.SetActive(true);
    }

    public void back(){
        controlsscreen.enabled = false;
        settingsscreen.enabled = false;
        backbutton.SetActive(false);
    }
}
