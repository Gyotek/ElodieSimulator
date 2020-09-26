using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAD_Sign : MonoBehaviour
{

   public enum SignTypes {Green,Red};

    [SerializeField]
    public SignTypes signtype = SignTypes.Green;

}