using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces.Entities
{
    public interface IUser
    {
        int ID { get; set; }
        string UserName { get; set; }
        string Hash { get; set; }
        string Salt { get; set; }
        IRole Role { get; set; }
    }
}
