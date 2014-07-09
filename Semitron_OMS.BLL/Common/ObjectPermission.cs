using System;
using System.Data;
using System.Collections.Generic;
using Semitron_OMS.Common;
using Semitron_OMS.Model;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Model.Common;
namespace Semitron_OMS.BLL.Common
{
	/// <summary>
	/// 对象权限关联表
	/// </summary>
	public partial class ObjectPermission
	{
        private readonly ObjectPermissionDAL dal = new ObjectPermissionDAL();
		public ObjectPermission()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ObjectPermissionModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ObjectPermissionModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ObjectPermissionModel GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ObjectPermissionModel GetModelByCache(int ID)
		{
			
			string CacheKey = "ObjectPermissionModel-" + ID;
			object objModel = Semitron_OMS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = Semitron_OMS.Common.ConfigHelper.GetConfigInt("ModelCache");
						Semitron_OMS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ObjectPermissionModel)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ObjectPermissionModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ObjectPermissionModel> DataTableToList(DataTable dt)
		{
			List<ObjectPermissionModel> modelList = new List<ObjectPermissionModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ObjectPermissionModel model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ObjectPermissionModel();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["PermissionID"]!=null && dt.Rows[n]["PermissionID"].ToString()!="")
					{
						model.PermissionID=int.Parse(dt.Rows[n]["PermissionID"].ToString());
					}
					if(dt.Rows[n]["ObjType"]!=null && dt.Rows[n]["ObjType"].ToString()!="")
					{
						model.ObjType=int.Parse(dt.Rows[n]["ObjType"].ToString());
					}
					if(dt.Rows[n]["ObjID"]!=null && dt.Rows[n]["ObjID"].ToString()!="")
					{
						model.ObjID=int.Parse(dt.Rows[n]["ObjID"].ToString());
					}
					if(dt.Rows[n]["AvailFlag"]!=null && dt.Rows[n]["AvailFlag"].ToString()!="")
					{
						model.AvailFlag=int.Parse(dt.Rows[n]["AvailFlag"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

                /// <summary>
        /// 绑定角色关系
        /// </summary>
        /// <param name="CodeId">代码ID</param>
        /// <param name="AreasId">地区ID集合</param>
        /// <param name="Key1">关键字1</param>
        /// <param name="Key2">关键字2</param>
        /// <param name="RContent">内容</param>
        /// <param name="SuccessRate">成功率</param>
        /// <returns></returns>
        public bool BindFeeCodeInfo(string CodeId, string[] AreasId, string[] AreaName, int ObjType)
        {
            return dal.BindObjectPermission(CodeId, AreasId, AreaName, ObjType);
        }
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

