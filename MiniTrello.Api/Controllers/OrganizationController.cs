using System.Net.Http;
using System.Web;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using MiniTrello.Api.Models;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;
using System.Collections.Generic;

using System.Linq;
namespace MiniTrello.Api.Controllers
{
    public class OrganizationController : ApiController
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;

        public OrganizationController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository, IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }
        [AcceptVerbs("POST")]
        [POST("createOrganization/{accesstoken}")]
        public ResponseMessageModel CreateOrganization([FromBody] CreateOrganizationModel model, string accesstoken)
        {

            var account =
                _readOnlyRepository.First<Account>(
                    account1 => account1.Token == accesstoken);

            Organization orga = _mappingEngine.Map<CreateOrganizationModel, Organization>(model);
            Organization orgaCreated = _writeOnlyRepository.Create(orga);

            if (orgaCreated != null)
            {
                account.AddOrganization(orgaCreated);
                return new ResponseMessageModel { Message = "Se ha creado una organizacion exitosamente" };
            }

            return new ResponseMessageModel { Message = "No se ha creado una organizacion exitosamente" };
        }

        [GET("organizations/{accessToken}")]
        public List<OrganizationModel> GetAllForUser(string accessToken)
        {
               var account =
              _readOnlyRepository.First<Account>(
                  account1 => account1.Token == accessToken);
            var mappedOrganizationModelList = _mappingEngine.Map<IEnumerable<Organization>,IEnumerable<
            OrganizationModel >> (account.Organizations).ToList();
            return mappedOrganizationModelList;
           
        }

        [AcceptVerbs("PUT")]
        [PUT("deleteOrganization/{id}/{accessToken}")]
        public OrganizationModel DeleteOrganization(long id, string accessToken)
        {
            var organization = _readOnlyRepository.GetById<Organization>(id);
            var archivedOrganization = _writeOnlyRepository.Archive(organization);
            return _mappingEngine.Map<Organization, OrganizationModel>(archivedOrganization);
        }

        [GET("organization/{accessToken}/{organizationId}")]
        public OrganizationModel GetById(string accessToken, long organizationId)
        {
            var organization = _readOnlyRepository.GetById<Organization>(organizationId);
            return _mappingEngine.Map<Organization, OrganizationModel>(organization);
        }

        [AcceptVerbs("PUT")]
        [PUT("organization/name/{accessToken}")]
        public OrganizationModel UpdateName(string accessToken, [FromBody] OrganizationUpdateNameModel model)
        {
            var organization = _readOnlyRepository.GetById<Organization>(model.Id);
            organization.Name = model.NewName;
            organization = _writeOnlyRepository.Update(organization);
            return _mappingEngine.Map<Organization, OrganizationModel>(organization);
        }

      

    }

    public class OrganizationUpdateNameModel
    {
        public long Id { get; set; }
        public string NewName { get; set; }
    }

    public class ResponseMessageModel
    {
        public string Message { get; set; }
    }

    public class CreateOrganizationModel
    {
        public string  Name{ get; set; }
        public string Description { get; set; }
    }
}