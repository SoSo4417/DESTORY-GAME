using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CinemachineCameraController : MonoBehaviour
{
    public CinemachinePath cinemachinePath;
    private CinemachineDollyCart dollyCart;
    public int[] pausePoints = new int[] { 2, 3, 5, 6, 7 };

    public bool isContinue;
    public GameObject pausePointUI;
    public GameObject startpanel;
    public GameObject method_1;
    public GameObject method_2;

    private float defaultSpeed;
    private int currentPauseIndex;
    private int currentPosition;
    //ｹﾘｿｨui
    public Image[] skillImages;
    public SkillSprites[] skillSprites;

    //ｹﾘｿｨﾇｰﾌ睫ｾ
    public Sprite[] levelTipSprites;
    public Image levelTipImage;
    public GameObject startLevelTip;
    //ｹﾘｿｨｺ睫ｾ
    public GameObject endLevelTip;

    private bool isWaitingLevelEnd;

    public AudioClip SE;

    public AudioSource[] BGM;

    public AudioSource Ashioto;

    // Start is called before the first frame update
    void Start()
    {
        dollyCart = GetComponent<CinemachineDollyCart>();
        defaultSpeed = dollyCart.m_Speed;
        currentPosition = pausePoints[currentPauseIndex];
        startpanel.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if (isWaitingLevelEnd)
        {
            return;
        }

        if (isContinue)
        {
            BGM_Stop(); // BGM停止
            currentPauseIndex++;
            if (currentPauseIndex >= pausePoints.Length)
            {
                SceneManager.LoadScene("ResultScene");
                currentPosition = cinemachinePath.m_Waypoints.Length - 1;
            }
            else
            {
                currentPosition = pausePoints[currentPauseIndex];
            }

            ResetAllThing();
            StartCoroutine(DelayForEndLevel());
            isContinue = false;
        }
        else if (dollyCart.m_Position >= currentPosition)
        {
            if (dollyCart.m_Speed != 0)
            {
                dollyCart.m_Speed = 0;
                dollyCart.enabled = false;
                Ashioto.Stop();

                if (currentPauseIndex < pausePoints.Length)
                    StartCoroutine(DelayForStartLevel());
            }


            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            if (x != 0 || y != 0)
            {
                transform.Rotate(0, x, 0, Space.World);
                float nextAngleX = transform.localEulerAngles.x - y;

                if (nextAngleX >= 330 || nextAngleX <= 30)
                    transform.Rotate(-y, 0, 0, Space.Self);
            }
        }
    }
    /// <summary>
    /// ﾇﾐｻｻｼｼﾄﾜﾍｼｱ・
    /// </summary>
    private void ChangeSkillSprites()
    {
        if (currentPauseIndex >= skillSprites.Length)
        {
            return;
        }

        for (int i = 0; i < skillImages.Length; i++)
        {
            skillImages[i].sprite = skillSprites[currentPauseIndex].sprites[i];
        }
        print("changed:" + currentPauseIndex);
    }

    // BGMを開始する
    private void BGM_Start()
    {
        BGM[currentPauseIndex].Play();
    }

    // BGMを終了する
    private void BGM_Stop()
    {
        BGM[currentPauseIndex].Stop();
    }
    /// <summary>
    /// ｿｪﾊｼｹﾘｿｨﾇｰｽﾐｹﾘｿｨﾌ睫ｾ5s
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayForStartLevel()
    {
        levelTipImage.sprite = levelTipSprites[currentPauseIndex];
        startLevelTip.SetActive(true);
        AudioSource.PlayClipAtPoint(SE, transform.position);// クリア音

        yield return new WaitForSeconds(3);
        startLevelTip.SetActive(false);

        ChangeSkillSprites();
        pausePointUI.SetActive(true);

        //ｿｪﾊｼｹﾘｿｨﾌ睫ｾｹﾘｱﾕｺｪﾊｼｼﾆﾊｱ
        StageManager.Instance.StartTickTime();

        // tuika takahashi
        ObjGenerate.Instance.Gen_Flag(true);
        //

        BGM_Start(); // BGM開始

        // takahashi tsuika
        float lim = StageManager.Instance.GetLimit(currentPauseIndex) + 5;
        //

        yield return new WaitForSeconds(lim);
        BGM_Stop(); // BGM停止
        currentPauseIndex++;

        //
        ResetAllThing();

        if (currentPauseIndex >= pausePoints.Length)
        {
            SceneManager.LoadScene("ResultScene");
            currentPosition = cinemachinePath.m_Waypoints.Length - 1;
        }
        else
        {
            currentPosition = pausePoints[currentPauseIndex];
        }

        StartCoroutine(DelayForEndLevel());

        isContinue = false;
    }
    private IEnumerator DelayForEndLevel()
    {
        isWaitingLevelEnd = true;

        endLevelTip.SetActive(true);
        AudioSource.PlayClipAtPoint(SE, transform.position); // クリア音
        yield return new WaitForSeconds(5);
        endLevelTip.SetActive(false);

        //ｻﾖｸｴﾏ狃撝ﾙｶﾈ
        dollyCart.m_Speed = defaultSpeed;
        dollyCart.enabled = true;
        Ashioto.Play();

        isWaitingLevelEnd = false;

    }
    /// <summary>
    /// ﾖﾘﾖﾃﾋﾐﾎ・蠎ﾍui
    /// </summary>
    private void ResetAllThing()
    {
        StopAllCoroutines();
        //ｹﾘｱﾕui
        startLevelTip.SetActive(false);
        pausePointUI.SetActive(false);

        // tuika takahashi
        ObjGenerate.Instance.Gen_Flag(false);

        // takahashi tsuika
        StageManager.Instance.GoNext();

        //
        //ﾉｾｵ菷・
        SwitchWeponsEveryStages.Instance.DestroyWeapon();
        //ﾖﾘﾖﾃｼﾆﾊｱ
        StageManager.Instance.ResetTime();
    }
}
[System.Serializable]
public struct SkillSprites
{
    public Sprite[] sprites;
}