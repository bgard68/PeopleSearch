using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Interface;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
    public class Person : IPerson
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }

        [StringLength(1, MinimumLength = 1, ErrorMessage = "MI must be exactly one character.")]
        [RegularExpression(@"^[A-Za-z]{1}$", ErrorMessage = "MI must be a single letter.")]
        public string MI { get; set; }

        public string LastName { get; set; }

        private string _phoneNumber;
        [RegularExpression(@"^\d{10}$", ErrorMessage = "PhoneNumber must be exactly 10 digits.")]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                var sanitized = SanitizePhone(value);
                _phoneNumber = sanitized?.Length == 10 ? sanitized : null;
            }
        }

        private string _cellNumber;
        [RegularExpression(@"^\d{10}$", ErrorMessage = "CellNumber must be exactly 10 digits.")]
        public string? CellNumber
        {
            get => _cellNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _cellNumber = null;
                }
                else
                {
                    var sanitized = SanitizePhone(value);
                    _cellNumber = sanitized?.Length == 10 ? sanitized : null;
                }
            }
        }

        public string Email { get; set; }
        
        public int AddressId { get; set; } // Foreign key
        public Address Address { get; set; } // Navigation property

        private static string SanitizePhone(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            // Remove all non-digit characters
            return Regex.Replace(input, @"\D", "");
        }
    }
}

