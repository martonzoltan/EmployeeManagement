namespace Shared.Constants;

public static class ConstantList
{
    /// <summary>
    /// Gets constant string for email validator regex
    /// </summary>
    public const string EmailValidatorRegex =
        @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
}