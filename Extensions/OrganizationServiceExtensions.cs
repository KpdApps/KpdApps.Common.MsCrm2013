using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace KpdApps.Common.MsCrm2013.Extensions
{
	/// <summary>
	/// Extensions for <see cref="IOrganizationService"/>.
	/// </summary>
	public static class OrganizationServiceExtensions
	{
		/// <summary>
		/// Create entity.
		/// </summary>
		/// <param name="organizationService"><see cref="IOrganizationService"/>.</param>
		/// <param name="entity">Entity to create.</param>
		public static Guid CreateEntity(this IOrganizationService organizationService, Entity entity)
		{
			if (entity == null || !entity.Attributes.Any())
				return Guid.Empty;

			return organizationService.Create(entity);
		}

		/// <summary>
		/// Replace for regular Retrieve, return <see cref="Entity"/> with all collumns.
		/// </summary>
		/// <param name="organizationService"><see cref="IOrganizationService"/>.</param>
		/// <param name="entityName">Entity name.</param>
		/// <param name="entityId">Entity identifier.</param>
		/// <returns><see cref="Entity"/></returns>
		public static Entity Retrieve(this IOrganizationService organizationService, string entityName, Guid entityId)
		{
			return organizationService.Retrieve(entityName, entityId, new ColumnSet(true));
		}


		/// <summary>
		/// Replace for regular Retrieve, return <see cref="Entity"/> with all collumns.
		/// </summary>
		/// <param name="organizationService"><see cref="IOrganizationService"/>.</param>
		/// <param name="entityReference"><see cref="EntityReference"/></param>
		/// <returns><see cref="Entity"/></returns>
		public static Entity Retrieve(this IOrganizationService organizationService, EntityReference entityReference)
		{
			return organizationService.Retrieve(entityReference.LogicalName, entityReference.Id, new ColumnSet(true));
		}

		/// <summary>
		/// Replace for regular Retrieve, return <see cref="Entity"/> with all collumns.
		/// </summary>
		/// <param name="organizationService"><see cref="IOrganizationService"/>.</param>
		/// <param name="source"><see cref="Entity"/></param>
		/// <param name="referencingAttributeName">Name of attribute of reference to retrieve.</param>
		/// <returns><see cref="Entity"/></returns>
		public static Entity Retrieve(this IOrganizationService organizationService, Entity source, string referencingAttributeName)
		{
			var reference = source[referencingAttributeName] as EntityReference;
			if (reference != null)
				return Retrieve(organizationService, reference);

			return null;
		}

		/// <summary>
		/// Replace for regular Retrieve, return <see cref="Entity"/> with all specified collumns.
		/// </summary>
		/// <param name="organizationService"><see cref="IOrganizationService"/>.</param>
		/// <param name="entityName">Entity name.</param>
		/// <param name="entityId">Entity identifier.</param>
		/// <param name="attrs">Attributes to select.</param>
		/// <returns><see cref="Entity"/></returns>
		public static Entity Retrieve(this IOrganizationService organizationService, string entityName, Guid entityId, params string[] attrs)
		{
			if (!attrs.Any())
				return organizationService.Retrieve(entityName, entityId, new ColumnSet(true));

			ColumnSet set = new ColumnSet(attrs);
			return organizationService.Retrieve(entityName, entityId, set);
		}
	}
}
