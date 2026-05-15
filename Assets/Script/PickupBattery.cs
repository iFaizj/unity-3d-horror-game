using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBattery : MonoBehaviour
{
    public GameObject batteryOB;
    public GameObject invOB;
    public GameObject pickUpText;
    public AudioSource pickUpSound;

    public bool inReach;


    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
        invOB.SetActive(false);
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
            batteryOB.SetActive(false);
            pickUpSound.Play();
            invOB.SetActive(true);
            pickUpText.SetActive(false);
        }
    }
}
