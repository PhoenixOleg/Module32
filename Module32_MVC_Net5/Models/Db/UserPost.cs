﻿using System;

namespace Module32_MVC_Net5.Models.Db
{
    /// <summary>
    ///  Модель поста в блоге
    /// </summary>
    public class UserPost
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
