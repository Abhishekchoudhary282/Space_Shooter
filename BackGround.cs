using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float y;
    [SerializeField] Transform StartPos, EndPos;
    [SerializeField] float speed;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, y + transform.position.y, transform.position.z);
        y = -speed * Time.deltaTime;

        if (transform.position.y <= EndPos.position.y)
        {
            transform.position = new Vector3(transform.position.x, StartPos.position.y, transform.position.z);
        }

    }
}
