USE [OMSOne]
GO

/****** Object:  StoredProcedure [dbo].[Pro_Report_ShippingListForFinance]    Script Date: 11/08/2015 17:40:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*  
******************************************************************************
--�洢�������ƣ��ͻ������ܱ�洢���̡�
--�洢����˵��: ����ͳ��ʱ�谴�ղ�ͬ�Ĳ�ѯ����
--���ߣ�Tristan
--����ʱ�䣺2015-04-19
--�汾��1.0    
--����˵������ps_��ʼ��Ϊ��ѯ�������μ�������ע�͡�
--�޸���ʷ:(�汾/ʱ��/�޸���/�޸�Ҫ��)
-- exec [Pro_Report_ShippingListForFinance] @strOrder='InnerOrderNO',@intRowsCount=0,@ps_SearchType='ExportExcel',@ps_BeginTime='2015-02-09',@ps_EndTime='2015-02-12'
******************************************************************************
#1 ���빫˾�����ֶ� 20151108
*/
ALTER PROCEDURE [dbo].[Pro_Report_ShippingListForFinance]
    @ps_SearchType VARCHAR(255) = '' ,
    @ps_BeginTime VARCHAR(255) = '' ,
    @ps_EndTime VARCHAR(255) = '' ,
    @ps_TimeType VARCHAR(255) = '' ,
    @pageSize INT = 20 ,	--ÿҳ��ʾ��¼��
    @pageIndex INT = 1 ,--��ǰҳ������
    @strOrder VARCHAR(100) = 'InnerOrderNO' , --��������
    @orderType BIT = 1 ,        --��������  0Ϊ����
    @intRowsCount BIGINT OUTPUT              --���ؼ�¼����    
AS 
    BEGIN
        SET NOCOUNT ON
        DECLARE @groupbyField VARCHAR(1000)  --������ֶ�
        DECLARE @innerFiled VARCHAR(1000)  --��ϲ�ѯ�е����ֶ�
        DECLARE @innerWhere VARCHAR(MAX)  --����ڲ�ѯ����
        DECLARE @strSql VARCHAR(MAX)  --�����
	
	--�����ܼ�¼��ʹ��
        DECLARE @SQL NVARCHAR(4000)
        DECLARE @R BIGINT
	
	--�����ҳʱʹ��
        DECLARE @strTemp VARCHAR(1000) --��ʱ����
        DECLARE @strOrders VARCHAR(1000) --�������
	
        SET @intRowsCount = 0	
        SET @groupbyField = ''
        SET @innerFiled = ''
        SET @innerWhere = ' AND 1 = 1 '
        SET @R = 0   
        
        IF @ps_TimeType IS NOT NULL
            AND @ps_TimeType != '' --��ϲ�ѯ�����г�ֵ,��Ϊ�ձ���ѡ�������
            BEGIN
                SET @strTemp = ' L.OutStockDate '
            END
        ELSE 
            SET @strTemp = ' L.OutStockDate ' 
            
        IF @ps_BeginTime IS NOT NULL
            AND @ps_BeginTime != '' --��ϲ�ѯ�����г�ֵ,��Ϊ�ձ���ѡ�������
            SET @innerWhere = @innerWhere + ' and ' + @strTemp
                + '>=CONVERT(DATE,''' + @ps_BeginTime + ''')'  --��ѯ����
                
        IF @ps_EndTime IS NOT NULL
            AND @ps_EndTime != '' --��ϲ�ѯ�����г�ֵ,��Ϊ�ձ���ѡ�������
            BEGIN
                SET @innerWhere = @innerWhere + ' and ' + @strTemp
                    + '<=CONVERT(DATE,''' + @ps_EndTime + ''')'  --��ѯ����
            END 
        SET @strSql = ' SELECT CN.CompanyName,O.InnerOrderNO ,CustomerDetailID=CD.ID, T.Value , O.InnerSalesMan , C.CustomerName , O.CustOrderDate , O.CustomerOrderNO , CD.CPN , CD.MPN , B.BrandName , CD.CustQuantity , OutQty = '''' , ShippingListNo = '''' , IsApproved = '''' , CD.SaleExchangeRate , SaleRealCurrency = CT.CurrencyName , SaleRealPrice , SaleRealTotalPrice = SaleRealPrice * CD.CustQuantity , PT.PaymentType , OutStockDate = '''' FROM   dbo.CustomerOrder AS O WITH ( NOLOCK ) INNER JOIN dbo.CustomerOrderDetail AS CD WITH ( NOLOCK ) ON CD.InnerOrderNO = O.InnerOrderNO AND CD.AvailFlag = 1 LEFT JOIN dbo.CommonTable AS T ON T.TableName = ''CustomerOrder'' AND T.FieldID = ''State'' AND O.State = T.[Key] LEFT JOIN dbo.Customer AS C ON C.ID = O.CustomerID LEFT JOIN dbo.Brand AS B ON B.ID = CD.MFG LEFT JOIN dbo.CurrencyType AS CT ON CT.ID = CD.SaleRealCurrency LEFT JOIN dbo.PaymentType AS PT ON PT.ID = O.PaymentTypeID LEFT JOIN dbo.Corporation AS CN
 WITH(NOLOCK) ON CN.ID=O.CorporationID '
        
        --SET @strSql = @strSql + @innerWhere
	
        IF @orderType = 0 
            BEGIN
			--Ϊ0������
                SET @strOrders = ' order by  ' + @strOrder + ' asc '
            END
        ELSE 
            BEGIN
			--����Ϊ����
                SET @strOrders = ' order by  ' + @strOrder + ' desc '
            END				
        --IF @ps_SearchType = '' 
        --    BEGIN
        --        SET @strSql = 'SELECT  rownum = ROW_NUMBER() OVER ( '
        --            + @strOrders + ' ) ,* FROM  (' + @strSql + ') E'
        --        PRINT @strSql
        --        EXEC (@strSql)  
        --    END
        
        --�ͻ�����״̬	�� ��	�ͻ�����	�ͻ���������	�ͻ�������	�ͻ��ͺ�	�����ͺ�	Ʒ��	��������	��������	���ⵥ��	�Ƿ����	������	ʵ��������	ʵ������	ʵ���ۼ��ܶ�	�ͻ����ʽ	��������
       
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
                                                  THEN ''��''
                                                  ELSE ''��''
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
                
                    
                SELECT  [��˾����] = CompanyName , --#1
                        [�ͻ�����״̬] = Value ,
                        [�� ��] = InnerSalesMan ,
                        [�ͻ�����] = CustomerName ,
                        [�ͻ���������] = CustOrderDate ,
                        [�ͻ�������] = CustomerOrderNO ,
                        [�ͻ��ͺ�] = CPN ,
                        [�����ͺ�] = MPN ,
                        [Ʒ��] = BrandName ,
                        [��������] = CustQuantity ,
                        [��������] = OutQty ,
                        [���ⵥ��] = ShippingListNo ,
                        [�Ƿ����] = IsApproved ,
                        [������] = SaleExchangeRate ,
                        [ʵ��������] = SaleRealCurrency ,
                        [ʵ������] = SaleRealPrice ,
                        [ʵ���ۼ��ܶ�] = SaleRealTotalPrice ,
                        [�ͻ����ʽ] = PaymentType ,
                        [��������] = OutStockDate
                FROM    #tempExcel
                
                IF OBJECT_ID('tempdb..#tempExcel') IS NOT NULL 
                    DROP TABLE #tempExcel
                IF OBJECT_ID('tempdb..#tempShippingList') IS NOT NULL 
                    DROP TABLE #tempShippingList
            END
        SET NOCOUNT OFF
    END


GO


