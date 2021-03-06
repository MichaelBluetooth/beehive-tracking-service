using System;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Models.DTO
{
    public abstract class ModelDTOBase
    {
        /// <summary>
        /// The unit identifier for this entity
        /// </summary>
        [Key]
        public Guid? id { get; set; }

        /// <summary>
        /// The tenant this belongs to
        /// </summary>
        public Guid? tenantId { get; set; }

        /// <summary>
        /// A unique id for this entity assigned by the client at the time of creation
        /// </summary>
        public Guid? clientId { get; set; }

        /// <summary>
        /// The last time this record was modified
        /// </summary>
        public DateTime? lastModified { get; set; }
    }
}