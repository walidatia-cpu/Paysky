using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;

namespace EmploymentSystem.Core.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileVerifyExtensionsAttribute : ValidationAttribute
    {
        private List<string> AllowedExtensions { get; set; }

        public FileVerifyExtensionsAttribute(string fileExtensions)
        {
            AllowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            //IFormFile file = value as IFormFile;

            //if (file != null)
            //{
            //    var fileName = file.FileName;

            //    if (!AllowedExtensions.Any(y => fileName.EndsWith(y)))
            //        return new ValidationResult(ErrorMessage);
            //}
            return ValidationResult.Success;
        }
    }
}
