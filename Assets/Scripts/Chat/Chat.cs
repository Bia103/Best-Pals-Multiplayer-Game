using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using MLAPI.NetworkVariable.Collections;
public class Chat : NetworkBehaviour
{

    [SerializeField] private GameObject chatUI = null;
    [SerializeField] private TMP_Text chatText = null;
    [SerializeField] private TMP_InputField inputField = null;
    static TMP_Text chatTextMultuplayer;
    string textMessage = "";
    [SerializeField] TextMesh playerTag;
    //[SerializeField] private TMP_Text allChat = null;
     NetworkVariableString allChat = new NetworkVariableString("");
    NetworkVariableString playerName1 = new NetworkVariableString("");
     NetworkVariableString playerName2 = new NetworkVariableString("");
    NetworkVariableString firstName = new NetworkVariableString("");
    NetworkVariableBool ok = new NetworkVariableBool(false);
     string name;
    void Start()
    {   
        chatTextMultuplayer = chatText.GetComponent<TMP_Text>();
        name = PlayfabManager.name;
        if(playerName1.Value == ""){
           ChangePlayerName1ServerRPC(name);
        }else if(playerName2.Value == ""){
           ChangePlayerName2ServerRPC(name);
        }
    }

    void Update()
    {
       chatText.text = allChat.Value;
       if(!IsHost){
           playerTag.text = firstName.Value;
       }
       
    }
    
    public void ReadString(string s){
        inputField.text = string.Empty;
        if(s != ""){
            Debug.Log("aaa");
            s = s + "\n";
            CallChatServerRPC(s, PlayfabManager.name);
        }
        
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void CallChatServerRPC(string s, string n){
        Debug.Log(s);
        allChat.Value += "["+ n +"]: ";
        allChat.Value += s;
       // allChat.Value += "\n";
      //  Debug.Log(allChat);
        
        //CallChatClientRPC(s);
    }
   [ClientRpc]
    private void CallChatClientRPC(string s){

        
    }
    [ServerRpc(RequireOwnership = false)]
    private void ChangePlayerName1ServerRPC(string name){
       // Debug.Log(name);
        playerName1.Value = name;
        
        playerTag.text = playerName1.Value;
        if(ok.Value == false){
            ok.Value = true;
            firstName.Value = name;
        }
       // Debug.Log(playerName1.Value);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangePlayerName2ServerRPC(string name){
        playerName2.Value = name;

        playerTag.text = playerName2.Value;
    }

            
}
