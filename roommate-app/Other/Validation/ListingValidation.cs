using roommate_app.Models;
using System.Text.RegularExpressions;

namespace roommate_app.Other.Validation;
public class ListingValidation
{
    public bool ValidateName(Listing? str)
    {
        string regExp1 = "^(?=.{1,40}$)[a-zA-Z]+(?:[-'\\s][a-zA-Z]+)*$";
        string regExp2 = "^[A-Za-z]+(((\\'|\\-|\\.)?([A-Za-z])+))?$";

        return !string.IsNullOrWhiteSpace(str.FirstName) && str.FirstName.Length < 31 && Regex.IsMatch(str.FirstName, regExp1)
            && !string.IsNullOrWhiteSpace(str.LastName) && str.LastName.Length < 31 && Regex.IsMatch(str.LastName, regExp2);
    }

    public bool ValidateEmail(Listing? str)
    {
        string regExp = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";

        return !string.IsNullOrWhiteSpace(str.Email) && Regex.IsMatch(str.Email, regExp);
    }

    public bool ValidateCity(Listing? str)
    {
        string regExp = "^[A-Za-z]+$";

        return !string.IsNullOrWhiteSpace(str.City) && str.City.Length < 31 && Regex.IsMatch(str.City, regExp);
    }

    public bool ValidateRoommateCount(Listing? number)
    {
        return number.RoommateCount > 0 && number.RoommateCount < 4;
    }

    public bool ValidatePhoneNumber(Listing? str)
    {
        string regExp1 = "\\+?370?\\s*\\(?-*\\.*(\\d{3})\\)?\\.*-*\\s*(\\d{2})\\.*-*\\s*(\\d{4})$"; // +370 validation

        string regExp2 = "^86?[1-9][0-9]{7,14}$"; // 86 validation

        return (!string.IsNullOrWhiteSpace(str.Phone)) && (str.Phone.Length == 9 || str.Phone.Length == 12)
            && (Regex.IsMatch(str.Phone, regExp1) || Regex.IsMatch(str.Phone, regExp2));
    }

    public bool ValidateExtraComment(Listing? str)
    {
        return (string.IsNullOrEmpty(str.ExtraComment) || str.ExtraComment.Length < 200);
    }

    public bool ValidateMaximumPrice(Listing? number)
    {
        List<Func<Listing, bool>> ValidationRules = new List<Func<Listing, bool>>
        {
            x => !(number.MaxPrice == 0),
            x => number.MaxPrice < 5001,
            x => number.MaxPrice > 0
        };
        return ValidationRules.All(x => x(number) == true);
    }
}