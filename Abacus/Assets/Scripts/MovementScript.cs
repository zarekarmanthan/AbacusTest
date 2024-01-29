using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveDistance = 1.0f; // Set the distance the object should move up and down
    public float speed = 5.0f; // Set the speed of the movement
    public float raycastDistance = 1f;

    public Rigidbody rigidbody;
   
    Vector3 targetPosition;

    private Vector3 offset;
    private bool isDragging = false;




    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            targetPosition = new Vector3(transform.position.x, GetMouseWorldPos().y + offset.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box"  || collision.gameObject.tag == "Ball")  // or if(gameObject.CompareTag("YourWallTag"))
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }


   

   

  
}
