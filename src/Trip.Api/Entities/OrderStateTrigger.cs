namespace Trip.Api.Entities;

/// <summary>
/// 订单状态触发器
/// </summary>
public enum OrderStateTrigger
{
    PlaceOrder, // 订单支付
    Approve, // 支付成功
    Reject, // 支付失败
    Cancel, // 取消支付
    Return // 退货
}