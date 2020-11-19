
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class DragOnTouch : MonoBehaviour
{
    Vector3 screenPoint;
    Vector3 initialPos; 

    void OnMouseDown()
    {
       initialPos = transform.position;

    }

    private void OnMouseDrag()
    {
        screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
       screenPoint.z = 20.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        
    }

    private void OnMouseUp()
    {
        transform.position = initialPos;
    }

}


    