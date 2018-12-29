using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINYHOMEV2
{
    class SerialMessenger
    {
        private SerialPort serialPort;
        private MessageBuilder messageBuilder;
        
        public int BaudRate { get { return serialPort.BaudRate; } }
        public string PortName { get { return serialPort.PortName; } }

        public SerialMessenger(string portName, int baudRate, MessageBuilder messageBuilder)
        {
            if (portName == null)
            {
                throw new ArgumentNullException("portName");

            }

            if (baudRate < 9600)
            {
                throw new ArgumentOutOfRangeException("baudRate");
            }

            if (messageBuilder == null)
            {
                throw new ArgumentNullException("messageBuilder");
            }
            serialPort = new SerialPort();
            serialPort.BaudRate = baudRate;
            serialPort.PortName = portName;

            this.messageBuilder = messageBuilder;
        }
        
        public void Connect()
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
                if (serialPort.IsOpen)
                {
                    serialPort.DiscardInBuffer();
                    serialPort.DiscardOutBuffer();
                }
            }
        }
        
        public void Disconnect()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
        
        public string[] GetAvailablePortNames()
        {
            return SerialPort.GetPortNames();
        }
        
        public bool IsConnected()
        {
            return serialPort.IsOpen;
        }
        
        public bool SendMessage(string message)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write(messageBuilder.MessageBeginMarker + message + messageBuilder.MessageEndMarker);
                return true;
            }
            return false;
        }
        
        public string[] ReadMessages()
        {
            if (serialPort.IsOpen
                && serialPort.BytesToRead > 0)
            {
                string data = serialPort.ReadExisting();
                messageBuilder.Add(data);

                int messageCount = messageBuilder.MessageCount;
                if (messageCount > 0)
                {
                    string[] messages = new string[messageCount];
                    for (int i = 0; i < messageCount; i++)
                    {
                        messages[i] = messageBuilder.GetNextMessage();
                    }
                    return messages;
                }
            }
            return null;
        }
    }
}

