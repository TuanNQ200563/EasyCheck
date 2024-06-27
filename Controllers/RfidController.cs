using Microsoft.AspNetCore.Mvc;

public class RfidController : Controller
{
    private readonly IRfidReaderService _rfidReaderService;

    public RfidController(IRfidReaderService rfidReaderService)
    {
        _rfidReaderService = rfidReaderService;
        _rfidReaderService.OnDataReceived += RfidReaderService_OnDataReceived;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult StartListening(string portName)
    {
        _rfidReaderService.StartListening(portName);
        ViewBag.Message = "Started listening on port " + portName;
        return View("Index");
    }

    [HttpPost]
    public IActionResult StopListening()
    {
        _rfidReaderService.StopListening();
        ViewBag.Message = "Stopped listening";
        return View("Index");
    }

    private void RfidReaderService_OnDataReceived(object sender, string data)
    {
        // Handle the data received from RFID reader
        // You can store the data in a database or pass it to the view
        ViewBag.ReceivedData = data;
    }
}
