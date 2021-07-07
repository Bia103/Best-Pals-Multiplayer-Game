using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using MLAPI;
public class PlayfabManager : NetworkBehaviour
{
    // Start is called before the first frame update
    [Header("UI")]
    public TMP_Text messageText;
    public TMP_InputField emailInputRegister;
    public TMP_InputField passwordInputRegister;
    public TMP_InputField emailInput;
    public TMP_InputField emailResetInput;
    public TMP_InputField passwordInput;
    public TMP_InputField usernameInput;
    public static string name;
    [SerializeField]private GameObject authentificationLogIn;
    [SerializeField]private Canvas registerUser;
    [SerializeField]private Canvas logInCanvas;
    [SerializeField]private TMP_Text error_message;
    [SerializeField]private TMP_Text error_message_register;
    [SerializeField]private TMP_Text error_message_reset_password;
    public void RegisterButton(){
        var request = new RegisterPlayFabUserRequest{
            Email = emailInputRegister.text,
            Password = passwordInputRegister.text,
            RequireBothUsernameAndEmail = false,
            DisplayName = usernameInput.text
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnErrorRegister);
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result){
        Debug.Log("Success on register");
    }

    public void LoginButton(){
        var request = new LoginWithEmailAddressRequest{
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucces, OnErrorLogin);
    }

    void OnLoginSucces(LoginResult result){
        Debug.Log("Success on login");
        
        name = result.InfoResultPayload.PlayerProfile.DisplayName;
        /*if(name != null)
            PlayerPrefs.SetString("DisplayName", name);
        else 
            PlayerPrefs.SetString("DisplayName", "Unknown");
        Debug.Log(name);*/
    }

    public void ResetPasswordButton(){
        var request = new SendAccountRecoveryEmailRequest{
            Email = emailResetInput.text,
            TitleId = "4A1FE"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnErrorReset);
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result){
        Debug.Log("Mail sent");
        error_message_reset_password.text = "<color=green> Mail sent with succes";
    }
    // Update is called once per frame
    void Login(){
        var request = new LoginWithCustomIDRequest{
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);

    }
    
    void OnSuccess(LoginResult result){
        Debug.Log("Successful login/account create");
    }
    void OnError(PlayFabError error){
        Debug.Log("Error while logging in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }
    void OnErrorLogin(PlayFabError error){
        Debug.Log("Error while logging in");
        Debug.Log(error.GenerateErrorReport());
        authentificationLogIn.gameObject.SetActive(true);
        error_message.text = "Incorrect Data";
    }
    void OnErrorRegister(PlayFabError error){
        Debug.Log("Error while registering");
        Debug.Log(error.GenerateErrorReport());
        logInCanvas.gameObject.SetActive(false);
        registerUser.gameObject.SetActive(true);
        error_message_register.text = "Incorrect Data";
    }
    void OnErrorReset(PlayFabError error){
        Debug.Log("Error while reseting password");
        error_message_reset_password.text = "<color=red>This email doesn't belong to our database";
    }
    public string GetName(){
        return name;
    }
}
