﻿
using System;

namespace Module32_MVC_Net5.Models.Db
{
    /// <summary>
    /// модель пользователя в блоге
    /// </summary>
    public class User
    {
        // Уникальный идентификатор сущности в базе
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
