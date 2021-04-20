using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Axis {
    
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
}

public class MouseAxis {
    
    public const string MOUSE_X = "Mouse X";
    public const string MOUSE_Y = "Mouse Y";
}

public class Tags {

    public const string ENEMY = "Enemy"; 
    public const string PLAYER = "Player";

}

public class AnimationTags {

    public const string ZOOM_IN = "Zoom_In"; // animation Base Layer tag
    public const string ZOOM_OUT = "Zoom_Out"; // animation Base Layer tag

    public const string SPEED = "Speed"; // animation Parameters tag

    public const string WALK_FLOAT = "Walk"; // animation Base Layer tag
    public const string RUN_FLOAT = "Run"; // animation Base Layer tag
    public const string SHOOT_TRIGGER = "Shoot"; // animation Parameter tag
    public const string RELOAD_BOOL = "Reload"; // animation Parameter tag
    public const string ATTACK = "Attack"; // animation Parameter tag
    public const string IDLE = "Idle"; // animation Parameter tag
    public const string AIM_WALK = "Aim_walk"; // animation Parameter tag
    public const string IDLE_AIM = "Idle_Aim"; // animation Parameter tag

}
