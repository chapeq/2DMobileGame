
using UnityEngine;
using System.Collections;


public class DragOnTouch : MonoBehaviour
{
    public float timeStart = 6f;
    public ValidateSpells validate;
    Vector3 screenPoint;
    Vector3 initialPos;
    private TrailRenderer tr;
    private float currentTime;

    void OnMouseDown()
    {
        currentTime = timeStart;
       initialPos = transform.position;
        AudioManager.instance.Play("ButtonSelect");
        tr = GetComponentInChildren<TrailRenderer>();
       StartCoroutine(Timer());
    }

    private void OnMouseDrag()
    {
        screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        screenPoint.z = 20.0f; 
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        
    }

    private void OnMouseUp()
    {
        validate.Fail();
    }

    public void Reset()
    {
        transform.position = initialPos;
        tr.Clear();
    }

    public  IEnumerator Timer()
    {
        bool finTimer = true;
        while (finTimer)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
            if (currentTime <= 0)
            {
                finTimer = false;
            }
        }
        if (!finTimer)
        {
            validate.Fail();
        }
    }
}


    