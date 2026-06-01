using AgroERP.Application.DTOs.Retailer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Validators.Retailer;

public class CreateRetailerValidator: AbstractValidator<CreateRetailerDto>
{
    public CreateRetailerValidator()
    {
        RuleFor(x => x.ShopName)
            .NotEmpty()
            .WithMessage(
                "Shop name is required")
            .MaximumLength(100);

        RuleFor(x => x.OwnerName)
            .NotEmpty()
            .WithMessage(
                "Owner name is required")
            .MaximumLength(100);

        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage(
                "Mobile number is required")
            .Matches(@"^[0-9]{10}$")
            .WithMessage(
                "Mobile number must be 10 digits");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(
                "Address is required");

        RuleFor(x => x.CreditLimit)
            .GreaterThanOrEqualTo(0)
            .WithMessage(
                "Credit limit cannot be negative");

        RuleFor(x => x.DueAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage(
                "Due amount cannot be negative");
    }
}
//namespace AgroERP.Application.Validators.Retailer
//{
//    public class CreateRetailerValidator : AbstractValidator<CreateRetailerDto>
//    {
//        public CreateRetailerValidator()
//        {
//            RuleFor(x => x.ShopName)
//                .NotEmpty()
//                .WithMessage("Shop name is required")
//                .MaximumLength(100);

//            RuleFor(x => x.OwnerName)
//                .NotEmpty()
//                .WithMessage("Owner name is required")
//                .MaximumLength(100);

//            RuleFor(x => x.MobileNumber)
//                .NotEmpty()
//                .WithMessage("Mobile number is required")
//                .Matches(@"^[0-9]{10}$")
//                .WithMessage("Mobile number must be 10 digits");

//            RuleFor(x => x.Address)
//                .NotEmpty()
//                .WithMessage("Address is required");

//            RuleFor(x => x.CreditLimit)
//                .GreaterThanOrEqualTo(0)
//                .WithMessage("Credit limit cannot be negative");

//            RuleFor(x => x.DueAmount)
//                .GreaterThanOrEqualTo(0)
//                .WithMessage("Due amount cannot be negative");
//        }
//    }
//}
