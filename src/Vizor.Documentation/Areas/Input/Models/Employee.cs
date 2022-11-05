using FluentValidation;

namespace Vizor.Documentation.Areas.Input.Models;

public class Employee
{
	public string FirstName { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public DateTime DateOfBirth { get; set; }

	public bool IsAdmin { get; set; }

	public string Role { get; set; } = string.Empty;

	public decimal Salary { get; set; }

	public string? PhoneNumber { get; set; }

	public int TravelDistance { get; set; }

	public Address Address { get; set; } = new Address();

	public string? Remarks { get; set; }
}

public class EmployeeValidator : AbstractValidator<Employee>
{
	public EmployeeValidator()
	{
		RuleFor(e => e.FirstName).NotEmpty().MaximumLength(64);
		RuleFor(e => e.LastName).NotEmpty().MaximumLength(64);
		RuleFor(e => e.Email).NotEmpty().EmailAddress().MaximumLength(128);
		RuleFor(e => e.Password).MinimumLength(8).MaximumLength(64);
		RuleFor(e => e.DateOfBirth).Must(d => d.Year < 2000).WithMessage("Date of birth must be before 1-1-2000");
		RuleFor(e => e.Role).NotEmpty();

		//TODO: validate phone number if not null

		RuleFor(e => e.Address.StreetAndNumber).NotEmpty().MaximumLength(64).When(x => x != null);
		RuleFor(e => e.Address.City).NotEmpty().MaximumLength(64).When(x => x != null);
		RuleFor(e => e.Address.PostalCode).NotEmpty().MaximumLength(16).When(x => x != null);
		RuleFor(e => e.Address.CountryCode).NotEmpty().MaximumLength(3).When(x => x != null);
	}
}