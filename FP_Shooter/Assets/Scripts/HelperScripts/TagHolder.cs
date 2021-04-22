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
    public const string IDLE = "Idle"; // animation Parameter tag

    public const string WALK_FLOAT = "Walk"; // animation Base Layer tag
    public const string RUN_FLOAT = "Run"; // animation Base Layer tag

    public const string SHOOT = "Shoot"; // animation Parameter tag
    public const string RELOAD = "Reload"; // animation Parameter tag
    public const string ATTACK = "Attack"; // animation Parameter tag

    public const string ZOMBIE_SPEED = "Zombie_speed"; // animation Parameter tag
    public const string ZOMBIE_ATTACK = "Zombie_attack"; // animation Parameter tag
    public const string ZOMBIE_DEAD = "Zombie_dead_back"; // animation Parameter tag
    // public const string ZOMBIE_DEAD_2 = "Zombie_dead_forward"; // animation Parameter tag
}
