using System.Collections;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveDistance = 1.0f; // Set the distance the object should move up and down
    public float speed = 5.0f; // Set the speed of the movement
    public float raycastDistance = 1f;


    void Update()
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
    }

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


    void CheckForObjectAbove()
    {

        moveDistance = 1f;
        speed = 0.5f;

        RaycastHit hit;

        // Cast a ray from the object's position upwards
        if (Physics.Raycast(transform.position, Vector3.up, out hit, raycastDistance))
        {
            Debug.Log("Object above detected: " + hit.collider.gameObject.name);
            // Do something with the object above (e.g., access hit.collider.gameObject)
        }
        else
        {
            Debug.Log("No object above detected.");

            // Calculate the new Y position based on the current position and moveDistance
            float newY = transform.position.y + moveDistance;

            // Move the object smoothly using Lerp
            StartCoroutine(MoveObject(transform.position, new Vector3(transform.position.x, newY, transform.position.z), speed));
        }
    }

    void CheckForObjectBelow()
    {
        RaycastHit hit;

        moveDistance = -1f;
        speed = 0.5f;

        // Cast a ray from the object's position downwards
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {

            if (hit.collider.gameObject.tag == "Box")
            {
                moveDistance = 0f;
                speed = 0f;
            }


            Debug.Log("Object below detected: " + hit.collider.gameObject.name);
            // Do something with the object below (e.g., access hit.collider.gameObject)
        }
        else
        {
            // Calculate the new Y position based on the current position and moveDistance
            float newY = transform.position.y + moveDistance;

            // Move the object smoothly using Lerp
            StartCoroutine(MoveObject(transform.position, new Vector3(transform.position.x, newY, transform.position.z), speed));
            Debug.Log("No object below detected.");
        }
    }
}
