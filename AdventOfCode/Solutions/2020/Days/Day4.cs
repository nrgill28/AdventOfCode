using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    [AoCSolution(Year = 2020, Day = 4, Name = "Passport Processing")]
    public class AoC2020Day4 : AoCSolution
    {
        private static readonly Regex PassportRegex = new(@"(?:([a-z]{3}):([^ ]+)\s?)", RegexOptions.Compiled);
        private static readonly Regex HairColorRegex = new(@"#[0-9a-f]{6}", RegexOptions.Compiled);
        private static readonly string[] ValidEyeColors = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
        private static List<Passport> _passports = new();

        public override void Setup()
        {
            foreach (var passport in Input.Split("\n\n"))
                _passports.Add(new Passport(passport.Replace('\n', ' ')));
        }

        public override object RunPart1()
        {
            return _passports.Count(x => x.IsComplete);
        }

        public override object RunPart2()
        {
            return _passports.Count(x => x.IsValid);
        }

        private class Passport
        {
            public int? BirthYear { get; }
            public int? IssueYear { get; }
            public int? ExpirationYear { get; }
            public string Height { get; }
            public string HairColor { get; }
            public string EyeColor { get; }
            public string PassportId { get; }
            public string CountryId { get; }

            public bool IsComplete =>
                BirthYear.HasValue && IssueYear.HasValue &&
                ExpirationYear.HasValue && !string.IsNullOrEmpty(Height) &&
                !string.IsNullOrEmpty(HairColor) && !string.IsNullOrEmpty(EyeColor) &&
                !string.IsNullOrEmpty(PassportId);

            public bool IsValid
            {
                get
                {
                    if (!IsComplete) return false;

                    var birthYearValid = BirthYear >= 1920 && BirthYear <= 2002;
                    var issueYearValid = IssueYear >= 2010 && IssueYear <= 2020;
                    var expirationYearValid = ExpirationYear >= 2020 && ExpirationYear <= 2030;

                    var heightValid = false;
                    if (Height.EndsWith("cm") || Height.EndsWith("in"))
                    {
                        var height = int.Parse(Height.Substring(0, Height.Length - 2));
                        heightValid = Height.EndsWith("cm")
                            ? height >= 150 && height <= 193
                            : height >= 56 && height <= 76;
                    }

                    var hairColorValid = HairColorRegex.IsMatch(HairColor);
                    var eyeColorValid = ValidEyeColors.Contains(EyeColor);
                    var passportIdValid = PassportId.Length == 9;

                    return birthYearValid && issueYearValid && expirationYearValid && heightValid && hairColorValid &&
                           eyeColorValid && passportIdValid;
                }
            }

            public Passport(string inp)
            {
                foreach (Match match in PassportRegex.Matches(inp))
                {
                    var value = match.Groups[2].Value;
                    switch (match.Groups[1].Value)
                    {
                        case "byr":
                            BirthYear = int.Parse(value);
                            break;
                        case "iyr":
                            IssueYear = int.Parse(value);
                            break;
                        case "eyr":
                            ExpirationYear = int.Parse(value);
                            break;
                        case "hgt":
                            Height = value;
                            break;
                        case "hcl":
                            HairColor = value;
                            break;
                        case "ecl":
                            EyeColor = value;
                            break;
                        case "pid":
                            PassportId = value;
                            break;
                        case "cid":
                            CountryId = value;
                            break;
                    }
                }
            }
        }
    }
}