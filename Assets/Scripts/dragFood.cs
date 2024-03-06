using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragFood : MonoBehaviour
{
    [SerializeField] bool isBeingDragged = false;

    void Update()
    {
        if (isBeingDragged)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.position = mousePosition;
        }
    }

    private void OnMouseDown()
    {
        isBeingDragged = true;
    }

    private void OnMouseUp()
    {
        isBeingDragged = false;
        Destroy(gameObject);
    }
}
