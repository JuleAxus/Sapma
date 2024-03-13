using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class gremlin : MonoBehaviour
{
    NavMeshAgent agent;
    bool walking;
    public coincounter counter;
    public float walkRadius;
    public AudioSource ausour;
    public Color[] colors;
    public GameObject child;
    float holdTime;
    Rigidbody rigid;
   public dialoguer dialog;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        Randomize();
        
    }
    private void FixedUpdate()
    {
        Debug.DrawLine(transform.position, agent.destination);

        if (!walking)
        {
            walking = true;
            Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
            Vector3 finalPosition = hit.position;

            agent.SetDestination(finalPosition);

        }

        float distance = Vector3.Distance(transform.position, agent.destination);

        if (walking && distance < walkRadius * 0.1f) walking = false;

    }

   public IEnumerator Released()
    {
      yield return new WaitForSeconds(3f);

        agent.updatePosition = true;
        agent.updateRotation = true;
        Debug.Log("Released");

    }

    public void Randomize()
    {
        agent.speed = agent.speed * Random.Range(0.5f, 2f);
        child.GetComponent<Renderer>().sharedMaterials[1].SetColor("_Color0", colors[Random.Range(0, colors.Length)]);

        float i = Random.Range(0f, 1f);

        if (i < 0.5f) GoAway();

    }

    public void Shot()
    {
        counter = coincounter.instance;
        counter.CoinGot();
        ausour.Play();
        child.SetActive(false);
        agent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        

    }

    public void GoAway()
    {
        GetComponent<Rigidbody>().Sleep();
        child.SetActive(false);
        agent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        enabled = false;
    }

    [ContextMenu("Pet")]
    public void Pet()
    {
        dialog.Speak(6f, Random.Range(0,dialog.phrases.Length));
    }



}
