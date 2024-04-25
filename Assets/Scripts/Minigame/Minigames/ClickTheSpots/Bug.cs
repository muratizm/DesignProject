using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Bug : Spot { 


    public override void Create(ClickTheSpots clickTheSpots, Image backgroundItem)
    {
        base.Create(clickTheSpots, backgroundItem);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Bug");
        StartPatrol();
    }

    
    private void StartPatrol()
    {
        StartCoroutine(Patrol());
    }


    private IEnumerator Patrol()
    {

        float speed = Vector2.Distance(leftBottom, rightTop) / 3f; // Speed is calculated so that leftBottom to rightTop is 3 seconds
        float patrolTime = UnityEngine.Random.Range(.5f, 1f);

        while (true) // Repeat the patrol indefinitely
        {
            Vector2 startPoint = spotRect.anchoredPosition;
            Vector2 targetPoint = new Vector2(UnityEngine.Random.Range(leftBottom.x, rightTop.x), UnityEngine.Random.Range(leftBottom.y, rightTop.y));

            float elapsedTime = 0f;

            while (elapsedTime < patrolTime)
            {
                float step = speed * Time.deltaTime;
                spotRect.anchoredPosition = Vector2.MoveTowards(spotRect.anchoredPosition, targetPoint, step);
                
                // Make the spot look towards the target
                Vector2 direction = targetPoint - startPoint;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                spotRect.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Subtract 90 to align the spot with the direction


                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Reset elapsedTime and swap startPoint and targetPoint for the return journey
            elapsedTime = 0f;
            Vector2 temp = startPoint;
            startPoint = targetPoint;
            targetPoint = temp;
        }
    }
}