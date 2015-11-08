USE [OMSOne]
GO

/****** Object:  StoredProcedure [dbo].[Pro_Report_ShippingListForFinance]    Script Date: 11/08/2015 17:40:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*  
******************************************************************************
--存储过程名称：客户交易总表存储过程。
--存储过程说明: 由于统计时需按照不同的查询条件
--作者：Tristan
--创建时间：2015-04-19
--版本：1.0    
--参数说明：以ps_开始的为查询条件，参见参数列注释。
--修改历史:(版本/时间/修改者/修改要点)
-- exec [Pro_Report_ShippingListForFinance] @strOrder='InnerOrderNO',@intRowsCount=0,@ps_SearchType='ExportExcel',@ps_BeginTime='2015-02-09',@ps_EndTime='2015-02-12'
******************************************************************************
#1 加入公司法人字段 20151108
*/
ALTER PROCEDURE [dbo].[Pro_Report_ShippingListForFinance]
    @ps_SearchType VARCHAR(255) = '' ,
    @ps_BeginTime VARCHAR(255) = '' ,
    @ps_EndTime VARCHAR(255) = '' ,
    @ps_TimeType VARCHAR(255) = '' ,
    @pageSize INT = 20 ,	--每页显示记录数
    @pageIndex INT = 1 ,--当前页面索引
    @strOrder VARCHAR(100) = 'InnerOrderNO' , --排序列名
    @orderType BIT = 1 ,        --排序类型  0为升序
    @intRowsCount BIGINT OUTPUT              --返回记录总数    
AS 
    BEGIN
        SET NOCOUNT ON
        DECLARE @groupbyField VARCHAR(1000)  --组合列字段
        DECLARE @innerFiled VARCHAR(1000)  --组合查询中的列字段
        DECLARE @innerWhere VARCHAR(MAX)  --组合内查询条件
        DECLARE @strSql VARCHAR(MAX)  --主语句
	
	--计算总记录数使用
        DECLARE @SQL NVARCHAR(4000)
        DECLARE @R BIGINT
	
	--排序分页时使用
        DECLARE @strTemp VARCHAR(1000) --临时变量
        DECLARE @strOrders VARCHAR(1000) --排序语句
	
        SET @intRowsCount = 0	
        SET @groupbyField = ''
        SET @innerFiled = ''
        SET @innerWhere = ' AND 1 = 1 '
        SET @R = 0   
        
        IF @ps_TimeType IS NOT NULL
            AND @ps_TimeType != '' --结合查询条件列出值,不为空表有选择该条件
            BEGIN
                SET @strTemp = ' L.OutStockDate '
            END
        ELSE 
            SET @strTemp = ' L.OutStockDate ' 
            
        IF @ps_BeginTime IS NOT NULL
            AND @ps_BeginTime != '' --结合查询条件列出值,不为空表有选择该条件
            SET @innerWhere = @innerWhere + ' and ' + @strTemp
                + '>=CONVERT(DATE,''' + @ps_BeginTime + ''')'  --查询条件
                
        IF @ps_EndTime IS NOT NULL
            AND @ps_EndTime != '' --结合查询条件列出值,不为空表有选择该条件
            BEGIN
                SET @innerWhere = @innerWhere + ' and ' + @strTemp
                    + '<=CONVERT(DATE,''' + @ps_EndTime + ''')'  --查询条件
            END 
        SET @strSql = ' SELECT CN.CompanyName,O.InnerOrderNO ,CustomerDetailID=CD.ID, T.Value , O.InnerSalesMan , C.CustomerName , O.CustOrderDate , O.CustomerOrderNO , CD.CPN , CD.MPN , B.BrandName , CD.CustQuantity , OutQty = '''' , ShippingListNo = '''' , IsApproved = '''' , CD.SaleExchangeRate , SaleRealCurrency = CT.CurrencyName , SaleRealPrice , SaleRealTotalPrice = SaleRealPrice * CD.CustQuantity , PT.PaymentType , OutStockDate = '''' FROM   dbo.CustomerOrder AS O WITH ( NOLOCK ) INNER JOIN dbo.CustomerOrderDetail AS CD WITH ( NOLOCK ) ON CD.InnerOrderNO = O.InnerOrderNO AND CD.AvailFlag = 1 LEFT JOIN dbo.CommonTable AS T ON T.TableName = ''CustomerOrder'' AND T.FieldID = ''State'' AND O.State = T.[Key] LEFT JOIN dbo.Customer AS C ON C.ID = O.CustomerID LEFT JOIN dbo.Brand AS B ON B.ID = CD.MFG LEFT JOIN dbo.CurrencyType AS CT ON CT.ID = CD.SaleRealCurrency LEFT JOIN dbo.PaymentType AS PT ON PT.ID = O.PaymentTypeID LEFT JOIN dbo.Corporation AS CN
 WITH(NOLOCK) ON CN.ID=O.CorporationID '
        
        --SET @strSql = @strSql + @innerWhere
	
        IF @orderType = 0 
            BEGIN
			--为0是升序
                SET @strOrders = ' order by  ' + @strOrder + ' asc '
            END
        ELSE 
            BEGIN
			--否则为降序
                SET @strOrders = ' order by  ' + @strOrder + ' desc '
            END				
        --IF @ps_SearchType = '' 
        --    BEGIN
        --        SET @strSql = 'SELECT  rownum = ROW_NUMBER() OVER ( '
        --            + @strOrders + ' ) ,* FROM  (' + @strSql + ') E'
        --        PRINT @strSql
        --        EXEC (@strSql)  
        --    END
        
        --客户订单状态	销 售	客户名称	客户订单日期	客户订单号	客户型号	厂商型号	品牌	订单数量	出库数量	出库单号	是否审核	卖汇率	实际卖货币	实际卖价	实际售价总额	客户付款方式	出货日期
       
        IF OBJECT_ID('tempdb..#tempExcel') IS NOT NULL 
            DROP TABLE #tempExcel
        CREATE TABLE #tempExcel
            (
              CompanyName NVARCHAR(128) ,  --#1
              InnerOrderNO NVARCHAR(50) ,
              CustomerDetailID INT ,
              Value NVARCHAR(50) ,
              InnerSalesMan NVARCHAR(50) ,
              CustomerName NVARCHAR(128) ,
              CustOrderDate DATE ,
              CustomerOrderNO NVARCHAR(512) ,
              CPN NVARCHAR(128) ,
              MPN NVARCHAR(128) ,
              BrandName NVARCHAR(128) ,
              CustQuantity INT ,
              OutQty NVARCHAR(1024) ,
              ShippingListNo NVARCHAR(1024) ,
              IsApproved NVARCHAR(1024) ,
              SaleExchangeRate DECIMAL(18, 2) ,
              SaleRealCurrency NVARCHAR(128) ,
              SaleRealPrice DECIMAL(18, 2) ,
              SaleRealTotalPrice DECIMAL(18, 2) ,
              PaymentType NVARCHAR(128) ,
              OutStockDate NVARCHAR(1024)
            )
       
        IF @ps_SearchType = 'ExportExcel' 
            BEGIN
                SET @strSql = 'INSERT  INTO #tempExcel ' + @strSql
                PRINT @strSql
                EXEC (@strSql)
                
              
                IF OBJECT_ID('tempdb..#tempShippingList') IS NOT NULL 
                    DROP TABLE #tempShippingList
               
                CREATE TABLE #tempShippingList
                    (
                      CustomerDetailID INT ,
                      OutQty INT ,
                      ShippingListNo NVARCHAR(50) ,
                      OutStockDate DATE ,
                      IsApproved NVARCHAR(20)
                    )
               
                SET @strSql = 'INSERT  INTO #tempShippingList
                        SELECT  PD.CustomerDetailID ,
                                D.OutQty ,
                                L.ShippingListNo ,
                                OutStockDate = CAST(L.OutStockDate AS DATE) ,
                                IsApproved = CASE WHEN L.IsApproved = 1
                                                  THEN ''是''
                                                  ELSE ''否''
                                             END
                        FROM    dbo.ShippingPlanDetail AS PD WITH ( NOLOCK )
                                INNER JOIN dbo.ShippingListDetail AS D WITH ( NOLOCK ) ON D.ShippingPlanDetailID = PD.ID
                                                              AND D.AvailFlag = 1
                                INNER JOIN dbo.ShippingList AS L WITH ( NOLOCK ) ON D.ShippingListID = L.ID
                                                              AND L.State = 1
                        WHERE   PD.CustomerDetailID IN ( SELECT
                                                              CustomerDetailID
                                                         FROM #tempExcel )
                                AND PD.AvailFlag = 1 '
                
                SET @strSql = @strSql + @innerWhere
                EXEC (@strSql)
                
                DELETE  FROM #tempExcel
                WHERE   CustomerDetailID NOT IN ( SELECT    CustomerDetailID
                                                  FROM      #tempShippingList )
                
                UPDATE  CD
                SET     OutQty = STUFF(( SELECT ','
                                                + CAST(PD.OutQty AS VARCHAR(50))
                                         FROM   #tempShippingList PD
                                         WHERE  PD.CustomerDetailID = CD.CustomerDetailID
                                       FOR
                                         XML PATH('')
                                       ), 1, 1, '') ,
                        ShippingListNo = STUFF(( SELECT ','
                                                        + CAST(PD.ShippingListNo AS VARCHAR(50))
                                                 FROM   #tempShippingList PD
                                                 WHERE  PD.CustomerDetailID = CD.CustomerDetailID
                                               FOR
                                                 XML PATH('')
                                               ), 1, 1, '') ,
                        OutStockDate = STUFF(( SELECT   ','
                                                        + CAST(PD.OutStockDate AS VARCHAR(50))
                                               FROM     #tempShippingList PD
                                               WHERE    PD.CustomerDetailID = CD.CustomerDetailID
                                             FOR
                                               XML PATH('')
                                             ), 1, 1, '') ,
                        IsApproved = STUFF(( SELECT ','
                                                    + CAST(PD.IsApproved AS VARCHAR(50))
                                             FROM   #tempShippingList PD
                                             WHERE  PD.CustomerDetailID = CD.CustomerDetailID
                                           FOR
                                             XML PATH('')
                                           ), 1, 1, '')
                FROM    #tempExcel AS CD 
                
                    
                SELECT  [公司法人] = CompanyName , --#1
                        [客户订单状态] = Value ,
                        [销 售] = InnerSalesMan ,
                        [客户名称] = CustomerName ,
                        [客户订单日期] = CustOrderDate ,
                        [客户订单号] = CustomerOrderNO ,
                        [客户型号] = CPN ,
                        [厂商型号] = MPN ,
                        [品牌] = BrandName ,
                        [订单数量] = CustQuantity ,
                        [出库数量] = OutQty ,
                        [出库单号] = ShippingListNo ,
                        [是否审核] = IsApproved ,
                        [卖汇率] = SaleExchangeRate ,
                        [实际卖货币] = SaleRealCurrency ,
                        [实际卖价] = SaleRealPrice ,
                        [实际售价总额] = SaleRealTotalPrice ,
                        [客户付款方式] = PaymentType ,
                        [出货日期] = OutStockDate
                FROM    #tempExcel
                
                IF OBJECT_ID('tempdb..#tempExcel') IS NOT NULL 
                    DROP TABLE #tempExcel
                IF OBJECT_ID('tempdb..#tempShippingList') IS NOT NULL 
                    DROP TABLE #tempShippingList
            END
        SET NOCOUNT OFF
    END


GO


