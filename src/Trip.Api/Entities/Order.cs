using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stateless;

namespace Trip.Api.Entities;

/// <summary>
/// 订单实体
/// </summary>
public class Order
{
    private StateMachine<OrderState, OrderStateTrigger> _stateMachine;

    public Order()
    {
        // 实例化一次订单，生成一个订单对应的状态机
        StateMachineInit();
    }

    [Key]
    public Guid Id { get; set; }

    [ForeignKey("AppUser")]
    public string? UserId { get; set; }

    public AppUser? AppUser { get; set; }

    public ICollection<CartLineItem>? OrderItems { get; set; }

    public OrderState OrderState { get; set; }

    public DateTime CreateTimeUtc { get; set; }

    public string? TransactionMetadata { get; set; }

    /// <summary>
    /// 状态机初始化
    /// </summary>
    private void StateMachineInit()
    {
        _stateMachine = new StateMachine<OrderState, OrderStateTrigger>(OrderState.Pending);

        _stateMachine.Configure(OrderState.Pending)
            .Permit(OrderStateTrigger.PlaceOrder, OrderState.Processing)
            .Permit(OrderStateTrigger.Cancel, OrderState.Canceled);

        _stateMachine.Configure(OrderState.Processing)
            .Permit(OrderStateTrigger.Approve, OrderState.Completed)
            .Permit(OrderStateTrigger.Reject, OrderState.Declined);

        _stateMachine.Configure(OrderState.Declined)
            .Permit(OrderStateTrigger.PlaceOrder, OrderState.Processing);

        _stateMachine.Configure(OrderState.Completed)
            .Permit(OrderStateTrigger.Return, OrderState.Refund);
    }
}