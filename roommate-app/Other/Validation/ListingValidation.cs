using System;
using System.Text.RegularExpressions;
using System.Globalization;
using roommate_app.Models;
namespace roommate_app.Other.Validation;
public static class ValidationExtensions
{
    public static bool ValidateName(this Listing str)
    {
        if (string.IsNullOrEmpty(str.FullName()))
        {
            return false;
        }

        if (str.FullName().Length > 61)
        {
            return false;
        }

        string regExp = "/^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u";

        if (!Regex.IsMatch(str.FullName(), regExp))
        {
            return false;
        }

        return true;
    }

    public static bool ValidateEmail(this Listing? str)
    {
        if (string.IsNullOrEmpty(str.Email))
        {
            return false;
        }

        string regExp = "^[A-Za-z0-9-.]+@([A-Za-z-]+.)+[A-Za-z-]{2,4}$";
        if (!Regex.IsMatch(str.Email, regExp))
        {
            return false;
        }

        return true;
    }

    public static bool ValidatePassword(this Listing? str)
    {
        if (string.IsNullOrEmpty(str.FirstName))
        {
            return false;
        }

        if (str.FirstName.Length > 20 && str.FirstName.Length < 6)
        {
            return false;
        }

        string regExp = "^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$";

        if (!Regex.IsMatch(str.FirstName, regExp))
        {
            return false;
        }

        return true;
    }

    public static bool ValidateCity(this Listing? str)
    {
        if (string.IsNullOrEmpty(str.City))
        {
            return false;
        }

        if (str.City.Length > 30)
        {
            return false;
        }

        string regExp = "^[A-Za-z]+$";

        if (!Regex.IsMatch(str.City, regExp))
        {
            return false;
        }

        return true;
    }

    public static bool ValidateExtraComment(this Listing? str)
    {
        if (string.IsNullOrEmpty(str.ExtraComment))
        {
            return false;
        }

        if (str.ExtraComment.Length > 200)
        {
            return false;
        }

        return true;
    }

    public static bool ValidateRoommateCount(this Listing? str)
    {
        if (string.IsNullOrEmpty(str.RoommateCount))
        {
            return false;
        }

        if (str.RoommateCount.Length > 1)
        {
            return false;
        }

        return true;
    }

}