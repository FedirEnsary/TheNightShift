using UnityEngine.SceneManagement;
using UnityEngine;

public class Scenemanager : MonoBehaviour
{
    GameObject player;
    int cooldown = 0;
    void Start() 
    {
        DontDestroyOnLoad(this.gameObject);
        player = GameObject.Find("Player");
    }
        public void PauseScene()
    {
        SceneManager.LoadScene("Pause Menu");
    }

        public void MainMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
        RoomScene("Office", -14.5f, 0f, 1f);
        player.GetComponent<playermovement>().paused = false;
    }

        public void RoomScene(string roomName, float x, float y, float direction)
    {
        if(cooldown < 1){
            cooldown = 10;
            SceneManager.LoadScene(roomName);
            player.GetComponent<playermovement>().blackouttimer = 12f;
            player.GetComponent<playermovement>().prevstate = "idle";
            player.GetComponent<playermovement>().state = "idle";
            //player.GetComponent<playermovement>().sprite.CrossFade("idle", 0.025f);
            player.transform.position = new Vector3(x, y, 0);
            player.transform.localScale = new Vector3(direction, player.transform.localScale.y, player.transform.localScale.z);
        }
    }

    void Update(){
        cooldown--;
    }

    public void RestartGame(){
        Destroy(player, 0);
        SceneManager.LoadScene("Main Menu");
        Destroy(gameObject, 0);
    }

    public void LoseScene(){
        Destroy(player, 0);
        SceneManager.LoadScene("Lose");
        Invoke(nameof(RestartGame), 3);
    }

    public void WinScene(){
        Destroy(player, 0);
        SceneManager.LoadScene("Win");
        Invoke(nameof(RestartGame), 3);
    }
}
