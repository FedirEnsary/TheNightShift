using UnityEngine;

public class balloonscript : MonoBehaviour
{
    GameObject manager;
    GameObject player;
    Animator sprite;
    bool idle = true;
    float distance;
    public int health = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.Find("enemymanager");
        player = GameObject.Find("Player");
        sprite = transform.GetComponent<Animator>();
        transform.GetComponent<Rigidbody2D>().linearDamping = 2f;
        if(0 < player.transform.position.x -  transform.position.x){
            transform.localScale = new Vector3(-7, 7, 1);
        }
        else{
            transform.localScale = new Vector3(7, 7, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){
            manager.GetComponent<enemymanagerscript>().defeated();;
            Destroy (gameObject, 0);
        }
        if(idle){
            distance = Vector3.Distance(player.transform.position, transform.position);
            if(distance < 9){
                idle = !idle;
                sprite.CrossFade("balloonattack", 0.2f);
            }
        }
        else{
            distance = player.transform.position.x -  transform.position.x;
            if(distance > 0){
                transform.GetComponent<Rigidbody2D>().AddForceX(5f);
                transform.localScale = new Vector3(-7, 7, 1);
            }
            else{
                transform.GetComponent<Rigidbody2D>().AddForceX(-5f);
                transform.localScale = new Vector3(7, 7, 1);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player"){
            if(transform.localScale.x > 0){
                //Debug.Log("Pushed Left");
                transform.GetComponent<Rigidbody2D>().AddForceX(1000f);
                player.GetComponent<playermovement>().gethit(4, 1);
            }
            else{
                //Debug.Log("Pushed Right");
                transform.GetComponent<Rigidbody2D>().AddForceX(-1000f);
                player.GetComponent<playermovement>().gethit(4, -1);
            }
        }
    }
}
