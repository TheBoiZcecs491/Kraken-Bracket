using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ.DatabaseQueryService;

namespace TBZ.DatabaseQueryTest
{
    [TestClass]
    public class DatabaseQueryTest
    {
        DatabaseQuery query = new DatabaseQuery();
        bool result = false;

        public string MakeRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }

        [TestMethod]
        public void Table_Exist_True()
        {
            var tableName = "user_info";

            try
            {
                query.TableExist(tableName);
                result = true;
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception)
            {
                result = false;
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Insert_Pass()
        {
            
        }
    }
}
