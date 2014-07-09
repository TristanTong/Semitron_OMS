/**  
* PODAL.cs
*
* 功 能： N/A
* 类 名： PODAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:30:29   童荣辉    初版
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
using Semitron_OMS.Common;
using Semitron_OMS.DAL.Common;//Please add references
namespace Semitron_OMS.DAL.OMS
{
    /// <summary>
    /// 数据访问类:PODAL
    /// </summary>
    public partial class PODAL
    {
        public PODAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "PO");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string PONO, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PO");
            strSql.Append(" where PONO=@PONO and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PONO", SqlDbType.VarChar,32),
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = PONO;
            parameters[1].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.POModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PO(");
            strSql.Append("PONO,POOrderDate,SupplierID,ContactPerson,Tel,IssuedDate,InnerBuyer,CorporationID,BillTo,BillManager,BillManagerTel,ShipTo,ShipManager,ShipManagerTel,DeliveryDate,PaymentTerms,Shipping,TotalFee,AcceptedBy,IssuedBy,State,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@PONO,@POOrderDate,@SupplierID,@ContactPerson,@Tel,@IssuedDate,@InnerBuyer,@CorporationID,@BillTo,@BillManager,@BillManagerTel,@ShipTo,@ShipManager,@ShipManagerTel,@DeliveryDate,@PaymentTerms,@Shipping,@TotalFee,@AcceptedBy,@IssuedBy,@State,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PONO", SqlDbType.VarChar,32),
					new SqlParameter("@POOrderDate", SqlDbType.DateTime,3),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@ContactPerson", SqlDbType.VarChar,16),
					new SqlParameter("@Tel", SqlDbType.VarChar,32),
					new SqlParameter("@IssuedDate", SqlDbType.DateTime,3),
					new SqlParameter("@InnerBuyer", SqlDbType.VarChar,16),
					new SqlParameter("@CorporationID", SqlDbType.Int,4),
					new SqlParameter("@BillTo", SqlDbType.NVarChar,1024),
					new SqlParameter("@BillManager", SqlDbType.VarChar,16),
					new SqlParameter("@BillManagerTel", SqlDbType.VarChar,32),
					new SqlParameter("@ShipTo", SqlDbType.NVarChar,1024),
					new SqlParameter("@ShipManager", SqlDbType.VarChar,16),
					new SqlParameter("@ShipManagerTel", SqlDbType.VarChar,32),
					new SqlParameter("@DeliveryDate", SqlDbType.DateTime,3),
					new SqlParameter("@PaymentTerms", SqlDbType.VarChar,32),
					new SqlParameter("@Shipping", SqlDbType.VarChar,16),
					new SqlParameter("@TotalFee", SqlDbType.Decimal,9),
					new SqlParameter("@AcceptedBy", SqlDbType.VarChar,16),
					new SqlParameter("@IssuedBy", SqlDbType.VarChar,16),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.PONO;
            parameters[1].Value = model.POOrderDate;
            parameters[2].Value = model.SupplierID;
            parameters[3].Value = model.ContactPerson;
            parameters[4].Value = model.Tel;
            parameters[5].Value = model.IssuedDate;
            parameters[6].Value = model.InnerBuyer;
            parameters[7].Value = model.CorporationID;
            parameters[8].Value = model.BillTo;
            parameters[9].Value = model.BillManager;
            parameters[10].Value = model.BillManagerTel;
            parameters[11].Value = model.ShipTo;
            parameters[12].Value = model.ShipManager;
            parameters[13].Value = model.ShipManagerTel;
            parameters[14].Value = model.DeliveryDate;
            parameters[15].Value = model.PaymentTerms;
            parameters[16].Value = model.Shipping;
            parameters[17].Value = model.TotalFee;
            parameters[18].Value = model.AcceptedBy;
            parameters[19].Value = model.IssuedBy;
            parameters[20].Value = model.State;
            parameters[21].Value = model.CreateUser;
            parameters[22].Value = model.CreateTime;
            parameters[23].Value = model.UpdateUser;
            parameters[24].Value = model.UpdateTime;

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
        public bool Update(Semitron_OMS.Model.OMS.POModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PO set ");
            strSql.Append("POOrderDate=@POOrderDate,");
            strSql.Append("SupplierID=@SupplierID,");
            strSql.Append("ContactPerson=@ContactPerson,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("IssuedDate=@IssuedDate,");
            strSql.Append("InnerBuyer=@InnerBuyer,");
            strSql.Append("CorporationID=@CorporationID,");
            strSql.Append("BillTo=@BillTo,");
            strSql.Append("BillManager=@BillManager,");
            strSql.Append("BillManagerTel=@BillManagerTel,");
            strSql.Append("ShipTo=@ShipTo,");
            strSql.Append("ShipManager=@ShipManager,");
            strSql.Append("ShipManagerTel=@ShipManagerTel,");
            strSql.Append("DeliveryDate=@DeliveryDate,");
            strSql.Append("PaymentTerms=@PaymentTerms,");
            strSql.Append("Shipping=@Shipping,");
            strSql.Append("TotalFee=@TotalFee,");
            strSql.Append("AcceptedBy=@AcceptedBy,");
            strSql.Append("IssuedBy=@IssuedBy,");
            strSql.Append("State=@State,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@POOrderDate", SqlDbType.DateTime,3),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@ContactPerson", SqlDbType.VarChar,16),
					new SqlParameter("@Tel", SqlDbType.VarChar,32),
					new SqlParameter("@IssuedDate", SqlDbType.DateTime,3),
					new SqlParameter("@InnerBuyer", SqlDbType.VarChar,16),
					new SqlParameter("@CorporationID", SqlDbType.Int,4),
					new SqlParameter("@BillTo", SqlDbType.NVarChar,1024),
					new SqlParameter("@BillManager", SqlDbType.VarChar,16),
					new SqlParameter("@BillManagerTel", SqlDbType.VarChar,32),
					new SqlParameter("@ShipTo", SqlDbType.NVarChar,1024),
					new SqlParameter("@ShipManager", SqlDbType.VarChar,16),
					new SqlParameter("@ShipManagerTel", SqlDbType.VarChar,32),
					new SqlParameter("@DeliveryDate", SqlDbType.DateTime,3),
					new SqlParameter("@PaymentTerms", SqlDbType.VarChar,32),
					new SqlParameter("@Shipping", SqlDbType.VarChar,16),
					new SqlParameter("@TotalFee", SqlDbType.Decimal,9),
					new SqlParameter("@AcceptedBy", SqlDbType.VarChar,16),
					new SqlParameter("@IssuedBy", SqlDbType.VarChar,16),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@PONO", SqlDbType.VarChar,32)};
            parameters[0].Value = model.POOrderDate;
            parameters[1].Value = model.SupplierID;
            parameters[2].Value = model.ContactPerson;
            parameters[3].Value = model.Tel;
            parameters[4].Value = model.IssuedDate;
            parameters[5].Value = model.InnerBuyer;
            parameters[6].Value = model.CorporationID;
            parameters[7].Value = model.BillTo;
            parameters[8].Value = model.BillManager;
            parameters[9].Value = model.BillManagerTel;
            parameters[10].Value = model.ShipTo;
            parameters[11].Value = model.ShipManager;
            parameters[12].Value = model.ShipManagerTel;
            parameters[13].Value = model.DeliveryDate;
            parameters[14].Value = model.PaymentTerms;
            parameters[15].Value = model.Shipping;
            parameters[16].Value = model.TotalFee;
            parameters[17].Value = model.AcceptedBy;
            parameters[18].Value = model.IssuedBy;
            parameters[19].Value = model.State;
            parameters[20].Value = model.CreateUser;
            parameters[21].Value = model.CreateTime;
            parameters[22].Value = model.UpdateUser;
            parameters[23].Value = model.UpdateTime;
            parameters[24].Value = model.ID;
            parameters[25].Value = model.PONO;

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
            strSql.Append("delete from PO ");
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
        public bool Delete(string PONO, int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PO ");
            strSql.Append(" where PONO=@PONO and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PONO", SqlDbType.VarChar,32),
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = PONO;
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
            strSql.Append("delete from PO ");
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
        public Semitron_OMS.Model.OMS.POModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,PONO,POOrderDate,SupplierID,ContactPerson,Tel,IssuedDate,InnerBuyer,CorporationID,BillTo,BillManager,BillManagerTel,ShipTo,ShipManager,ShipManagerTel,DeliveryDate,PaymentTerms,Shipping,TotalFee,AcceptedBy,IssuedBy,State,CreateUser,CreateTime,UpdateUser,UpdateTime from PO ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.POModel model = new Semitron_OMS.Model.OMS.POModel();
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
        public Semitron_OMS.Model.OMS.POModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.POModel model = new Semitron_OMS.Model.OMS.POModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["PONO"] != null)
                {
                    model.PONO = row["PONO"].ToString();
                }
                if (row["POOrderDate"] != null && row["POOrderDate"].ToString() != "")
                {
                    model.POOrderDate = DateTime.Parse(row["POOrderDate"].ToString());
                }
                if (row["SupplierID"] != null && row["SupplierID"].ToString() != "")
                {
                    model.SupplierID = int.Parse(row["SupplierID"].ToString());
                }
                if (row["ContactPerson"] != null)
                {
                    model.ContactPerson = row["ContactPerson"].ToString();
                }
                if (row["Tel"] != null)
                {
                    model.Tel = row["Tel"].ToString();
                }
                if (row["IssuedDate"] != null && row["IssuedDate"].ToString() != "")
                {
                    model.IssuedDate = DateTime.Parse(row["IssuedDate"].ToString());
                }
                if (row["InnerBuyer"] != null)
                {
                    model.InnerBuyer = row["InnerBuyer"].ToString();
                }
                if (row["CorporationID"] != null && row["CorporationID"].ToString() != "")
                {
                    model.CorporationID = int.Parse(row["CorporationID"].ToString());
                }
                if (row["BillTo"] != null)
                {
                    model.BillTo = row["BillTo"].ToString();
                }
                if (row["BillManager"] != null)
                {
                    model.BillManager = row["BillManager"].ToString();
                }
                if (row["BillManagerTel"] != null)
                {
                    model.BillManagerTel = row["BillManagerTel"].ToString();
                }
                if (row["ShipTo"] != null)
                {
                    model.ShipTo = row["ShipTo"].ToString();
                }
                if (row["ShipManager"] != null)
                {
                    model.ShipManager = row["ShipManager"].ToString();
                }
                if (row["ShipManagerTel"] != null)
                {
                    model.ShipManagerTel = row["ShipManagerTel"].ToString();
                }
                if (row["DeliveryDate"] != null && row["DeliveryDate"].ToString() != "")
                {
                    model.DeliveryDate = DateTime.Parse(row["DeliveryDate"].ToString());
                }
                if (row["PaymentTerms"] != null)
                {
                    model.PaymentTerms = row["PaymentTerms"].ToString();
                }
                if (row["Shipping"] != null)
                {
                    model.Shipping = row["Shipping"].ToString();
                }
                if (row["TotalFee"] != null && row["TotalFee"].ToString() != "")
                {
                    model.TotalFee = decimal.Parse(row["TotalFee"].ToString());
                }
                if (row["AcceptedBy"] != null)
                {
                    model.AcceptedBy = row["AcceptedBy"].ToString();
                }
                if (row["IssuedBy"] != null)
                {
                    model.IssuedBy = row["IssuedBy"].ToString();
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
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PONO,POOrderDate,SupplierID,ContactPerson,Tel,IssuedDate,InnerBuyer,CorporationID,BillTo,BillManager,BillManagerTel,ShipTo,ShipManager,ShipManagerTel,DeliveryDate,PaymentTerms,Shipping,TotalFee,AcceptedBy,IssuedBy,State,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM PO ");
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
            strSql.Append(" ID,PONO,POOrderDate,SupplierID,ContactPerson,Tel,IssuedDate,InnerBuyer,CorporationID,BillTo,BillManager,BillManagerTel,ShipTo,ShipManager,ShipManagerTel,DeliveryDate,PaymentTerms,Shipping,TotalFee,AcceptedBy,IssuedBy,State,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM PO ");
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
            strSql.Append("select count(1) FROM PO ");
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
            strSql.Append(")AS Row, T.*  from PO T ");
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
            parameters[0].Value = "PO";
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
        /// 分页查询采购订单记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>采购订单记录数据</returns>
        public DataSet GetPOPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.PO AS P LEFT JOIN dbo.Supplier AS S ON P.SupplierID = S.ID LEFT JOIN dbo.Corporation AS C ON P.CorporationID = C.ID LEFT JOIN CommonTable AS CT ON CT.TableName = 'PO' AND CT.FieldID = 'State' AND CT.[KEY] = P.STATE LEFT JOIN PaymentType AS PT ON PT.ID=P.PaymentTerms";
            //查询字段
            string strGetFields = " P.ID ,PONO ,POOrderDate= CONVERT(VARCHAR(10), POOrderDate, 120) ,S.SupplierName ,P.ContactPerson , P.Tel ,IssuedDate= CONVERT(VARCHAR(10), IssuedDate, 120) ,InnerBuyer ,C.CompanyName ,BillTo ,BillManager ,BillManagerTel ,ShipTo ,ShipManager ,ShipManagerTel ,DeliveryDate= CONVERT(VARCHAR(10), DeliveryDate, 120)  ,PaymentTerms=PaymentType ,Shipping ,TotalFee ,AcceptedBy ,IssuedBy ,State = CT.Value ,CreateTime = CONVERT(VARCHAR(20), P.CreateTime, 120) ,UpdateTime = CONVERT(VARCHAR(20), P.UpdateTime, 120) ";
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
        /// 生成采购计划
        /// </summary>
        /// <param name="iPOId">采购订单Id</param>
        /// <param name="strCreateUser">操作用户</param>
        /// <returns>>0：成功</returns>
        public string GeneratePOPlan(int iPOId, string strCOIds, string strCreateUser)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@POId", SqlDbType.Int,4),
                    new SqlParameter("@COIds", SqlDbType.VarChar),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
                    new SqlParameter("@Result", SqlDbType.NVarChar,2048)
			};
            parameters[0].Value = iPOId;
            parameters[1].Value = strCOIds;
            parameters[2].Value = strCreateUser;
            parameters[3].Direction = ParameterDirection.Output;
            int rowsAffected = 0;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.GeneratePOPlan, parameters, out rowsAffected);
            return parameters[3].Value.ToString();
        }

        /// <summary>
        /// 根据PONO获取实体
        /// </summary>
        public Model.OMS.POModel GetModelByPONO(string strPONO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,PONO,POOrderDate,SupplierID,ContactPerson,Tel,IssuedDate,InnerBuyer,CorporationID,BillTo,BillManager,BillManagerTel,ShipTo,ShipManager,ShipManagerTel,DeliveryDate,PaymentTerms,Shipping,TotalFee,AcceptedBy,IssuedBy,State,CreateUser,CreateTime,UpdateUser,UpdateTime from PO ");
            strSql.Append(" where PONO=@PONO");
            SqlParameter[] parameters = {
					new SqlParameter("@PONO", SqlDbType.VarChar,32)
			};
            parameters[0].Value = strPONO;

            Semitron_OMS.Model.OMS.POModel model = new Semitron_OMS.Model.OMS.POModel();
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
        /// 逻辑删除采购订单
        /// </summary>
        public string ValidateAndDelPO(string strUser, int iId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
                    new SqlParameter("@Result", SqlDbType.NVarChar,2048)
			};
            parameters[0].Value = iId;
            parameters[1].Value = strUser;
            parameters[2].Direction = ParameterDirection.Output;
            int rowsAffected = 0;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ValidateAndDelPO, parameters, out rowsAffected);
            return parameters[2].Value.ToString();
        }
        /// <summary>
        /// 获取采购订单关联的客户清单明细
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>客户清单明细数据</returns>
        public DataSet GetPOBindCustomerOrderDetail(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            SqlParameter[] parameters = { 
                    new SqlParameter("@POId", SqlDbType.Int),
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
            return DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.GetPOBindCustomerOrderDetail, parameters, "ds", out o_RowsCount, 5);
        }
        /// <summary>
        /// 获取产品采购分析列表数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <returns>产品采购分析列表数据</returns>
        public DataSet GetConfirmSecondData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            SqlParameter[] parameters = { 
                    new SqlParameter("@POId", SqlDbType.Int),
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
            return DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.GetConfirmSecondData, parameters, "ds", out o_RowsCount, 5);
        }
        /// <summary>
        /// 执行采购审核
        /// </summary> 
        public string ConfirmSecond(string strUserName, int iId, int iType)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@POId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
                    new SqlParameter("@Result", SqlDbType.NVarChar,2048)
			};
            parameters[0].Value = iId;
            parameters[1].Value = iType;
            parameters[2].Value = strUserName;
            parameters[3].Direction = ParameterDirection.Output;
            int rowsAffected = 0;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ConfirmSecond, parameters, out rowsAffected);
            return parameters[3].Value.ToString();
        }

        /// <summary>
        /// 根据PONO更新采购订单状态
        /// </summary>
        public bool UpdateStateByPONO(string strPONO, string strUser, int iState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update PO Set State=@State,UpdateUser=@UpdateUser,UpdateTime=getdate()");
            strSql.Append(" where PONO=@PONO");
            SqlParameter[] parameters = {
					new SqlParameter("@PONO", SqlDbType.VarChar,32),
                    new SqlParameter("@UpdateUser", SqlDbType.VarChar,32),
					new SqlParameter("@State", SqlDbType.Int,4)};
            parameters[0].Value = strPONO;
            parameters[1].Value = strUser;
            parameters[2].Value = iState;

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
        /// 绑定采购计划
        /// </summary>
        /// <param name="iPOId">采购订单ID</param>
        /// <param name="strPOPlanIdList">采购计划ID串</param>
        /// <param name="strUser">操作用户</param>
        /// <returns>是否成功</returns>
        public int BindPOPlan(int iPOId, string strPOPlanIdList, string strUser)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@POId", SqlDbType.Int,4),
                    new SqlParameter("@OrderIds", SqlDbType.VarChar),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16)
			};
            parameters[0].Value = iPOId;
            parameters[1].Value = strPOPlanIdList;
            parameters[2].Value = strUser;
            int rowsAffected = 0;
            return DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.BindPOPlan, parameters, out rowsAffected);
        }

        /// <summary>
        /// 通过采购订单Id获取相关联采购计划列表树
        /// </summary>
        /// <param name="iPOId">采购订单ID</param>
        /// <returns>计划数据表</returns>
        public DataSet GetBindPOPlanByPoId(int iPOId, int iAdminID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@POId", SqlDbType.Int,4),
                   new SqlParameter("@AdminID", SqlDbType.Int,4) //加入权限控制
			};
            parameters[0].Value = iPOId;
            parameters[1].Value = iAdminID;
            return DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.GetBindPOPlanByPoId, parameters, "GetBindPOPlanByPoId");
        }

        /// <summary>
        /// 供应商审核
        /// </summary>
        /// <param name="iPOId">采购订单ID</param>
        /// <param name="strUser">操作用户</param>
        /// <returns>执行结果</returns>
        public string ConfirmSupplier(int iPOId, string strUser)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@POId", SqlDbType.Int,4),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
                     new SqlParameter("@Result", SqlDbType.NVarChar,2048) 
			};
            parameters[0].Value = iPOId;
            parameters[1].Value = strUser;
            parameters[2].Direction = ParameterDirection.Output;
            int rowsAffected = 0;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ConfirmSupplier, parameters, out rowsAffected);
            return parameters[2].Value.ToString();
        }


        #endregion  ExtensionMethod


    }
}

