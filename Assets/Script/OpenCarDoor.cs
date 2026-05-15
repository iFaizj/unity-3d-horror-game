using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCarDoor : MonoBehaviour
{
    public Animator CarDoorOB;
    public GameObject CarkeyOBNeeded;
    public GameObject openText;
    public GameObject CarkeyMissingText;
    public GameObject boxKey;
    public AudioSource openSound;

    public bool inReach;
    public bool canTakeKey = true;
    public bool isOpen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inReach = false;
        openText.SetActive(false);
        CarkeyMissingText.SetActive(false);
        boxKey.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Reach"){
            if(canTakeKey){
                inReach = true;
                openText.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Reach"){
            inReach = false;
            openText.SetActive(false);
            CarkeyMissingText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CarkeyOBNeeded.activeInHierarchy == true && inReach && Input.GetButtonDown("Interact")){
            CarkeyOBNeeded.SetActive(false);
            openSound.Play();
            CarDoorOB.SetBool("open", true);
            openText.SetActive(false);
            CarkeyMissingText.SetActive(false);
            isOpen = true;
            boxKey.SetActive(true);
            canTakeKey = false;
        }else if(CarkeyOBNeeded.activeInHierarchy == false && inReach && Input.GetButtonDown("Interact")){
            if(canTakeKey){
                openText.SetActive(false);
                CarkeyMissingText.SetActive(true);
            }
        }

    }
}
