using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BDPlayer : MonoBehaviour
{
    [SerializeField] private float bdmoveSpeed = 5f;
    [SerializeField] private float bdrotateSpeed = 5f;

    [SerializeField] private Animator bdAnimator;
    private bool bdIsWalking = false;
    private bool bdIsUsing = false;

    [SerializeField] private Interactable interactableTarget;

    // Update is called once per frame
    private PlayerInputActions playerInputActions;

    void Awake()
    {
        playerInputActions = new PlayerInputActions(); // créer une instance de l'inputmanager
        playerInputActions.Player.Enable(); //activer le mapping "Player"
        playerInputActions.Player.Use.performed += UseInputHandler;
    }

    void Update()
    {
        MovementHandler();
        InteractionHandler();



    }
    void UseInputHandler (InputAction.CallbackContext context){
    Debug.Log("coucou");
    interactableTarget?.Interact();

    if(interactableTarget != null) {
        bdIsUsing = true;
    } else {
        bdIsUsing = false;
    }
    bdAnimator.SetBool("is_using", bdIsUsing);

}
    void MovementHandler()
    {
       
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
       Vector3 moveVector = new Vector3(inputVector.x, 0f, inputVector.y);
       bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 2, 0.6f, moveVector, bdmoveSpeed * Time.deltaTime);
       if(canMove){

       bdIsWalking = (moveVector != Vector3.zero);

       bdAnimator.SetBool("is_walking", bdIsWalking);

       transform.position = transform.position + moveVector * Time.deltaTime * bdmoveSpeed;
       }

       transform.forward = Vector3.Slerp(transform.forward, moveVector, Time.deltaTime * bdrotateSpeed);
    }
   void InteractionHandler(){
    Vector3 playerOrientation = transform.forward;

    if(Physics.Raycast(transform.position, playerOrientation, out RaycastHit hitObj, 1f, LayerMask.GetMask("Usables"))){
        Debug.Log("Touché" + hitObj.transform.name);

         if(hitObj.transform.TryGetComponent<PowerUp>(out PowerUp powerupObj)){
            powerupObj.Pickup();} 
        

        if(hitObj.transform.TryGetComponent<Interactable>(out Interactable interactableObj)){
            interactableTarget = interactableObj;
        }
    } else {
            interactableTarget = null;
        }
}
            










    }
