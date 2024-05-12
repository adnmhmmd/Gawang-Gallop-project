using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateMapSelection : MonoBehaviour
{
    public GameObject mapSelectionObject; // Reference to the Map Selection GameObject

    // Function to be called when the button is clicked
    public void OnDeactivateButtonClick()
    {
        StartCoroutine(DeactivateAfterDelay(2.25f));
    }

    // Coroutine to deactivate the map selection object after a delay
    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mapSelectionObject.SetActive(false);
    }
}