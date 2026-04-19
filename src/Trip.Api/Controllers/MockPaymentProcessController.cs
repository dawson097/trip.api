using Microsoft.AspNetCore.Mvc;

namespace Trip.Api.Controllers;

/// <summary>
/// 模拟订单支付控制器路由
/// </summary>
[ApiController, Route("api/payment-process")]
public class MockPaymentProcessController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostProcessPayment([FromQuery] Guid orderNumber,
        [FromQuery] bool returnFault = false)
    {
        await Task.Delay(3000);

        if (returnFault)
        {
            return Ok(new
            {
                id = Guid.NewGuid(),
                created = DateTime.UtcNow,
                approved = false,
                meessage = "Reject",
                paymentMethod = "信用卡支付",
                orderNumber,
                card = new
                {
                    cartType = "信用卡",
                    lastFour = "1234"
                }
            });
        }

        return Ok(new
        {
            id = Guid.NewGuid(),
            created = DateTime.UtcNow,
            approved = true,
            meessage = "Reject",
            paymentMethod = "信用卡支付",
            orderNumber,
            card = new
            {
                cartType = "信用卡",
                lastFour = "1234"
            }
        });
    }
}