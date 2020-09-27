using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Students_Movement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Transform waypoint;
    [SerializeField] Transform spawnpoint;
    [SerializeField] Transform targetpoint;
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

    void FixedUpdate()
    {
        if (currentState == studentStates.Moving)
            Move();
    }

    void Spawn()
    {
        currentState = studentStates.Moving;
        transform.position = spawnpoint.transform.position;
        transform.rotation = spawnpoint.transform.rotation;
        SetNextWaypoint();
    }

    void Move()
    {
        rb.velocity = new Vector2(waypoint.position.x - transform.position.x, waypoint.position.y - transform.position.y).normalized * moveSpeed;
        if ((waypoint.position - transform.position).magnitude <= 0.1f)
        {
            SetNextWaypoint();
        }
    }

    void SetNextWaypoint()
    {
        waypoint.position = transform.position;
        waypoint.rotation = Quaternion.identity;

        float randomAngle = Random.Range(0f, wpMaxAngle) - wpMaxAngle / 2;
        Vector3 rotation = new Vector3(0, 0, transform.rotation.z + randomAngle * Mathf.Deg2Rad);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(rotation), wpMaxDistance, 10);
        float distance = hit.distance;
        if (hit.distance <= 0)
            distance = wpMaxDistance;
        //float size = transform.GetComponentInChildren<SpriteRenderer>().size.x;
        float randomDistance = Random.Range(0f, distance);
        Vector2 nextWaypoint = rotation * randomDistance;
        //Debug.Log("distance : " + distance + " rotation : " + rotation + " Position : " + nextWaypoint);

        waypoint.rotation = Quaternion.Euler(rotation * Mathf.Rad2Deg);
        waypoint.Translate(Vector2.up * distance, Space.Self);

        Debug.DrawLine(transform.position, waypoint.position);
    }

    GameObject lastNavSquare = null;
    GameObject actualNavSquare = null;
    List<GameObject> visitedNavPoints = new List<GameObject>();
    KeyValuePair<GameObject, Vector2> nextPosition = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.GetComponent<NavigationSquare>())
        {
            if (lastNavSquare == null)
                lastNavSquare = collision.GetComponent<NavigationSquare>().gameObject;
            else
                lastNavSquare = actualNavSquare;

            actualNavSquare = collision.GetComponent<NavigationSquare>().gameObject;

            nextPosition = actualNavSquare.GetComponent<NavigationSquare>().GetNextSquare(targetpoint.position);
            Debug.Log(actualNavSquare.transform.position + " - - - " + nextPosition.Value);

            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(nextPosition.Value.y - transform.position.y, nextPosition.Value.x - nextPosition.Value.x) * Mathf.Rad2Deg - 90);
        }
    }
}
