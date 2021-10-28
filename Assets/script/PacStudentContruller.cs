using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentContruller : MonoBehaviour
{
    public Tweener tweener;
    private Vector3 target;
    public string lastInput;
    public string currentInput;
    public LayerMask mask;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        getInput();
        if (!hitCheck() )
        { currentInput = lastInput; }
        else
        { Debug.Log("hid"); }
        Move();
    }

    private void getInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {            
            lastInput = "right";
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            
            lastInput = "left";
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            
            lastInput = "up";
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            lastInput = "down";
        }
    }
    private void Move()
    {
        
        if (tweener != null && currentInput == "right" )
        {
            if (!tweener.TweenExists(transform))
            {
                target = transform.position;
                target.x = target.x + 1;
                tweener.AddTween(transform, transform.position, target, 1);

            }
           
        }
        //move left
        if (tweener != null && currentInput == "left"  )
        {
            if (!tweener.TweenExists(transform))
            {
                target = transform.position;
                target.x = target.x -1;
                tweener.AddTween(transform, transform.position, target, 1);

            }
        }
        //move up
        if (tweener != null && currentInput == "up" )
        {
            if (!tweener.TweenExists(transform))
            {
                target = transform.position;
                target.y = target.y + 1;
                tweener.AddTween(transform, transform.position, target, 1);

            }
        }
        //move down
        if (tweener != null && currentInput == "down" )
        {
            if (!tweener.TweenExists(transform))
            {
                target = transform.position;
                target.y = target.y - 1;
                tweener.AddTween(transform, transform.position, target, 0.6f);

            }
        }
   
    }
    bool hitCheck()
    {
        //Rigidbody2D wall;
        if (lastInput == "left")
        { direction = Vector2.left; }
        else if (lastInput == "right")
        { direction = Vector2.right; }
        else if (lastInput == "up")
        { direction = Vector2.up; }
        else 
        { direction = Vector2.down; }
        float dist = 1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction, dist, mask );
        Debug.DrawRay(transform.position, direction * dist);
        if (hit.collider)
        { Debug.Log(hit.collider.name); }
        if (hit.collider != null && hit.collider.gameObject.tag == "wall")
        {
            if (lastInput == currentInput)
            {
                currentInput = "";
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
