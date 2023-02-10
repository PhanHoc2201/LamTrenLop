using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEditor.Build;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float movehorizontal;
    public float speed=3;public float jumpSpeed = 5;
    public bool isright;
    public bool isjump;
    public bool ismove;
    public bool isdie;
    public bool isclimping;
    public bool isclimping_rope;
    public float speedClimping=4f;
    Vector3 move;
    public float y;
    public float x;
    public Animator animator;
    Rigidbody2D rigidbody2D;
    private void Start()
    {
         rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        Animation();
        Jump();
        
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        Climping();
        Climping_Rope();
    }

    protected void Move()
    {
        


        move = transform.position;
        if (x>0 )
        {
           
            isright = true;
            move.x += Time.deltaTime * speed;
            Moving(isright);
        } 
        else if (x<0 )
        {
            isright = false;
           
            move.x -= Time.deltaTime * speed;
            Moving(isright);
        }
        
        transform.position = move;
    }
    protected void Jump()
    {
        
        move= transform.position;
        if (Input.GetAxis("Jump") > 0)
        {
            Debug.Log(Input.GetAxis("Jump"));
            move.y = jumpSpeed;
        }

        transform.position = move;
    }
    protected virtual void Animation()
    {
        
        
        Moving(isright);
        if (Input.GetAxis("Jump") > 0)
        {
            transform.GetComponent<Animator>().SetBool("jump", true);
        }
        if (Input.GetAxis("Jump") <= 00)
        {
            transform.GetComponent<Animator>().SetBool("jump", false);
        }

    }
    protected virtual void Moving(bool isright)
    {   
        transform.GetComponent<Animator>().SetFloat("Run", Mathf.Abs(x));
        if (isright)
            transform.GetComponent<SpriteRenderer>().flipX = false;
        if (!isright)
            transform.GetComponent<SpriteRenderer>().flipX = true;
        
    }
  
    protected virtual void Climping()
    {

        y=Input.GetAxis("Vertical");

       


        if (isclimping && Mathf.Abs(y )> 0)
        {
            rigidbody2D.velocity = new Vector2(0, speedClimping * y);
            Debug.Log("===" + y);
            
        }
        if (isclimping && Mathf.Abs(y) == 0)
        {
            rigidbody2D.velocity = new Vector2(0,0);
        }
    
    }
    protected virtual void Climping_Rope()
    {
        x = Input.GetAxis("Horizontal");
       

        if (isclimping_rope && Mathf.Abs(x) > 0)
        {
            rigidbody2D.velocity = new Vector2(speedClimping * x, 0);
        }
        if (isclimping_rope && Mathf.Abs(x) == 0)
        { 
            rigidbody2D.velocity = new Vector2(0, 0);
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Die"))
        {
            Debug.Log("OnTriggerStay2D");
            transform.GetComponent<Animator>().SetBool("Die_K", true);
        }
        if (other.CompareTag("Ladder"))
        {
            isclimping = true;
           
            Debug.Log("In Ladder");
            rigidbody2D.gravityScale = 0;
            transform.GetComponent<Animator>().SetBool("climping", true);
            
        }
        if (other.CompareTag("rope"))
        {
            isclimping_rope = true;
            Debug.Log("In rope");
            rigidbody2D.gravityScale = 0;
            transform.GetComponent<Animator>().SetBool("climping", true);
            
        }
        



    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Die"))
        {
            Debug.Log("OnTriggerExit2D");
            transform.GetComponent<Animator>().SetBool("Die_K", false);
        }
        if (other.CompareTag("Ladder"))
        {
            rigidbody2D.gravityScale = 1;
            Debug.Log("out Ladder");
            transform.GetComponent<Animator>().SetBool("climping", false);
            isclimping = false;
        }
        if (other.CompareTag("rope"))
        {
          
            Debug.Log("In rope");
            rigidbody2D.gravityScale = 1;
            transform.GetComponent<Animator>().SetBool("climping", false);
            isclimping_rope = false;
        }
    }
}
