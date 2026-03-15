using System;
using System.Collections.Generic;
using System.Linq;
using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service.Mapping
{
    /// <summary>
    /// PersonalLog mapping extensions for converting between data objects and domain models.
    /// </summary>
    static class PersonalLogMappingExtensions
    {
        /// <summary>
        /// Converts the data object into a domain model.
        /// </summary>
        /// <returns>The domain model.</returns>
        /// <param name="dataObject">The data object.</param>
        internal static PersonalLog ToDomainModel(this PersonalLogEntity dataObject)
            => new(
                DateOnly.Parse(dataObject.Date),
                TimeOnly.TryParse(dataObject.Time, out TimeOnly time) ? time : null,
                dataObject.TimeZone)
            {
                Id = dataObject.Id,
                Template = Enum.Parse<PersonalLogTemplate>(dataObject.Template),
                Data = dataObject.Data,
                CreatedDateTime = DateTime.Parse(dataObject.CreatedDT),
                UpdatedDateTime = DateTime.TryParse(dataObject.UpdatedDT, out DateTime updatedDT) ? updatedDT : null
            };

        /// <summary>
        /// Converts the domain model into a data object.
        /// </summary>
        /// <returns>The data object.</returns>
        /// <param name="domainModel">The domain model.</param>
        internal static PersonalLogEntity ToDataObject(this PersonalLog domainModel) => new()
        {
            Id = domainModel.Id,
            Date = domainModel.Date.ToString("yyyy-MM-dd"),
            Time = domainModel.Time?.ToString("HH:mm"),
            TimeZone = domainModel.TimeZone,
            Template = domainModel.Template.ToString(),
            Data = domainModel.Data,
            CreatedDT = domainModel.CreatedDateTime.ToString("o"),
            UpdatedDT = domainModel.UpdatedDateTime?.ToString("o")
        };

        /// <summary>
        /// Converts the data objects into domain models.
        /// </summary>
        /// <returns>The domain models.</returns>
        /// <param name="dataObjects">The data objects.</param>
        internal static IEnumerable<PersonalLog> ToDomainModels(this IEnumerable<PersonalLogEntity> dataObjects)
            => dataObjects.Select(dataObject => dataObject.ToDomainModel());

        /// <summary>
        /// Converts the domain models into data objects.
        /// </summary>
        /// <returns>The data objects.</returns>
        /// <param name="domainModels">The domain models.</param>
        internal static IEnumerable<PersonalLogEntity> ToDataObjects(this IEnumerable<PersonalLog> domainModels)
            => domainModels.Select(domainModel => domainModel.ToDataObject());
    }
}
