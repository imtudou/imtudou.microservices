using Imtudou.Core.Data;

using SqlSugar;
using System.Security.Principal;

namespace heimawebapi.Entity
{
    /// <summary>
    /// 优惠券-订单
    /// </summary>
    [SugarTable("tb_voucher_order")]
    public class tbVoucherOrderEntity : IKey<int>
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public string UserID { get; set; }

        [SugarColumn(ColumnName = "voucher_id")]
        public string VoucherId { get; set; }

        /// <summary>
        /// 支付方式（1：支付宝；2：微信；3：银联）
        /// </summary>
        [SugarColumn(ColumnName = "pay_type")]
        public string PayType { get; set; }

        /// <summary>
        /// 订单状态（1：未支付；2：已支付）
        /// </summary>
        [SugarColumn(ColumnName = "status")]
        public string Status { get; set; }

        [SugarColumn(ColumnName = "pay_time")]
        public DateTime? PayTime { get; set; }

        [SugarColumn(ColumnName = "use_time")]
        public DateTime? UseTime { get; set; }

        [SugarColumn(ColumnName = "refund_time")]
        public DateTime? Refund_Time { get; set; }

        [SugarColumn(ColumnName = "create_time")]
        public DateTime? CreateTime { get; set; }

        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }

        [SugarColumn(ColumnName = "orderid")]
        public string Orderid { get; set; }

        
    }
}
