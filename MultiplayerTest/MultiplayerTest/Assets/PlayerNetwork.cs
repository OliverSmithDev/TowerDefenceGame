using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        
        Vector3 moveDir = new vector3(0,0,0);
        
        if(Input.GetKey(KeyCode.W)) movedir.Z = +1f;
        if(Input.GetKey(KeyCode.S)) movedir.Z = -1f;
        if(Input.GetKey(KeyCode.A)) movedir.x = -1f;
        if(Input.GetKey(KeyCode.D)) movedir.x = +1f;
        
        float moveSpeed = 3f;
        transform.position += movedir * moveSpeed * Time.deltaTime;
        
        
    }
}
