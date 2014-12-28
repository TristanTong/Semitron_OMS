/**  
* CustomerOrderDetailDAL.cs
*
* 功 能： N/A
* 类 名： CustomerOrderDetailDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:12:28   童荣辉    初版
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
    /// 数据访问类:CustomerOrderDetailDAL
    /// </summary>
    public partial class CustomerOrderDetailDAL
    {
        public CustomerOrderDetailDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "CustomerOrderDetail");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CustomerOrderDetail");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.CustomerOrderDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CustomerOrderDetail(");
            strSql.Append("InnerOrderNO,CPN,MPN,MFG,DC,ROHS,CRD,CustQuantity,SaleExchangeRate,SaleRealCurrency,SaleRealPrice,SaleStandardCurrency,SalePrice,OtherFee,OtherFeeRemark,AlreadyQty,ShipmentDate,CustomerInStockDate,IsCustomerPay,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@InnerOrderNO,@CPN,@MPN,@MFG,@DC,@ROHS,@CRD,@CustQuantity,@SaleExchangeRate,@SaleRealCurrency,@SaleRealPrice,@SaleStandardCurrency,@SalePrice,@OtherFee,@OtherFeeRemark,@AlreadyQty,@ShipmentDate,@CustomerInStockDate,@IsCustomerPay,@AvailFlag,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@InnerOrderNO", SqlDbType.VarChar,32),
					new SqlParameter("@CPN", SqlDbType.VarChar,32),
					new SqlParameter("@MPN", SqlDbType.VarChar,32),
					new SqlParameter("@MFG", SqlDbType.NVarChar,32),
					new SqlParameter("@DC", SqlDbType.VarChar,16),
					new SqlParameter("@ROHS", SqlDbType.Bit,1),
					new SqlParameter("@CRD", SqlDbType.DateTime),
					new SqlParameter("@CustQuantity", SqlDbType.Int,4),
					new SqlParameter("@SaleExchangeRate", SqlDbType.Decimal,9),
					new SqlParameter("@SaleRealCurrency", SqlDbType.VarChar,16),
					new SqlParameter("@SaleRealPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SaleStandardCurrency", SqlDbType.VarChar,16),
					new SqlParameter("@SalePrice", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFee", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFeeRemark", SqlDbType.NVarChar,512),
					new SqlParameter("@AlreadyQty", SqlDbType.Int,4),
					new SqlParameter("@ShipmentDate", SqlDbType.DateTime),
					new SqlParameter("@CustomerInStockDate", SqlDbType.DateTime),
					new SqlParameter("@IsCustomerPay", SqlDbType.Bit,1),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.InnerOrderNO;
            parameters[1].Value = model.CPN;
            parameters[2].Value = model.MPN;
            parameters[3].Value = model.MFG;
            parameters[4].Value = model.DC;
            parameters[5].Value = model.ROHS;
            parameters[6].Value = model.CRD;
            parameters[7].Value = model.CustQuantity;
            parameters[8].Value = model.SaleExchangeRate;
            parameters[9].Value = model.SaleRealCurrency;
            parameters[10].Value = model.SaleRealPrice;
            parameters[11].Value = model.SaleStandardCurrency;
            parameters[12].Value = model.SalePrice;
            parameters[13].Value = model.OtherFee;
            parameters[14].Value = model.OtherFeeRemark;
            parameters[15].Value = model.AlreadyQty;
            parameters[16].Value = model.ShipmentDate;
            parameters[17].Value = model.CustomerInStockDate;
            parameters[18].Value = model.IsCustomerPay;
            parameters[19].Value = model.AvailFlag;
            parameters[20].Value = model.CreateUser;
            parameters[21].Value = model.CreateTime;
            parameters[22].Value = model.UpdateUser;
            parameters[23].Value = model.UpdateTime;

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
        public bool Update(Semitron_OMS.Model.OMS.CustomerOrderDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CustomerOrderDetail set ");
            strSql.Append("InnerOrderNO=@InnerOrderNO,");
            strSql.Append("CPN=@CPN,");
            strSql.Append("MPN=@MPN,");
            strSql.Append("MFG=@MFG,");
            strSql.Append("DC=@DC,");
            strSql.Append("ROHS=@ROHS,");
            strSql.Append("CRD=@CRD,");
            strSql.Append("CustQuantity=@CustQuantity,");
            strSql.Append("SaleExchangeRate=@SaleExchangeRate,");
            strSql.Append("SaleRealCurrency=@SaleRealCurrency,");
            strSql.Append("SaleRealPrice=@SaleRealPrice,");
            strSql.Append("SaleStandardCurrency=@SaleStandardCurrency,");
            strSql.Append("SalePrice=@SalePrice,");
            strSql.Append("OtherFee=@OtherFee,");
            strSql.Append("OtherFeeRemark=@OtherFeeRemark,");
            strSql.Append("AlreadyQty=@AlreadyQty,");
            strSql.Append("ShipmentDate=@ShipmentDate,");
            strSql.Append("CustomerInStockDate=@CustomerInStockDate,");
            strSql.Append("IsCustomerPay=@IsCustomerPay,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@InnerOrderNO", SqlDbType.VarChar,32),
					new SqlParameter("@CPN", SqlDbType.VarChar,32),
					new SqlParameter("@MPN", SqlDbType.VarChar,32),
					new SqlParameter("@MFG", SqlDbType.NVarChar,32),
					new SqlParameter("@DC", SqlDbType.VarChar,16),
					new SqlParameter("@ROHS", SqlDbType.Bit,1),
					new SqlParameter("@CRD", SqlDbType.DateTime),
					new SqlParameter("@CustQuantity", SqlDbType.Int,4),
					new SqlParameter("@SaleExchangeRate", SqlDbType.Decimal,9),
					new SqlParameter("@SaleRealCurrency", SqlDbType.VarChar,16),
					new SqlParameter("@SaleRealPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SaleStandardCurrency", SqlDbType.VarChar,16),
					new SqlParameter("@SalePrice", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFee", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFeeRemark", SqlDbType.NVarChar,512),
					new SqlParameter("@AlreadyQty", SqlDbType.Int,4),
					new SqlParameter("@ShipmentDate", SqlDbType.DateTime),
					new SqlParameter("@CustomerInStockDate", SqlDbType.DateTime),
					new SqlParameter("@IsCustomerPay", SqlDbType.Bit,1),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.InnerOrderNO;
            parameters[1].Value = model.CPN;
            parameters[2].Value = model.MPN;
            parameters[3].Value = model.MFG;
            parameters[4].Value = model.DC;
            parameters[5].Value = model.ROHS;
            parameters[6].Value = model.CRD;
            parameters[7].Value = model.CustQuantity;
            parameters[8].Value = model.SaleExchangeRate;
            parameters[9].Value = model.SaleRealCurrency;
            parameters[10].Value = model.SaleRealPrice;
            parameters[11].Value = model.SaleStandardCurrency;
            parameters[12].Value = model.SalePrice;
            parameters[13].Value = model.OtherFee;
            parameters[14].Value = model.OtherFeeRemark;
            parameters[15].Value = model.AlreadyQty;
            parameters[16].Value = model.ShipmentDate;
            parameters[17].Value = model.CustomerInStockDate;
            parameters[18].Value = model.IsCustomerPay;
            parameters[19].Value = model.AvailFlag;
            parameters[20].Value = model.CreateUser;
            parameters[21].Value = model.CreateTime;
            parameters[22].Value = model.UpdateUser;
            parameters[23].Value = model.UpdateTime;
            parameters[24].Value = model.ID;

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
            strSql.Append("delete from CustomerOrderDetail ");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CustomerOrderDetail ");
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
        public Semitron_OMS.Model.OMS.CustomerOrderDetailModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,InnerOrderNO,CPN,MPN,MFG,DC,ROHS,CRD,CustQuantity,SaleExchangeRate,SaleRealCurrency,SaleRealPrice,SaleStandardCurrency,SalePrice,OtherFee,OtherFeeRemark,AlreadyQty,ShipmentDate,CustomerInStockDate,IsCustomerPay,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime from CustomerOrderDetail ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.CustomerOrderDetailModel model = new Semitron_OMS.Model.OMS.CustomerOrderDetailModel();
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
        public Semitron_OMS.Model.OMS.CustomerOrderDetailModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.CustomerOrderDetailModel model = new Semitron_OMS.Model.OMS.CustomerOrderDetailModel();
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
                if (row["CPN"] != null)
                {
                    model.CPN = row["CPN"].ToString();
                }
                if (row["MPN"] != null)
                {
                    model.MPN = row["MPN"].ToString();
                }
                if (row["MFG"] != null)
                {
                    model.MFG = row["MFG"].ToString();
                }
                if (row["DC"] != null)
                {
                    model.DC = row["DC"].ToString();
                }
                if (row["ROHS"] != null && row["ROHS"].ToString() != "")
                {
                    if ((row["ROHS"].ToString() == "1") || (row["ROHS"].ToString().ToLower() == "true"))
                    {
                        model.ROHS = true;
                    }
                    else
                    {
                        model.ROHS = false;
                    }
                }
                if (row["CRD"] != null && row["CRD"].ToString() != "")
                {
                    model.CRD = DateTime.Parse(row["CRD"].ToString());
                }
                if (row["CustQuantity"] != null && row["CustQuantity"].ToString() != "")
                {
                    model.CustQuantity = int.Parse(row["CustQuantity"].ToString());
                }
                if (row["SaleExchangeRate"] != null && row["SaleExchangeRate"].ToString() != "")
                {
                    model.SaleExchangeRate = decimal.Parse(row["SaleExchangeRate"].ToString());
                }
                if (row["SaleRealCurrency"] != null)
                {
                    model.SaleRealCurrency = row["SaleRealCurrency"].ToString();
                }
                if (row["SaleRealPrice"] != null && row["SaleRealPrice"].ToString() != "")
                {
                    model.SaleRealPrice = decimal.Parse(row["SaleRealPrice"].ToString());
                }
                if (row["SaleStandardCurrency"] != null)
                {
                    model.SaleStandardCurrency = row["SaleStandardCurrency"].ToString();
                }
                if (row["SalePrice"] != null && row["SalePrice"].ToString() != "")
                {
                    model.SalePrice = decimal.Parse(row["SalePrice"].ToString());
                }
                if (row["OtherFee"] != null && row["OtherFee"].ToString() != "")
                {
                    model.OtherFee = decimal.Parse(row["OtherFee"].ToString());
                }
                if (row["OtherFeeRemark"] != null)
                {
                    model.OtherFeeRemark = row["OtherFeeRemark"].ToString();
                }
                if (row["AlreadyQty"] != null && row["AlreadyQty"].ToString() != "")
                {
                    model.AlreadyQty = int.Parse(row["AlreadyQty"].ToString());
                }
                if (row["ShipmentDate"] != null && row["ShipmentDate"].ToString() != "")
                {
                    model.ShipmentDate = DateTime.Parse(row["ShipmentDate"].ToString());
                }
                if (row["CustomerInStockDate"] != null && row["CustomerInStockDate"].ToString() != "")
                {
                    model.CustomerInStockDate = DateTime.Parse(row["CustomerInStockDate"].ToString());
                }
                if (row["IsCustomerPay"] != null && row["IsCustomerPay"].ToString() != "")
                {
                    if ((row["IsCustomerPay"].ToString() == "1") || (row["IsCustomerPay"].ToString().ToLower() == "true"))
                    {
                        model.IsCustomerPay = true;
                    }
                    else
                    {
                        model.IsCustomerPay = false;
                    }
                }
                if (row["AvailFlag"] != null && row["AvailFlag"].ToString() != "")
                {
                    if ((row["AvailFlag"].ToString() == "1") || (row["AvailFlag"].ToString().ToLower() == "true"))
                    {
                        model.AvailFlag = true;
                    }
                    else
                    {
                        model.AvailFlag = false;
                    }
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
            strSql.Append("select ID,InnerOrderNO,CPN,MPN,MFG,DC,ROHS,CRD,CustQuantity,SaleExchangeRate,SaleRealCurrency,SaleRealPrice,SaleStandardCurrency,SalePrice,OtherFee,OtherFeeRemark,AlreadyQty,ShipmentDate,CustomerInStockDate,IsCustomerPay,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM CustomerOrderDetail ");
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
            strSql.Append(" ID,InnerOrderNO,CPN,MPN,MFG,DC,ROHS,CRD,CustQuantity,SaleExchangeRate,SaleRealCurrency,SaleRealPrice,SaleStandardCurrency,SalePrice,OtherFee,OtherFeeRemark,AlreadyQty,ShipmentDate,CustomerInStockDate,IsCustomerPay,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM CustomerOrderDetail ");
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
            strSql.Append("select count(1) FROM CustomerOrderDetail ");
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
            strSql.Append(")AS Row, T.*  from CustomerOrderDetail T ");
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
            parameters[0].Value = "CustomerOrderDetail";
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
        /// 分页查询产品请单记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>产品请单记录数据</returns>
        public DataSet GetCustomerOrderDetailPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.CustomerOrderDetail AS D LEFT JOIN dbo.CustomerOrder AS O ON O.InnerOrderNO = D.InnerOrderNO LEFT JOIN dbo.Customer AS C ON C.ID = O.CustomerID LEFT JOIN Brand AS B ON B.ID=D.MFG LEFT JOIN dbo.PaymentType AS P ON O.PaymentTypeID = P.ID LEFT JOIN  dbo.Corporation AS M ON M.ID=O.CorporationID LEFT JOIN CommonTable AS CT ON CT.TableName='CustomerOrder' AND CT.FieldID='State' AND CT.[KEY]=O.STATE LEFT JOIN Admin AS A1 ON A1.AdminID=O.AssignToInnerBuyer  LEFT JOIN (SELECT  ObjId ,FileNum = COUNT(1) FROM dbo.Attachment WITH ( NOLOCK ) WHERE ObjType = 'CustomerOrder' AND AvailFlag = 1 GROUP BY ObjId) AS A ON A.ObjId=O.ID LEFT JOIN dbo.ShippingPlanDetail AS SPD WITH(NOLOCK) ON SPD.CustomerDetailID=D.ID AND SPD.AvailFlag=1 LEFT JOIN dbo.ShippingPlan AS SP WITH(NOLOCK) ON SP.ID=SPD.ShippingPlanID LEFT JOIN dbo.ShippingListDetail AS SLD WITH(NOLOCK) ON SLD.ShippingPlanDetailID=SPD.ID AND SLD.AvailFlag=1 LEFT JOIN dbo.ShippingList AS SL WITH(NOLOCK) ON SL.ID=SLD.ShippingListID AND SL.State=1 LEFT JOIN dbo.Warehouse AS W ON W.WCode=SLD.StockCode AND W.AvailFlag=1 LEFT JOIN dbo.ProductInfo AS PIF WITH(NOLOCK) ON PIF.ProductCode=SPD.ProductCode LEFT JOIN dbo.Supplier AS S WITH(NOLOCK) ON S.ID=PIF.SupplierID  ";
            //查询字段
            string strGetFields = " D.ID , D.InnerOrderNO , O.CustomerOrderNO , C.CustomerName , D.CPN , D.MPN , MFG=B.BrandName , D.DC , D.ROHS , D.CRD , D.CustQuantity , D.SaleExchangeRate , SaleRealCurrency = ( SELECT TOP 1 ShortName FROM   CurrencyType WHERE  CurrencyType.ID = D.SaleRealCurrency ) , D.SaleRealPrice , SaleStandardCurrency = ( SELECT TOP 1 ShortName FROM   CurrencyType WHERE  CurrencyType.ID = D.SaleStandardCurrency ) , D.SalePrice , D.OtherFee , D.OtherFeeRemark , IsCustomerPay = CASE D.IsCustomerPay WHEN 1 THEN '是' ELSE '否' END , AvailFlag = CASE D.AvailFlag WHEN 1 THEN '是' ELSE '否' END , ShipmentDate = CONVERT(VARCHAR(10), ISNULL(D.ShipmentDate,SL.OutStockDate), 120) , CustomerInStockDate = CONVERT(VARCHAR(10), D.CustomerInStockDate, 120) , CreateTime = CONVERT(VARCHAR(20), D.CreateTime, 120) , UpdateTime = CONVERT(VARCHAR(20), D.UpdateTime, 120),M.CompanyName,O.InnerSalesMan ,AssignToInnerBuyer=A1.Name, PaymentType = P.PaymentType,State=CT.Value ,FileNum=ISNULL(A.FileNum,0),SLD.ProductCode,SPD.PlanQty,SLD.OutQty,SP.ShippingPlanNo,SLD.StockCode,W.WName,S.SupplierName,ProductMPN=PIF.MPN,SL.ShippingListNo,OutStockDate= CONVERT(VARCHAR(10), SL.ShippingListDate, 120) ";
            //查询条件
            string strWhere = SQLOperateHelper.GetSQLCondition(searchInfo.ConditionFilter, false);
            LogHelper.WriteLogInfo("GetCustomerOrderDetailPageData:" + strWhere, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
        /// 设置明细状态为无效
        /// </summary>
        /// <param name="iId">明细数据Id</param>
        /// <param name="iStatus">有效无效状态 1 有效 0 无效</param>
        /// <returns></returns>
        public int SetValid(int iId, int iStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CustomerOrderDetail set AvailFlag=@AvailFlag ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@AvailFlag", SqlDbType.Int,4)
			};
            parameters[0].Value = iId;
            parameters[1].Value = iStatus;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据内部订单号与客户型号判断是否存在记录
        /// </summary>
        /// <param name="strInnerOrderNO">内部订单号</param>
        /// <param name="strCPN">客户型号</param>
        /// <returns>是否存在</returns>
        public bool Exists(string strInnerOrderNO, string strCPN)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CustomerOrderDetail");
            strSql.Append(" where InnerOrderNO=@InnerOrderNO AND CPN=@CPN AND AvailFlag=1");
            SqlParameter[] parameters = {
					new SqlParameter("@InnerOrderNO", SqlDbType.VarChar,32),
                    new SqlParameter("@CPN", SqlDbType.VarChar,32)
			};
            parameters[0].Value = strInnerOrderNO;
            parameters[1].Value = strCPN;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 逻辑删除明细
        /// </summary>
        public string ValidateAndDelCustomerOrderDetail(string strUser, int iId)
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
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ValidateAndDelCustomerOrderDetail, parameters, out rowsAffected);
            return parameters[2].Value.ToString();
        }
        /// <summary>
        /// 设置客户是否已付款
        /// </summary>
        /// <param name="iId">明细数据Id</param>
        /// <param name="bIsCustomerPay">是否已付款 1 已付款 0 未付款</param>
        /// <param name="dtCustomerInStockDate">客户入库时间</param>
        /// <returns>是否成功</returns>
        public int SetCustomerOrderDetailItems(int iId, bool bIsCustomerPay, DateTime? dtCustomerInStockDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CustomerOrderDetail set IsCustomerPay=@IsCustomerPay,CustomerInStockDate=@CustomerInStockDate ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@IsCustomerPay", SqlDbType.Bit),
                    new SqlParameter("@CustomerInStockDate", SqlDbType.DateTime)
			};
            parameters[0].Value = iId;
            parameters[1].Value = bIsCustomerPay;
            parameters[2].Value = dtCustomerInStockDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取产品清单查找列表
        /// </summary>
        /// <param name="lstFilter"></param>
        /// <returns></returns>
        public DataTable GetCustomerOrderDetailLookupList(System.Collections.Generic.List<SQLConditionFilter> lstFilter)
        {
            //查询字段
            string strGetFields = " CustomerOrderDetailId=D.ID,D.InnerOrderNo,O.CustomerOrderNO,O.PaymentTypeID,D.CPN,D.MPN,CustOrderDate=CONVERT(VARCHAR(10),O.CustOrderDate,120),O.InnerSalesMan,AssignToInnerBuyer=A1.Name,D.CustQuantity,PlaningQty=ISNULL(SUM(CASE WHEN ISNULL(SP.IsApproved,0)=0 THEN LD.OutQty ELSE 0 END),0),DoingOutStockQty=ISNULL(SUM(CASE WHEN ISNULL(SL.IsApproved,0)=0 THEN LD.OutQty ELSE 0 END),0),AlreadyQty=ISNULL(SUM(CASE WHEN ISNULL(SL.IsApproved,0)=1 THEN LD.OutQty ELSE 0 END),0),CustomerCode=C.CCode,C.CustomerName,CustomerID=C.ID,O.CorporationID,CorporationName=MAX(CP.CompanyName),SalePrice=MAX(D.SalePrice),SaleTotal =MAX(D.SalePrice*D.CustQuantity) ";

            //查询表名
            string strTableName = " dbo.CustomerOrderDetail AS D WITH (NOLOCK) INNER JOIN dbo.CustomerOrder AS O ON D.InnerOrderNO=O.InnerOrderNO LEFT JOIN dbo.Customer AS C WITH(NOLOCK) ON C.ID=O.CustomerID LEFT JOIN dbo.Admin AS A1 ON A1.AdminID=O.AssignToInnerBuyer LEFT JOIN dbo.ShippingPlanDetail AS PD WITH(NOLOCK) ON PD.CustomerDetailID=D.ID AND PD.AvailFlag=1 LEFT JOIN dbo.ShippingPlan AS SP WITH(NOLOCK) ON Sp.ID=PD.ShippingPlanID AND SP.State=1 LEFT JOIN dbo.ShippingListDetail AS LD WITH(NOLOCK) ON LD.ShippingPlanDetailID=PD.ID AND LD.AvailFlag=1 LEFT JOIN dbo.ShippingList AS SL WITH(NOLOCK) ON SL.ID=LD.ShippingListID AND SL.State=1 LEFT JOIN dbo.Corporation AS CP ON CP.ID=O.CorporationID ";

            //查询条件 只有指定了采购后客户订单才显示，可作出货计划
            string strWhere = SQLOperateHelper.GetSQLCondition(lstFilter, false) + " AND D.AvailFlag=1 AND O.State NOT IN (-100,100,110)";

            //分组条件
            string strGroupBy = " D.ID,D.InnerOrderNo,O.CustomerOrderNO,D.CPN,D.MPN,O.CustOrderDate,O.InnerSalesMan,A1.Name,D.CustQuantity,D.AlreadyQty,C.CCode,C.CustomerName,C.ID,O.CorporationID,O.PaymentTypeID ";

            return DbHelperSQL.Query("SELECT " + strGetFields + " FROM " + strTableName + " WHERE " + strWhere + " GROUP BY " + strGroupBy).Tables[0];
        }

        #endregion  ExtensionMethod


    }
}

