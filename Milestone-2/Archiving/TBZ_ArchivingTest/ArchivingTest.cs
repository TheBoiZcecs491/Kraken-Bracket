using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TBZ_Archiving;

namespace TBZ_ArchivingTest
{
    [TestClass]
    public class ArchivingTest
    {
        [TestMethod]
        public void Archiving_CreateObj_Pass()
        {
            //Arrange
            Boolean result = true;
            //Act
            try
            {
                var a = new Archiving();
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }
    }
}
