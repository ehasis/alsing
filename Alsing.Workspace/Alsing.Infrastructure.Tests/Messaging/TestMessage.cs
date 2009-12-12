using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing.Infrastructure.Tests.Messaging
{
    using Alsing.Messaging;

    public class TestMessage : IMessage
    {
        public string Text { get; set; }
    }
}
