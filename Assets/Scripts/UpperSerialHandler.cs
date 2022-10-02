using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class UpperSerialHandler : MonoBehaviour
{
    const string portName = "COM7"; // �|�[�g�ԍ��͎����Ŋm�F���Ă�������
    const int baudRate = 9600;
    public SerialPort serialPort_ = new SerialPort(portName, baudRate);

    void Start()
    {

        serialPort_.Open();
    }


}