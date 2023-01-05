using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject elevator;
    public Transform startpoint;
    public Transform endpoint;

    public float velocidad;

    private Vector3 Movimiento;

    // Start is called before the first frame update
    void Start()
    {
        Movimiento = endpoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, Movimiento, velocidad * Time.deltaTime);

        if(elevator.transform.position == endpoint.position)
        {
            Movimiento = startpoint.position;
        }
        if (elevator.transform.position == startpoint.position)
        {
            Movimiento = endpoint.position;
        }
    }
}
