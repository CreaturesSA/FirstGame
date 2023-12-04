using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed, jumpForce, lineLenght, offset;

    private Rigidbody2D rig;

    [SerializeField] private bool isJumping = false;
    // Start is called before the first frame update
    void Start()
    {
            //Camera.main.transform.SetParent(transform);
            //Camera.main.transform.position = transform.position + (Vector3.up) + (transform.forward * -10);
            rig = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
           rig.velocity = (transform.right * speed * Input.GetAxis("Horizontal")) +
           (transform.up * rig.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }else if(Input.GetAxisRaw("Horizontal") == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetButtonDown("Fire1") && !isJumping)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //Dibujamos la línea
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - offset);
        Vector2 target = new Vector2(transform.position.x, transform.position.y - offset - lineLenght);
        Debug.DrawLine(origin, target, Color.black);

        //Hacemos el Raycast
        RaycastHit2D raycast = Physics2D.Raycast(origin, Vector2.down, lineLenght);

        //Detectamos colisiones con el Raycast
        if (raycast.collider == null)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }


        //if (Input.GetButtonDown("Jump"))
        //    {
        //        rig.AddForce(transform.up * jumpForce);
        //    }
            //anim.SetFloat("VelocityX", Mathf.Abs(rig.velocity.x));
            //anim.SetFloat("VelocityY", rig.velocity.y);
        
    }
}
