using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MLAPI;
using System.Text;
using System;

public class PasswordNetworkManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private GameObject passwordEntryUI;
    [SerializeField] private GameObject leaveButton;
    void Update(){
        //Debug.Log("Intra");
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Intra");
            Leave();
        }
    }
    private void Start(){
        NetworkManager.Singleton.OnServerStarted += HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnect;
    }

    private void OnDestroy(){

        if(NetworkManager.Singleton == null){ return; }

        NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
    }

    public void Host(){

        // Adaugam o metoda specifica prin care lasam un jucator sa se conecteze la camera noastra
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(new Vector3(-2f, 0f, 0f), Quaternion.Euler(0f, 135f, 0f));
    }



    public void Client(){

        NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.ASCII.GetBytes(passwordInputField.text);
        NetworkManager.Singleton.StartClient();
        
        
    }

    public void Leave(){
        if(NetworkManager.Singleton.IsHost){
            NetworkManager.Singleton.StopHost();
            NetworkManager.Singleton.ConnectionApprovalCallback -= ApprovalCheck;
        }else if(NetworkManager.Singleton.IsClient){
            NetworkManager.Singleton.StopClient();
        }
        passwordEntryUI.SetActive(true);
        leaveButton.SetActive(false);
    }

    private void HandleClientConnected(ulong clientID){
        if(clientID == NetworkManager.Singleton.LocalClientId){
            passwordEntryUI.SetActive(false);
            leaveButton.SetActive(true);
        }
    }

    private void HandleClientDisconnect(ulong clientID){
        if(clientID == NetworkManager.Singleton.LocalClientId){
            passwordEntryUI.SetActive(true);
            leaveButton.SetActive(false);
        }
    }

    private void HandleServerStarted(){
        if(NetworkManager.Singleton.IsHost){
            HandleClientConnected(NetworkManager.Singleton.LocalClientId);
        }
    }
    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        /*
            connectionData = retine parola
        */

        string password = Encoding.ASCII.GetString(connectionData);

        bool approveConnection = password == passwordInputField.text;
        /* 
        callback:
            - primul argument: daca spawnam un player
            - al doilea: cu ce prefab (daca dam null, e prefabul default)
            - al treilea: daca e in regula parola
            - al patrulea: pozitie
            - al cincilea: rotatie
        */
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        switch(NetworkManager.Singleton.ConnectedClients.Count){

            case 1:
                spawnPos = new Vector3(-4f, 0f, 0f);
                spawnRot = Quaternion.Euler(0f, 180f, 0f);
                break;
            case 2:
                spawnPos = new Vector3(-4f, 0f, 0f);
                spawnRot = Quaternion.Euler(0f, 225f, 0f);
                break;


        }

        callback(true, null, approveConnection, spawnPos, spawnRot);
    }

}
