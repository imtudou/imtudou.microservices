using Imtudou.Core.Data;

using SqlSugar;

using System.Security.Principal;

namespace heimawebapi.Entity
{
    /// <summary>
    /// 秒杀优惠券
    /// </summary>
    //实体与数据库结构一样
    [SugarTable("tb_speckill_voucher")]
    public class tbSpecillVoucherEntity : IKey<string>
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public string Id { get; set; }

        [SugarColumn(ColumnName = "voucherid")]
        public int VoucherID { get; set; }

        [SugarColumn(ColumnName = "stock")]
        public int Stock { get; set; }

        [SugarColumn(ColumnName = "begintime")]
        public DateTime? BeginTime { get; set; }

        [SugarColumn(ColumnName = "endtime")]
        public DateTime? EndTime { get; set; }

        [SugarColumn(ColumnName = "create_time")]
        public DateTime? CreateTime { get; set; }

        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }
    }
}
