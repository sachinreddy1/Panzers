﻿using System.Collections;
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

    void OnTriggerEnter(Collider hitInfo) {
        if (hitInfo.gameObject.tag == "Environment") {
            m_Rigidbody.velocity = shotAngle * speed;

            Debug.Log(shotAngle);
        }
        // Destroy(gameObject);
    }
}
