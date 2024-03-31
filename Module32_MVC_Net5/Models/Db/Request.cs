using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Module32_MVC_Net5.Models.Db
{
    /// <summary>
    /// Модель для сохранения запроса к сайту в БД. Таблица Requests
    /// </summary>
    [Table("Requests")]
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}
