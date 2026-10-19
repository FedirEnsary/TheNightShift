using UnityEngine;

public class attackscript : MonoBehaviour
{
    bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(kill), 0.24f);
        Invoke(nameof(activate), 0.21f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(active){
            //Debug.Log("hit " + other.name);
            if(other.name == "pizzabox(Clone)"){
                if(other.transform.localScale.x > 0){
                    //Debug.Log("Pushed Left");
                    other.transform.GetComponent<Rigidbody2D>().AddForceX(200f);
                }
                else{
                    //Debug.Log("Pushed Right");
                    other.transform.GetComponent<Rigidbody2D>().AddForceX(-200f);
                }
                other.GetComponentInParent<pizzaboxscript>().gethit(1);
            }
            else if(other.name == "balloons(Clone)"){
                other.GetComponentInParent<balloonscript>().health--;
            }
        }
    }

    void kill(){
        Destroy (gameObject, 0);
    }

    void activate(){
        active = true;
    }
}
