using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine.UI;
using TMPro;
using MLAPI.NetworkVariable;
public class HoldToInteractScript : NetworkBehaviour
{
    private bool cube1 = false, cube2 = false, cube3 = false, cube4 = false;
    private Item eclipse = null, fullMoon = null, halfMoon = null, sunCloud = null;
    [SerializeField] private TMP_Text failCode;
    [SerializeField] private Canvas introduceCode;
    [SerializeField] private TMP_InputField number1;
    [SerializeField] private TMP_InputField number2;
    [SerializeField] private TMP_InputField number3;
    [SerializeField] private TMP_InputField number4;
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float interactTime = 2f;
    [SerializeField] RectTransform interactImageRoot;
    [SerializeField] Image interactProgressImage;
    private Item itemInteractedWith;
    private float currentInteractTimeElapsed;
    [SerializeField] TMP_Text itemNameText;
    NetworkVariableBool unlocked = new NetworkVariableBool(false);
    NetworkVariableBool firstDoorOpened = new NetworkVariableBool(false);
    void Update()
    {
        SelectAnItemBeingInteractedWithFromARay();
        if(HasItemTargetted()){
            if(IsOwner)
                interactImageRoot.gameObject.SetActive(true);
            if(Input.GetKey(KeyCode.E)){
                IncrementInteractProgressAndTryToComplete();
                
            }else{
                currentInteractTimeElapsed = 0f;
            }

            UpdateInteractProgressImage();
        }
        else{
            interactImageRoot.gameObject.SetActive(false);
            currentInteractTimeElapsed = 0f;
        }
    }
    private bool HasItemTargetted(){
        return itemInteractedWith != null;
    }
    private void UpdateInteractProgressImage(){
        float pct = currentInteractTimeElapsed / interactTime;
        interactProgressImage.fillAmount = pct;
        // Debug.Log(pct);
       // Debug.Log(currentInteractTimeElapsed);
    }
    private void IncrementInteractProgressAndTryToComplete(){
        currentInteractTimeElapsed += Time.deltaTime;
        if(currentInteractTimeElapsed >= interactTime){
            ItemDoAction();

        }
    }
    private void SelectAnItemBeingInteractedWithFromARay(){
        
        Ray ray = camera.ViewportPointToRay(Vector3.one/2f);
        RaycastHit hitInfo;
        
        if(Physics.Raycast(ray,out hitInfo, 2f, layerMask)){
            var hitItem = hitInfo.collider.GetComponent<Item>();
            
            if(hitItem == null){
                itemInteractedWith= null;
            }else if(hitItem != null && hitItem != itemInteractedWith){
                itemInteractedWith = hitItem;
                
                itemNameText.text = "Interact with " + itemInteractedWith.gameObject.name;
            }

        }else{
            itemInteractedWith = null;
        }

    }
    private void ItemDoAction(){
        if(IsLocalPlayer)
            CallServerRPC();
        itemInteractedWith = null;
    }

    [ServerRpc]
    public void CallServerRPC(){
        

        CallClientRPC();
    }
    [ClientRpc]
    private void CallClientRPC(){
        
         if(itemInteractedWith != null){
            if(itemInteractedWith.gameObject.name == "Door" && !firstDoorOpened.Value){
                Vector3 tilt = new Vector3(0,90f,0);
                //itemInteractedWith.gameObject.SetActive(false);
                //itemInteractedWith = null;
                CloseFirstDoorServerRPC();
               // Debug.Log("PLZ KILL ME");
                }
            if(itemInteractedWith.gameObject.name == "LockedDoor"){
                if(!unlocked.Value){
                    if(IsOwner)
                    introduceCode.gameObject.SetActive(true);
                }else{
                    
                    itemInteractedWith.gameObject.transform.Rotate(0,90,0);
                }
                
            }
            if(itemInteractedWith.gameObject.name == "Eclipse"){
                eclipse = itemInteractedWith;
                eclipse.gameObject.SetActive(false);
                cube1 = true;
            }else if(itemInteractedWith.gameObject.name == "FullMoon" || itemInteractedWith.gameObject.name == "HalfMoon" || itemInteractedWith.gameObject.name == "SunCloud")
            {
                if(itemInteractedWith.gameObject.name == "FullMoon"){
                    if(cube1){
                        fullMoon = itemInteractedWith;
                        fullMoon.gameObject.SetActive(false);
                        cube2 = true;
                    }else{
                        if(eclipse)
                        eclipse.gameObject.SetActive(true);
                        cube1 = false;
                    }
                }else if(itemInteractedWith.gameObject.name == "HalfMoon"){
                    if(cube1 && cube2){
                        halfMoon = itemInteractedWith;
                        halfMoon.gameObject.SetActive(false);
                        cube3 = true;
                    }else{
                        if(eclipse)
                        eclipse.gameObject.SetActive(true);
                        cube1 = false;
                        if(fullMoon)
                        fullMoon.gameObject.SetActive(true);
                        cube2 = false;
                    }
                }else if(itemInteractedWith.gameObject.name == "SunCloud"){
                    if(cube1 && cube2 && cube3){
                        sunCloud = itemInteractedWith;
                        sunCloud.gameObject.SetActive(false);
                        cube4 = true;
                    }else{
                        eclipse.gameObject.SetActive(true);
                        cube1 = false;
                        if(fullMoon)
                        fullMoon.gameObject.SetActive(true);
                        cube2 = false;
                        if(halfMoon)
                        halfMoon.gameObject.SetActive(true);
                        cube3 = false;
                    }
                }
            
            }
            if(itemInteractedWith.gameObject.name == "Door To Livingroom" &&cube1 && cube2 && cube3 && cube4){
                Destroy(itemInteractedWith.gameObject);
            }else if(itemInteractedWith.gameObject.name == "Door To Livingroom"){
                Destroy(itemInteractedWith.gameObject);
            }
            if(itemInteractedWith.tag == "Door"){
                Destroy(itemInteractedWith.gameObject);
            }
            //Destroy(itemInteractedWith.gameObject);
        }
    }
    public void OnClickSubmit(){
        if(number1.text == "7" && number2.text == "2" && number3.text == "5" && number4.text == "9"){
            ModifyValueServerRPC();
            introduceCode.gameObject.SetActive(false);
        }else{
            failCode.text = "<color=red>Wrong Code, try again.";
        }
    }
    [ServerRpc]
    private void ModifyValueServerRPC(){
        unlocked.Value = true;
    }
    [ServerRpc]
    private void CloseFirstDoorServerRPC(){
        firstDoorOpened.Value = true;
    }
}
