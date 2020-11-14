using System;

namespace Application.Data.Common
{
    public class VersionableEntity
    {
        /// <summary>
        /// Флаг актуального поля, которое будет учитываться в выборке
        /// </summary>
        public bool IsActual { get; set; }

        /// <summary>
        /// Номер пользоавтеля создавшего запись
        /// </summary>
        public string CreatedByUserId { get; set; }

        /// <summary>
        /// Фамилия И.О. пользователя создавшего запись
        /// </summary>
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Номер пользователя изменившего запись
        /// </summary>
        public string LastModifiedByUserId { get; set; }

        /// <summary>
        /// Фамилия И.О. пользователя изменившего запись
        /// </summary>
        public string LastModifiedByUserName { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime? LastModified { get; set; }
    }
}