<%@ Page Title="" Language="C#" MasterPageFile="~/NursingCarePlan.Master" AutoEventWireup="true"
    CodeBehind="SHIFT_EXCHANGE_RECORD_View2.aspx.cs" Inherits="longtermcare.NursingPlan.Shift_Exchange.SHIFT_EXCHANGE_RECORD_View2"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/jquery-ui-1.8.24.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-2.0.3.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="../../DateTimeCheck.js" type="text/javascript"></script>
    <style type="text/css">
        .mainPanel
        {
            padding: 0,0,1px,0;
            margin: 0,0,1px,0;
            background: #FFFFFF;
            border: solid 2px #006600;
            border-top-width: 0;
        }
        .style2
        {
            width: 100%;
            height: 30px;
        }
        .style3
        {
            width: 15%;
            padding: 0;
            margin: 0 auto;
        }
        .style4
        {
            width: 25%;
            padding: 0;
            margin: 0 auto;
        }
        .style5
        {
            width: 25%;
            padding: 0;
            margin: 0 auto;
        }
        .style6
        {
            width: 20%;
            padding: 0;
            margin: 0 auto;
        }
        .style7
        {
            width: 15%;
            padding: 0;
            margin: 0 auto;
        }
        .functionitem1
        {
            padding: 0 0 2px 0;
            margin: 0px;
            text-align: center;
            background: #F5F5F5;
            border: solid 1px #006600;
            border-top-width: 1px;
            border-bottom-width: 2px;
            height: 22px;
        }
        .functionitem2
        {
            padding: 0 0 2px 0;
            margin: 0px;
            text-align: center;
            background: #FFFFFF;
            border: solid 1px #006600;
            border-top-width: 1px;
            border-bottom-width: 0;
            border-left-width: 2px;
            height: 22px;
        }
        .functionitem3
        {
            padding: 0 0 2px 0;
            margin: 0px;
            text-align: center;
            background: #F5F5F5;
            border: solid 1px #006600;
            border-top-width: 1px;
            border-bottom-width: 2px;
            height: 22px;
        }
        .functionitem4
        {
            padding: 0 0 2px 0;
            margin: 0px;
            text-align: center;
            background: #F5F5F5;
            border: solid 1px #006600;
            border-top-width: 1px;
            border-bottom-width: 2px;
            height: 22px;
        }
        .functionitem5
        {
            padding: 0 0 2px 0;
            margin: 0px;
            text-align: center;
            background: #F5F5F5;
            border: solid 1px #006600;
            border-top-width: 1px;
            border-bottom-width: 2px;
            height: 22px;
        }
        .style3 div
        {
            height: 1px;
            overflow: hidden;
            background: #006600;
            border-left: solid 1px #000;
            border-right: solid 1px #000;
        }
        .style4 div
        {
            height: 1px;
            overflow: hidden;
            background: #006600;
            border-left: solid 1px #000;
            border-right: solid 1px #000;
        }
        .style5 div
        {
            height: 1px;
            overflow: hidden;
            background: #006600;
            border-left: solid 1px #000;
            border-right: solid 1px #000;
        }
        .style6 div
        {
            height: 1px;
            overflow: hidden;
            background: #006600;
            border-left: solid 1px #000;
            border-right: solid 1px #000;
        }
        .style7 div
        {
            height: 1px;
            overflow: hidden;
            background: #006600;
            border-left: solid 1px #000;
            border-right: solid 1px #000;
        }
        .row1
        {
            margin: 0 5px;
            background: #000;
        }
        .row2
        {
            margin: 0 3px;
            border: 0 2px;
        }
        .row3
        {
            margin: 0 2px;
        }
        .row4
        {
            margin: 0 1px;
            height: 2px;
        }
        
        .style105
        {
            height: 50px;
            width: 80px;
        }
        
        .toggler
        {
            position: fixed;
            top: 180px;
            right: 350px;
            width: 200px;
            height: 20px;
        }
        #effect
        {
            position: absolute;
            top: 180px;
            right: 350px;
            width: 200px;
            height: 20px;
            padding: 0.4em;
            position: relative;
        }
        #effect p
        {
            margin: 0;
            padding: 0.4em;
            text-align: center;
        }
        .ui-widget-content
        {
            border: 1px solid #aaaaaa;
            background: #FFCC22;
            color: #222222;
        }
        
        #basic-accordian
        {
            border: 5px solid #EEE;
            padding: 5px;
            width: 100%;
            margin-left: 0px;
            z-index: 2;
            margin-top: 0px;
        }
        .accordion_headings
        {
            padding: 5px;
            background: #99CC00;
            color: #FFFFFF;
            border: 1px solid #FFF;
            cursor: pointer;
            font-weight: bold;
        }
        .accordion_headings:hover
        {
            background: #00CCFF;
        }
        .accordion_child
        {
            padding: 10px;
            background: #EEE;
        }
        .header_highlight
        {
            padding: 5px;
            background: #00CCFF;
            color: #FFFFFF;
            font-weight: bold;
        }
        
        .btnsym
        {
            width: 40px;
            height: 40px;
        }
        .btnsym1
        {
            width: 40px;
            height: 40px;
            font-size: 8px;
        }
        #effect1
        {
            position: absolute;
            top: 180px;
            right: 350px;
            width: 300px;
            height: 20px;
            padding: 0.4em;
            position: relative;
        }
        
        #effect1 p
        {
            margin: 0;
            padding: 0.4em;
            text-align: center;
        }
        
        .ui-widget-content1
        {
            border: 1px solid #aaaaaa;
            background: #FF0000;
            color: #FFFFFF;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--BACKtoTOP-START
    <a style="display: scroll; position: fixed; bottom: 0px; right: 200px;" href="#"
        title="Back to Top" onfocus="if(this.blur)this.blur()">
        <img alt='' border='0' onmouseover="this.src='/Image/WebImage/B2T.png'" src="/Image/WebImage/B2T_medium.png"
            onmouseout="this.src='/Image/WebImage/B2T_medium.png'" /></a>
    BACKtoTOP-STOP-->
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    <h1>
        <asp:Label ID="lblShiftExchangeRecord" runat="server" Text="交班紀錄"></asp:Label></h1>
    <div style="display: none;">
        <asp:Button ID="btnAdd" runat="server" Text="新增交班" OnClick="btnAdd_Click" Visible="False" />
        <asp:Button ID="btnModify_shift" runat="server" OnClick="btnModify_shift_Click" Text="修改交班"
            Visible="False" />
        <asp:Button ID="btnNRS_ShiftRec" runat="server" OnClick="btnNRS_ShiftRec_Click" Text="查詢護理交班"
            Visible="False" />
        <asp:Button ID="btn" runat="server" OnClick="btn_Click" Text="查詢雜項交班" Visible="False" />
    </div>
    <div>
        <div>
            <table border="0" class="style2">
                <tbody>
                    <tr>
                        <td class="style3">
                            <div class="row1">
                            </div>
                            <div class="row2">
                            </div>
                            <div class="row3">
                            </div>
                            <div class="row4">
                            </div>
                            <p class="functionitem1">
                                <asp:LinkButton ID="LinkBtnGoToAddView" runat="server" Font-Bold="False" Font-Size="16px"
                                    ForeColor="#003300" OnClick="LinkBtnGoToAddView_Click">新增雜項交班</asp:LinkButton></p>
                        </td>
                        <td class="style4">
                            <div class="row1">
                            </div>
                            <div class="row2">
                            </div>
                            <div class="row3">
                            </div>
                            <div class="row4">
                            </div>
                            <p class="functionitem2">
                                <asp:LinkButton ID="LinkBtnGoToUView" runat="server" Font-Size="16px" ForeColor="#003300"
                                    OnClick="LinkBtnGoToUView_Click" Font-Bold="True">查詢/修改/刪除 雜項交班</asp:LinkButton></p>
                        </td>
                        <td class="style7">
                            <div class="row1">
                            </div>
                            <div class="row2">
                            </div>
                            <div class="row3">
                            </div>
                            <div class="row4">
                            </div>
                            <p class="functionitem5">
                                <asp:LinkButton ID="LinkBtnGoToAddNRView" runat="server" Font-Size="16px" ForeColor="#003300"
                                    OnClick="LinkBtnGoToAddNRView_Click">新增護理交班</asp:LinkButton></p>
                        </td>
                        <td class="style5">
                            <div class="row1">
                            </div>
                            <div class="row2">
                            </div>
                            <div class="row3">
                            </div>
                            <div class="row4">
                            </div>
                            <p class="functionitem3">
                                <asp:LinkButton ID="LinkBtnGoToQView" runat="server" Font-Size="16px" ForeColor="#003300"
                                    OnClick="LinkBtnGoToQView_Click">查詢/修改/刪除 護理交班</asp:LinkButton></p>
                        </td>
                        <td class="style6">
                            <div class="row1">
                            </div>
                            <div class="row2">
                            </div>
                            <div class="row3">
                            </div>
                            <div class="row4">
                            </div>
                            <p class="functionitem4">
                                <asp:LinkButton ID="LinkBtnGoToMQView" runat="server" Font-Size="16px" ForeColor="#003300"
                                    OnClick="LinkBtnGoToMQView_Click">查詢交班紀錄</asp:LinkButton></p>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="mainPanel">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <br />
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View1" runat="server">
                            <asp:Label ID="txt_sql" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="txt_sqlcom" runat="server" Visible="False">1</asp:Label>
                            <asp:Label ID="txt_columnIndex" runat="server" Visible="False">1</asp:Label>
                            <asp:Label ID="str_account" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="sstr_ipno" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="sstr_hid" runat="server" Visible="False"></asp:Label>
                            <table border="1" style="width: 100%;">
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="lblSearchDate1" runat="server" ForeColor="Black" Text="請選擇欲查詢住民"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListIP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListIP_SelectedIndexChanged">
                                            <asp:ListItem Value="0">此一住民</asp:ListItem>
                                            <asp:ListItem Value="1">所有住民</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="lblSearchDate2" runat="server" ForeColor="Black" Text="請選擇欲查詢區域(或樓層)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListArea" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="AREA_STATE"
                                            DataValueField="AREA_STATE" Enabled="False">
                                            <asp:ListItem Value="-99">全部</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="lblSearchDate3" runat="server" ForeColor="Black" Text="請選擇評估人員"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropP_USER" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Login">
                                            <asp:ListItem Value="-99">全部</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label131" runat="server" ForeColor="Black" Text="查詢評估期間"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtC_date_s1" runat="server" onchange="txtC_date_s1_Changed();"
                                            MaxLength="10" Width="100px"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="txtStartDate_TextBoxWatermarkExtender" runat="server"
                                            TargetControlID="txtC_date_s1" WatermarkCssClass="Watermark" WatermarkText="輸入格式yyyy/MM/dd">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" Enabled="True"
                                            Format="yyyy/MM/dd" TargetControlID="txtC_date_s1">
                                        </asp:CalendarExtender>
                                        <asp:Label ID="Label148" runat="server" ForeColor="Black" Text="到"></asp:Label>
                                        <asp:TextBox ID="txtC_date_s2" runat="server" onchange="txtC_date_s2_Changed();"
                                            MaxLength="10" Width="100px"></asp:TextBox><asp:TextBoxWatermarkExtender
                                            ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtC_date_s2"
                                            WatermarkCssClass="Watermark" WatermarkText="輸入格式yyyy/MM/dd">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                            TargetControlID="txtC_date_s2">
                                        </asp:CalendarExtender>
                                        <asp:Button ID="btnS" runat="server" Height="24px" Text="查詢" UseSubmitBehavior="False"
                                            OnClick="btnS_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label133" runat="server" Text="快速查詢"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <asp:Button ID="btnSearchLast1" runat="server" Text="今日" OnClick="btnSearchLastDay1_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnSearchLast5" runat="server" Text="最近3日" OnClick="btnSearchLastDay3_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnSearchLastDay7" runat="server" Text="最近7日" OnClick="btnSearchLastDay7_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnSearchLastDay14" runat="server" Text="最近14日" OnClick="btnSearchLastDay14_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label1" runat="server" Text="查詢填寫期間"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="TextBox1" runat="server" onchange="txtC_date_s1_Changed();"
                                            MaxLength="10" Width="100px"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                            TargetControlID="txtC_date_s1" WatermarkCssClass="Watermark" WatermarkText="輸入格式yyyy/MM/dd">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                            Format="yyyy/MM/dd" TargetControlID="txtC_date_s1">
                                        </asp:CalendarExtender>
                                        <asp:Label ID="Label3" runat="server" ForeColor="Black" Text="到"></asp:Label>
                                        <asp:TextBox ID="TextBox2" runat="server" onchange="txtC_date_s2_Changed();"
                                            MaxLength="10" Width="100px"></asp:TextBox><asp:TextBoxWatermarkExtender
                                            ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtC_date_s2"
                                            WatermarkCssClass="Watermark" WatermarkText="輸入格式yyyy/MM/dd">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                            TargetControlID="txtC_date_s2">
                                        </asp:CalendarExtender>
                                        <asp:Button ID="Button1" runat="server" Height="24px" Text="查詢" UseSubmitBehavior="False"
                                            OnClick="btnS_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label2" runat="server" Text="修改日期查詢"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <asp:Button ID="btnSearchOP_date1" runat="server" Text="今日"/>&nbsp;&nbsp;
                                        <asp:Button ID="btnSearchOP_date3" runat="server" Text="最近3日" />&nbsp;&nbsp;
                                        <asp:Button ID="btnSearchOP_date7" runat="server" Text="最近7日" />&nbsp;&nbsp;
                                        <asp:Button ID="btnSearchOP_date14" runat="server" Text="最近14日" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Button ID="btnPrint" runat="server" Height="24px" Text="列印查詢後的結果" UseSubmitBehavior="False"
                                Visible="False" OnClick="btnPrint_Click" />
                            <br />
                            <br />
                            <asp:Label ID="lblQueryResult" runat="server" ForeColor="Red"></asp:Label>
                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" AllowPaging="True" BorderColor="#3366CC"
                                Width="100%" AllowSorting="True" OnSorting="GridView3_Sorting" OnPageIndexChanging="GridView3_PageIndexChanging"
                                PageSize="10" OnRowCommand="GridView3_RowCommand" Visible="False">
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" CommandName="search" Text="檢視資料" />
                                    <asp:TemplateField HeaderText="NO" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lab_r_id" runat="server" Text='<%# Bind("REC_NO") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="建立日期" SortExpression="A_DATETIME">
                                        <ItemTemplate>
                                            <asp:Label ID="lab_r_id0" runat="server" Text='<%# FormatDATETIME(Convert.ToString(DataBinder.Eval(Container.DataItem,"A_DATETIME"))) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="建立人員" ReadOnly="True" SortExpression="Name" />
                                    <asp:BoundField DataField="IP_NO_NEW" HeaderText="住民編號" ReadOnly="True" SortExpression="IP_NO_NEW" />
                                    <asp:BoundField DataField="IP_NAME" HeaderText="住民姓名" ReadOnly="True" SortExpression="IP_NAME" />
                                    <asp:TemplateField ControlStyle-Height="98%" ControlStyle-Width="98%" HeaderStyle-Width="50%"
                                        HeaderText="交班內容">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtShiftContent" runat="server" BorderColor="White" BorderStyle="None"
                                                MaxLength="10" ReadOnly="True" Text='<%#  FormatContent(Convert.ToString(DataBinder.Eval(Container.DataItem,"NRS_CONTENT")))  %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle Height="98%" Width="98%" />
                                        <HeaderStyle Width="50%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" ForeColor="#003399" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle CssClass="asc" BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle CssClass="desc" BackColor="#002876" />
                            </asp:GridView>
                            <br />
                            <br />
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT [AREA_STATE] FROM [CODE_ROOM_AREA] ORDER BY [AREA_ID] ASC">
                            </asp:SqlDataSource>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <br />
                            <asp:Button ID="Button2" runat="server" Height="24px" OnClick="Button2_Click" Text="修改這一筆資料"
                                UseSubmitBehavior="False" />
                            <asp:Button ID="btn_DELET" runat="server" Height="24px" OnClick="btn_DELET_Click"
                                Text="刪除這一筆資料" UseSubmitBehavior="False" />
                            <asp:ConfirmButtonExtender ID="btn_DELET_ConfirmButtonExtender" runat="server" ConfirmText="確定要刪除這筆資料?"
                                Enabled="True" TargetControlID="btn_DELET">
                            </asp:ConfirmButtonExtender>
                            <asp:Button ID="btnPrintThisRecord" runat="server" Height="24px" OnClick="btnPrintThisRecord_Click"
                                Text="列印這一筆資料" UseSubmitBehavior="False" />
                            &nbsp;
                            <asp:Button ID="btnRESEARCH" runat="server" Height="24px" OnClick="btnRESEARCH_Click"
                                Text="返回查詢" UseSubmitBehavior="False" />
                            <asp:Label ID="lblIP_NO" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblRecUserID" runat="server" Visible="False"></asp:Label>
                            <br />
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label153" runat="server" Text="住民編號："></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblipnonew" runat="server" ForeColor="Blue"></asp:Label>
                                        <asp:Label ID="lb_ip_no" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lb_rec_no" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label155" runat="server" Text="姓　名："></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbname" runat="server" ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label69" runat="server" Text="床　號："></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblroombed" runat="server" ForeColor="Blue"></asp:Label>
                                        <asp:Label ID="txtIPAREA" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label68" runat="server" Text="性　別："></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblipsex" runat="server" ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label72" runat="server" Text="年　齡："></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblipage" runat="server" ForeColor="Blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="PanelData" runat="server" Enabled="False">
                                <br />
                                <table id="table1" style="width: 100%; color: Black;" border="1">
                                    <tr>
                                        <td bgcolor="#FFFFCC" class="style105">
                                            <asp:Label ID="lblC_DATE" runat="server" Text="建立日期"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtC_DATE" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td bgcolor="#FFFFCC" class="style105">
                                            <asp:Label ID="lblA_DATE" runat="server" Text="紀錄日期"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtA_DATE" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#FFFFCC" class="style105">
                                            <asp:Label ID="lblC_TIME" runat="server" Text="建立時間"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtC_TIME" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td bgcolor="#FFFFCC" class="style105">
                                            <asp:Label ID="lblA_TIME" runat="server" Text="紀錄時間"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtA_TIME" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#FFFFCC" class="style105">
                                            <asp:Label ID="lblC_USER" runat="server" Text="建立人員"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtC_USER" runat="server" Text=""></asp:Label>
                                            <br />
                                        </td>
                                        <td bgcolor="#FFFFCC" class="style105">
                                            <asp:Label ID="lblA_USER" runat="server" Text="紀錄人員"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtA_USER" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#FFFFCC" class="style105">
                                            <asp:Label ID="lblCONTENT" runat="server" Text="交班內容"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <br />
                                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="100px" Width="94%"></asp:TextBox>
                                            <br />
                                            <asp:ImageButton ID="ibtnPhraseShiftExchangeContent" runat="server" BorderStyle="None"
                                                Height="20px" ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif"
                                                OnClick="ibtnPhraseShiftExchangeContent_Click" OnClientClick="return showWaitPanel_Content();"
                                                ToolTip="片語" Width="20px" />
                                            &nbsp;<asp:Button ID="btnSymbolsTools" runat="server" OnClick="btnSymbolsTools_Click"
                                                Text="符號表" />
                                            <asp:Panel ID="PanelSymbolsTools" runat="server" Visible="False">
                                                <asp:Accordion ID="Accordion1" HeaderCssClass="accordion_headings" HeaderSelectedCssClass="header_highlight"
                                                    ContentCssClass="accordion_child" runat="server" SelectedIndex="0" FadeTransitions="true"
                                                    SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40"
                                                    RequireOpenedPane="true" AutoSize="None">
                                                    <Panes>
                                                        <asp:AccordionPane ID="AccPan1" runat="server">
                                                            <Header>
                                                                常用符號</Header>
                                                            <Content>
                                                                <table style="width: 95%;">
                                                                    <tr>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_1" value="，" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_2" value="、" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_3" value="。" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_4" value="？" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_5" value="！" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_6" value="：" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_7" value="；" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_8" value="＜" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_9" value="＞" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_10" value="＝" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_11" value="≠" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_12" value="≒" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_13" value="≦" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_14" value="≧" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_15" value="％" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_16" value="‰" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_17" value="℃" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_18" value="㎎" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_19" value="㎏" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_20" value="㎝" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_21" value="㎜" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_22" value="ml" onclick="set_symbols(this.value)"
                                                                                class="btnsym1">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_23" value="㏄" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan1_24" value="mmHg" onclick="set_symbols(this.value)"
                                                                                class="btnsym1">
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </Content>
                                                        </asp:AccordionPane>
                                                        <asp:AccordionPane ID="AccPan2" runat="server">
                                                            <Header>
                                                                基本符號</Header>
                                                            <Content>
                                                                <table style="width: 95%;">
                                                                    <tr>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_1" value="，" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_2" value="、" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_3" value="。" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_4" value="．" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_5" value="？" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_6" value="！" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_7" value="﹕" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_8" value="﹔" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_9" value="『" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_10" value="』" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_11" value="「" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_12" value="」" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_13" value="｛" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_14" value="｝" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_15" value="〔" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_16" value="〕" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_17" value="（" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_18" value="）" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_19" value="＜" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_20" value="＞" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_21" value="‘" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_22" value="’" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_23" value="“" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_24" value="”" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_25" value="〝" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_26" value="〞" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_27" value="‵" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_28" value="′" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_29" value="＠" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_30" value="＆" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_31" value="＃" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_32" value="＊" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_33" value="…" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_34" value="‥" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_35" value="＄" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_36" value="～" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_37" value="─" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan2_38" value="│" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </Content>
                                                        </asp:AccordionPane>
                                                        <asp:AccordionPane ID="AccPan3" runat="server">
                                                            <Header>
                                                                單位符號</Header>
                                                            <Content>
                                                                <table style="width: 95%;">
                                                                    <tr>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_1" value="℃" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_2" value="℉" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_3" value="㎎" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_4" value="㎏" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_5" value="㎝" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_6" value="㎜" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_7" value="㎡" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_8" value="㎞" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_9" value="ml" onclick="set_symbols(this.value)"
                                                                                class="btnsym1">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_10" value="㏄" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_11" value="㏕" onclick="set_symbols(this.value)"
                                                                                class="btnsym">
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" name="btn_AccPan3_12" value="mmHg" onclick="set_symbols(this.value)"
                                                                                class="btnsym1">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </Content>
                                                        </asp:AccordionPane>
                                                    </Panes>
                                                </asp:Accordion>
                                            </asp:Panel>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ibtnPhraseShiftExchangeContent" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <asp:Button ID="btnSAVE" runat="server" Enabled="False" Height="24px" OnClick="btnSAVE_Click"
                                Text="儲存" />
                            <asp:Button ID="btnREWRITE" runat="server" Enabled="False" Height="24px" OnClick="btnREWRITE_Click"
                                Text="清除" UseSubmitBehavior="False" />
                            <asp:ConfirmButtonExtender ID="btnREWRITE_ConfirmButtonExtender" runat="server" ConfirmText="確定要重填這筆資料?"
                                Enabled="True" TargetControlID="btnREWRITE">
                            </asp:ConfirmButtonExtender>
                            <br />
                            <br />
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSAVE" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnPrintThisRecord" />
                    <asp:PostBackTrigger ControlID="btnPrint" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="phrase" runat="server" style="display: none; cursor: default;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr align="center">
                        <td colspan="2" align="center">
                            <asp:Label ID="lblPhraseTitle" runat="server" ForeColor="Black" Font-Bold="true"
                                Font-Size="Medium" Font-Names="新細明體" Text="雜項交班紀錄單常用片語"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ImageButton2" runat="server" Height="32px" ImageUrl="~/Image/WebImage/633855842283694909.jpg"
                                Width="31px" OnClientClick="return Exit();" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Panel ID="Panel3" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:TabContainer ID="TabContainerPHRASE" runat="server" ActiveTabIndex="0" Width="410px">
                                                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="" Visible="false">
                                                </asp:TabPanel>
                                            </asp:TabContainer>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                        </td>
                        <td align="right" valign="bottom">
                            &nbsp;&nbsp;<asp:Button ID="btnSENT_SER_Content" runat="server" Text="送出" OnClientClick="return phrase_btnSENT_SER_Content();" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="toggler">
        <div id="effect" class="ui-widget-content">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <p>
                        <asp:Label ID="lblShowMsg" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="toggler">
                <div id="effect1" class="ui-widget-content1">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <p>
                                <asp:Label ID="lblShowErrMsg" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
    <asp:UpdatePanel ID="UpdatePanelErrMsg" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function txtC_date_s1_Changed() {
            var txtdate = document.getElementById("ContentPlaceHolder1_txtC_date_s1").value;
            var war = datecheckfun(txtdate);
            if (war == "f") {
                document.getElementById("ContentPlaceHolder1_lblShowErrMsg").innerHTML = "日期格式錯誤";
                runEffect1();
            }
        };
        function txtC_date_s2_Changed() {
            var txtdate = document.getElementById("ContentPlaceHolder1_txtC_date_s2").value;
            var war = datecheckfun(txtdate);
            if (war == "f") {
                document.getElementById("ContentPlaceHolder1_lblShowErrMsg").innerHTML = "日期格式錯誤";
                runEffect1();
            }
        };
        
        var CaretPos;
        $(function () {
            $().ajaxStart(
            function () {
                setTimeout($.blockUI, 0);
            }).ajaxStop($.unblockUI);
        });

        function showWaitPanel_Content() {
            var id = '<%=this.phrase.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '500px', position: 'fixed', top: '20px', left: '40px'} });
            var id = '<%=this.txtContent.ClientID %>';
            if (document.selection) {
                document.getElementById(id).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id).value.length);
                CaretPos = Sel.text.length;
            }
            // Firefox support
            else if (document.getElementById(id).selectionStart || document.getElementById(id).selectionStart == '0') {
                CaretPos = document.getElementById(id).selectionStart;
            }
        }

        function Exit() {
            $.unblockUI();
        }

        function phrase_btnSENT_SER_Content() {
            var id2 = '<%=this.btnSENT_SER_Content.ClientID%>';
            var id3 = '<%=this.txtContent.ClientID %>';
            //宣告一個字串
            var text = '';
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id3).value == "" && text == "")
                    text = $(this).val();
                else
                    text += "<,>" + $(this).val();
            });
            //跑完迴圈將值塞進TextBox中
            var b = document.getElementById(id3).value;
            document.getElementById(id3).value = b.substr(0, CaretPos) + text + b.substr(CaretPos, b.length);
            b = document.getElementById(id3).value;
            document.getElementById(id3).value = document.getElementById(id3).value.split("<,>");

            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            $.unblockUI();
        }

        // run the currently selected effect
        function runEffect() {
            // get effect type from
            var selectedEffect = "drop";
            // run the effect
            $("#effect").show(selectedEffect, 0, callback);
        };

        //callback function to bring a hidden box back
        function callback() {
            setTimeout(function () {
                $("#effect:visible").removeAttr("style").fadeOut();
            }, 1500);
        };

        $("#effect").hide();

        function set_symbols(symbol) {
            var textBoxContent = '<%=this.txtContent.ClientID %>';
            var Pos;
            if (document.selection) {
                document.getElementById(textBoxContent).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(textBoxContent).value.length);
                Pos = Sel.text.length;
            }
            // Firefox support
            else if (document.getElementById(textBoxContent).selectionStart || document.getElementById(textBoxContent).selectionStart == '0') {
                Pos = document.getElementById(textBoxContent).selectionStart;
            }
            var b = document.getElementById(textBoxContent).value;
            var pre = b.substr(0, Pos);
            var post = b.substr(Pos, b.length);
            document.getElementById(textBoxContent).value = pre + symbol.toString() + post;
        };

        function runEffect1() {
            // get effect type from
            var selectedEffect = "drop";
            // run the effect
            $("#effect1").show(selectedEffect, 0, callback1);
        };

        //callback function to bring a hidden box back
        function callback1() {
            setTimeout(function () {
                $("#effect1:visible").removeAttr("style").fadeOut();
            }, 1500);
        };

        $("#effect1").hide();

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
