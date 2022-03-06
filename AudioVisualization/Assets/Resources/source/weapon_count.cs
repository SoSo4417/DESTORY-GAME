using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_count : MonoBehaviour
{
    // x軸武器カウント
    public void Xweapon_count()
    {
        dataManager.weapon_x += 1;
        dataManager.weapon_all += 1;
        Debug.Log("x軸武器使用回数：" + dataManager.weapon_x + "，武器使用回数合計：" + dataManager.weapon_all);
    }

    // Y軸武器カウント
    public void Yweapon_count()
    {
        dataManager.weapon_y += 1;
        dataManager.weapon_all += 1;
        Debug.Log("y軸武器使用回数：" + dataManager.weapon_y + "，武器使用回数合計：" + dataManager.weapon_all);
    }

    // Z軸武器カウント
    public void Zweapon_count()
    {
        dataManager.weapon_z += 1;
        dataManager.weapon_all += 1;
        Debug.Log("z軸武器使用回数：" + dataManager.weapon_z + "，武器使用回数合計：" + dataManager.weapon_all);
    }
}
