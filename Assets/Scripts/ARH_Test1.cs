using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ARH_Test1 : SerializedMonoBehaviour
{
    [Button]
    void TestLog() => Debug.Log("Test succed");
    private enum testEnum {one, two, chocolat };
    [SerializeField] private Dictionary<testEnum, string> testDico;

}
