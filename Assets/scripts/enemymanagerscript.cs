using UnityEngine;

public class enemymanagerscript : MonoBehaviour
{
    public float left;
    public float right;
    public GameObject pizzabox;
    public GameObject balloons;
    GameObject player;
    public int roomnumber;
    int prevmess;
    public int mess;
    int prevcleaned;
    public int cleaned;
    public  int spawned = 0;
    int spawncooldown = 100;
    public bool recalculatelocalmess = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        mess = player.GetComponent<playermovement>().mess[roomnumber];
        prevmess = mess;
        cleaned = player.GetComponent<playermovement>().cleaned[roomnumber];
        prevcleaned = cleaned;
        player.GetComponent<playermovement>().room = roomnumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if(cleaned != prevcleaned){
            player.GetComponent<playermovement>().cleaned[roomnumber] = cleaned;
            prevcleaned = cleaned;
            recalculatelocalmess = true;
        }
        if(mess > cleaned + spawned){
            if(Random.value > 0.97 || spawncooldown == 0){
                //Debug.Log("Spawned");
                spawncooldown = 100;
                spawned++;
                GameObject instance;
                float position = (Random.value * (right - left)) + left;
                while(12 >Mathf.Abs(position - player.transform.position.x)){
                    position = (Random.value * (right - left)) + left;
                }
                if(Random.value > 0.5){
                    instance = Instantiate(balloons);
                    instance.transform.position = new Vector2(position, 0);
                }
                else{
                    instance = Instantiate(pizzabox);
                    instance.transform.position = new Vector2(position, 0);
                }
            }
        }  
        else{
            spawncooldown--;
        }
    }

    public void defeated(){
        spawned--;
        cleaned++;
        player.GetComponent<playermovement>().cleanedsum++;
    }
}
