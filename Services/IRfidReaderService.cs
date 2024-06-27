using System;
using System.IO.Ports;

public interface IRfidReaderService
{
    void StartListening(string portName, int baudRate = 9600);
    void StopListening();
    event EventHandler<string> OnDataReceived;
}

public class RfidReaderService : IRfidReaderService, IDisposable
{
    private SerialPort? _serialPort;

    public event EventHandler<string>? OnDataReceived;

    public void StartListening(string portName, int baudRate = 9600)
    {
        _serialPort = new SerialPort(portName, baudRate);
        _serialPort.DataReceived += SerialPort_DataReceived;
        _serialPort.Open();
    }

    public void StopListening()
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.Close();
            _serialPort.DataReceived -= SerialPort_DataReceived;
        }
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            if (_serialPort != null)
            {
                string data = _serialPort.ReadExisting();
                OnDataReceived?.Invoke(this, data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error receiving data: {ex.Message}");
        }
    }

    public void Dispose()
    {
        StopListening();
        _serialPort?.Dispose();
    }
}
