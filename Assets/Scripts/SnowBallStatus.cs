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

    //�|�[�g�悩��擾�������[�e�[�V�����̉�]����������ϐ�
    float inputRotateCount = 0;
    //inputRotateCount���ꎞ�I�ɕۑ����Ă����ׂ̈ꎞ�I�ȕϐ�
    float previousRotateCount = 0;
    // �X�s�[�h���o�͂����
    public float rotateSpeed = 0;
    //���Ԃ̌o�ߎ��Ԃ��v������^�C�~���O�����b�J���邩���w�肵�Ă�����
    [SerializeField] float waitTime = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        FadeController.isFadeIn = true;
        Speed = 0;
        FieldSpeed = 0;
        Size = 1.65f;

        //�M������M�����Ƃ��ɁA���̃��b�Z�[�W�̏������s��
        serialHandler.OnDataReceived += OnDataReceived;
    }

    // Update is called once per frame
    void Update()
    {
        //�g���b�J�[�̈ʒu�ɐ�ʂ������Ă���ver
        //this.gameObject.transform.position = SnowBallTracker.transform.position;


        if (Input.GetKey(KeyCode.UpArrow))
        {
            serialHandler.Write("u");
            Debug.Log("aaaa");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            serialHandler.Write("d");
        }
        else {
            serialHandler.Write("s");
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
