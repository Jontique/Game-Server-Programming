using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace game_server_course.Validation
{
	class DateValidationModelAttribute : ValidationAttribute //, IClientModelValidator
	{
		//private DateTime dateTime;

		public DateValidationModelAttribute()
		{
		//	dateTime = new DateTime(year, month, day);
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			DateTime dateTime = (DateTime)validationContext.ObjectInstance;
			DateTime today = DateTime.Today;
			if((today - dateTime).TotalSeconds >= 0)
			{
				return ValidationResult.Success;
			}
			else
			{
				return new ValidationResult("Error");
			}
		}
	}
}