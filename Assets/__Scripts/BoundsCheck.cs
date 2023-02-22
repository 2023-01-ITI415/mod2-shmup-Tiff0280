using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If you type /// in Visual Studio, it will automatically expand to a <summary>
/// <summary>
/// Keeps a GameObject on screen. 
/// Checks whether a GameObject is on screen and can force it to stay on screen.
/// Note that this ONLY works for an orthographic Main Camera.
/// </summary>
public class BoundsCheck : MonoBehaviour
{                                   

    public enum eType { center, inset, outset };                             

    [Header("Inscribed")]
    public eType boundsType = eType.center;                                 
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;                             
        camWidth = camHeight * Camera.main.aspect;                            
    }

    void LateUpdate()
    {
        // Find the checkRadius that will enable center, inset, or outset     // b
        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;
        isOnScreen = true;

        // Restrict the X position to camWidth
        if (pos.x > camWidth + checkRadius)
        {                                               
            pos.x = camWidth + checkRadius;
            isOnScreen = false;
        }
        if (pos.x < -camWidth - checkRadius)
        { 
            pos.x = -camWidth - checkRadius;
            isOnScreen = false;
        }

        // Restrict the Y position to camHeight
        if (pos.y > camHeight + checkRadius)
        {                                              
            pos.y = camHeight + checkRadius;
            isOnScreen = false;
        }
        if (pos.y < -camHeight - checkRadius)
        {                                            
            pos.y = -camHeight - checkRadius;
            isOnScreen = false;
        }
        if (keepOnScreen && !isOnScreen)
        {                                  
            transform.position = pos;                                      
            isOnScreen = true;
        }
    }
}
