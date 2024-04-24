using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spot : MonoBehaviour
{
    protected ClickTheSpots _clickTheSpots;
    protected Image _backgroundItem;
    protected RectTransform bgCanvasRect;
    protected Vector2 leftBottom;
    protected Vector2 rightTop;
    protected RectTransform spotRect;


    public virtual void Create(ClickTheSpots clickTheSpots, Image backgroundItem)
    {
        _clickTheSpots = clickTheSpots;
        _backgroundItem = backgroundItem;

        gameObject.AddComponent<Image>();
        gameObject.AddComponent<Button>().onClick.AddListener(OnClick);

        bgCanvasRect = _backgroundItem.GetComponent<RectTransform>();
        spotRect = GetComponent<RectTransform>();
        spotRect.sizeDelta = new Vector2(50, 50);
        spotRect.position = new Vector2(0, 0);


        leftBottom = bgCanvasRect.anchoredPosition + new Vector2((spotRect.rect.width-bgCanvasRect.rect.width) / 2 , (spotRect.rect.height-bgCanvasRect.rect.height) / 2);
        rightTop = bgCanvasRect.anchoredPosition + new Vector2((bgCanvasRect.rect.width-spotRect.rect.width) / 2, (bgCanvasRect.rect.height-spotRect.rect.height) / 2);


        spotRect.anchoredPosition = new Vector2(UnityEngine.Random.Range(leftBottom.x, rightTop.x), UnityEngine.Random.Range(leftBottom.y, rightTop.y));



    }

    public void OnClick()
    {
        _clickTheSpots.OnClickSpot(this);
        Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }







}