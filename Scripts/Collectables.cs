using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    void OnTriggerEnter(Collider cd)
    {
        if(cd.gameObject.tag=="Player")
        {
            cd.gameObject.GetComponent<PlayerBehaviour>().Collectable();
            gameObject.SetActive(false);
        }
    }
}
