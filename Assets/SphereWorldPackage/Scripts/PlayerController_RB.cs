using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController_RB : MonoBehaviour {

    // movement
    public float speed = 15.0f;
    private Vector3 moveDirection;
    private Rigidbody playerRigidBody;
    private Transform playerMesh;
    private FakeGravityBody worldGravity;
    public GameObject[] worlds;
    public int currentWorld = 0;

    // Use this for initialization
    void Start() {
        playerRigidBody = GetComponent<Rigidbody>();
        playerMesh = transform.GetChild(0).transform;
        worldGravity = GetComponent<FakeGravityBody>();
        worlds = GameObject.FindGameObjectsWithTag("World");
        SpeedUpdate();
    }

    // Update is called once per frame
    void Update() {
        // update move direction
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;


        // world swap
        if (Input.GetKeyDown("n"))
        {
            if (currentWorld + 1 > worlds.Length)
            {
                currentWorld = 0;
            }
            else
            {
                currentWorld++;
            }


            worldGravity.attractor = worlds[currentWorld].GetComponent<FakeGravity>();
            SpeedUpdate();
        }


    }

    void FixedUpdate()
    {
        // update movement
        playerRigidBody.MovePosition(playerRigidBody.position + transform.TransformDirection(moveDirection * speed * Time.fixedDeltaTime));
        // rotate player to face the right direction
        RotatePlayer();
    }

    // Rotate player to face direction of movement
    void RotatePlayer()
    {
        Vector3 dir = moveDirection;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.up);
        if (Vector3.Magnitude(dir) > 0.0f)
        {
            playerMesh.localRotation = targetRotation;
        }
    }

    void SpeedUpdate()
    {
        if (worldGravity.attractor.gameObject.name == "PlaneWorld")
        {
            speed = 10.0f;
        }
        else
        {
            speed = 15.0f;
        }
    }
}
