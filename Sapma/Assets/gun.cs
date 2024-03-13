using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    Transform camtrans;
    public float distance;
    public LayerMask shootable;
    public float force;
    public Animation anim;
    public AudioSource ausour;
    public float shootTime;
    public GameObject grab;
    private void Awake()
    {
        ausour = GetComponent<AudioSource>();
        camtrans = Camera.main.transform;
    }

    public void FixedUpdate()
    {
        if (shootTime > 0f) shootTime -= Time.deltaTime;
    }
    void Use()
    {
       
        if(shootTime <= 0f) Shoot();
    }

    void Shoot()
    {
        ausour.Play();
        anim.Play();
        RaycastHit hit;

        if (Physics.Raycast(camtrans.position, camtrans.forward * distance, out hit, shootable))
        {
            if(grab.activeSelf && grab.GetComponent<GrabandThrow>().holding)
            {
                grab.GetComponent<GrabandThrow>().Release();
            }

            if (hit.collider.gameObject.layer == 7) hit.collider.attachedRigidbody.AddForce(camtrans.forward * force);
            hit.collider.SendMessageUpwards("Shot",SendMessageOptions.DontRequireReceiver);

           

        }
        shootTime = 1f;
    }

}
