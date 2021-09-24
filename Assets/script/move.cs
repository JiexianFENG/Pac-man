using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class move : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startP = new Vector3(-1.5f, 9.5f, 0);
    Vector3 endP = new Vector3(-12.5f, 9.5f, 0);
    public Animator PacMan;
    void Start()
    {
        PacMan.transform.SetPositionAndRotation ( startP , Quaternion.identity);
        PacMan.SetTrigger("left");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float distances = Vector3.Distance(PacMan.transform.position, endP);
        if (distances > 0.1f)
        { PacMan.transform.SetPositionAndRotation( Vector3.Lerp(PacMan.transform.position, endP,0.09f / distances), Quaternion.identity); }
        else
        {
            endP = changeEndPoint(endP,PacMan.transform.position);
        }
        
        
    }

    Vector3 changeEndPoint(Vector3 oldEndPoint, Vector3 location)
    {
        float distances = Vector3.Distance(PacMan.transform.position, oldEndPoint);
        if (distances <= 0.1f && oldEndPoint == new Vector3(-1.5f, 9.5f, 0))
        {
            PacMan.transform.SetPositionAndRotation(oldEndPoint,Quaternion.identity);
            PacMan.SetTrigger("left");
            return new Vector3(-12.5f, 9.5f, 0);
        }
        else if (distances <= 0.1f && oldEndPoint == new Vector3(-12.5f, 9.5f, 0))
        {
            PacMan.transform.SetPositionAndRotation(oldEndPoint, Quaternion.identity);
            PacMan.SetTrigger("up");
            return new Vector3(-12.5f, 13.5f, 0);
        }
        else if (distances <= 0.1f && oldEndPoint == new Vector3(-12.5f, 13.5f, 0))
        {
            PacMan.transform.SetPositionAndRotation(oldEndPoint, Quaternion.identity);
            PacMan.SetTrigger("right");
            return new Vector3(-1.5f, 13.5f, 0);
        }
        else
        {
            PacMan.transform.SetPositionAndRotation(oldEndPoint, Quaternion.identity);
            PacMan.SetTrigger("down");
            return new Vector3(-1.5f, 9.5f, 0);
        }
    }
    
}
