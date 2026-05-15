using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public NavMeshAgent Ai;
    public List<Transform> destinations;
    public Animator Anim;

    public float walkSpeed = 2f;
    public float chaseSpeed = 5f;
    public float minIdleTime = 2f;
    public float maxIdleTime = 4f;
    public float rayCastDistance = 15f;
    public float catchDistance = 2f;
    public float jumpscareTime = 2f;
    public Transform player;
    public Vector3 rayCastOffset;

    public Transform currentDest;
    public bool walking = true;
    public bool chasing = false;

    public AudioSource walkSound;
    public AudioSource stepChaseSound;
    public AudioSource chaseSound;
    public AudioSource jumpscareSound;


    void Start()
    {
        if(destinations.Count == 0){
            Debug.LogError("No patrol destinations set on Monster!");
            return;
        }
        Ai.speed = walkSpeed;
        SetRandomDestination();
        Anim.SetTrigger("walk");
        walkSound.Play();
    }

    void Update()
    {
        Vector3 direction = (player.position - (transform.position + rayCastOffset)).normalized;
        RaycastHit hit;
        if(Physics.Raycast(transform.position + rayCastOffset, direction, out hit, rayCastDistance)){
            if(hit.collider.CompareTag("Player") && !chasing){
                StartChasing();
            }
        }

        if(chasing == true){
            Ai.destination = player.position;
            if(Ai.remainingDistance <= catchDistance){
                player.gameObject.SetActive(false);
                Anim.ResetTrigger("run");
                Anim.SetTrigger("jumpscare");
                jumpscareSound.Play();
                StartCoroutine(DeathRoutine());
                chasing = false;
            }else{
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if(!Physics.Raycast(transform.position + rayCastOffset, direction, out hit, rayCastDistance) || !hit.collider.CompareTag("Player") && distanceToPlayer > 5f){
                    StopChasing();
                }
            }
        }else if(walking){
            PatrolBehavior();
        }
    }

    void PatrolBehavior(){
        Ai.destination = currentDest.position;
        if(!Ai.pathPending && Ai.remainingDistance <= Ai.stoppingDistance && Ai.velocity.sqrMagnitude < 0.1f){
            int rand = Random.Range(0, 2);
            if(rand == 0){
                SetRandomDestination();
            }else{
                StartCoroutine(StayIdle());
            }
        }
    }

    void SetRandomDestination(){
        int randIndex = Random.Range(0, destinations.Count);
        currentDest = destinations[randIndex];
        Ai.destination = currentDest.position;
    }

    IEnumerator StayIdle(){
        walking = false;
        Anim.ResetTrigger("walk");
        Anim.SetTrigger("idle");

        if(walkSound.isPlaying) walkSound.Stop();
        if(chaseSound.isPlaying) chaseSound.Stop();
        if(stepChaseSound.isPlaying) stepChaseSound.Stop();

        float idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);

        walking = true;
        SetRandomDestination();
        Anim.ResetTrigger("idle");
        Anim.SetTrigger("walk");

        if(!walkSound.isPlaying){
            walkSound.Play();
        }
    }

    void StartChasing(){
        chasing = true;
        walking = false;
        Ai.speed = chaseSpeed;
        Anim.ResetTrigger("walk");
        Anim.ResetTrigger("idle");
        Anim.SetTrigger("run");
        StopAllCoroutines();

        if(walkSound.isPlaying){
            walkSound.Stop();
        }
        if(!chaseSound.isPlaying){
            chaseSound.Play();
        }
        if(!stepChaseSound.isPlaying){
            stepChaseSound.Play();
        }
    }

    void StopChasing(){
        chasing = false;
        walking = true;
        Ai.speed = walkSpeed;
        SetRandomDestination();
        Anim.ResetTrigger("run");
        Anim.SetTrigger("walk");

        if(chaseSound.isPlaying){
            chaseSound.Stop();
        }
        if(stepChaseSound.isPlaying){
            stepChaseSound.Stop();
        }
        if(!walkSound.isPlaying){
            walkSound.Play();
        }
    }

    IEnumerator DeathRoutine(){
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene("deathScene");
    }
    
}
