using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float xMin, xMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, xMin, xMax), transform.position.y, transform.position.z);

    }
}
