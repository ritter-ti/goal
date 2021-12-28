using System.Threading.Tasks;
using Goal.Application.Services;
using Goal.DemoCqrsCqrs.Application.DTO.People.Requests;
using Goal.DemoCqrsCqrs.Application.DTO.People.Responses;
using Goal.Infra.Crosscutting.Collections;

namespace Goal.DemoCqrsCqrs.Application.People
{
    public interface IPersonAppService : IAppService
    {
        Task<PersonResponse> AddPerson(AddPersonRequest request);
        Task<PersonResponse> UpdatePerson(string id, UpdatePersonRequest request);
        Task<bool> DeletePerson(string id);
        Task<IPagedCollection<PersonResponse>> FindPaginatedAsync(Pagination pagination);
        Task<PersonResponse> GetPersonAsync(string id);
    }
}
