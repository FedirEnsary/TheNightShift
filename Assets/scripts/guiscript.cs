using UnityEngine;
using TMPro;

public class guiscript : MonoBehaviour
{
    GameObject player;
    GameObject enemymanager;
    public TMP_Text local;
    float completion = 0;
    string localmess = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        enemymanager = GameObject.Find("enemymanager");
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(4).localScale = new Vector2(Mathf.Max(0, player.GetComponent<playermovement>().health)/100f ,1);
        completion = (float)(player.GetComponent<playermovement>().cleanedsum) / (float)(player.GetComponent<playermovement>().messsum);
        transform.GetChild(5).localScale = new Vector2(completion ,1);
        if(enemymanager.GetComponent<enemymanagerscript>().recalculatelocalmess){
            recalculatelocal();
        }
    }

    void recalculatelocal(){
        int total = enemymanager.GetComponent<enemymanagerscript>().mess - enemymanager.GetComponent<enemymanagerscript>().cleaned;
        localmess = "";
        for(int i = 0; i < total; i++){
            localmess = localmess + "█ ";
        }
        local.text = localmess;
        enemymanager.GetComponent<enemymanagerscript>().recalculatelocalmess = false;
    }
}
