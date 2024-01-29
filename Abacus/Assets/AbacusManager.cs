using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbacusManager : MonoBehaviour
{
    private List<MovementScript> beads = new List<MovementScript>();

    void Start()
    {
        // Populate the 'beads' list with references to all bead controllers
        beads.AddRange(FindObjectsOfType<MovementScript>());
    }

    // Call this method when you want to move all beads simultaneously
    public void MoveAllBeads(float deltaY)
    {
        foreach (MovementScript bead in beads)
        {
            // Move each bead by the same deltaY value
            bead.transform.position += new Vector3(0f, deltaY, 0f);
        }
    }
}
