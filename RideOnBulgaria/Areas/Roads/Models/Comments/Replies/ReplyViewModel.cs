﻿using System;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Models.Comments.Replies
{
    public class ReplyViewModel
    {
        public string Id { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime PostedOn { get; set; }


    }
}