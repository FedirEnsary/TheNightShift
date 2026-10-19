using UnityEngine;

public class sideexittriggerscript : MonoBehaviour
{
    public float x;
    public float y;
    public float direction = 1;
    public string room;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player"){
            GameObject.Find("Scenemanager").GetComponent<Scenemanager>().RoomScene(room, x, y, direction);
        }
    }
}
