using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class PlayerName : NetworkBehaviour
{
    [SerializeField] TextMesh playerTag;
   /* NetworkVariableString playerName1 = new NetworkVariableString("");
     NetworkVariableString playerName2 = new NetworkVariableString("");
     NetworkVariableBool nameTaken1 = new NetworkVariableBool(false);
     NetworkVariableBool nameTaken2 = new NetworkVariableBool(false);
    private string name;
    private bool isTheHost = false;
    //private static bool nameTaken1 = false;
    //private static bool nameTaken2 = false;*/
 /*  public void Start()
    {
        name = PlayfabManager.name;
        if(IsHost){
            Debug.Log("e host");
           // Debug.Log(name);
            ChangePlayerName1ServerRPC(name);
            playerTag.text = playerName1.Value;
            isTheHost = true;
        }else{
            Debug.Log("e client");
            ChangePlayerName2ServerRPC(name);
            playerTag.text = playerName2.Value;
        }
        Debug.Log(playerName1.Value);
        Debug.Log(playerName2.Value);
    }
    public void Update(){
        if(playerName1.Value != null){
        if(!nameTaken1.Value){
             Debug.Log("de se nu intri drasieeee");
             Debug.Log(playerName1.Value);
            playerTag.text = playerName1.Value;
            ChangeValueNameTaken1ServerRPC();
        }else if(!nameTaken2.Value){
            playerTag.text = playerName2.Value;
            ChangeValueNameTaken2ServerRPC();
        }}
    }


    [ServerRpc(RequireOwnership = false)]
    private void ChangePlayerName1ServerRPC(string name){
       // Debug.Log(name);
        playerName1.Value += name;
       // Debug.Log(playerName1.Value);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangePlayerName2ServerRPC(string name){
        playerName2.Value += name;
    }
    [ServerRpc(RequireOwnership = false)]
    private void ChangeValueNameTaken1ServerRPC(){
        nameTaken1.Value = true;
    }
    [ServerRpc(RequireOwnership = false)]
    private void ChangeValueNameTaken2ServerRPC(){
        nameTaken1.Value = true;
    }
*/
}
