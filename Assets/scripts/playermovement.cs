using UnityEngine;
using UnityEngine.InputSystem;

public class playermovement : MonoBehaviour
{
    public Animator sprite;
    Rigidbody2D rb;
    SpriteRenderer blackout;
    public GameObject broomup;
    public GameObject broomhit;
    public GameObject broomsweep;

    //Script interactable = null;
    //wether to display debug in console
    bool inputdebug = false;
    bool actiondebug = false;

    public string prevstate = "idle";
    public string state = "idle";

    //stats
    public int health = 100;
    public int healthmax = 100;
    public int room = 0;
    //mess room order: office ehall whall main stage kitchen closet shall abath bbath
    public int[] mess = {0, 3, 6, 12, 12, 8, 8, 8, 4, 6};
    public int[] cleaned = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    public int messsum = 0;
    public int cleanedsum = 0;


    //key press states 
    bool left = false; 
    bool right = false;
    bool up = false;
    bool down = false;
    bool jump = false;
    bool hit = false;
    public bool interact = false;
    bool menu = false;
    public bool paused = true;

    //speeds and cooldowns
    float hspeed = 10f;
    float hspeedbuff = 1f;
    int immunity = 30;
    int jumpcooldown = 0;
    int hitcooldown = 0;
    int damagecooldown = 0;
    public float blackouttimer = 0; 
    //int interactcooldown = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() 
    {
        rb = transform.GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0).GetComponent<Animator>();
        rb.freezeRotation = true;
        blackout = transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(this.gameObject);
        for(int i = 0; i < mess.Length; i ++){
            messsum += mess[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        if(cleanedsum == messsum){
            paused = true;
            GameObject.Find("Scenemanager").GetComponent<Scenemanager>().WinScene();
        }
        if(!paused){
            hspeedbuff = 1f;
            if(jumpcooldown>0){
                jumpcooldown--;
            }
            if(damagecooldown>0){
                damagecooldown--;
            }
            if(hitcooldown>0){
                hitcooldown--;
                if(jumpcooldown>0){
                    hspeedbuff = 0.25f;
                }
                else{
                    hspeedbuff = 0.125f;
                }
            }
            if(jumpcooldown>0){
                jumpcooldown--;
            }
            rb.AddForceY(-30f);

            state = "idle";
            if(left && !right){
                if(actiondebug){
                        Debug.Log("Left");
                }
                state = "walk";
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                rb.position = new Vector2(rb.position.x - hspeed * Time.deltaTime * hspeedbuff, rb.position.y);
            }
            else if(right && !left){
                if(actiondebug){
                        Debug.Log("Right");
                }
                state = "walk";
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                rb.position = new Vector2(rb.position.x + hspeed * Time.deltaTime * hspeedbuff, rb.position.y);
            }
            if(jump && hitcooldown==0){
                if(jumpcooldown == 0){
                    if(actiondebug){
                        Debug.Log("Jump");
                    }
                    rb.AddForceY(800f);
                    jumpcooldown = 110;
                    state = "jump";
                }
            }
            if(jumpcooldown>10){
                state = "jump";
            }
            if(hitcooldown>10){
                state = prevstate;
            }
            if(hit && (hitcooldown == 0)){
                if((jump) || up){
                    if(actiondebug){
                        Debug.Log("Hit Up");
                    }
                    state = "jumphit";
                    hitcooldown = 40;
                    Instantiate(broomup, transform);
                }
                else if(down){
                    if(actiondebug){
                        Debug.Log("Hit Down");
                    }
                    state = "sweep";
                    hitcooldown = 31;
                    Instantiate(broomsweep, transform);
                }
                else{
                    if(actiondebug){
                        Debug.Log("Hit Straight");
                    }
                    state = "hit";
                    hitcooldown = 24;
                    Instantiate(broomhit, transform);
                }
            }
            if(state != prevstate){
                //Debug.Log(state);
                if (state == "sweep" || state == "hit"){
                    sprite.CrossFade(state, 0.025f);
                }
                else{
                    sprite.CrossFade(state, 0.1f);
                }
            }
            prevstate = state;
        }
        if(blackouttimer > 0){
            blackouttimer--;
            blackout.color = new Color(0f, 0f, 0f, 1f - (10 - blackouttimer)/10);
        }

    }

    public void gethit(int damage, int direction){
        if(damagecooldown == 0){
            rb.AddForceX(direction * -100f);
            damagecooldown = immunity;
            health -= damage;
            if(health <= 0){
                paused = true;
                GameObject.Find("Scenemanager").GetComponent<Scenemanager>().LoseScene();
            }
        }

    }




    public void setleft(InputAction.CallbackContext value){
        if(inputdebug){
            Debug.Log("left: " + value.ReadValue<float>());
        }
        left = value.ReadValue<float>() == 1f;
    }

    public void setright(InputAction.CallbackContext value){
        if(inputdebug){
            Debug.Log("right: " + value.ReadValue<float>());
        }
        right = value.ReadValue<float>() == 1f;
    }

    public void setup(InputAction.CallbackContext value){
        if(inputdebug){
            Debug.Log("up: " + value.ReadValue<float>());
        }
        up = value.ReadValue<float>() == 1f;
    }

    public void setdown(InputAction.CallbackContext value){
        if(inputdebug){
            Debug.Log("down: " + value.ReadValue<float>());
        }
        down = value.ReadValue<float>() == 1f;
    }
    public void setjump(InputAction.CallbackContext value){
        if(inputdebug){
            Debug.Log("jump: " + value.ReadValue<float>());
        }
        jump = value.ReadValue<float>() == 1f;
    }

    public void sethit(InputAction.CallbackContext value){
        if(inputdebug){
            Debug.Log("hit: " + value.ReadValue<float>());
        }
        hit = value.ReadValue<float>() == 1f;
    }

    public void setinteract(InputAction.CallbackContext value){
        if(inputdebug){
            Debug.Log("interact: " + value.ReadValue<float>());
        }
        interact = value.ReadValue<float>() == 1f;
    }

    public void setmenu(InputAction.CallbackContext value){
        if(inputdebug){
            Debug.Log("menu: " + value.ReadValue<float>());
        }
        menu = value.ReadValue<float>() == 1f;
    }
}
