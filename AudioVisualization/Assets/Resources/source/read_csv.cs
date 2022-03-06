using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class read_csv : MonoBehaviour
{
    static TextAsset csvFile;
    // �Ώە��f�[�^�i�[�z��
    static List<string[]> objectData = new List<string[]>();

    // csv�t�@�C���ǂݍ���
    static void CsvReader()
    {
        // ���ɒ��g����������N���A
        if (objectData.Count > 0)
            objectData = new List<string[]>();
        csvFile = Resources.Load("data/info") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            objectData.Add(line.Split(','));

        }
    }

    // �ǂݍ��݃f�[�^��ϐ��Ɋi�[
    public void Awake()
    {
        CsvReader();
        for (int i = 0; i < objectData.Count; i++)
        {
            dataManager.Tag[i] = objectData[i][0];
            //dataManager.Value[i] = int.Parse(objectData[i][1]);
            //dataManager.Scale[i] = int.Parse(objectData[i][2]);
            List<float> VSlist = new List<float>() { int.Parse(objectData[i][1]), int.Parse(objectData[i][2]) };
            dataManager.VandS.Add(VSlist);
        }
        Array.Resize<string>(ref dataManager.Tag, objectData.Count);

        //Array.Resize<int>(ref dataManager.Value, objectData.Count);
        //Array.Resize<int>(ref dataManager.Scale, objectData.Count);
    }
}