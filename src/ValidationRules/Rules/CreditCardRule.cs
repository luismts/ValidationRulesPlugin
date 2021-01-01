using Plugin.ValidationRules.Interfaces;
using System.Linq;

namespace Plugin.ValidationRules.Rules
{
    public class CreditCardRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; } = "Credit Card is not valid.";

		public bool Check(string value)
        {
			if (value == null)
			{
				return true;
			}

			value = value.Replace("-", "").Replace(" ", "");

			int checksum = 0;
			bool evenDigit = false;

			// http://www.beachnet.com/~hstiles/cardtype.html
			foreach (char digit in value.ToCharArray().Reverse())
			{
				if (!char.IsDigit(digit))
				{
					return false;
				}

				int digitValue = (digit - '0') * (evenDigit ? 2 : 1);
				evenDigit = !evenDigit;

				while (digitValue > 0)
				{
					checksum += digitValue % 10;
					digitValue /= 10;
				}
			}

			return (checksum % 10) == 0;
		}
    }
}
