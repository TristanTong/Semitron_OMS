/**  
* CustomerOrderDAL.cs
*
* 功 能： N/A
* 类 名： CustomerOrderDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:30:59   童荣辉    初版
*
* Copyright (c) 2013 SemitronElec Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：森美创（深圳）科技有限公司　　　　　　　　　　　　　　  │
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Semitron_OMS.DBUtility;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Common;//Please add references
namespace Semitron_OMS.DAL.OMS
{
    /// <summary>
    /// 数据访问类:CustomerOrderDAL
    /// </summary>
    public partial class CustomerOrderDAL
    {
        public CustomerOrderDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "CustomerOrder");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string InnerOrderNO, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CustomerOrder");
            strSql.Append(" where InnerOrderNO=@InnerOrderNO and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@InnerOrderNO", SqlDbType.VarChar,32),
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = InnerOrderNO;
            parameters[1].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.CustomerOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CustomerOrder(");
            strSql.Append("InnerOrderNO,CustomerOrderNO,CustomerID,CustOrderDate,CustomerBuyer,CorporationID,InnerSalesMan,PaymentTypeID,State,CreateUser,CreateTime,UpdateUser,UpdateTime,AssignToInnerBuyer)");
            strSql.Append(" values (");
            strSql.Append("@InnerOrderNO,@CustomerOrderNO,@CustomerID,@CustOrderDate,@CustomerBuyer,@CorporationID,@InnerSalesMan,@PaymentTypeID,@State,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime,@AssignToInnerBuyer)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@InnerOrderNO", SqlDbType.VarChar,32),
					new SqlParameter("@CustomerOrderNO", SqlDbType.VarChar,32),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@CustOrderDate", SqlDbType.Date,3),
					new SqlParameter("@CustomerBuyer", SqlDbType.NVarChar,32),
					new SqlParameter("@CorporationID", SqlDbType.Int,4),
					new SqlParameter("@InnerSalesMan", SqlDbType.NVarChar,32),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@AssignToInnerBuyer", SqlDbType.Int,4)};
            parameters[0].Value = model.InnerOrderNO;
            parameters[1].Value = model.CustomerOrderNO;
            parameters[2].Value = model.CustomerID;
            parameters[3].Value = model.CustOrderDate;
            parameters[4].Value = model.CustomerBuyer;
            parameters[5].Value = model.CorporationID;
            parameters[6].Value = model.InnerSalesMan;
            parameters[7].Value = model.PaymentTypeID;
            parameters[8].Value = model.State;
            parameters[9].Value = model.CreateUser;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.UpdateUser;
            parameters[12].Value = model.UpdateTime;
            parameters[13].Value = model.AssignToInnerBuyer;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.CustomerOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CustomerOrder set ");
            strSql.Append("CustomerOrderNO=@CustomerOrderNO,");
            strSql.Append("CustomerID=@CustomerID,");
            strSql.Append("CustOrderDate=@CustOrderDate,");
            strSql.Append("CustomerBuyer=@CustomerBuyer,");
            strSql.Append("CorporationID=@CorporationID,");
            strSql.Append("InnerSalesMan=@InnerSalesMan,");
            strSql.Append("PaymentTypeID=@PaymentTypeID,");
            strSql.Append("State=@State,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("AssignToInnerBuyer=@AssignToInnerBuyer");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerOrderNO", SqlDbType.VarChar,32),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@CustOrderDate", SqlDbType.Date,3),
					new SqlParameter("@CustomerBuyer", SqlDbType.NVarChar,32),
					new SqlParameter("@CorporationID", SqlDbType.Int,4),
					new SqlParameter("@InnerSalesMan", SqlDbType.NVarChar,32),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@AssignToInnerBuyer", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@InnerOrderNO", SqlDbType.VarChar,32)};
            parameters[0].Value = model.CustomerOrderNO;
            parameters[1].Value = model.CustomerID;
            parameters[2].Value = model.CustOrderDate;
            parameters[3].Value = model.CustomerBuyer;
            parameters[4].Value = model.CorporationID;
            parameters[5].Value = model.InnerSalesMan;
            parameters[6].Value = model.PaymentTypeID;
            parameters[7].Value = model.State;
            parameters[8].Value = model.CreateUser;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.UpdateUser;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.AssignToInnerBuyer;
            parameters[13].Value = model.ID;
            parameters[14].Value = model.InnerOrderNO;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CustomerOrder ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string InnerOrderNO, int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CustomerOrder ");
            strSql.Append(" where InnerOrderNO=@InnerOrderNO and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@InnerOrderNO", SqlDbType.VarChar,32),
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = InnerOrderNO;
            parameters[1].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CustomerOrder ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Semitron_OMS.Model.OMS.CustomerOrderModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,InnerOrderNO,CustomerOrderNO,CustomerID,CustOrderDate,CustomerBuyer,CorporationID,InnerSalesMan,PaymentTypeID,State,CreateUser,CreateTime,UpdateUser,UpdateTime,AssignToInnerBuyer from CustomerOrder ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.CustomerOrderModel model = new Semitron_OMS.Model.OMS.CustomerOrderModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Semitron_OMS.Model.OMS.CustomerOrderModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.CustomerOrderModel model = new Semitron_OMS.Model.OMS.CustomerOrderModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["InnerOrderNO"] != null)
                {
                    model.InnerOrderNO = row["InnerOrderNO"].ToString();
                }
                if (row["CustomerOrderNO"] != null)
                {
                    model.CustomerOrderNO = row["CustomerOrderNO"].ToString();
                }
                if (row["CustomerID"] != null && row["CustomerID"].ToString() != "")
                {
                    model.CustomerID = int.Parse(row["CustomerID"].ToString());
                }
                if (row["CustOrderDate"] != null && row["CustOrderDate"].ToString() != "")
                {
                    model.CustOrderDate = DateTime.Parse(row["CustOrderDate"].ToString());
                }
                if (row["CustomerBuyer"] != null)
                {
                    model.CustomerBuyer = row["CustomerBuyer"].ToString();
                }
                if (row["CorporationID"] != null && row["CorporationID"].ToString() != "")
                {
                    model.CorporationID = int.Parse(row["CorporationID"].ToString());
                }
                if (row["InnerSalesMan"] != null)
                {
                    model.InnerSalesMan = row["InnerSalesMan"].ToString();
                }
                if (row["PaymentTypeID"] != null && row["PaymentTypeID"].ToString() != "")
                {
                    model.PaymentTypeID = int.Parse(row["PaymentTypeID"].ToString());
                }
                if (row["State"] != null && row["State"].ToString() != "")
                {
                    model.State = int.Parse(row["State"].ToString());
                }
                if (row["CreateUser"] != null)
                {
                    model.CreateUser = row["CreateUser"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["UpdateUser"] != null)
                {
                    model.UpdateUser = row["UpdateUser"].ToString();
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["AssignToInnerBuyer"] != null && row["AssignToInnerBuyer"].ToString() != "")
                {
                    model.AssignToInnerBuyer = int.Parse(row["AssignToInnerBuyer"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,InnerOrderNO,CustomerOrderNO,CustomerID,CustOrderDate,CustomerBuyer,CorporationID,InnerSalesMan,PaymentTypeID,State,CreateUser,CreateTime,UpdateUser,UpdateTime,AssignToInnerBuyer ");
            strSql.Append(" FROM CustomerOrder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,InnerOrderNO,CustomerOrderNO,CustomerID,CustOrderDate,CustomerBuyer,CorporationID,InnerSalesMan,PaymentTypeID,State,CreateUser,CreateTime,UpdateUser,UpdateTime,AssignToInnerBuyer ");
            strSql.Append(" FROM CustomerOrder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM CustomerOrder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from CustomerOrder T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "CustomerOrder";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 分页查询客户订单数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>客户订单数据</returns>
        public DataSet GetCustomerOrderPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.CustomerOrder AS C LEFT JOIN dbo.PaymentType AS P ON C.PaymentTypeID = P.ID LEFT JOIN dbo.Customer AS T ON C.CustomerID = T.ID LEFT JOIN  dbo.Corporation AS M ON M.ID=C.CorporationID LEFT JOIN CommonTable AS CT ON CT.TableName='CustomerOrder' AND CT.FieldID='State' AND CT.[KEY]=C.STATE LEFT JOIN Admin AS A1 ON A1.AdminID=C.AssignToInnerBuyer  LEFT JOIN (SELECT  ObjId ,FileNum = COUNT(1) FROM dbo.Attachment WITH ( NOLOCK ) WHERE ObjType = 'CustomerOrder' AND AvailFlag = 1 GROUP BY ObjId) AS A ON A.ObjId=C.ID";
            //查询字段
            string strGetFields = " C.ID ,C.InnerOrderNO ,C.CustomerOrderNO ,CustomerName = T.CustomerName , CustOrderDate =Convert(varchar(20),C.CustOrderDate,120),C.CustomerBuyer ,M.CompanyName,C.InnerSalesMan ,AssignToInnerBuyer=A1.Name, PaymentType = P.PaymentType  ,ShipmentDate=( SELECT TOP 1 Convert(varchar(20),D.ShipmentDate,120) FROM CustomerOrderDetail AS D WHERE D.InnerOrderNO=C.InnerOrderNO) ,State=CT.Value ,CreateTime=Convert(varchar(20),C.CreateTime,120) ,UpdateTime=Convert(varchar(20),C.UpdateTime,120),FileNum=ISNULL(A.FileNum,0)";
            //查询条件
            string strWhere = SQLOperateHelper.GetSQLCondition(searchInfo.ConditionFilter, false);
            //数据查询
            CommonDAL commonDAL = new CommonDAL();
            return commonDAL.GetDataExt(ConstantValue.ProcedureNames.PageProcedureName,
                strTableName,
                strGetFields,
                searchInfo.PageSize,
                searchInfo.PageIndex,
                strWhere,
                searchInfo.OrderByField,
                searchInfo.OrderType,
                out o_RowsCount);
        }

        /// <summary>
        /// 根据内部订单号获得客户订单实体
        /// </summary>
        /// <param name="strInnerOrderNO">内部订单号</param>
        /// <returns>客户订单实体</returns>
        public Model.OMS.CustomerOrderModel GetModelByInnerOrderNO(string strInnerOrderNO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,InnerOrderNO,CustomerOrderNO,CustomerID,CustOrderDate,CustomerBuyer,CorporationID,InnerSalesMan,PaymentTypeID,State,CreateUser,CreateTime,UpdateUser,UpdateTime,AssignToInnerBuyer ");
            strSql.Append(" FROM CustomerOrder ");
            strSql.Append(" where InnerOrderNO=@InnerOrderNO");
            SqlParameter[] parameters = {
					new SqlParameter("@InnerOrderNO", SqlDbType.VarChar,16)
			};
            parameters[0].Value = strInnerOrderNO;

            Semitron_OMS.Model.OMS.CustomerOrderModel model = new Semitron_OMS.Model.OMS.CustomerOrderModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据内部订单号判断是否存在记录
        /// </summary>
        /// <param name="strInnerOrderNO">内部订单号</param>
        /// <returns>是否存在</returns>
        public bool Exists(string strInnerOrderNO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CustomerOrder");
            strSql.Append(" where InnerOrderNO=@InnerOrderNO");
            SqlParameter[] parameters = {
					new SqlParameter("@InnerOrderNO", SqlDbType.VarChar,32)		};
            parameters[0].Value = strInnerOrderNO;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 验证并取消客户订单
        /// </summary>
        public string ValidateAndCancelCustomerOrder(string strCreateUser, int iId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
                    new SqlParameter("@Result", SqlDbType.NVarChar,2048)
			};
            parameters[0].Value = iId;
            parameters[1].Value = strCreateUser;
            parameters[2].Direction = ParameterDirection.Output;
            int rowsAffected = 0;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ValidateAndCancelCustomerOrder, parameters, out rowsAffected);
            return parameters[2].Value.ToString();
        }

        /// <summary>
        /// 客户订单出货
        /// </summary>
        public string ValidateAndOutStock(string strCreateUser, int iId, string strShipmentDate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
                    new SqlParameter("@ShipmentDate", SqlDbType.DateTime),
                    new SqlParameter("@Result", SqlDbType.NVarChar,2048)
			};
            parameters[0].Value = iId;
            parameters[1].Value = strCreateUser;
            parameters[2].Value = strShipmentDate;
            parameters[3].Direction = ParameterDirection.Output;
            int rowsAffected = 0;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ValidateAndOutStock, parameters, out rowsAffected);
            return parameters[3].Value.ToString();
        }

        /// <summary>
        /// 获取产品销售分析列表数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <returns>产品销售分析列表数据</returns>
        public DataSet GetConfirmFirstData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            SqlParameter[] parameters = { 
                    new SqlParameter("@COId", SqlDbType.Int),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),                    
                    new SqlParameter("@strOrder", SqlDbType.VarChar,100),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@iRowsCount", SqlDbType.Int),
                    };

            parameters[0].Value = Convert.ToInt32(searchInfo.ConditionFilter[0].Value);
            parameters[1].Value = searchInfo.PageSize;
            parameters[2].Value = searchInfo.PageIndex;
            parameters[3].Value = searchInfo.OrderByField;
            parameters[4].Value = searchInfo.OrderType;
            parameters[5].Direction = ParameterDirection.Output;
            return DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.GetConfirmFirstData, parameters, "ds", out o_RowsCount, 5);
        }
        /// <summary>
        /// 执行销售审核
        /// </summary>   
        public string ConfirmFirst(string strUserName, int iId, int iType)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@COId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
                    new SqlParameter("@Result", SqlDbType.NVarChar,2048)
			};
            parameters[0].Value = iId;
            parameters[1].Value = iType;
            parameters[2].Value = strUserName;
            parameters[3].Direction = ParameterDirection.Output;
            int rowsAffected = 0;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ConfirmFirst, parameters, out rowsAffected);
            return parameters[3].Value.ToString();
        }

        /// <summary>
        /// 根据客户产品清单明细ID设定字段各项值
        /// </summary>
        /// <param name="iCustomerOrderDetailID">客户产品清单明细ID</param>
        /// <param name="iPaymentTypeID">付款方式</param>
        public int SetItemsByCustomerOrderDetailID(int iCustomerOrderDetailID, int iPaymentTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE  O ");
            strSql.Append("SET     O.PaymentTypeID = @PaymentTypeID ");
            strSql.Append("FROM    CustomerOrder AS O ");
            strSql.Append("        INNER JOIN dbo.CustomerOrderDetail AS D ON O.InnerOrderNO = D.InnerOrderNO ");
            strSql.Append("WHERE   D.ID = @CustomerOrderDetailID");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerOrderDetailID", SqlDbType.Int,4),
                    new SqlParameter("@PaymentTypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = iCustomerOrderDetailID;
            parameters[1].Value = iPaymentTypeID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        #endregion  ExtensionMethod
    }
}

