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
    

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = transform.forward * speed;
        Destroy(gameObject, 2f);
    }

    // void OnTriggerEnter(Collider hitInfo) {
    //     if (hitInfo.gameObject.tag == "Environment") {
    //         m_Rigidbody.velocity = shotAngle * speed;
    //         // m_Rigidbody.velocity = Vector3.Reflect(m_Rigidbody.velocity, hitInfo.contacts[0].normal);
    //     }
    //     // Destroy(gameObject);
    // }

    void Update () {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        LayerMask collisionMask = LayerMask.GetMask("Environment");
        if (Physics.Raycast(ray, out hit, Time.deltaTime * speed + 0.1f, collisionMask)) {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }
    }

}
