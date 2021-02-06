using System;
using System.Collections.Generic;

namespace Matches.Contracts.Member
{
    public class MemberViewModel
    {
        public Guid Id { get; set; }
        public Dictionary<string, object> Attributes { get; set; }
    }
}
