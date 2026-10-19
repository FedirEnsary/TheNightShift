using UnityEngine;

public class backexitscript : MonoBehaviour
{
    public float x;
    public float y;
    public float direction = 1;
    public string room;

    SpriteRenderer sprite;
    Color active = new Color(1f, 1f, 0.5f, 0.5f);
    Color nonactive = new Color(1f, 1f, 0.5f, 0.0f);
    //bool isinteractable = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = transform.GetComponent<SpriteRenderer>();
        sprite.color = nonactive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player"){
            //Debug.Log("enter backexit");
            sprite.color = active;
            //isinteractable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.name == "Player"){
            sprite.color = nonactive;
            //isinteractable = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.name == "Player"){
            if(other.GetComponent<playermovement>().interact){
                GameObject.Find("Scenemanager").GetComponent<Scenemanager>().RoomScene(room, x, y, direction);
            }
        }
    }
}
