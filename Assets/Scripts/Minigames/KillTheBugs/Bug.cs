using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bug : MonoBehaviour
{
    private KillTheBugs _killTheBugs;
    private Image _backgroundItem;
    RectTransform bgCanvasRect;
    Vector2 leftBottom;
    Vector2 rightTop;
        
    RectTransform bugRect;


    public void Create(KillTheBugs killTheBugs, Sprite sprite, Image backgroundItem)
    {
        _killTheBugs = killTheBugs;
        _backgroundItem = backgroundItem;

        gameObject.AddComponent<Image>().sprite = sprite;
        gameObject.AddComponent<Button>().onClick.AddListener(OnClick);

        bgCanvasRect = _backgroundItem.GetComponent<RectTransform>();
        bugRect = GetComponent<RectTransform>();
        bugRect.sizeDelta = new Vector2(50, 50);
        bugRect.position = new Vector2(0, 0);

        StartCoroutine(Patrol());
    }


    public void OnClick()
    {
        _killTheBugs.OnClickBug(this);
        Kill();
    }

    private IEnumerator Patrol()
    {
        leftBottom = bgCanvasRect.anchoredPosition + new Vector2((bugRect.rect.width-bgCanvasRect.rect.width) / 2 , (bugRect.rect.height-bgCanvasRect.rect.height) / 2);
        rightTop = bgCanvasRect.anchoredPosition + new Vector2((bgCanvasRect.rect.width-bugRect.rect.width) / 2, (bgCanvasRect.rect.height-bugRect.rect.height) / 2);

        float speed = Vector2.Distance(leftBottom, rightTop) / 3f; // Speed is calculated so that leftBottom to rightTop is 3 seconds
        float patrolTime = UnityEngine.Random.Range(.5f, 1f);

        bugRect.anchoredPosition = new Vector2(UnityEngine.Random.Range(leftBottom.x, rightTop.x), UnityEngine.Random.Range(leftBottom.y, rightTop.y));

        while (true) // Repeat the patrol indefinitely
        {
            Vector2 startPoint = bugRect.anchoredPosition;
            Vector2 targetPoint = new Vector2(UnityEngine.Random.Range(leftBottom.x, rightTop.x), UnityEngine.Random.Range(leftBottom.y, rightTop.y));

            float elapsedTime = 0f;

            while (elapsedTime < patrolTime)
            {
                float step = speed * Time.deltaTime;
                bugRect.anchoredPosition = Vector2.MoveTowards(bugRect.anchoredPosition, targetPoint, step);
                
                // Make the bug look towards the target
                Vector2 direction = targetPoint - startPoint;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bugRect.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Subtract 90 to align the bug with the direction


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

    private void Kill()
    {
        Destroy(gameObject);
    }







}