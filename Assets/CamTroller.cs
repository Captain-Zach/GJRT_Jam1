using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTroller : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x < 0)
        {
            this.transform.position = new Vector3 (0, this.transform.position.y, -10);
        }
        if(this.transform.position.y > 0)
        {
            this.transform.position = new Vector3 (this.transform.position.x, 0, -10);
        }
        this.transform.position = new Vector3(this.transform.position.x + Input.GetAxis("Horizontal") * speed, this.transform.position.y + Input.GetAxis("Vertical") * speed, -10);
    }
}
