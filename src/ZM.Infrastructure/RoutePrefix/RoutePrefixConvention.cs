using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ZM.Infrastructure.RoutePrefix;

public class RoutePrefixConvention : IControllerModelConvention
{
	public void Apply(ControllerModel controller)
	{
		var prefixes = GetPrefixes(controller.ControllerType);

		if (prefixes.Count == 0)
			return;

		var prefixRouteModels = prefixes
			   .Select(p => new AttributeRouteModel(new RouteAttribute(p.Prefix)))
			   .Aggregate((acc, prefix) => AttributeRouteModel.CombineAttributeRouteModel(prefix, acc));

		foreach (var selector in controller.Selectors)
		{
			selector.AttributeRouteModel = selector.AttributeRouteModel != null ?
				AttributeRouteModel.CombineAttributeRouteModel(prefixRouteModels, selector.AttributeRouteModel) :
				prefixRouteModels;
		}
	}

	private static List<RoutePrefixAttribute> GetPrefixes(Type controlerType)
	{
		var routePrefixes = new List<RoutePrefixAttribute>();

		FindPrefixesRec(controlerType, ref routePrefixes);

		return routePrefixes.Where(r => r != null).ToList();

		static void FindPrefixesRec(Type type, ref List<RoutePrefixAttribute> routePrefixes)
		{
			var prefix = type
				.GetCustomAttributes(false)
				.OfType<RoutePrefixAttribute>()
				.FirstOrDefault();

			routePrefixes.Add(prefix);

			if (type.BaseType is null)
				return;

			FindPrefixesRec(type.BaseType, ref routePrefixes);
		}
	}
}
