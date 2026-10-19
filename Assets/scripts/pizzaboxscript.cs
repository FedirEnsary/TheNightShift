using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pizzaboxscript : MonoBehaviour
{
    
    GameObject manager;
    GameObject player;
    Animator sprite;
    bool idle = true;
    float distance;
    public int health = 2;
    public int hitcooldown = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.Find("enemymanager");
        player = GameObject.Find("Player");
        sprite = transform.GetComponent<Animator>();
        transform.GetComponent<Rigidbody2D>().linearDamping = 3f;
        transform.position = new Vector2(transform.position.x, 0.135f);
    }

    // Update is called once per frame
    void Update()
    {
        if(hitcooldown > 0){
            hitcooldown--;
        }
        if(idle){
            distance = Vector3.Distance(player.transform.position, transform.position);
            if(distance < 7){
                idle = !idle;
                sprite.CrossFade("pizzaboxattack", 0.2f);
                transform.GetComponent<Rigidbody2D>().AddForceY(60f);
            }
        }
        else{
            transform.GetComponent<Rigidbody2D>().AddForceY(-10f);
            distance = player.transform.position.x -  transform.position.x;
            if(distance > 0){
                transform.GetComponent<Rigidbody2D>().AddForceX(10f);
                transform.localScale = new Vector3(-7, 7, 1);
            }
            else{
                transform.GetComponent<Rigidbody2D>().AddForceX(-10f);
                transform.localScale = new Vector3(7, 7, 1);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player"){
            if(transform.localScale.x > 0){
                //Debug.Log("Pushed Left");
                transform.GetComponent<Rigidbody2D>().AddForceX(1000f);
                player.GetComponent<playermovement>().gethit(10, 1);
            }
            else{
                //Debug.Log("Pushed Right");
                transform.GetComponent<Rigidbody2D>().AddForceX(-1000f);
                player.GetComponent<playermovement>().gethit(10, -1);
            }
        }
    }

    public void gethit( int damage){
        if(hitcooldown == 0){
            hitcooldown = 20;
            health -= damage;
            if(health <= 0){
                manager.GetComponent<enemymanagerscript>().defeated();
                Destroy (gameObject, 0);
            }
        }
    }
}
