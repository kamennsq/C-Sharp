using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowChain : MonoBehaviour
{
    public Transform chain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (transform.GetComponent<MainCharacterMove>().isLookingRight())
            {
                Instantiate(chain, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation);
            }
            else
            {
                Instantiate(chain, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), transform.rotation);
            }
        }
    }
}
