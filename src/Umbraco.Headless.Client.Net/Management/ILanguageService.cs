using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAll();
        Task<Language> GetByIsoCode(string isoCode);
        Task<Language> Create(Language language);
        Task<Language> Update(string isoCode, Language language);
        Task<Language> Delete(string isoCode);
    }
}
