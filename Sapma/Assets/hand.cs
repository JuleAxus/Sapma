using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour
{
    public Transform Camera;
    Animation anim;
    public float distance;
    public LayerMask interactable;



    private void Awake()
    {
        anim = GetComponent<Animation>();
    }
    void Use()
    {

        RaycastHit hit;

        
        anim.Play();

      
            if (Physics.Raycast(Camera.position, Camera.forward, out hit, distance, interactable))
            {

            hit.collider.SendMessage("Pet");
            }
    



    }
}
