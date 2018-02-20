using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack;
using NUnit.Framework;

namespace ReturnAttributeTests
{
    public class User
    {
        [Return]
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class TestsBase
    {
        private OrmLiteConnectionFactory dbFactory;

        protected string ConnectionString { get; set; }
        protected IOrmLiteDialectProvider DialectProvider { get; set; }
        protected OrmLiteConnectionFactory DbFactory => (dbFactory == null) ? dbFactory = new OrmLiteConnectionFactory(ConnectionString, DialectProvider) : dbFactory;


        public TestsBase()
        {
            typeof(User).AddAttributes(new AliasAttribute("Users"));
        }

        protected void Init()
        {

        }

    }
}
