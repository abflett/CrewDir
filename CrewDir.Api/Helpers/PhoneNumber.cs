namespace CrewDir.Api.Helpers
{
    public static class PhoneNumber
    {
        public static string FormatPhoneNumberForDisplay(string phoneNumber)
        {
            string cleanedPhoneNumber = new(phoneNumber.Where(char.IsDigit).ToArray());
            string maskFormat = "(XXX) XXX - XXXX";
            string formattedPhoneNumber = string.Empty;
            int maskIndex = maskFormat.Length - 1;
            for (int i = cleanedPhoneNumber.Length - 1; i >= 0; i--)
            {
                char digit = cleanedPhoneNumber[i];

                while (maskIndex >= 0 && maskFormat[maskIndex] != 'X')
                {
                    formattedPhoneNumber = maskFormat[maskIndex] + formattedPhoneNumber;
                    maskIndex--;
                }

                formattedPhoneNumber = digit + formattedPhoneNumber;
                maskIndex--;
            }
            while (maskIndex >= 0)
            {
                formattedPhoneNumber = maskFormat[maskIndex] + formattedPhoneNumber;
                maskIndex--;
            }
            return formattedPhoneNumber;
        }

        public static string FormatPhoneNumberForStorage(string phoneNumber)
        {
            return new string(phoneNumber.Where(char.IsDigit).ToArray());
        }
    }
}
