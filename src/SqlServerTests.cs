using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.Data;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using NUnit.Framework;

namespace ReturnAttributeTests
{
    public class SqlServerTests: TestsBase
    {
        public SqlServerTests(): base()
        {
            ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=ASPNET-Identity;Persist Security Info=True;User ID=sa;Password=masterkey";
            DialectProvider = SqlServer2017Dialect.Provider;
        }

        [Test]
        public void TestIdOnInsert()
        {
            Init();
            using (var db = DbFactory.Open())
            {
                db.CreateTable<User>(true);

                var user = new User { Name = "me", Email = "me@mydomain.com" };
                user.UserName = user.Email;

                db.Insert(user);               
                Assert.That(user.Id, Is.GreaterThan(0), "normal Insert");
            }
        }

        [Test]
        public void TestIdOnSave()
        {
            Init();
            using (var db = DbFactory.Open())
            {
                db.CreateTable<User>(true);

                var user = new User { Name = "me", Email = "me@mydomain.com" };
                user.UserName = user.Email;

                db.Save(user);
                Assert.That(user.Id, Is.GreaterThan(0), "normal Insert");
            }
        }


        [Test]
        public void TestSqlOnInsert()
        {
            Init();
            using (var db = DbFactory.Open())
            {
                db.CreateTable<User>(true);

                var user = new User { Name = "me", Email = "me@mydomain.com" };
                user.UserName = user.Email;

                var id = db.Insert(user);
                var sql = db.GetLastSql();
                Assert.That(sql, Is.EqualTo("INSERT INTO \"User\" (\"Id\",\"Name\",\"UserName\",\"Email\") OUTPUT INSERTED.\"Id\" VALUES (NEXT VALUE FOR \"Gen_User_Id\",@Name,@UserName,@Email)"), "normal Insert");
            }
        }
    }
}
