using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody m_Rigidbody;
    // 
    [HideInInspector]
    public Vector3 shotAngle;
    // 
    public int maxNumBounces = 1;
    private int numBounces = 0;
    
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision collision) {
        if (numBounces == maxNumBounces) {
            Destroy(gameObject, 0);
            return;
        }
            
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            m_Rigidbody.velocity = transform.forward * speed;

            numBounces++;
        }
    }

}
