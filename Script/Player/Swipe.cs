using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    //private PlayerMovement player_move;
    private const float DEADZONE = 12.0f;
    public static Swipe Instance { set; get; }
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;

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

    private void Start()
    {
        //player_move = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;

            startTouch = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(0))
        {

            startTouch = swipeDelta = Vector2.zero;

        }

        #endregion

        #region Mobile Inputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                  print("Hello");
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                print("Hello");
                startTouch = swipeDelta = Vector2.zero;
            }
        }
        #endregion

        swipeDelta = Vector2.zero;


        if (startTouch != Vector2.zero)
        {
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }



            if (swipeDelta.magnitude > DEADZONE)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    // Left or right
                    if (x < 0)
                    {

                       // player_move.movingDir = Direction.LEFT;
                        //deathPlaform.isMove = true;
                           swipeLeft = true;
                    }

                    else
                       // player_move.movingDir = Direction.RIGHT;
                    //deathPlaform.isMove = true;
                       swipeRight = true;

                }
                else
                {
                    // Up or down
                    if (y < 0)
                    {

                        //player_move.movingDir = Direction.DOWN;
                        //deathPlaform.isMove = true;
                           swipeDown = true;
                    }

                    else
                        //player_move.movingDir = Direction.UP;
                    //deathPlaform.isMove = true;
                       swipeUp = true;
                }

            }
        }

    }
}
