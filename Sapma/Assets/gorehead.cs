using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class gorehead : MonoBehaviour
{
    public MeshRenderer rnder;
    public NavMeshAgent agent;
    public Transform player;
    public AudioSource ausour;
    public float stunduration = 0f;
    float defspeed;
    public float barktime = 0f;
    public float distance;
    private void Awake()
    {

       
        Randomize();

    }
    private void FixedUpdate()
    {
         distance = Vector3.Distance(transform.position,player.position);

        agent.SetDestination(player.transform.position);

        if (barktime > 0f) barktime -= Time.deltaTime;
        else if (distance < 75) Bark();

       if(stunduration > 0f) stunduration -= Time.deltaTime;
        Stun();

    }

    public void Shot()
    {
       
        stunduration = 3f;

    }

    public void Bark()
    {
        ausour.Play();
        barktime = Random.Range(10, 20);
    }

    public void Stun()
    {
        if(stunduration > 0f)
        {
            agent.speed = 0;
        }
        else
        {
            agent.speed = defspeed;
        }
    }
    public void Randomize()
    {
        agent.speed = agent.speed * Random.Range(0.5f, 4f);
        defspeed = agent.speed;
        ausour.pitch = Random.Range(0.95f, 1.05f);
        float i = Random.Range(0f, 1f);

        if (i < 0.5f) GoAway();

        transform.localScale = transform.localScale * Random.Range(0.5f,3f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Application.Quit();
            Debug.Log("should quit");
        }

        if (other.gameObject.layer == 9) other.SendMessage("Shot");
    }
    public void GoAway()
    {
        rnder.enabled = false;
        agent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        enabled = false;
    }
}
