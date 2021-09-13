using System.Threading.Tasks;

namespace FormulaManager.DAL.Entities.Services
{
    public interface IUserAdministrationService
    {
        public Task<bool> IsExist(string username, string email);

        public Task<bool> IsConfirmed(string username);
    }
}