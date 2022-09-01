using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

/// <summary>
/// ???????????T?C?Y?????]???????????X?N???v?g
/// </summary>
/// 
public class SnowBallStatus : MonoBehaviour
{
    public float Speed;//???]???x
    public float FieldSpeed;//?n???????????x
    public static float Size;//?????T?C?Y

    public GameObject SnowBallTracker;
    public GameObject LeftHandTracker;
    public GameObject SnowFirstPos;
    public GameObject Field;

    public Text message1;
    public Text message2;

    float timer = 0;
    float endtimer = 0;

    public RotateWheel rotateWheel;
    public SerialHandler serialHandler;
    public UpperSerialHandler upperSerialHandler;

    //�|�[�g�悩��擾�������[�e�[�V�����̉�]����������ϐ�
    float inputRotateCount = 0;
    //inputRotateCount���ꎞ�I�ɕۑ����Ă����ׂ̈ꎞ�I�ȕϐ�
    float previousRotateCount = 0;
    // �X�s�[�h���o�͂����
    public float rotateSpeed = 0;
    //���Ԃ̌o�ߎ��Ԃ��v������^�C�~���O�����b�J���邩���w�肵�Ă�����
    [SerializeField] float waitTime = 0.5f;

    private int isJack= 0;

    // Start is called before the first frame update
    void Start()
    {
        FadeController.isFadeIn = true;
        Speed = 0;
        FieldSpeed = 0;
        Size = 1.65f;
        
        //�M������M�����Ƃ��ɁA���̃��b�Z�[�W�̏������s��
        serialHandler.OnDataReceived += OnDataReceived;
        upperSerialHandler.serialPort_.Write("d");
    }

    // Update is called once per frame
    void Update()
    {
        //�g���b�J�[�̈ʒu�ɐ�ʂ������Ă���ver
        //this.gameObject.transform.position = SnowBallTracker.transform.position;


        if (Input.GetKeyDown(KeyCode.U))
        {

            upperSerialHandler.serialPort_.Write("u");

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            upperSerialHandler.serialPort_.Write("d");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            upperSerialHandler.serialPort_.Write("s");
        }

        
        if (FieldSpeed <=1){
            upperSerialHandler.serialPort_.Write("d");
        }

        if (FieldSpeed >= 5 && isJack == 0) {
            isJack = 1;
            upperSerialHandler.serialPort_.Write("u");

            DOVirtual.DelayedCall(3.0f, () => {
                upperSerialHandler.serialPort_.Write("d");
            });

        }
        else if (FieldSpeed >= 15 && isJack == 1)
        {
            isJack = 2;
            upperSerialHandler.serialPort_.Write("u");

            DOVirtual.DelayedCall(3.0f, () => {
                upperSerialHandler.serialPort_.Write("d");
            });

        }
        else if (FieldSpeed >= 20 && isJack == 2)
        {
            isJack = 3;
            upperSerialHandler.serialPort_.Write("u");

            DOVirtual.DelayedCall(3.0f, () => {
                upperSerialHandler.serialPort_.Write("d");
            });

        }

        //��̈ʒu�����ʂ̈ʒu�����߂�ver
        if (timer < 10)
        {
            message1.text = "�r��L�΂�����ԂŁA\n���ʂ̐�ʂ�������ʒu�Ɉړ����Ă��������B";
            message2.text = "�ʒu������...";
            timer += Time.deltaTime;
            SnowFirstPos.transform.position = new Vector3(LeftHandTracker.transform.position.x, LeftHandTracker.transform.position.y - 0.45f, LeftHandTracker.transform.position.z + 0.85f);
        }
        else
        {
            message1.text = "";
            message2.text = "";
            this.gameObject.transform.position = SnowFirstPos.transform.position;

            //speed����
            
            if (rotateSpeed == 0)
            {
                Speed = 0;
            }
            else
            {
                Speed = rotateSpeed + 2f;
                //message1.text = Speed.ToString();
            }
        }
        
        
        Size += Speed * 0.001f;
        FieldSpeed += Speed *0.02f;
        this.gameObject.transform.Rotate(new Vector3(Speed, 0, 0));
        this.gameObject.transform.localScale = new Vector3(Size, Size, Size);

        //???????n????????????????????????????
        //this.gameObject.transform.position = new Vector3(0, 0+Size/2, -7);

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


    //��M�����M��(message)�ɑ΂��鏈��
    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] { "\n" }, System.StringSplitOptions.None);
        
        try
        {
            inputRotateCount = float.Parse( data[0]);
            //�X�s�[�h�����߂�
            //inputRotateCount(����̉�]��) - previousRotateCount(�O��̉�]��)�ŉ�]�������������߂�
            //��]���𑬓x���v�Z����܂ł̑ҋ@����(waitTime)�Ŋ��邱�ƂŁA���x�����߂�B
            rotateSpeed = ((inputRotateCount - previousRotateCount) / waitTime) / 70;
            //���̉�]��������Ɍv�Z�ł���悤�ɂ���ׂ�previousRotateCount�֕ۑ�����
            previousRotateCount = inputRotateCount;
            // waitTime�Ŏw�肵���b�����[�v���܂�
            //Debug.Log(rotateSpeed);//Unity�̃R���\�[���Ɏ�M�f�[�^��\��
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//�G���[��\��
        }
    }
}
