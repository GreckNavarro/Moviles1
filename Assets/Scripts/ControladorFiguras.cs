using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ControladorFiguras : MonoBehaviour
{
    [SerializeField] GameObject[] FigureArray;
    [SerializeField] GameObject CurrentFigure;
     List<GameObject> FigureList;


    public bool tapping = false;
    public float lastTapTime = 0;
    public float doubleTapThreshold = 0.3f;




    void Start()
    {
        FigureList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);        
            if (touch.phase == TouchPhase.Began)
            {

                if (!tapping)
                {
                    tapping = true;
                    StartCoroutine(SingleTap(touch.position));
                    
                }


                if ((Time.time - lastTapTime) < doubleTapThreshold)
                {
                    Debug.Log("DoubleTap");
                    tapping = false;
                    Vector2 secondTapPosition = touch.position;
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(secondTapPosition), Vector2.zero);
                    if (hit.collider != null)
                    {
                        GameObject figuraHit = hit.collider.gameObject;
                        if (FigureList.Contains(figuraHit))
                        {
                            FigureList.Remove(figuraHit);
                            Destroy(figuraHit);
                        }
                    }
                }

                lastTapTime = Time.time;

            }
            
        }
    }

    IEnumerator SingleTap(Vector2 touchposition)
    {
        yield return new WaitForSeconds(doubleTapThreshold);
        if (tapping)
        {
            Debug.Log("Creando Figura");
            Vector2 pos = Camera.main.ScreenToWorldPoint(touchposition);
            if (pos.y < 3.0f)
            {
                GameObject newFigure = Instantiate(CurrentFigure, pos, Quaternion.identity);
                FigureList.Add(newFigure);
            }

        }
        tapping = false;
    }













    public void ChangeSquare()
    {
        CurrentFigure = FigureArray[2];
    }
    public void ChangeTriangle()
    {
        CurrentFigure = FigureArray[0];
    }
    public void ChangeCircle()
    {
        CurrentFigure = FigureArray[1];
    }
}
