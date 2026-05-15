using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCarkey : MonoBehaviour
{
    public GameObject CarKeyOB;
    public GameObject invOB;
    public GameObject pickUpText;
    public AudioSource keySound;

    public bool inReach;

    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
        invOB.SetActive(false);
        gameObject.SetActive(false);
        CarKeyOB.SetActive(true);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Reach"){
            inReach = true;
            pickUpText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Reach"){
            inReach = false;
            pickUpText.SetActive(false);
        }
    }


    void Update()
    {
        if(inReach && Input.GetButtonDown("Interact")){
            CarKeyOB.SetActive(false);
            keySound.Play();
            invOB.SetActive(true);
            pickUpText.SetActive(false);
        }
    }
}
