using System.Collections;
using UnityEngine;
using System.IO.Ports;

public class RotateWheel : MonoBehaviour
{
    //serial communication variables
    const string portName = "COM2"; // �|�[�g�ԍ��͎����Ŋm�F���Ă�������
    const int baudRate = 9600;
    SerialPort serialPort_;
    // Variable for speed calculation
    [SerializeField] float resetSpeedTime = 0.5f;
    float previousRotateCount = 0;
    float inputRotateCount = 0;
    public float speed = 0;
    private void Start()
    {
        Debug.Log("0"); 
        serialPort_ = new SerialPort(portName, baudRate);
        Debug.Log("start!!");
        serialPort_.Open();
        Debug.Log(serialPort_);
        
        StartCoroutine(SpeedChecker(resetSpeedTime));
    }
    // Update is called once per frame
    void Update()
    {
        if (serialPort_.IsOpen)
        {
            float.TryParse(serialPort_.ReadLine(), out inputRotateCount);
            Debug.Log(inputRotateCount);
        }
    }
    IEnumerator SpeedChecker(float waitTime = 1)
    {
        while (true)
        {
            Debug.Log("inputRotateCount : " + inputRotateCount);
            Debug.Log("previousRotateCount : " + previousRotateCount);
            speed = ((inputRotateCount - previousRotateCount) / resetSpeedTime) / 70;
            Debug.Log("speed : " + speed);
            previousRotateCount = inputRotateCount;
            // second�Ŏw�肵���b�����[�v���܂�
            yield return new WaitForSeconds(waitTime);
        }
    }
}



