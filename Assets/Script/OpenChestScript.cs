using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestScript : MonoBehaviour
{
    public Animator chestOB;
    public GameObject keyOBNeeded;
    public GameObject openText;
    public GameObject keyMissingText;
    public GameObject reachChainsaw;
    public AudioSource openPadLockSound;
    public AudioSource openChestSound;

    public bool inReach;
    public bool isOpen;



    void Start()
    {
        inReach = false;
        openText.SetActive(false);
        keyMissingText.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Reach"){
            inReach = true;
            openText.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Reach"){
            inReach = false;
            openText.SetActive(false);
            keyMissingText.SetActive(false);
        }
    }


    void Update()
    {
        if(keyOBNeeded.activeInHierarchy == true && inReach && Input.GetButtonDown("Interact")){

            keyOBNeeded.SetActive(false);
            openPadLockSound.Play();
            openChestSound.Play();
            chestOB.SetBool("open", true);
            openText.SetActive(false);
            keyMissingText.SetActive(false);
            isOpen = true;
            reachChainsaw.SetActive(true);


        }else if(keyOBNeeded.activeInHierarchy == false && inReach && Input.GetButtonDown("Interact")){
            openText.SetActive(false);
            keyMissingText.SetActive(true);
        }

        if(isOpen){
            chestOB.GetComponent<BoxCollider>().enabled = false;
            chestOB.GetComponent<OpenChestScript>().enabled = false;
        }
    }
}
