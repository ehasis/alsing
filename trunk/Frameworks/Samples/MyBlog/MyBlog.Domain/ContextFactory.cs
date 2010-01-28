using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBlog.Domain.Entities;
using System.Data.SqlClient;
using Microsoft.Data.Objects;

namespace MyBlog.Domain
{
    public static class ContextFactory
    {
        public static Entities.Entities CreateContext()
        {

            return new Entities.Entities();
        }
    }
}


