using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationSquare : MonoBehaviour
{
    [SerializeField] public List<GameObject> navSquares = new List<GameObject>();

    public Vector2 GetRandomPos()
    {
        Vector2 size = GetComponent<BoxCollider2D>().bounds.size;
        float minX = transform.position.x - size.x / 2 * transform.localScale.x;
        float maxX = transform.position.x + size.x / 2 * transform.localScale.x;
        float minY = transform.position.y - size.y / 2 * transform.localScale.y;
        float maxY = transform.position.y + size.y / 2 * transform.localScale.y;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }

    public KeyValuePair<GameObject, Vector2> GetNextSquare(Vector2 targetpoint)
    {
        List<Vector2> nvSquaresRandomPos = new List<Vector2>();
        
        for (int i = 0; i < navSquares.Count; i++)
        {
            nvSquaresRandomPos.Add(navSquares[i].GetComponent<NavigationSquare>().GetRandomPos());
        }

        GameObject closestNS = navSquares[0];
        Vector2 closestPos = nvSquaresRandomPos[0];

        if (nvSquaresRandomPos.Count>0)
            for (int i = 1; i < nvSquaresRandomPos.Count; i++)
            {
                if (Vector2.Distance(closestPos, targetpoint) < Vector2.Distance(nvSquaresRandomPos[i], targetpoint))
                {
                    closestPos = nvSquaresRandomPos[i];
                    closestNS = navSquares[i];
                }
            }
            
        return new KeyValuePair<GameObject, Vector2>(closestNS, closestPos);
    }
}
