using System;
using System.ComponentModel.DataAnnotations;

namespace ChatServer.ValidationAttributes
{
    /// <summary>
    /// Not empty field validation attribute
    /// </summary>
    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NotEmptyValidationAttribute:ValidationAttribute
    {
        public const string DefaultErrorMessage = "The {0} field must not be empty";
        public NotEmptyValidationAttribute() : base(DefaultErrorMessage) { }

        public override bool IsValid(object value)
        {
            var val = value as string;
            return string.IsNullOrEmpty(val);
        }
    }
}
