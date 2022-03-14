using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D coll;
    public AudioSource jumpAudio,hurtAudio,cherryAudio;
    public float speed;
    public float JumpForce;
    public Animator anim;
    public LayerMask ground;
    public int Cherry=0;
   

    public Text CherryNum;
    public bool isHurt=false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurt) 
        { Movement(); }
        SwitchAnim();
        
    }
    //角色移动
    void Movement()
    {
        float HorizontalMove=Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        if(HorizontalMove!=0)
        {
            rb.velocity=new Vector2 (HorizontalMove*speed,rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(facedirection));
        }
        if (facedirection!=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        //角色跳跃
        if(Input.GetButtonDown("Jump")&& coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce );
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
    }

    //动画改变
    void SwitchAnim()
    {
        anim.SetBool("idle",false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x)< 0.1f )
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }
    //收集
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            cherryAudio.Play();
            Destroy(collision.gameObject);
            Cherry++;
            CherryNum.text = Cherry.ToString();
        }    
    }

    //消灭怪物
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                enemy.Jumpon();
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                anim.SetBool("jumping", true);
            }

            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
        }
    }
    
}
