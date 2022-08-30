using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ???????????T?C?Y?????]???????????X?N???v?g
/// </summary>
/// 
public class SnowBallStatus : MonoBehaviour
{
    public float Speed;//???]???x
    public float FieldSpeed;//?n???????????x
    public float Size;//?????T?C?Y

    public GameObject SnowBallTracker;
    public GameObject LeftHandTracker;
    public GameObject SnowFirstPos;
    public GameObject Field;

    public Text message1;
    public Text message2;

    float timer = 0;
    float endtimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        FadeController.isFadeIn = true;
        Speed = 0;
        FieldSpeed = 0;
        Size = 1.65f;
    }

    // Update is called once per frame
    void Update()
    {
        //�g���b�J�[�̈ʒu�ɐ�ʂ������Ă���ver
        //this.gameObject.transform.position = SnowBallTracker.transform.position;

        //��̈ʒu�����ʂ̈ʒu�����߂�ver
        if (timer < 7)
        {
            message1.text = "�r��L�΂�����ԂŁA\n���ʂ̐�ʂ�������ʒu�Ɉړ����Ă��������B";
            message2.text = "�ʒu������...";
            timer += Time.deltaTime;
            SnowFirstPos.transform.position = new Vector3(LeftHandTracker.transform.position.x, LeftHandTracker.transform.position.y - 0.45f, LeftHandTracker.transform.position.z + 0.78f);
        }
        else
        {
            message1.text = "";
            message2.text = "";
            this.gameObject.transform.position = SnowFirstPos.transform.position;
        }

        Size += Speed * 0.001f;
        FieldSpeed += Speed *0.02f;
        this.gameObject.transform.Rotate(new Vector3(Speed, 0, 0));
        this.gameObject.transform.localScale = new Vector3(Size, Size, Size);

        //???????n????????????????????????????
        this.gameObject.transform.position = new Vector3(0, 0+Size/2, -7);

        //?X?e?[?W????????
        Field.transform.position = new Vector3(-500, 0, -500-FieldSpeed);

        if (this.gameObject.transform.localScale.x > 3) End();
    }

    public void End()
    {
        FadeController.isFadeOut = true;
        endtimer += Time.deltaTime;
        if(endtimer > 3) SceneManager.LoadScene("EndScene");
    }

    public void RePosButton()//�ʒu�Ē����{�^���������ꂽ�Ƃ�
    {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
        SnowFirstPos.transform.position = new Vector3(LeftHandTracker.transform.position.x, LeftHandTracker.transform.position.y - 0.45f, LeftHandTracker.transform.position.z + 0.78f);
        timer = 0;
    }
}
