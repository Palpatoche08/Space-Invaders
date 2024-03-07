using System.Collections;
using UnityEngine;

public class AlienBoard : MonoBehaviour
{
    float teleportDistance = 4f;
    float descentDistance = 2f;
    float baseWaitTime = 2f;
    float waitTime = 2f;  
    bool shouldMoveLeft = true;

    PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(TeleportSequence());
    }

    IEnumerator TeleportSequence()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            UpdateWaitTime();

            if (transform.position.x <= -43f && shouldMoveLeft)
            {
                transform.Translate(Vector3.down * descentDistance);
                shouldMoveLeft = false;
            }
            else if (transform.position.x >= -20f && !shouldMoveLeft)
            {
                transform.Translate(Vector3.down * descentDistance);
                shouldMoveLeft = true;
            }
            else
            {
                transform.Translate(Vector3.right * (shouldMoveLeft ? -teleportDistance : teleportDistance));
            }
        }
    }

    void UpdateWaitTime()
    {
        int playerScore = playerController.GetScore();
        waitTime = baseWaitTime - (playerScore / 1200) * 0.25f;

        waitTime = Mathf.Max(waitTime, 0.25f);
    }
}
