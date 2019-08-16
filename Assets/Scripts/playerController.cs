using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float horizontalMove = 0f;
    public float verticalMove = 0f;

    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Rigidbody m_Rigidbody;

    private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * movementSpeed;
    }

    void FixedUpdate() {            
        Move();
    }

    void Move() {
        Vector3 targetVelocity = new Vector3(horizontalMove * 10f, 0f, verticalMove * 10f);
        m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }


}
