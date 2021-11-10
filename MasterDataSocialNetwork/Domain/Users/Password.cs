
using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{

    public class Password : IValueObject
    {

        /// <summary> The password itself. Represents the password string with no encryption </summary>
        public string Value { get; set; }

        /// <summary> An integer from 0-4 representing the password strength</summary>
        public int Strength { get; private set; }

        public Password() { }

        public Password(string password)
        {
            if (password == null)
            {
                throw new BusinessRuleValidationException("The password cannot be null");
            }
            try
            {
                var result = Zxcvbn.Core.EvaluatePassword(password);
                // If the password is weak, it returns feedback to help to improve it.
                if (result.Score <= 2)
                {
                    throw new BusinessRuleValidationException(result.Feedback.Warning);
                }
                else
                {
                    this.Value = password;
                    this.Strength = result.Score;
                }
            }
            catch (Exception)
            {
                throw new Exception("The provided password is invalid");
            }
        }

    }
}