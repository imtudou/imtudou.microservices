using heimawebapi.Entity;

using Imtudou.Core.CommonEnum;
using Imtudou.Core.Data.SqlSugar;
using Imtudou.Core.Data;
using Imtudou.Core.Helpers;

namespace heimawebapi.Service
{
    public class VoucherOrderService : IVoucherOrderService
    {
        private readonly ISqlSugarRepository<tbVoucherOrderEntity, int> _voucherOrderRepository;

        public VoucherOrderService(ISqlSugarRepository<tbVoucherOrderEntity, int> voucherOrderRepository)
        {
            _voucherOrderRepository = voucherOrderRepository.Instance(new SqlOptions(AppSettingsHelper.Configuration.GetConnectionString("mysql"), DataBaseTypeEnum.MySql)) as ISqlSugarRepository<tbVoucherOrderEntity, int>;
        }

        public bool AddVoucherOrder(tbVoucherOrderEntity input)
        {
            var result = _voucherOrderRepository.Insert(input);
            return result > 0;
        }
    }
}
