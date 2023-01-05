using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformas : MonoBehaviour
{
    private PlatformEffector2D effector;

    public float starWaitTime;
    private float WaitedTime;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp("s")){
            WaitedTime = starWaitTime;
        }
        if (Input.GetKey("s"))
        {
            if (WaitedTime <= 0)
            {
                effector.rotationalOffset = 180f;
                WaitedTime = starWaitTime;
            }
            else
            {
                WaitedTime-= Time.deltaTime;
            }
        }

        if (Input.GetKey("space"))
        {
            effector.rotationalOffset = 0;
        }
    }
}
