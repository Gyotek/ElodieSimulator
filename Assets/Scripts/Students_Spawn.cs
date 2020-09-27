using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Students_Spawn : MonoBehaviour
{
    [SerializeField] List<GameObject> studentsPrefabs = new List<GameObject>();

    [SerializeField] List<GameObject> spawnsPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnCoroutine");
    }

    IEnumerator SpawnCoroutine()
    {
        GameObject randomStudent = studentsPrefabs[Random.Range(0, studentsPrefabs.Count-1)];
        GameObject randomSpawn = spawnsPoints[Random.Range(0, spawnsPoints.Count - 1)];

        GameObject student = Instantiate(randomStudent, randomSpawn.transform.position, Quaternion.identity);

        GameObject randomTarget = spawnsPoints[Random.Range(0, spawnsPoints.Count - 1)];
        student.GetComponentInChildren<Students_Movement>().ChangeTarget(randomTarget.transform.position);

        yield return new WaitForSeconds(Random.Range(0.3f, 1.6f));

        StartCoroutine("SpawnCoroutine");
    }
}
