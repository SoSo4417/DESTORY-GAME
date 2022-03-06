using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// XChartsの参照
using XCharts;

public class make_chart : MonoBehaviour
{
    [Header("ScatterChart本体")]
    public GameObject ScatterChart;
    private ScatterChart scatterchart;

    // Start is called before the first frame update
    void Start()
    {
        scatterchart = ScatterChart.GetComponent<ScatterChart>();

        TitleSet();
        //BackgroundSet();
        ThemeSet();
        YAxisSet();
        XAxisSet();
        PutData();
    }

    //タイトル編集
    private void TitleSet()
    {
        scatterchart.title.show = false;
        //scatterchart.title.textStyle.fontSize = 40;
        //scatterchart.title.textStyle.offset = new Vector2(0, 30);
        // scatterchart.title.textStyle.color = new Color(0f, 0.2f, 1f, 1f);
        //scatterchart.title.text = "破壊傾向";
    }
    /*
    private void BackgroundSet()
    {
        scatterchart.background.show = true;
        scatterchart.background.image = "../img/background";
    }
    */
    // 全体テーマ設定
    private void ThemeSet()
    {
        scatterchart.theme.colorPalette.Clear();
        scatterchart.theme.axis.splitLineColor = new Color(0f, 0f, 0f, 1f);
        scatterchart.theme.backgroundColor = new Color(0f, 0f, 0f, 0f);
        //string[] color_palette = new string[dataManager.Tag.Length];
        string[] color_palette = new string[] {
            "#e49511",
            "#b7523b",
            "#88315b",
            "#62457a",
            "#99C53D",
            "#b6d134",
            "#e7b312",
            "#c18c3b",
            "#d80012",
            "#bc703c",
            "#530a75",
            "#95685c",
            "#a59e51",
            "#adb846",
            "#9c8359",
            "#eace15",
            "#dd5810",
            "#b33439",
            "#af0636",
            "#5a2c79",
            "#747872",
            "#99c53d",
            "#d1dc29",
            "#ccc332",
            "#e07710",
            "#af0636",
            "#820b58",
            "#6a5f78",
            "#7f9267",
            "#8cac55",
            "#c7a838",
            "#ece71a"};
        new Color(1.0f, 1.0f, 0.0f, 0.7f);
        for (int i = 0; i < dataManager.Tag.Length; i++)
        {
            //scatterchart.theme.colorPalette.Add(new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.7f));

            if (ColorUtility.TryParseHtmlString(color_palette[i], out Color color))
            {
                scatterchart.theme.colorPalette.Add(color);
            }

        }
    }

    //Y軸設定
    private void YAxisSet()
    {
        for (int i = 0; i < 2; i++)
        {
            scatterchart.yAxes[i].show = true;
            scatterchart.yAxes[i].minMaxType = Axis.AxisMinMaxType.Custom;
            scatterchart.yAxes[i].min = 0;
            scatterchart.yAxes[i].max = 10;
            scatterchart.yAxes[i].splitNumber = 1;
            scatterchart.yAxes[i].axisLabel.show = false;
            scatterchart.yAxes[i].axisLine.show = false;
            scatterchart.yAxes[i].axisTick.show = false;
            scatterchart.yAxes[i].axisName.show = false;
            scatterchart.yAxes[i].axisName.location = AxisName.Location.Middle;
        }
    }

    // X軸設定
    private void XAxisSet()
    {
        for (int i = 0; i < 2; i++)
        {
            scatterchart.xAxes[i].show = true;
            scatterchart.xAxes[i].minMaxType = Axis.AxisMinMaxType.Custom;
            scatterchart.xAxes[i].min = 0;
            scatterchart.xAxes[i].max = 10;
            scatterchart.xAxes[i].splitNumber = 2;
            scatterchart.xAxes[i].axisLabel.show = false;
            scatterchart.xAxes[i].axisLine.show = false;
            scatterchart.xAxes[i].axisTick.show = false;
            scatterchart.xAxes[i].axisName.show = false;
            scatterchart.xAxes[i].axisName.location = AxisName.Location.Middle;
        }
        scatterchart.xAxes[0].splitLine.show = false;
    }

    // データ
    private void PutData()
    {
        //string tag_name;
        int num;

        for (int i = 0; i < dataManager.Tag.Length; i++)    
        {
            string tag_name = dataManager.Tag[i];
            scatterchart.series.list[i].data.Clear();
            num = dataManager.object_dict[tag_name];         //Random.Range(0, 16);
            Debug.Log(num);
            PointSet(i, num);
            scatterchart.AddSerie(SerieType.Scatter);
            SerieData adddata = new SerieData();
            adddata.data = new List<double> { dataManager.VandS[i][0], dataManager.VandS[i][1] };   // dataManager.VandS[i][0]:scale, dataManager.VandS[i][1]:value
            scatterchart.series.list[i].data.Add(adddata);

        }

    }

    // ポイント設定
    private void PointSet(int i, int break_num)
    {
        float symblo_size = 0.60f * (5.0f + (break_num * 2.5f));
        if (symblo_size > 35.0f)
        {
            symblo_size = 35.0f;
        }
        scatterchart.series.list[i].symbol.size = symblo_size;
        if (break_num == 0)
        {
            scatterchart.series.list[i].itemStyle.opacity = 0.0f;
        }
        else
        {
            scatterchart.series.list[i].itemStyle.opacity = 0.7f;
        }
        scatterchart.series.list[i].itemStyle.color = scatterchart.theme.colorPalette[i];
    }

}