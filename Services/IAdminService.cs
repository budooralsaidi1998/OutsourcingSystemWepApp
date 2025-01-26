using OutsourcingSystemWepApp.Data.DTOs;

namespace OutsourcingSystemWepApp.Services
{
    public interface IAdminService
    {
        Task<bool> RegisterAdmin(AdminRegisterDto adminDto);
    }
}