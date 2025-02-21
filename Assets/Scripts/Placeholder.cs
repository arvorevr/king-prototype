using UnityEngine;

public class Placeholder : MonoBehaviour
{
    [SerializeField] private GameObject[] placePositions;

    public void PlaceOnRandom(GameObject target)
    {
        if (placePositions.Length == 0) return;
        var randomIndex = Random.Range(0, placePositions.Length);
        target.transform.position = placePositions[randomIndex].transform.position;
        target.transform.rotation = placePositions[randomIndex].transform.rotation;
    }
}
