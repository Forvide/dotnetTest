using System;

namespace dotnetTest.Data
{
    public class AppUser
    {
        public virtual Guid Id { get; set; }
        public virtual string Iin { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime BirthDate { get; set; }
    }
}
