using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    private const float DEADZONE = 100.0F;

    public static MobileInput Instance { set; get; }

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown; //gestos admitidos en la pantalla tactil
    private Vector2 swipeDelta, startToutch; //swipeDelta = posicion actual al tocar

    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // teseteando las booleanas
        tap = false;
        swipeLeft = false;
        swipeRight = false;
        swipeUp = false;
        swipeDown = false;

        //comprobar las entradas

        //entradas de pc
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startToutch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startToutch = Vector2.zero;
            swipeDelta = Vector2.zero;
        }

        #endregion

        //entradas de movil
        #region Movile Inputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startToutch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startToutch = Vector2.zero;
                swipeDelta = Vector2.zero;
            }
        }


        #endregion

        // calcular distancia de tap inicial a posicion actual

        swipeDelta = Vector2.zero;
        if (startToutch != Vector2.zero)
        {
            // comprobacion para movil
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startToutch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2) Input.mousePosition - startToutch;
            }
        }

        // comprobar si estoy en la zona muerta 
        if (swipeDelta.magnitude > DEADZONE)
        {
            // movimiento confirmado
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            //abs para comvertir todo a positivo y comprobar mas facilmente si es un movimiento horizontal o vertical
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //izquierda o derecha
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                //arriba o abajo
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            startToutch = Vector2.zero;
            swipeDelta = Vector2.zero;
        }
    }
}
