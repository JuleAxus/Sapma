using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrabandThrow : MonoBehaviour
{
    public Transform Camera;
    public LayerMask interactable;
    public LayerMask RayCollidde;
    public float distance;
    public GameObject holding;
    public Vector3 slot;
    public float force;
    public float lockTime = 3f;
    AudioSource ausour;
    Animation anim;


    private void Awake()
    {
        ausour = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
    }
    void Use()
    {

        RaycastHit hit;

        ausour.Play();
        anim.Play();

        if (!holding)
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out hit, distance, interactable))
            {
                holding = hit.collider.gameObject;
                holding.GetComponent<Rigidbody>().useGravity = false;

               if(holding.TryGetComponent(out NavMeshAgent agent))
                {
                    Debug.Log(agent.gameObject.name);
                    agent.updatePosition = false;
                    agent.updateRotation = false;
                }

                
            }
        }
        else
        {
            if (holding.TryGetComponent(out NavMeshAgent agent))
            {

                holding.SendMessage("Released");
                agent.updatePosition = false;
                agent.updateRotation = false;
            }
            holding.GetComponent<Rigidbody>().useGravity = true;
            holding.GetComponent<Rigidbody>().AddForce(Camera.forward * force);
            holding = null;
        }



    }

    public void Release()
    {
        if (holding.TryGetComponent(out NavMeshAgent agent))
        {

            holding.SendMessage("Released");
            agent.updatePosition = false;
            agent.updateRotation = false;
        }
        holding.GetComponent<Rigidbody>().useGravity = true;
        holding.GetComponent<Rigidbody>().AddForce(Camera.forward * force);
        holding = null;
    }

    private void FixedUpdate()
    {

        RaycastHit hit;


        if (holding)
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out hit, distance,RayCollidde))
            {

                slot = hit.point;

            }
            else
            {
                slot = Camera.position + (Camera.forward * distance);
            }
            holding.gameObject.transform.position = Vector3.Lerp(holding.gameObject.transform.position, slot, Time.deltaTime * 5);
        }
    }

    float timeElapsed;
    float lerpDuration = 3;

    float startValue = 0;
    float endValue = 10;
    float valueToLerp;
    private void MoveObject()
    {
        if (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            valueToLerp = endValue;
        }
    }
}
