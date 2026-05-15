using System.Collections;
using UnityEngine;

public class CutTree : MonoBehaviour
{
    public GameObject treeCut;
    public GameObject chainsawOBneeded;
    public GameObject BatteryOBneeded;
    public GameObject cutText;
    public GameObject missingChainsawText;
    public GameObject missingBatteryText;
    public GameObject missingBothText;

    public AudioSource cutTreeSound;

    public bool inReach;
    public bool isCut;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inReach = false;
        isCut = false;
        cutText.SetActive(false);
        missingChainsawText.SetActive(false);
        missingBatteryText.SetActive(false);
        missingBothText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Reach"){
            inReach = true;
            cutText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Reach"){
            inReach = false;
            cutText.SetActive(false);
            missingChainsawText.SetActive(false);
            missingBatteryText.SetActive(false);
            missingBothText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inReach && Input.GetButtonDown("Interact") && !isCut){
            if(chainsawOBneeded.activeInHierarchy == true && BatteryOBneeded.activeInHierarchy == true){
                cutText.SetActive(false);
                StartCoroutine(PlaySoundCut());
                isCut = true;
                missingChainsawText.SetActive(false);
                missingBatteryText.SetActive(false);
                missingBothText.SetActive(false);
                treeCut.SetActive(false);
                
            }else{
                cutText.SetActive(false);
                if(!chainsawOBneeded.activeInHierarchy && !BatteryOBneeded.activeInHierarchy){
                    missingBothText.SetActive(true);
                }else if(!BatteryOBneeded.activeInHierarchy){
                    missingBatteryText.SetActive(true);
                }else if(!chainsawOBneeded.activeInHierarchy){
                    missingChainsawText.SetActive(true);
                }
            }
        }
    }

    IEnumerator PlaySoundCut(){
        cutTreeSound.Play();
        yield return new WaitForSeconds(1.0f);
        cutTreeSound.Stop();
    }
}
