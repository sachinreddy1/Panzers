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
    // 
    private Vector3 m_DesiredPosition;
    public float x_offset = 10f;
    public float y_offset = 10f;
    // 
    public Transform[] m_Targets;

    private void Awake()
    {
        m_Camera = transform.GetComponent<Camera>();
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
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }

    // ----------------------------------------------- //

    private Vector3 FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;
        for (int i = 0; i < m_Targets.Length; i++)
        {
            if (!m_Targets[i].gameObject.activeSelf)
                continue;
            averagePos += m_Targets[i].position;
            numTargets++;
        }

        if (numTargets > 0)
            averagePos /= numTargets;

        averagePos.y = y_offset;
        averagePos.x += x_offset;

        return averagePos;
    }

    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);
        float size = 0f;

        for (int i = 0; i < m_Targets.Length; i++)
        {
            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].position);
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        size = Mathf.Max (size, m_MinSize);
        return size;
    }

}
