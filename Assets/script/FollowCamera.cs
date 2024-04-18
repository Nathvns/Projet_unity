using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    
   //récupérer la référence de l'objet à suivre
   [SerializeField] private  GameObject target; 
   [SerializeField] private float cameraHeight = 12f;
    void Start()
   // a chaque frame
   {
        MatchPosition();
   }
    void Update()
   // a chaque frame
   {
        MatchPosition();
   }


   void MatchPosition()
   // a chaque frame
   {
    transform.position = new Vector3(target.transform.position.x, cameraHeight, target.transform.position.z);

   }

}
