using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeCar : MonoBehaviour
{
    public GameObject treeOB;
    public GameObject escapeText;
    public GameObject cutTreeText;

    public AudioSource CarOn;

    public bool inReach; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inReach = false;
        escapeText.SetActive(false);
        cutTreeText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Reach"){
            inReach = true;
            escapeText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Reach"){
            inReach = false;
            escapeText.SetActive(false);
            cutTreeText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inReach && Input.GetButtonDown("Interact")){
            if(treeOB.activeInHierarchy){
                escapeText.SetActive(false);
                cutTreeText.SetActive(true);
            }else{
                StartCoroutine(playCar());
            }
            
        }
    }

    IEnumerator playCar(){
        CarOn.Play();
        yield return new WaitForSeconds(5.0f);
        CarOn.Stop();
        SceneManager.LoadScene("EndGame");
    }
}
