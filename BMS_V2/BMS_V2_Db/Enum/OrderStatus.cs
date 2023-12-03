namespace BMS_V2_Db.Enum;

public enum OrderStatus
{
    未接单=0,
    已接单=1,
    已估价=2,
    确认接单=3,
    已完成待发送快递 =4,
    快递已发出=5,
    订单完成=6,
    订单拒绝=-1
}