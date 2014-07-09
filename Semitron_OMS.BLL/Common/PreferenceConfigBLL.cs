using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Semitron_OMS.DAL.Common;

namespace Semitron_OMS.BLL.Common
{
    public class PreferenceConfigBLL
    {
        PreferenceConfigDAL dal = new PreferenceConfigDAL();
        public Semitron_OMS.Model.Common.PreferenceConfigModel GetModel(int userId, string pageCode)
        {
            return dal.GetModel(userId, pageCode);
        }

        public bool AddOrUpdatePreferenceConfig(Semitron_OMS.Model.Common.PreferenceConfigModel model)
        {
            return dal.AddOrUpdatePreferenceConfig(model);
        }
    }
}
