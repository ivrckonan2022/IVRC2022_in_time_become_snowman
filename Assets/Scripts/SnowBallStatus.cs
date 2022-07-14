using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ⴞ��܂̃T�C�Y�Ɖ�]�𑀍삷��X�N���v�g
/// </summary>
/// 
public class SnowBallStatus : MonoBehaviour
{
    public float Speed;//��]���x
    public float FieldSpeed;//�n�ʂ��������x
    public float Size;//��ʃT�C�Y

    public GameObject Field;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 0;
        FieldSpeed = 0;
        Size = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Size += Speed * 0.001f;
        FieldSpeed += Speed *0.02f;
        this.gameObject.transform.Rotate(new Vector3(Speed, 0, 0));
        this.gameObject.transform.localScale = new Vector3(Size, Size, Size);

        //��ʂ��n�ʂɖ��܂�Ȃ��悤�ɂ��邽��
        this.gameObject.transform.position = new Vector3(0, 0+Size/2, -7);

        //�X�e�[�W�𓮂���
        Field.transform.position = new Vector3(-500, 0, -500-FieldSpeed);
    }
}
