using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Semitron_OMS.BLL.OMS;
using Semitron_OMS.Model.OMS;
using Semitron_OMS.Common;

namespace Semitron_OMS.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Assert.Equals(new POBLL().Exists("1", 1), false);
            Assert.Equals(new BLL.OMS.AttachmentBLL().BatchSaveFiles("D://file_system//", "CustomerOrder", "a", "http://localhost:20910/file_system/2013/7/15/1521_a.jpg", "admin"), true);
        }

        [TestMethod]
        public void TestMethod2()
        {
            GodownEntryModel model = new GodownEntryModel();
            model.EntryNo = "A001";
            GodownEntryModel oldModel = new GodownEntryModel();
            oldModel.EntryNo = "A001";
            oldModel.ID = 1;
            oldModel.InStockDate = DateTime.Now;
            SQLOperateHelper.SetEntityByOldNoNull(model, oldModel);

            Assert.AreEqual(model.EntryNo, oldModel.EntryNo);
            Assert.AreEqual(model.InStockDate, oldModel.InStockDate);
            Assert.AreEqual(1, model.ID);
        }
    }
}
