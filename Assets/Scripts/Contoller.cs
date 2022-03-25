using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Contoller : MonoBehaviour
{
    public delegate void LeftUp();
    public event LeftUp leftUp;
    public delegate void LeftRight();
    public event LeftRight leftRight;
    public delegate void LeftLeft();
    public event LeftLeft leftLeft;
    public delegate void LeftDown();
    public event LeftDown leftDown;

    public delegate void RightUp();
    public event RightUp rightUp;
    public delegate void RightRight();
    public event RightRight rightRight;
    public delegate void RightLeft();
    public event RightLeft rightLeft;
    public delegate void RightDown();
    public event RightDown rightDown;


}
