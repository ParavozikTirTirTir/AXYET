using NUnit;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform point;
    private bool trigger;
    private GameObject Obj;
    void Start()
    {
        Obj = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            trigger = true;
        }

    void OnTriggerExit2D(Collider2D collision) 
        {
            if (collision.gameObject.tag == "Player")
            {
                trigger = false;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // ��� ������� �� ������� � � ���� ����� ����� � ���
        {
            Obj.transform.position = point.transform.position;
            trigger = false;
        }
    }
}