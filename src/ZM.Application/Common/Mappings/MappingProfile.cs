using AutoMapper;
using System.Reflection;

namespace ZM.Application.Common.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
