using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class SimpleStart : NetworkBehaviour
{
    // Start is called before the first frame update
    CharacterController characterController;
    [SerializeField] GameObject door;
    bool doorClosed = false;
    void OnTriggerEnter(Collider collision){
        GameObject obj = collision.gameObject;
         Debug.Log("o sa mor");
        if (obj.CompareTag("Player") && doorClosed == false)
         {
             doorClosed = true;
             door.gameObject.transform.Rotate(0,-90,0);
         }
    }
}
