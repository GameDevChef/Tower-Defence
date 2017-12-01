using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float m_moveSpeed,
        m_scrollSpeed;

    [SerializeField]
    private float m_scrollBorderWidth;

    private Vector3 VectorZero, VectorFor, VectorBack, VectorRight, VectorLeft;

    private bool m_move;

    private Vector3 m_moveTo;

    [SerializeField]
    private Vector2 m_scrollLimits;

    private void Start()
    {
        VectorZero = Vector3.zero;
        VectorFor = Vector3.forward;
        VectorBack = Vector3.back;
        VectorLeft = Vector3.left;
        VectorRight = Vector3.right;
     
    }

    private void Update()
    {
        Vector3 direction = VectorZero;
        Vector3 toPos = VectorZero;
        if (Input.GetKey(KeyCode.W) /*|| Input.mousePosition.y > Screen.height - m_scrollBorderWidth*/)
        {
            direction = VectorFor;
        }
        else if (Input.GetKey(KeyCode.S) /*|| Input.mousePosition.y < m_scrollBorderWidth*/)
        {
            direction = VectorBack;
        }
        else if (Input.GetKey(KeyCode.A) /*|| Input.mousePosition.x < m_scrollBorderWidth*/)
        {
            direction = VectorLeft;
        }
        else if (Input.GetKey(KeyCode.D) /*|| Input.mousePosition.x > Screen.width - m_scrollBorderWidth*/)
        {
            direction = VectorRight;
        }

        if(direction != VectorZero)
        {

            transform.Translate(direction * m_moveSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll != 0)
        {
            Vector3 pos = transform.position;
            pos.y -= scroll * 100f * m_scrollSpeed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, m_scrollLimits.x, m_scrollLimits.y);
            transform.position = pos;
        }
       
       
        
    }
}
