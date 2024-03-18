using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ControladorFiguras : MonoBehaviour
{

    //CREACION FIGURAS
    [SerializeField] GameObject[] FigureArray;
    [SerializeField] GameObject CurrentFigure;
    List<GameObject> FigureList;
    

    // DOUBLE TAP;
    bool tapping = false;
    float lastTapTime = 0;
    float doubleTapThreshold = 0.2f;

    //PRESS AND DRAG
    bool presstouch = false;
    GameObject FiguraMover;



    //SWIPE
    Vector2 startPos;
    Vector2 endPos;
    [SerializeField] float swipeThreshold = 300f;
    bool endswipe = true;
    bool swiperealizado = false;

    [SerializeField] GameObject trailRenderer;




  
    void Start()
    {
        FigureList = new List<GameObject>();
        trailRenderer.SetActive(false);
        trailRenderer.GetComponent<TrailRenderer>().startColor = CurrentFigure.GetComponent<SpriteRenderer>().color;
        trailRenderer.GetComponent<TrailRenderer>().endColor = CurrentFigure.GetComponent<SpriteRenderer>().color;


    }


    void Update()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            Vector2 updatetrailposition = Camera.main.ScreenToWorldPoint(touch.position);
            trailRenderer.transform.position = updatetrailposition;
            

            

            if (touch.phase == TouchPhase.Began)
            {

                startPos = touch.position;
                swiperealizado = false;

                Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);

                
                RaycastHit2D hit = Physics2D.Raycast(touchpos, Vector2.zero);
                if (hit.collider != null)
                {
                        FiguraMover = hit.collider.gameObject;

                }
                



                if (!tapping)
                {
                    tapping = true;
                    StartCoroutine(SingleTap(touch.position));
                }


                if ((Time.time - lastTapTime) < doubleTapThreshold)
                {
                    tapping = false;
                    Vector2 secondTapPosition = touch.position;
                    RaycastHit2D hitdoubletap = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(secondTapPosition), Vector2.zero);
                    if (hit.collider != null)
                    {
                        GameObject figuraHit = hitdoubletap.collider.gameObject;
                        FigureList.Remove(figuraHit);
                        Destroy(figuraHit);

                    }
                }
                lastTapTime = Time.time;

            }

            if (touch.phase == TouchPhase.Moved)
            {
                    presstouch = true;
                    PressAndDrag(touch.position);
                    trailRenderer.SetActive(true);


            }

            if (touch.phase == TouchPhase.Ended)
            {
                presstouch = false;
                FiguraMover = null;

                endPos = touch.position;
                Vector2 DiferencePosition = endPos - startPos;
                if(DiferencePosition.magnitude > swipeThreshold && endswipe == true)
                {

                    for (int i = 0; i < FigureList.Count; i++)
                    {
                        Destroy(FigureList[i]);
                    }

                    FigureList.Clear();
                    swiperealizado = true;
                }

                endswipe = true;
                trailRenderer.SetActive(false);
            }


        }
    }

    IEnumerator SingleTap(Vector2 touchposition)
    {
        yield return new WaitForSeconds(doubleTapThreshold);
        if (tapping && presstouch == false && swiperealizado == false)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(touchposition);
            if (pos.y < 3.0f)
            {
                GameObject newFigure = Instantiate(CurrentFigure, pos, Quaternion.identity);
                FigureList.Add(newFigure);
            }

        }
        tapping = false;
    }


    void PressAndDrag(Vector2 touchPosition)
    {
        Vector2 touchpos = Camera.main.ScreenToWorldPoint(touchPosition);
        if (FiguraMover != null)
        {
            FiguraMover.transform.position = touchpos;
            endswipe = false;
        }
    }













    public void ChangeSquare()
    {
        CurrentFigure = FigureArray[2];
        trailRenderer.GetComponent<TrailRenderer>().startColor = FigureArray[2].GetComponent<SpriteRenderer>().color;
        trailRenderer.GetComponent<TrailRenderer>().endColor = FigureArray[2].GetComponent<SpriteRenderer>().color;
    }
    public void ChangeTriangle()
    {
        CurrentFigure = FigureArray[0];
        trailRenderer.GetComponent<TrailRenderer>().startColor = FigureArray[0].GetComponent<SpriteRenderer>().color;
        trailRenderer.GetComponent<TrailRenderer>().endColor = FigureArray[0].GetComponent<SpriteRenderer>().color;
    }
    public void ChangeCircle()
    {
        CurrentFigure = FigureArray[1];
        trailRenderer.GetComponent<TrailRenderer>().startColor = FigureArray[1].GetComponent<SpriteRenderer>().color;
        trailRenderer.GetComponent<TrailRenderer>().endColor = FigureArray[1].GetComponent<SpriteRenderer>().color;
    }
}
