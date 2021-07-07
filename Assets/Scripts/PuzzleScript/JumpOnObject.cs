using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject door;
    bool doorClosed = false;
    void OnTriggerEnter(Collider collision){
        GameObject obj = collision.gameObject;
        
        if (obj.CompareTag("Player") && doorClosed == false)
         {
             doorClosed = true;
             door.gameObject.transform.Rotate(0,-90,0);
         }
    }
}
