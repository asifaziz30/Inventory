using System.Reflection;
using AutoMapper;
using Inventory.Service.Models;
using Inventory.Service.Models.DTO.Request;
using Inventory.Service.Models.DTO.Response;

namespace Inventory.Service.Infrastructure.Config
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            var dbEntitiesNameSpace = new string[] { "Inventory.Service.Models" };
            var dtoNameSpace = new string[] { "Inventory.Service.Models.DTO.Request", "Inventory.Service.Models.DTO.Response" };
            //"GEXP.Core" 
            var dbEntities = GetTypesInNamespace(Assembly.GetExecutingAssembly(), dbEntitiesNameSpace).ToList();
            var requests = GetTypesInNamespace(Assembly.GetExecutingAssembly(), dtoNameSpace).ToList();
            foreach (Type entity in dbEntities)
            {

                Type createDTo = requests.Where(a => a.Name == "Create" + entity.Name + "Request").FirstOrDefault();
                if (createDTo != null)
                {
                    CreateMap(entity, createDTo).ReverseMap();
                }
                Type createApiDTo = requests.Where(a => a.Name == "Create" + entity.Name + "ApiRequest").FirstOrDefault();
                if (createApiDTo != null)
                {
                    CreateMap(entity, createApiDTo).ReverseMap();
                }
                Type createMobileApiDTo = requests.Where(a => a.Name == "Create" + entity.Name + "MobileApiRequest").FirstOrDefault();
                if (createMobileApiDTo != null)
                {
                    CreateMap(entity, createMobileApiDTo).ReverseMap();
                }
                if (createDTo is not null && createApiDTo is not null)
                {
                    CreateMap(createDTo, createApiDTo).ReverseMap();
                }
                Type updateDTo = requests.Where(a => a.Name == "Update" + entity.Name + "Request").FirstOrDefault();
                if (updateDTo != null)
                {
                    CreateMap(entity, updateDTo).ReverseMap();
                }
                Type updateApiDTo = requests.Where(a => a.Name == "Update" + entity.Name + "ApiRequest").FirstOrDefault();
                if (updateApiDTo != null)
                {
                    CreateMap(entity, updateApiDTo).ReverseMap();
                }
                Type deleteDTo = requests.Where(a => a.Name == "Delete" + entity.Name + "Request").FirstOrDefault();
                if (deleteDTo != null)
                {
                    CreateMap(entity, deleteDTo).ReverseMap();
                }
                Type deleteApiDTo = requests.Where(a => a.Name == "Delete" + entity.Name + "ApiRequest").FirstOrDefault();
                if (deleteApiDTo != null)
                {
                    CreateMap(entity, deleteApiDTo).ReverseMap();
                }
                Type response = requests.Where(a => a.Name == entity.Name + "Response").FirstOrDefault();
                if (response != null)
                {
                    CreateMap(entity, response).ReverseMap();
                    if (createDTo is not null)
                    {
                        CreateMap(createDTo, response).ReverseMap();
                    }
                    if (createApiDTo is not null)
                    {
                        CreateMap(createApiDTo, response).ReverseMap();
                    }
                    if (updateDTo is not null)
                    {
                        CreateMap(updateDTo, response).ReverseMap();
                    }
                    if (updateApiDTo is not null)
                    {
                        CreateMap(updateApiDTo, response).ReverseMap();
                    }
                    if (deleteDTo is not null)
                    {
                        CreateMap(deleteDTo, response).ReverseMap();
                    }
                    if (deleteApiDTo is not null)
                    {
                        CreateMap(deleteApiDTo, response).ReverseMap();
                    }
                }
                Type translation = requests.Where(a => a.Name == entity.Name + "Request").FirstOrDefault();
                if (translation != null)
                {
                    CreateMap(entity, translation).ReverseMap();
                }
            }
        }

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes()
            .Where(t =>
            {
                return string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal) && t.IsClass && t.IsPublic;
            })
            .ToArray();
        }

        private static List<Type> GetTypesInNamespace(Assembly assembly, string[] nameSpaces)
        {
            var types = new List<Type>();
            foreach (var nameSpace in nameSpaces)
            {
                types.AddRange(assembly.GetTypes().Where(t =>
                {
                    return string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal) && t.IsClass && t.IsPublic;
                }).ToArray());
            }

            return types;
        }
    }
}
