using System;

namespace Application.Data.Common
{
    public abstract class AuditableEntity
    {
        /// <summary>
        /// Создатель
        /// </summary>
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Кто внес последние изменения
        /// </summary>
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }

        /// <summary>
        /// Последняя дата изменения
        /// </summary>
        public DateTime? LastModified { get; set; }
    }
}
