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

        Vector2 mousePositionInGame = Camera.main.ScreenToWorldPoint(Input.mousePosition); //получаем координаты мыши в рамках игры, а не экрана
        Vector2 moveDirection = (mousePositionInGame - new Vector2(rigidbody2D.transform.position.x, rigidbody2D.transform.position.y)); //получаем направление, в котором вращать
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg; //вычисляем угол, на который нужно повернуть тело
        rigidbody2D.MoveRotation(angle); //вращаем тело по оси Z (по кругу на плоскости). 
    }
}
