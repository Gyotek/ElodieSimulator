using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAD_Controller : MonoBehaviour
{
    //Variable Mouvement Perso
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    private object firePoint;

    public Transform SignPoint;
    public GameObject Green_Sign;
    public GameObject Red_Sign;
    public bool closeToSign;
    private GameObject CloseSign = null;

    private int MaxSign = 4;
    private int TotalSign;


    void Start()
    {

    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.E)) 
        {

            if (closeToSign)
            {
                Destroy(CloseSign);
                TotalSign--;
            }
            else if (MaxSign > TotalSign)
            {
                SetSign(BAD_Sign.SignTypes.Green);
                TotalSign++;
            }

        }

        if(Input.GetKeyDown(KeyCode.A))

        {
            if (closeToSign)
            {
                Destroy(CloseSign);
                TotalSign--;
            }
            else if (MaxSign > TotalSign)
            {
                SetSign(BAD_Sign.SignTypes.Red);
                TotalSign++;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x, movement.y).normalized * moveSpeed;
    }

    void SetSign(BAD_Sign.SignTypes _signType)
    {
        GameObject SignPrefab = null;
        switch (_signType)
        {
            case BAD_Sign.SignTypes.Green:
                SignPrefab = Green_Sign;
                break;
            case BAD_Sign.SignTypes.Red:
                SignPrefab = Red_Sign;
                break;
            default:
                break;
        }

        GameObject _SignPrefab = Instantiate(SignPrefab, SignPoint.position, SignPoint.rotation);
        Rigidbody2D rb = _SignPrefab.GetComponent<Rigidbody2D>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BAD_Sign>() && closeToSign != true)
        {
            closeToSign = true;
            CloseSign = collision.gameObject;

        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<BAD_Sign>())
        {
            closeToSign = false;
            CloseSign = null;
          
        }

    }




}
