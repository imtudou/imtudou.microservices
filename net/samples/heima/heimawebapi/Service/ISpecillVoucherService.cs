using heimawebapi.Entity;

namespace heimawebapi.Service
{
    public interface ISpecillVoucherService
    {
        tbSpecillVoucherEntity GetSpecillVoucher(string voucherid);

        bool EditStock(string voucherid);

    }
}                                                           
