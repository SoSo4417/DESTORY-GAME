using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_count : MonoBehaviour
{
    // x������J�E���g
    public void Xweapon_count()
    {
        dataManager.weapon_x += 1;
        dataManager.weapon_all += 1;
        Debug.Log("x������g�p�񐔁F" + dataManager.weapon_x + "�C����g�p�񐔍��v�F" + dataManager.weapon_all);
    }

    // Y������J�E���g
    public void Yweapon_count()
    {
        dataManager.weapon_y += 1;
        dataManager.weapon_all += 1;
        Debug.Log("y������g�p�񐔁F" + dataManager.weapon_y + "�C����g�p�񐔍��v�F" + dataManager.weapon_all);
    }

    // Z������J�E���g
    public void Zweapon_count()
    {
        dataManager.weapon_z += 1;
        dataManager.weapon_all += 1;
        Debug.Log("z������g�p�񐔁F" + dataManager.weapon_z + "�C����g�p�񐔍��v�F" + dataManager.weapon_all);
    }
}
