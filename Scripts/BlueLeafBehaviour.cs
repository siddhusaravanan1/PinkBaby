using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLeafBehaviour : MonoBehaviour
{
    public float jumpSpeed;
    public GameObject player;
    float dist;

    private void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        if(dist<1.2f)
        {
            Debug.Log("working");
            player.GetComponent<PlayerBehaviour>().JumperLeaf(jumpSpeed);
        }
    }
}
