namespace Trip.Api.Entities;

/// <summary>
/// 订单状态
/// </summary>
public enum OrderState
{
    Pending, // 订单已生成
    Processing, // 支付处理中
    Completed, // 交易成功
    Declined, // 交易失败
    Canceled, // 订单取消
    Refund // 已退款
}