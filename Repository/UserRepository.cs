using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using Newtonsoft.Json;
using Bernhoeft.Entitys;
using Bernhoeft.Utils;
using Bernhoeft.Repository.Base;
using Bernhoeft.DataContext;

namespace Bernhoeft.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(Context context) : base(context)
        {
        }
    }
}
