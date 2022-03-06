using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class dataManager
{
    public static string[] Tag = new string[100];
    public static List<List<float>> VandS = new List<List<float>>();

    public static Dictionary<string, int> object_dict = new Dictionary<string, int>();
    public static float value_all = 0;
    public static float scale_all = 0;

    public static int weapon_x = 0;
    public static int weapon_y = 0;
    public static int weapon_z = 0;
    public static int weapon_all = 0;

    public static float Xaccel_max = 0.0f;
    public static float Yaccel_max = 0.0f;
    public static float Zaccel_max = 0.0f;
}
