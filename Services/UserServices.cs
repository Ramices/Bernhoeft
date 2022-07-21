using Bernhoeft.DataContext;
using Bernhoeft.Entitys;
using Bernhoeft.Repository;
using Bernhoeft.Services.Base;
using System.Collections.Generic;
using System.Linq;

namespace Bernhoeft.Services
{
    public class UserServices: Service<User>
    {
        public UserServices(Context context) : base(new UserRepository(context))
        {

        }

        public User GetUsersByUsername(string username)
        {
            return GetAll().Where(user => user.Username.Equals(username)).ToList().FirstOrDefault();
        }

        public List<User> GetUsersByName(string name)
        {
            return GetAll().Where(user=> user.Name.Equals(name)).ToList();
        }

        public bool UserExists(string username)
        {
            return GetAll().Where(user => user.Username.Equals(username)).FirstOrDefault() != null;
        }

    }
}
