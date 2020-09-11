using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private float move;
    private float distance;
    private float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 directionPos = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), transform.position.z); //собирем вектор, в котором храним направление движения по X и Y
        rigidbody2D.MovePosition(rigidbody2D.transform.position + directionPos * speed * Time.fixedDeltaTime); //перемещаем тело, прибавляя к текущим координатам вектор с направлениями

        //rigidbody2D.MoveRotation(rigidbody2D.rotation + distance * 10f * Time.fixedDeltaTime); вращаем тело по оси Z (по кругу на плоскости). 
        //Нужно реализовать поворот гусеницы за курсором. Я думал пользовать RayCast2D
    }
}
