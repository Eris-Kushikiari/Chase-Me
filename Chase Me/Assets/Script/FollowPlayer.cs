using UnityEngine;
using System.Collections.Generic;
public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float delay = 1.0f; // How many seconds behind
    public float updateRate = 0.1f; // How often to record position

    private Queue<Vector3> positionHistory = new Queue<Vector3>();
    private float timer = 0f;

    void FixedUpdate()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        // Record player's position every `updateRate` seconds
        if (timer >= updateRate)
        {
            positionHistory.Enqueue(player.position);
            timer = 0f;
        }

        // Keep enough history for the delay
        float maxHistory = delay / updateRate;
        if (positionHistory.Count > maxHistory)
        {
            Vector3 targetPosition = positionHistory.Dequeue();
            transform.position = targetPosition;
        }
    }
}
