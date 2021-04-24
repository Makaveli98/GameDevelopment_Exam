using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    private int c_Health;
    public int m_Health;

    void Start() {
        c_Health = m_Health;
    }

    public void Z_ApplyDamage(int damage) {
        c_Health -= damage;
    }
}
