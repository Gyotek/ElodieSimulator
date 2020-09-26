using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Students_Movement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Transform waypoint;
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
        SetNextWaypoint();
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
        transform.position = waypoint.transform.position;
    }

    void Move()
    {
        rb.velocity = new Vector2(waypoint.position.x - transform.position.x, waypoint.position.y - transform.position.y).normalized * moveSpeed;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(waypoint.position.y - transform.position.y, waypoint.position.x - transform.position.x) * Mathf.Rad2Deg - 90);
        if ((waypoint.position - transform.position).magnitude <= 0.1f)
        {
            waypointIndex += 1;
            SetNextWaypoint();
        }
    }

    void SetNextWaypoint()
    {
        waypoint.position = transform.position;
        waypoint.rotation = Quaternion.identity;

        float randomAngle = Random.Range(0f, wpMaxAngle) - wpMaxAngle / 2;
        Vector3 rotation = new Vector3(Vector3.forward.x, Vector3.forward.y, Vector3.forward.z + randomAngle * Mathf.Deg2Rad);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(rotation), wpMaxDistance, 10);
        float distance = hit.distance;
        if (hit.distance <= 0)
            distance = wpMaxDistance;
        //float size = transform.GetComponentInChildren<SpriteRenderer>().size.x;
        float randomDistance = Random.Range(0f, distance);
        Vector2 nextWaypoint = rotation * randomDistance;
        Debug.Log("distance : " + distance + " rotation : " + rotation + " Position : " + nextWaypoint);

        waypoint.rotation = Quaternion.Euler(rotation * Mathf.Rad2Deg);
        waypoint.Translate(Vector2.up * distance, Space.Self);
    }
}
