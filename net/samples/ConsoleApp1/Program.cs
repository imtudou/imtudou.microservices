using Newtonsoft.Json;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SIDataJson());
            Console.WriteLine(testmethold());
            Console.ReadKey();
        }
        public static string testmethold()
        {
            string str = string.Empty;
            try
            {
                str = "try";
                throw new AggregateException(str);
                return str;
            }
            catch (Exception ex)
            {
                str = "catch";
                return str;

            }
            return str;
        }

        public static string SIDataJson()
        {
            var data = new
            {
                PayOrdId = string.Empty,
                OrdStas = string.Empty,
                FeeSumamt = string.Empty,
                OwnPayAmt = string.Empty,
                PsnAcctPay = string.Empty,
                FundPay = string.Empty,
                Deposit = string.Empty,
                ExtData = string.Empty,
                OthFeeAmt = string.Empty
            };

            var cc = JsonConvert.SerializeObject(data);
            return cc;
        }
    }
}