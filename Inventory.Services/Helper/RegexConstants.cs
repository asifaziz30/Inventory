using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Helper
{
    public static class RegexConstants
    {
        public const string AlphabetsWithoutSpace = @"^[a-zA-Z]*$";
        public const string AlphabetsWitSpace = @"^[a-zA-Z ]*$";
        public const string AlphabetsUnicodeWitSpace = @"^[a-zA-Z ]*$";
        public const string EnglishArabicLetters = @"^(?=.{2,30}$)[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+(?:\s[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+)?$";
        public const string AlphaNumericWithoutSpace = @"^[a-zA-Z0-9]*$";
        public const string AlphaNumericSpace = @"^[a-zA-Z0-9\s]+$";
        public const string AlphaNumericDash = @"^[a-zA-Z0-9-]*$";
        public const string AlphaNumericSpaceDash = @"^[a-zA-Z0-9- ]*$";
        public const string AlphaNumericSpaceDashUnderScoreAndDot = @"^[a-zA-Z0-9. _—-]*$";
        public const string AlphaNumericSpaceCommaHypenSlash = @"^[a-zA-Z0-9 ,-\\]*$";
        public const string AlphaNumericLimitedSpecialCharecters = @"^[a-zA-Z0-9 _-]*$";
        public const string AddressRemarksAndContents = @"^[A-Za-z0-9,+-._#\/،:|() \r\n\u0600-\u06FF]*$";
        public const string AlphaNumericSpaceAndSpecialCharecters = @"^[a-zA-Z0-9 @#$%&*+\-_(),+':;?.,![\]\s\\/]+$";
        public const string AlphaNumericAndSpecialCharecters = @"^[a-zA-Z0-9 !""#$%&'()*+,-./:;<=>?@[\]^_`{|}~]+$";
        public const string FirstLetterAlphaNumericOnly = @"^[a-zA-Z][a-zA-Z\d]*$";
        public const string PhoneNumber = @"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$";
        public const string MobileNumber = @"^[0-9-+]{7,16}$";
        public const string MobilePhoneNumber = @"^\\+?[1-9][0-9]{7,14}$";
        public const string Alphabet = @"^[a-zA-Z -]*$";
        public const string Numeric = @"^[0-9]*$";
        public const string LongAndLatitudue = @"^\d+(\.\d{1,10})?$";
        public const string Latitude = @"^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$";
        public const string Longitude = @"^(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$";
        public const string Email = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public const string Decimal = @"^\d+(\.\d{1,4})?$";
        public const string Decimal2 = @"^\d+(\.\d{1,2})?$";
        public const string AlphaNumericWithSpecialCharacters = @"^[a-zA-Z0-9 #_()-]*$";
        public const string AlphaNumericWithSpecialCharactersExtended = @"^[a-zA-Z0-9 ,./#_()-]*$";
        public const string AlphaNumericWithUnderScoreAndDashes = @"^[a-zA-Z0-9_-]*$";
        public const string CustomePhoneRegex = @"^[0-9-+]*$";
    }
}
