using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class BlockPuzzle : NetworkBehaviour
{
    public TransparencySortMode teleportTarget;
    CharacterController characterController;
    void OnTriggerEnter(Collider collision){
        GameObject obj = collision.gameObject;
        Debug.Log(obj.tag);
        if (obj.CompareTag("Player"))
         {
             Debug.Log("Coliziune");
             characterController = obj.GetComponent<CharacterController>();// = new Vector3(68.5f, 32, 31);
             Vector3 move = new Vector3(20.83f, -0.62f, -10.45f);
             characterController.enabled = false;
             characterController.transform.position = move;
             characterController.enabled = true;
         }
    }
}
