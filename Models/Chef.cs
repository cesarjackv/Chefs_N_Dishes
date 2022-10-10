#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Chef
{
    [Key]
    public int ChefId {get; set;}
    public class DoBAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value1)
        {
            if(value1 == null)
            {
                return true;
            }
            var val = (DateTime)value1;
            return (val.AddYears(18) < DateTime.Now);
        }
    }




    [Required(ErrorMessage = "is required.")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
    [Display(Name = "First Name")]
    public string FirstName {get; set;}

    [Required(ErrorMessage = "is required.")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
    [Display(Name = "Last Name")]
    public string LastName {get; set;}

    [Required(ErrorMessage = "is required.")]
    [DataType(DataType.EmailAddress)]
    public string Email {get; set;}




    public class UnderAge : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext value1)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            DateTime date = (DateTime)value;

            if (date <= DateTime.Now)
            {
                return new ValidationResult("must be over 18");
            }
            return ValidationResult.Success;
        }
    }

    [Required(ErrorMessage = "is required.")]
    [Display(Name = "Date Of Birth")]
    [DataType(DataType.Date)]
    public DateTime DOB {get; set;}




    
    [Required(ErrorMessage = "is required.")]
    [MinLength(8, ErrorMessage = "must be at least 8 characters.")]
    [DataType(DataType.Password)]
    public string Password {get; set;}

    [NotMapped]
    [Required(ErrorMessage = "is required.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "doesn't match password.")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword {get; set;}



    public List<Dish> ChefDishes {get; set;} = new List<Dish> ();





    public DateTime CreatedAt {get; set;} = DateTime.Now;

    public DateTime UpdatedAt {get; set;} = DateTime.Now;
}


