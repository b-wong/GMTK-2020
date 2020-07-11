using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 15.0f;
    private Vector3 m_moveDirection;
    private Rigidbody m_RigidBody;
    private Transform m_playerMesh;
    private FakeGravityBody m_worldGravity;

    //public GameObject[] worlds;
    //public int currentWorld = 0;
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_playerMesh = transform.GetChild(0).transform;
        m_worldGravity = GetComponent<FakeGravityBody>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectMovement();
    }

    void DetectMovement()
    {
        // update move direction
        m_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        // update movement
        m_RigidBody.MovePosition(m_RigidBody.position + transform.TransformDirection(m_moveDirection * speed * Time.fixedDeltaTime));
        // rotate player to face the right direction
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        Vector3 dir = m_moveDirection;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.up);
        
        if (Vector3.Magnitude(dir) > 0.0f)
        {
            m_playerMesh.localRotation = targetRotation;
        }
    }

}
