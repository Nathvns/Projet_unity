using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float dir = 1;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
        
        transform.Translate(0, dir * Time.deltaTime, 0);
        if(transform.position.y >= 4 || transform.position.y <= 2) dir = dir * -1;


    }

public void Pickup(){
    Debug.Log("+1 pièce");

    GetComponent<AudioSource>().Play();

    GetComponent<BoxCollider>().enabled = false;
    transform.Find("Visual").gameObject.SetActive(false);
    Invoke("DestroyPowerUp", 1f);
    gameManager.Addscore();

}
    private void DestroyPowerUp(){
        Destroy(gameObject);
    }
}