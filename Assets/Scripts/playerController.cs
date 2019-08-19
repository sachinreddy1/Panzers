using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float movementSpeed = 6f;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    // 
    private Rigidbody m_Rigidbody;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;    
    // 
    public Transform tankHead;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // ----------------------------------------------- //

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    // ----------------------------------------------- //

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * movementSpeed;
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 targetVelocity = new Vector3(-verticalMove, 0f, horizontalMove);
        m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (horizontalMove != 0 || verticalMove != 0) {
            float angle = (Mathf.Atan2(verticalMove, -horizontalMove) * Mathf.Rad2Deg) - 90f;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }


    private void Turn()
    {
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookPos = new Vector3(lookPos.x - 10f, lookPos.y, lookPos.z);
        lookPos = lookPos - transform.position;
        
        float angle = Mathf.Atan2(lookPos.z, -lookPos.x) * Mathf.Rad2Deg;
        tankHead.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        
        // m_Rigidbody.angularVelocity = Vector3.zero;
    }

}
