using UnityEngine;

public class sideexitscript : MonoBehaviour
{
    SpriteRenderer sprite;
    Color active = new Color(1f, 1f, 0.5f, 0.5f);
    Color nonactive = new Color(1f, 1f, 0.5f, 0.0f);
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
        sprite.color = active;
        //Debug.Log("enter sideexit");
    }

    private void OnTriggerExit2D(Collider2D other) {
        sprite.color = nonactive;
    }
}
