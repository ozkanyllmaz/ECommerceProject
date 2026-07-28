using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Abstractions
{
    public interface ISecuredRequest
    {
        string[] Roles { get; }
    }
}
