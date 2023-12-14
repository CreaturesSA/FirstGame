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
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetButtonDown("Fire1") && !isJumping)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            AudioManager.instance.PlaySFX("Jump");
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


        // ------------------------------------------------------------------------------------------
        // APLICACIÓN AL JUEGO DE PLATAFORMAS QUE UTILIZA RAYCAST PARA DETECTAR QUE ESTÁ EN EL SUELO
        // ------------------------------------------------------------------------------------------
        // Si el raycast no toca con nada el personaje está en el aire
        if (raycast.collider == null)
        {
            SetAnimation("Jump");
        }
        else
        {
            // Si está sobre una superficie pero se mueve lateralmente
            if (GetComponent<Rigidbody2D>().velocity.x != 0) SetAnimation("Run");
            else SetAnimation("Idle"); // Si está sobre una superficie pero no se mueve
        }
    }


    // -----------------------------------------------------------------------------
    // Método que desactiva todos los parámetros del Animator y activa uno concreto
    // -----------------------------------------------------------------------------
    void SetAnimation(string name)
    {

        // Obtenemos todos los parámetros del Animator
        AnimatorControllerParameter[] parametros = GetComponent<Animator>().parameters;

        // Recorremos todos los parámetros y los ponemos a false
        foreach (var item in parametros) GetComponent<Animator>().SetBool(item.name, false);

        // Activamos el pasado por parámetro
        GetComponent<Animator>().SetBool(name, true);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                AudioManager.instance.PlaySFX("Hit");
                AudioManager.instance.PlayMusic("LoseALife");
                SCManager.instance.LoadScene("GameOver");
            }
        }
    }

}



//if (Input.GetButtonDown("Jump"))
//    {
//        rig.AddForce(transform.up * jumpForce);
//    }
//anim.SetFloat("VelocityX", Mathf.Abs(rig.velocity.x));
//anim.SetFloat("VelocityY", rig.velocity.y);
