using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwhook : MonoBehaviour
{
    public GameObject hook;
    public bool ropeActive;

    GameObject curHook;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ropeActive == false)
            {
                Vector2 destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);
                curHook.GetComponent<RopeScript>().destiny = destiny;

                ropeActive = true;
            }
            else
            {
                //delete rope
                Destroy(curHook);
                ropeActive = false;
            }
        }
        
    }
}
