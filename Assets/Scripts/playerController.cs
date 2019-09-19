using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float movementSpeed = 6f;
    // 
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private float turnSpeed = 0.1f;
    // 
    private Rigidbody m_Rigidbody;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    // 
    public Transform tankHead;
    public GameObject bulletPrefab;
    public Transform firePoint;
    //
    //public GameObject destroyEffect;
    //
    public GameManager gameManager;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // ----------------------------------------------- //

    private void Update()
    {
        if (GameManager.gameEnded)
            return;

        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * movementSpeed;
        // 
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    // ----------------------------------------------- //

    private void Move()
    {
        Vector3 targetVelocity = new Vector3(horizontalMove, 0f, verticalMove);
        m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (horizontalMove != 0 || verticalMove != 0) {
            float turnAngle = (Mathf.Atan2(verticalMove, -horizontalMove) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, turnAngle, 0f), turnSpeed);
        }
    }


    private void Turn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Vector3 dir = hit.point - tankHead.position;
            dir.y = 0;

            float shotAngle = Mathf.Atan2(dir.z, -dir.x) * Mathf.Rad2Deg;
            tankHead.rotation = Quaternion.AngleAxis(shotAngle, Vector3.up);
        }

        m_Rigidbody.angularVelocity = Vector3.zero;
    }

    // ----------------------------------------------- //

    void Shoot() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void Die()
    {
        Destroy(gameObject);
        gameManager.EndGame();

        //GameObject effect = (GameObject)Instantiate(destroyEffect, transform.position, transform.rotation);
        //Destroy(effect, 2f);

        // Call GameOver screen
    }

}
