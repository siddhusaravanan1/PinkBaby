using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;
    public Camera cam;

    float fov;
    // Start is called before the first frame update
    void Start()
    {
        fov = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), 5f * Time.deltaTime);
       transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        /*   if (player.transform.position.y > 2.65)
          {
              cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov+18,2*Time.deltaTime);
          }
          else
          {
              cam.fieldOfView = cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, 2 * Time.deltaTime);
          }*/
    }
}
