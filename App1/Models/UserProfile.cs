using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    public sealed class UserProfile
    {
        public string NickName { get; internal set; }

        public string Names { get; set; }

        public string LastName { get; set; }

        public string SurName { get; set; }

        public string FullName { get; set; }

        public IEnumerable<string> Roles { get; internal set; }

        public IEnumerable<Guid> Branches { get; internal set; }

        public Guid EmployeeId { get; internal set; }

        public Guid Id { get; internal set; }

        public Guid ClientId { get; internal set; }

        public UserType UserType { get; internal set; }
    }

    public enum UserType
    {
        Employee,
        Client
    }
}
