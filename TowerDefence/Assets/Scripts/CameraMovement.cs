using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraMovement : MonoBehaviour
{
    public Vector3 SpawnPoint;
    public float panSpeed = 20f;
    public float scrollSpeed = 20f; 
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = SpawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float Scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += Scroll * scrollSpeed * 100f * Time.deltaTime;
        
        transform.position = pos;
    }
}
