using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alsing.Workspace;
using Alsing.Messaging;

namespace MyBlog.WebSite
{
    public static class Config
    {
        public static IWorkspace GetWorkspace()
        {
            var context = new MyBlog.Domain.ModelDataContext();
            return new LinqToSqlWorkspace(context);
        }

        public static IMessageBus GetMessageBus(IWorkspace workspace)
        {
            var messageBus = new MessageBus();

            return messageBus;
        }
    }
}
