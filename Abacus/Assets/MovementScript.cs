using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveDistance = 1.0f; // Set the distance the object should move up and down
    public float speed = 5.0f; // Set the speed of the movement
    public float raycastDistance = 1f;


    public List<GameObject> beads = new List<GameObject>();

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
            //targetPosition = new Vector3(transform.position.x, GetMouseWorldPos().y + offset.y, transform.position.z);
            // transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

            CheckForObjectAbove();
            CheckForObjectBelow();

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            isDragging = false;
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


   /* void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // Change to the appropriate mouse button if needed
        {
            CheckForObjectAbove();



        }

        if (Input.GetMouseButtonDown(1))
        {
            CheckForObjectBelow();

        }
    }*/

    IEnumerator MoveObject(Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos; // Ensure the final position is exactly as intended
    }


    void MoveBeads()
    {
        for (int i = 0; i < beads.Count; i++)
        {
            beads[i].transform.position += new Vector3(0,1f,0);
        }
    }

    void CheckForObjectAbove()
    {
        moveDistance = 1f;
        speed = 0.5f;

        RaycastHit hit;

        // Cast a ray from the object's position upwards
        if (Physics.Raycast(transform.position, Vector3.up, out hit, raycastDistance))
        {
            if (hit.collider.tag == "Box")
            {
                moveDistance = 0f;
            }

            Debug.Log("Object above detected: " + hit.collider.gameObject.name);
            // Do something with the object above (e.g., access hit.collider.gameObject)
        }
        else
        {
            moveDistance = 1f;
            speed = 0.5f;

            // Calculate the new Y position based on the current position and moveDistance
            float newY = transform.position.y + moveDistance;

            // Move the object smoothly using Lerp
            StartCoroutine(MoveObject(transform.position, new Vector3(transform.position.x, newY, transform.position.z), speed));
            Debug.Log("No object above detected.");
        }
    }

    void CheckForObjectBelow()
    {
        RaycastHit hit;

        // Cast a ray from the object's position downwards
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.tag == "Box")
            {
                moveDistance = 0f;
            }

            Debug.Log("Object below detected: " + hit.collider.gameObject.name);
            // Do something with the object below (e.g., access hit.collider.gameObject)
        }
        else
        {

            moveDistance = -1f;
            speed = 0.5f;

            // Calculate the new Y position based on the current position and moveDistance
            float newY = transform.position.y + moveDistance;

            // Move the object smoothly using Lerp
            StartCoroutine(MoveObject(transform.position, new Vector3(transform.position.x, newY, transform.position.z), speed));
            Debug.Log("No object below detected.");
        }
    }
}
