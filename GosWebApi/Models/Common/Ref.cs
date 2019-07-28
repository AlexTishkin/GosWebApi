﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GosWebApi.Models
{
    public class Ref
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Ref(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}