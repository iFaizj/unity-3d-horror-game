using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public GameObject player;
    public GameObject keypadOB;
    public GameObject hud;
    public GameObject inv;

    public GameObject doorOB;
    public Animator Anim;

    public Text textOB;
    public string answer = "123";

    public AudioSource button;
    public AudioSource correct;
    public AudioSource wrong;
    public AudioSource doorOpen;

    public bool animateState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(textOB.text == "RIGHT" && animateState){
            Anim.SetBool("open", true);
            doorOpen.Play();
            // Debug.Log("its open");
        }

        if(keypadOB.activeInHierarchy){
            hud.SetActive(false);
            inv.SetActive(false);
            player.GetComponent<FPSPlayerController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Number(int number){
        textOB.text += number.ToString();
        button.Play();
    }

    public void Execute(){
        if(textOB.text == answer){
            correct.Play();
            textOB.text = "RIGHT";
            animateState = true;
        }else{
            wrong.Play();
            textOB.text = "WRONG";
        }
    }

    public void Clear(){
        textOB.text = "";
        button.Play();
    }

    public void Exit(){
        keypadOB.SetActive(false);
        inv.SetActive(true);
        hud.SetActive(true);
        player.GetComponent<FPSPlayerController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
