namespace Goal.DemoCqrsCqrs.Application.DTO.People.Requests.Validators
{
    public sealed class AddPersonRequestValidator : PersonRequestValidator<AddPersonRequest>
    {
        public AddPersonRequestValidator()
        {
            ValidateFistName();
            ValidateLastName();
            ValidateCpf();
        }
    }
}
