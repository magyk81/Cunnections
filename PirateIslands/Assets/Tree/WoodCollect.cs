using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodCollect : MonoBehaviour
{
    [SerializeField]
    private Text woodText;
    [SerializeField]
    private RawImage dynamicImg, stillImg;
    private bool movingDynamicImg = false;
    private Vector2 dynamicImgEnd, dynamicImgStart;
    private int framesPassed = 0;
    private int woodInInventory = 0;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private int framesTravelling;

    // Start is called before the first frame update
    void Start()
    {
        dynamicImgEnd = new Vector2(
            dynamicImg.GetComponent<RectTransform>().anchoredPosition.x,
            dynamicImg.GetComponent<RectTransform>().anchoredPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingDynamicImg)
        {
            framesPassed++;
            dynamicImg.GetComponent<RectTransform>().anchoredPosition
                = Vector2.Lerp(dynamicImgStart, dynamicImgEnd, ((float) framesPassed) / ((float) framesTravelling));
            
            if (framesPassed == framesTravelling)
            {
                dynamicImg.gameObject.SetActive(false);
                framesPassed = 0;
                movingDynamicImg = false;

                woodInInventory++;
                woodText.text = "" + woodInInventory;
            }
        }
    }

    public void SetIncrement(Transform target)
    {
        movingDynamicImg = true;
        Vector3 targetPos = cam.WorldToScreenPoint(target.position);

        dynamicImgStart = new Vector2(targetPos.x, targetPos.y);
        Debug.Log(dynamicImgStart);
        dynamicImg.GetComponent<RectTransform>().anchoredPosition
            = new Vector2(targetPos.x, targetPos.y);

        dynamicImg.gameObject.SetActive(true);
    }
}
