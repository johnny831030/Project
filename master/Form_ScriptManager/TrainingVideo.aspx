<%@ Page Title="" Language="C#" MasterPageFile="~/TrainingVideo.Master" AutoEventWireup="true" CodeBehind="TrainingVideo.aspx.cs" Inherits="longtermcare.WebForm10" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="http://jwpsrv.com/library/gsJFrjG+EeSFtSIAC0MJiQ.js"></script>
    <script type="text/javascript" src="../../Scripts/jwplayer.js" ></script>
    <script type="text/javascript">        jwplayer.key = "ehasu0CKeX6h2zXmZ2Szz2RIua78JmDmLb5L3g==";</script>

<script type="text/javascript">

    
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div  style="background: url('Image/WebImage/Icon/h2.jpg') no-repeat bottom;  line-height:30px;  padding-left:86px;  margin-left:0px; width:150px; height:32px;">
       <h2 >
             <asp:Label ID="Label1" runat="server" Text="教學影片" 
                 style="font-family: 微軟正黑體; font-size: large" ForeColor="White"></asp:Label>
       </h2>
    </div>
    <!--<h2>建置中。陸續會將教學檔上傳，敬請期待！</h2>-->
    <!--<h4>建議使用Chrome或IE瀏覽器觀看；</h4>-->

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        SelectCommand="SELECT MM_NO, MM_NAME FROM MainMenu">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server"></asp:SqlDataSource>

        <br />
    <asp:Label ID="lblMain" runat="server" Text="主選單："></asp:Label>

        <asp:DropDownList ID="DropDownList1" runat="server" 
        DataSourceID="SqlDataSource1" DataTextField="MM_NAME" DataValueField="MM_NO" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
        AutoPostBack="True" AppendDataBoundItems="true" >
        </asp:DropDownList>

        　<asp:Label ID="lblSubmain" runat="server" Text="子選單："></asp:Label>

        <asp:DropDownList ID="DropDownList2" runat="server" 
        AutoPostBack="True" 
        onselectedindexchanged="DropDownList2_SelectedIndexChanged" >
        </asp:DropDownList>

        <br />
    <asp:Label ID="lblFunction" runat="server" Text="功　能："></asp:Label>

        <asp:DropDownList ID="DropDownList3" runat="server" 
        AutoPostBack="True" onselectedindexchanged="DropDownList3_SelectedIndexChanged"  >
        </asp:DropDownList>

        　<asp:Label ID="lblSubfunction" runat="server" Text="子功能："></asp:Label>
        <asp:DropDownList ID="DropDownList4" runat="server" AppendDataBoundItems="true"
        AutoPostBack="True" onselectedindexchanged="DropDownList4_SelectedIndexChanged">
        </asp:DropDownList>

    
    <br />

    
    <br />

    
    <asp:Panel ID="Panel1" runat="server" Visible="False" >
        <asp:Label ID="Label3" runat="server" Font-Size="Large" Text="影片名稱："></asp:Label>
        <br />
        <br />

        <div id="myElement">Loading the player ...</div>
        <script type="text/javascript">
            jwplayer("myElement").setup({
                autostart: false,
                controls: true,
                file: "<%=wmvurl%>",
                width: 880,
                height: 500
            });
        </script>
        

    </asp:Panel>

    <br />
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource2" 
        Visible="False">
    </asp:GridView>
    <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource3" 
        Visible="False">
    </asp:GridView>

    <br />
    <br />
    
    <!--   
    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" 
            BackColor="#FFFFAA" onmenuitemclick="Menu1_MenuItemClick" 
            Font-Size="Large" ForeColor="#2020FF" BorderColor="Black" 
        BorderStyle="Solid" BorderWidth="2px" DisappearAfter="300" Visible="False">
                <DynamicMenuItemStyle BackColor="#FFFFAA" ForeColor="#2020FF"/>
                <Items>
                    <%--住民管理 --%>
                    <asp:MenuItem Text="住民管理 " Value="1">
                        <asp:MenuItem Text="　住民資料管理" Value="1-1">
                            <asp:MenuItem Text="　住民基本資料新增" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_Basic_A.mp4" ></asp:MenuItem>
                            <asp:MenuItem Text="　住民基本資料查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_Basic_MM.mp4" ></asp:MenuItem>
                            <asp:MenuItem Text="　住民補助資料新增" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_Allowance.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民補助資料查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_Allowance_MM.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民補助資料區間查詢列印" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_Allowance_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民障礙手冊新增" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_HandicapBook.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民障礙手冊查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_HandicapBook_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民障礙手冊區間查詢列印" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_HandicapBook_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　潛在住民資料管理新增" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/Potential_IP.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　潛在住民資料管理查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/Potential_IP_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　潛在住民資料管理區間查詢列印" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/Potential_IP_RQ.mp4"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　住民入出院管理" Value="1-2">
                            <asp:MenuItem Text="　住民預約入住新增" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_In_Expect_A.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民預約入住查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_In_Expect_MM.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民入住管理新增" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_In_A.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民入住管理查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_In_MM.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民轉床管理新增" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_ChBed_A.mp4"></asp:MenuItem>                            
                            <asp:MenuItem Text="　住民轉床管理查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_ChBed_MM.mp4"></asp:MenuItem>                           
                            <asp:MenuItem Text="　住民轉床管理區間查詢列印" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_ChBed_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民退住管理新增" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_Out_A.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民退住管理查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_Out_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民退住管理區間查詢列印" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_Out_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民一覽表清單" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/PeopleLook_S1.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民一覽表簡易列表+住民床位一覽+住民聯絡電話一覽" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/PeopleLook_S235.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　歷次入住記錄查詢" Value="http://140.123.174.27:618/TrainingVideo/IPMangement/PeopleLook/HistoryInOut.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　辦理住民請假*" Value="0"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　會辦事項管理*" Value="1-3">
                            <asp:MenuItem Text="　會辦事項查詢修改" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　管理者會辦事項查詢" Value="0"></asp:MenuItem>
                        </asp:MenuItem>
                    </asp:MenuItem>                    
                    <%--住民照護 --%>
                    <asp:MenuItem Text="住民照護 " Value="2">
                        <asp:MenuItem Text="　護理照護評估(一)" Value="2-1">
                            <asp:MenuItem Text="　巴氏量表新增" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_ADL_A_P.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　巴氏量表查詢/修改/列印" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_ADL_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　巴氏量表區間查詢列印" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_ADL_A_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　失智量表新增" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_MMSE_A.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　失智量表查詢" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_MMSE_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　憂鬱量表新增+查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_GDS_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　憂鬱量表2新增" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/GDS_LI3_A.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　憂鬱量表2查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/GDS_LI3_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　柯氏量表新增" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_Karnofsky_Scale_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　入院評估表新增查詢" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_Admission_Assess_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　Gordon評估表新增" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/GORDON/Admission_Assess_New_A.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　Gordon評估表查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/GORDON/Admission_Assess_New_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　護理評估表新增+查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_ADMSA_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　TPR登錄表*" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　InOut登錄表新增+查詢/修改/刪除+區間列印" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_IO_A_M_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　健康評估表新增+查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/HA_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　生活需要表*" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_LifeCareList_A_M_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　跌倒高危險群篩選表新增+查詢/修改/刪除" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_HighRiskFall_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　跌倒危險因子評估表(8項)" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_HighRiskFall8_A_M_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　壓瘡評估表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_PS_ASSEMENT_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　壓瘡監測表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_PS_MONITOR_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　壓瘡換藥紀錄表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_PS_DRESS_A_M.mp4"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　護理照護評估(二)" Value="2-2">                            
                            <asp:MenuItem Text="　住民約束登錄表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_Constraint_A_Form_A_M_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民約束評估表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_Constraint_A_M_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　意外事件登錄表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_AccidentRecord_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　TAI分級評量表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_TAI_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　初步疼痛評估表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_Pain_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　疼痛狀態評估表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_Pain_State_A_M_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　職能治療評估表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_OTHerapy_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　物理治療評估表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_PhysicalTherapy_A_M"></asp:MenuItem>
                            <asp:MenuItem Text="　身體評估單" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_BodyEvaluate_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民入住院狀態表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_STATE_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　簡易精神狀態檢查量表(SPMSQ)" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/SPMSQ_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　住民診療紀錄表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_MEDICAL_RECORD_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　每日輸入輸出紀錄表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_DAY_IO_A_M_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　定期身體評估表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/REG_BodyEvaluate_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　皮膚危險因子評估表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/SKIN_DANGER_A_M_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　復健綜合評估表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/PhysicalTherapy_sws_A_M.mp4"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　醫事藥事管理" Value="2-3">
                            <asp:MenuItem Text="　檢驗報告" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/MedicalManagement/LabRecord/MM_LabRecord_A_M_Year.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　血糖紀錄單" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/MedicalManagement/IP_BLOOD_SUGAR/IP_BLOOD_SUGAR_A_M_RQ.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　生命徵象紀錄表" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/MedicalManagement/VitalSigns/VitalSignsRecord_A_M_RQ.mp4"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　感染監測評估" Value="2-4">
                            <asp:MenuItem Text="　呼吸道感染評估" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Breath_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　眼耳鼻口感染評估" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Eye_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　皮膚感染評估" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Skin_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　腸胃炎感染評估" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Stomach_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　系統性感染評估" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Systemic_A_M.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　泌尿道感染評估" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Urinary_A_M.mp4"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　住民轉介管理" Value="2-5">
                            <asp:MenuItem Text="　住民轉介單" Value="http://140.123.174.27:618/TrainingVideo/NursingRemind/TransferList/IP_TransferList_A_M.mp4"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　表單批次輸入*" Value="2-6">
                            <asp:MenuItem Text="　住民每月體重輸入表" Value="mms://140.123.174.27/IP_Weight_A.wmv"></asp:MenuItem>
                            <asp:MenuItem Text="　住民每月壓瘡輸入表" Value="mms://140.123.174.27/IP_Maklebust_A.wmv"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　營養評估*" Value="2-7">
                            <asp:MenuItem Text="　72小時營養評估表" Value="mms://140.123.174.27/MM_72NA_A.wmv"></asp:MenuItem>
                            <asp:MenuItem Text="　迷你營養評估表*" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　營養評估表*" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　營養師記錄表" Value="mms://140.123.174.27/MM_DRA.wmv"></asp:MenuItem>
                        </asp:MenuItem>                       
                        <asp:MenuItem Text="　社工管理*" Value="2-8">
                            <asp:MenuItem Text="　個案適應評估表" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　個案服務計畫表" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　住民個別活動紀錄表" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　社工輔導記錄表" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　社會工作個案紀錄" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　團體活動紀錄" Value="0"></asp:MenuItem>
                        </asp:MenuItem>
                    </asp:MenuItem>
                    <%--護理計畫 --%>
                    <asp:MenuItem Text="護理計畫 " Value="3">
                        <asp:MenuItem Text="　護理計畫作業" Value="3-1">
                            <asp:MenuItem Text="　護理計畫" Value="http://140.123.174.27:618/TrainingVideo/TrainingNCP_1.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　護理紀錄(DART版)" Value="http://140.123.174.27:618/TrainingVideo/TrainingNCP_2.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　護理記錄(簡單版)" Value="http://140.123.174.27:618/TrainingVideo/TrainingNCP_3.mp4"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　管理作業" Value="3-2">
                            <asp:MenuItem Text="　護理計畫總表查詢" Value="http://140.123.174.27:618/TrainingVideo/TrainingNCP_4.mp4"></asp:MenuItem>
                            <asp:MenuItem Text="　待評值目標查詢*" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　健康問題表列印*" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　護理計畫列印*" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　護理紀錄列印*" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="　交班紀錄查詢*" Value="0"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　病歷摘要*" Value="3-3">
                            <asp:MenuItem Text="　病歷摘要" Value="0"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="　照護會診作業*" Value="3-4">
                            <asp:MenuItem Text="　住民照護會診單" Value="0"></asp:MenuItem>
                        </asp:MenuItem>
                    </asp:MenuItem>
                    <%--醫事管理 --%>
                    <asp:MenuItem Text="醫事管理* " Value="4">
                            <asp:MenuItem Text="　行程管理" Value="4-1">                                
                                <asp:MenuItem Text="　行程預約" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　行程出發" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　行程返回" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　就醫登錄" Value="4-2">
                                <asp:MenuItem Text="　就醫記錄" Value="0"></asp:MenuItem>
                            </asp:MenuItem>                            
                            <asp:MenuItem Text="　領藥記錄" Value="4-3">
                                <asp:MenuItem Text="　查詢修改領藥記錄" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　目前用藥檢核(藥物交互作用與重複用藥檢核)" Value="0"></asp:MenuItem>
                            </asp:MenuItem>                            
                            <asp:MenuItem Text="　給藥記錄" Value="4-4">
                                <asp:MenuItem Text="　今日給藥記錄" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　查詢給藥記錄" Value="0"></asp:MenuItem>
                            </asp:MenuItem>                            
                            <asp:MenuItem Text="　統計報表(指標)" Value="4-5">                                
                                <asp:MenuItem Text="　六項指標-月份明細表" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　六項指標-月份統計表" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　六項指標-年度統計表" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　六項指標-統計圖表" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　住民基本資料統計分析圖" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　住民體重統計圖" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　住民用餐狀況表" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　表單批次列印" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　量表趨勢統計圖" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                    </asp:MenuItem>
                    <%--行政管理 --%>
                    <asp:MenuItem Text="行政管理 " Value="5">
                            <asp:MenuItem Text="　住民使用耗材管理*" Value="5-1">
                                <asp:MenuItem Text="　耗材使用紀錄" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　耗材撥補住民" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　住民耗材查詢" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　住民轉帳管理*" Value="5-2">
                                <asp:MenuItem Text="　收費轉帳" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　應收帳款查詢" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　住民款項管理*" Value="5-3">
                                <asp:MenuItem Text="　零用金管理" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　預收款管理" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　收費管理*" Value="5-4">
                                <asp:MenuItem Text="　住民繳費" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　查詢繳費" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　物品管理*" Value="5-5">
                                <asp:MenuItem Text="　物品管理單" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　進銷存管理*" Value="5-6">
                                <asp:MenuItem Text="　廠商基本檔" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　請購流程設定" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　審核作業" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　議價作業" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　訂購作業" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　驗收作業" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　盤存作業" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　小庫請購作業" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　小庫撥補作業" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　住民耗用排程設定" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　歷史議價紀錄" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　SOP文件管理" Value="5-7">
                                <asp:MenuItem Text="　SOP文件上傳" Value="mms://140.123.174.27/SOPUpload.wmv"></asp:MenuItem>
                                <asp:MenuItem Text="　SOP文件下載*" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　SOP文件刪除*" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　SOP目錄管理" Value="mms://140.123.174.27/Document_Path.wmv"></asp:MenuItem>
                            </asp:MenuItem>
                    </asp:MenuItem>
                    <%--基本資料 --%>
                    <asp:MenuItem Text="基本資料* " Value="6">
                            <asp:MenuItem Text="　基本檔設定" Value="6-1">
                                <asp:MenuItem Text="　床位基本檔管理" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　六項指標閥值設定" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　住民補助資料維護檔" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　耗材基本檔管理" Value="6-2">
                                <asp:MenuItem Text="　耗材品項管理" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　常見衛耗材管理" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　耗材成本管理" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　連帶檔管理" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　藥品基本檔" Value="6-3">
                                <asp:MenuItem Text="　藥品檔設定" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　醫院常見藥品檔" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　藥品交互作用" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　片語管理" Value="6-4">
                                <asp:MenuItem Text="　片語管理表" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　公告管理" Value="6-5">
                                <asp:MenuItem Text="　全院公告管理" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　個人提醒管理" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　護理計畫項目" Value="6-6">
                                <asp:MenuItem Text="　系統別項目" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　醫學診斷項目" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　健康問題項目" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　醫學診斷與健康問題的連結" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　定義性特徵項目" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　相關因素項目" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　護理目標項目" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　護理措施項目" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　條碼產生作業" Value="6-7">
                                <asp:MenuItem Text="　條碼產生" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                    </asp:MenuItem>
                    <%--權限管理 --%>
                    <asp:MenuItem Text="權限管理* " Value="7">
                            <asp:MenuItem Text="　員工基本檔" Value="7-1">
                                <asp:MenuItem Text="　員工基本資料管理" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　員工受訓時數登錄表" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　員工一覽表" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　權限設定" Value="7-2">
                                <asp:MenuItem Text="　人員權限設定" Value="0"></asp:MenuItem>
                                <asp:MenuItem Text="　群組權限設定" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="　修改密碼" Value="7-3">
                                <asp:MenuItem Text="　密碼變更" Value="0"></asp:MenuItem>
                            </asp:MenuItem>
                    </asp:MenuItem>
                </Items>
            </asp:Menu>
    
    <asp:Label ID="Label4" runat="server"></asp:Label>
    -->
    
    <br />


</asp:Content>
            