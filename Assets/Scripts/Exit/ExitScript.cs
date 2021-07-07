using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class ExitScript : MonoBehaviour
{
    PasswordNetworkManager net;
    [SerializeField]GameObject passUI;
    void OnTriggerEnter(Collider collision){
        GameObject obj = collision.gameObject;
        Debug.Log("Intra la exit");
        if (obj.CompareTag("Player"))
         {
            net = passUI.GetComponent<PasswordNetworkManager>();
            net.Leave();
         }
    }
}
