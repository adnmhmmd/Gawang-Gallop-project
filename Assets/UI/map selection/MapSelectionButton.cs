using UnityEngine;
using UnityEngine.UI;

public class MapSelectionButton : MonoBehaviour
{
    public Animator mapSelectionAnimator; // Drag-and-drop your Map Selection UI Animator here in the Inspector

    private int maps = 1; // Initial value of the "maps" parameter

    // Function to be called when the increment button is clicked
    public void OnIncrementButtonClick()
    {
        if (maps < 5) // Prevent "maps" from exceeding 5
        {
            maps++; // Increment the value of "maps" by 1
            mapSelectionAnimator.SetInteger("maps", maps); // Update the "maps" parameter in the Animator
        }
    }

    // Function to be called when the decrement button is clicked
    public void OnDecrementButtonClick()
    {
        if (maps > 1) // Ensure maps doesn't go below 1
        {
            maps--; // Decrement the value of "maps" by 1
            mapSelectionAnimator.SetInteger("maps", maps); // Update the "maps" parameter in the Animator
        }
    }
}
