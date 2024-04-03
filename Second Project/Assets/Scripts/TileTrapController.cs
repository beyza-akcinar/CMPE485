using UnityEngine;
using System.Collections;

public class TileTrapController : MonoBehaviour
{
    public Transform[] tiles; // Array to hold references to tile transforms
    public float descendSpeed = 3f; // Speed at which tiles descend
    public float riseSpeed = 3f; // Speed at which tiles rise back
    public float descendDelay = 1f; // Delay after descending
    public float riseDelay = 1f; // Delay after rising
    public float thresholdY = -0.7f; // Threshold y-coordinate for triggering endgame

    private Vector3[] initialPositions; // Array to hold initial positions of tiles
	public UIManager uiManager;

    void Start()
    {
        // Store initial positions of tiles
        initialPositions = new Vector3[tiles.Length];
        for (int i = 0; i < tiles.Length; i++)
        {
            initialPositions[i] = tiles[i].position;
        }

        // Start the trap coroutine
        StartCoroutine(TrapCoroutine());
    }

    IEnumerator TrapCoroutine()
    {
        while (true)
        {
            // Descend tiles gradually
            for (int i = 0; i < tiles.Length; i++)
            {
                Vector3 targetPosition = initialPositions[i] - new Vector3(0f, descendSpeed, 0f);
                yield return StartCoroutine(MoveTile(tiles[i], targetPosition));
            }

            // Check for objects below the threshold
            CheckForObjectsBelowThreshold();

            // Wait for descendDelay seconds
            yield return new WaitForSeconds(descendDelay);

            // Rise tiles gradually back to their initial positions
            for (int i = 0; i < tiles.Length; i++)
            {
                yield return StartCoroutine(MoveTile(tiles[i], initialPositions[i]));
            }

            // Wait for riseDelay seconds
            yield return new WaitForSeconds(riseDelay);
        }
    }

    IEnumerator MoveTile(Transform tile, Vector3 targetPosition)
    {
        while (tile.position != targetPosition)
        {
            tile.position = Vector3.MoveTowards(tile.position, targetPosition, descendSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void CheckForObjectsBelowThreshold()
    {
        GameObject[] playerAndKeyObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in playerAndKeyObjects)
        {
            if (obj.transform.position.y < thresholdY)
            {
                uiManager.ShowCanvas();
                return;
            }
        }
    }
}
