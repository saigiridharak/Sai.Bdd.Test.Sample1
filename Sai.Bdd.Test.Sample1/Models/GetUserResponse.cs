﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sai.Bdd.Test.Sample1.Models
{
    public class GetUserResponse
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public List<User> Data { get; set; }

    }
}
