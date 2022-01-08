using System;
namespace Comdis.Helpers
{
    public class CounterHelper
    {

        public static decimal CalculatePercentage(string pValue)
        {
            decimal value = 0;

            var taxvalue = Decimal.TryParse(pValue, out  value);

            
                return value  / 100;
          

        }

        public CounterHelper()
        {
        }
    }
}
