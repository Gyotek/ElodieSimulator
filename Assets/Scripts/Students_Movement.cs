using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Students_Movement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Transform[] waypoints;
    int waypointIndex = 0;

    [Range(0f, 10f)] [SerializeField] float moveSpeed = 2f;
    [Range(0.1f, 2.5f)] [SerializeField] float wpMaxDistance = 0.5f;
    [Range(0f, 180f)] [SerializeField] float wpMaxAngle = 0.35f;

    public enum studentStates { Stopped, Moving}
    studentStates currentState = studentStates.Stopped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Spawn();
    }

    private void Update()
    {
        /*
        Vector2 leftVector = new Vector2(Mathf.Cos(-wpMaxAngle * Mathf.Deg2Rad / 2 + 90 * Mathf.Deg2Rad), Mathf.Sin(-wpMaxAngle * Mathf.Deg2Rad / 2 + 90 * Mathf.Deg2Rad)) * wpMaxDistance;
        Vector2 rightVector = new Vector2(Mathf.Cos(wpMaxAngle * Mathf.Deg2Rad / 2 + 90 * Mathf.Deg2Rad), Mathf.Sin(wpMaxAngle * Mathf.Deg2Rad / 2 + 90 * Mathf.Deg2Rad)) * wpMaxDistance;
        Debug.DrawRay(this.transform.position, leftVector, Color.black); //Left
        Debug.DrawRay(this.transform.position, transform.rotation.eulerAngles * wpMaxDistance, Color.black); //middle
        Debug.DrawRay(this.transform.position, rightVector, Color.black); //Right
        */



    }

    void FixedUpdate()
    {
        if (currentState == studentStates.Moving)
            Move();
    }

    void Spawn()
    {
        currentState = studentStates.Moving;
        transform.position = waypoints[waypointIndex].transform.position;

    }

    void Move()
    {
        rb.velocity = new Vector2(waypoints[waypointIndex].transform.position.x - transform.position.x, waypoints[waypointIndex].transform.position.y - transform.position.y).normalized * moveSpeed;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(waypoints[waypointIndex].transform.position.y - transform.position.y, waypoints[waypointIndex].transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90);



        if ((waypoints[waypointIndex].transform.position - transform.position).magnitude <= 0.1f)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == waypoints.Length)
        {
            rb.velocity = Vector2.zero;
            currentState = studentStates.Stopped;
        }
    }

    void SetNextWaypoint()
    {
        float randomAngle = Random.Range(0f, wpMaxAngle) - wpMaxAngle / 2;
        LayerMask layer;
        RaycastHit2D hit;
        Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.forward), wpMaxDistance, layer);
    }


}
