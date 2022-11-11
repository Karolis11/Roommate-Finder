using roommate_app.Models;
using System.Text.RegularExpressions;

namespace roommate_app.Other.Validation;
public static class ValidationExtensions
{
    public static bool ValidateName(this Listing str)
    {
        string regExp = "/^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u";

        return string.IsNullOrWhiteSpace(str.FullName()) && str.FullName().Length > 61 && !Regex.IsMatch(str.FullName(), regExp);
    }

    public static bool ValidateEmail(this Listing? str)
    {
        string regExp = "^[A-Za-z0-9-.]+@([A-Za-z-]+.)+[A-Za-z-]{2,4}$";

        return string.IsNullOrWhiteSpace(str.Email) && !Regex.IsMatch(str.Email, regExp);
    }

    public static bool ValidateCity(this Listing? str)
    {
        string regExp = "^[A-Za-z]+$";

        return string.IsNullOrWhiteSpace(str.City) && str.City.Length > 30 && !Regex.IsMatch(str.City, regExp);
    }

    public static bool ValidateRoommateCount(this Listing? number)
    {
        return number.RoommateCount == null && number.RoommateCount >= 1 && number.RoommateCount <= 3; ;
    }

    public static bool ValidatePhoneNumber(this Listing? str)
    {
        string regExp1 = "^+370?[1-9][0-9]{7,14}$"; // +370 validation

        string regExp2 = "^86?[1-9][0-9]{7,14}$"; // 86 validation

        return string.IsNullOrWhiteSpace(str.Phone) && (str.Phone.Length != 9 && str.Phone.Length != 12)
               && (!Regex.IsMatch(str.City, regExp1) || !Regex.IsMatch(str.City, regExp2));
    }
    public static bool ValidateExtraComment(this Listing? str)
    {
        List<Func<Listing, bool>> ValidationRules = new List<Func<Listing, bool>>
        {
            x => string.IsNullOrWhiteSpace(str.ExtraComment),
            x => str.ExtraComment.Length > 200
        };
        return ValidationRules.All(x => x(str) == false);
    }
    public static bool ValidateMaximumPrice(this Listing? number)
    {
        List<Func<Listing, bool>> ValidationRules = new List<Func<Listing, bool>>
        {
            x => number.MaxPrice == null,
            x => number.MaxPrice > 999999 && number.MaxPrice < 0
        };
        return ValidationRules.All(x => x(number) == false);
    }

}