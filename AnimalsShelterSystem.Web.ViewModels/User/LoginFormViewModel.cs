﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.ViewModels.User
{
	public class LoginFormViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;


		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }

	}
}
