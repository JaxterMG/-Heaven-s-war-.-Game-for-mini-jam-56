using UnityEngine;

public class SortingScript : MonoBehaviour
{
    private void FixedUpdate()
    {
        SpriteRenderer[] renderes = FindObjectsOfType<SpriteRenderer>();
        foreach  (SpriteRenderer renderer in renderes)
        {
         
            renderer.sortingOrder = Mathf.RoundToInt(renderer.transform.position.y * -100);
        }
    }
}
