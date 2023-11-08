using heimawebapi.Entity;

using Imtudou.Core.CommonEnum;
using Imtudou.Core.Data;
using Imtudou.Core.Data.SqlSugar;
using Imtudou.Core.Helpers;

using SqlSugar;

namespace heimawebapi.Service
{
    public class SpecillVoucherService : ISpecillVoucherService
    {
        private readonly ISqlSugarRepository<tbSpecillVoucherEntity, string> _specillVoucherRepository;

        public SpecillVoucherService(ISqlSugarRepository<tbSpecillVoucherEntity, string> specillVoucherRepository)
        {
            _specillVoucherRepository = specillVoucherRepository.Instance(new SqlOptions(AppSettingsHelper.Configuration.GetConnectionString("mysql"), DataBaseTypeEnum.MySql)) as ISqlSugarRepository<tbSpecillVoucherEntity, string>;
        }

        public tbSpecillVoucherEntity GetSpecillVoucher(string voucherid)
        {
            var result = _specillVoucherRepository.GetById(voucherid);
            return result;
        }

        public bool EditStock(string voucherid)
        {
            var sql = $"update tb_speckill_voucher set stock = stock-1,update_time='{DateTime.Now}' where voucherid = {voucherid} and stock > 0";
            var result = _specillVoucherRepository.DbContext.Ado.ExecuteCommand(sql);
            return result > 0;
        }
    }
}
