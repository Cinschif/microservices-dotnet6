using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNETUdemy.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
  
    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ILogger<CalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("sum/{firstNumber}/{secondNumber}")]
    public IActionResult Sum (string firstNumber, string secondNumber)
    {
        if ( IsNumeric (firstNumber) && IsNumeric (secondNumber))
        {
            var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
            return Ok(sum.ToString());
        }

        return BadRequest("Invalid Input");
    }

    [HttpGet("subtract/{firstNumber}/{secondNumber}")]
    public IActionResult Subtract (string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        {
            var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
            return Ok(sub.ToString());
        }

        return BadRequest("Invalid Input");
    }

    [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
    public IActionResult Multiplication(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        {
            var multi = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
            return Ok(multi.ToString());
        }

        return BadRequest("Invalid Input");
    }

    [HttpGet("division/{firstNumber}/{secondNumber}")]
    public IActionResult Division (string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        {
            var div = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
            return Ok(div.ToString());
        }

        return BadRequest("Invalid Input");
    }

    [HttpGet("mean/{firstNumber}/{secondNumber}")]
    public IActionResult Mean(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        {
            var media = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber))/2;
            return Ok(media.ToString());
        }

        return BadRequest("Invalid Input");
    }
    [HttpGet("squareRoot/{firstNumber}")]
    public IActionResult SquareRoot(string firstNumber)
    {
        if (IsNumeric(firstNumber))
        {
            var root = Math.Sqrt((double)Convert.ToDecimal(firstNumber));
            return Ok(root.ToString());
        }

        return BadRequest("Invalid Input");
    }

    private bool IsNumeric(string strNumber)
    {
        double number;
        bool isNumber = double.TryParse(
            strNumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo,
            out number);
        return isNumber;      
    }
    private decimal ConvertToDecimal(string strNumber)
    {
        decimal decimalValue;
        if(decimal.TryParse(strNumber, out decimalValue))
        {
            return decimalValue;
        }
            return 0;
    }

    
}
