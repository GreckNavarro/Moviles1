using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ControladorFiguras : MonoBehaviour
{
    [SerializeField] GameObject[] FigureArray;
    [SerializeField] GameObject CurrentFigure;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            int tapcount = 0;

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
                Instantiate(CurrentFigure, pos, Quaternion.identity);

                RaycastHit hit;

                if(Physics.Raycast(new Vector3(pos.x, pos.y, -10), Vector3.forward, out hit, 100f))
                {
                    Debug.Log("Se hizo el raycast");
                    Debug.DrawLine(new Vector3(pos.x, pos.y, -10), Vector3.forward * 100, Color.green, 5.0f);

                    if (hit.collider.gameObject.tag == "Figura")
                    {
                        Debug.Log("Hola");
                        Debug.DrawLine(new Vector3(pos.x, pos.y, -10), Vector3.forward, Color.green);
                    }
                }

            }
            
        }
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
