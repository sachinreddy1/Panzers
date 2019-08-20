using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float m_DampTime = 0.2f;
    public float m_MinSize = 3f;
    // 
    private Camera m_Camera;
    // 
    private float m_ZoomSpeed;
    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;
    private float cameraOffset;
    // 
    public Transform m_Target;

    private void Awake()
    {
        m_Camera = transform.GetComponent<Camera>();
    }

    void Start() {
        cameraOffset = transform.position.z;
    }

    // ----------------------------------------------- //

    private void FixedUpdate ()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        m_DesiredPosition = FindAveragePosition();
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }

    private void Zoom()
    {
        float requiredSize = 5f;
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }

    // ----------------------------------------------- //

    private Vector3 FindAveragePosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = m_Target.position.x;
        newPosition.z = m_Target.position.z + cameraOffset;
        return newPosition;
    }

}
