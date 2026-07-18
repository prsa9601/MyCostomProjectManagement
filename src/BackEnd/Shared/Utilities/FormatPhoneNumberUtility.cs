namespace BackEnd.Shared.Utilities
{
    public static class FormatPhoneNumberUtility
    {
        public static string EnsureLeadingZero(this string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return phoneNumber.Replace(" ", "");

            return phoneNumber.StartsWith("0") ? phoneNumber.Replace(" ", "") : "0" + phoneNumber.Replace(" ","");
        }
    }
}
