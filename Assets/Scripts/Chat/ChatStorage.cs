using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatStorage : MonoBehaviour
{
    // Start is called before the first frame update
    public static string chatLogs = "";
    public void changeLogs(string s){
        chatLogs += s;
    }
}
