namespace Customer.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult> GetCustomerByUsernameAsync(string userName);

        Task<IResult> GetCustomersAsync();
    }
}
