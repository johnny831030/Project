<%@ Page Title="" Language="C#" MasterPageFile="~/NursingCarePlan.Master" AutoEventWireup="true"
    CodeBehind="NursingRecordSimple_M.aspx.cs" Inherits="longtermcare.NursingRecordSimple.NursingRecordSimple_M"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/jquery-2.0.3.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.js" type="text/javascript"></script>
    <script src="../../js/lightbox-2.6.min.js" type="text/javascript"></script>
    <link href="../../css/lightbox.css" rel="stylesheet" />
    <script src="../../DateTimeCheck.js" type="text/javascript"></script>
    <style type="text/css">
        .twoColFixLtHdr
        {
            text-align: center;
        }
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
            width: 50%;
            padding: 0;
            margin: 0 auto;
        }
        .style4
        {
            width: 50%;
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
        
        .style6
        {
            color: Black;
        }
        
        .style10
        {
            width: 98%;
        }
        .style101
        {
            width: 80%;
        }
        .style102
        {
            width: 99%;
        }
        
        .style104
        {
            width: 720px;
        }
        
        .btnsym
        {
            width: 50px;
            height: 50px;
        }
        .btnsym1
        {
            width: 50px;
            height: 50px;
            font-size: 7px;
        }
        
        .toggler
        {
            position: fixed;
            top: 200px;
            right: 300px;
            width: 200px;
            height: 20px;
        }
        #effect
        {
            position: absolute;
            top: 170px;
            right: 200px;
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
    <h1>
        <asp:Label ID="lbltablename" runat="server" Text="敘述性護理紀錄"></asp:Label></h1>
    <div style="display: none;">
        <asp:Button ID="Button1" runat="server" Text="新增" PostBackUrl="~/NursingRecordSimple/NursingRecordSimple_A.aspx"
            Visible="False" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Enabled="False" Text="查詢/修改" PostBackUrl="~/NursingRecordSimple/NursingRecordSimple_M.aspx"
            Visible="False" />
        <br />
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
                                    ForeColor="#003300" OnClick="LinkBtnGoToAddView_Click">新增護理紀錄</asp:LinkButton></p>
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
                                <asp:LinkButton ID="LinkBtnGoToQUDView" runat="server" Font-Size="16px" ForeColor="#003300"
                                    OnClick="LinkBtnGoToQUDView_Click" Font-Bold="True">查詢/修改/刪除 護理紀錄</asp:LinkButton></p>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="mainPanel">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanelMsg" runat="server">
                <ContentTemplate>
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View1" runat="server">
                            <table class="style101" style="width: 100%; color: Black;" border="1">
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="lblSearchDate1" runat="server" ForeColor="Black" Text="請選擇欲查詢住民"></asp:Label>
                                    </td>
                                    <td class="style105">
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
                                            AutoPostBack="True" DataSourceID="SqlDataSource5" DataTextField="AREA_STATE"
                                            DataValueField="AREA_STATE" Enabled="False">
                                            <asp:ListItem Value="-99">全部</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="lblSearchlimit" runat="server" ForeColor="Black" Text="篩選條件"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" ForeColor="Black" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="A">全部</asp:ListItem>
                                            <asp:ListItem Value="N">尚未帶入護理交班的護理紀錄</asp:ListItem>
                                            <asp:ListItem Value="Y">已帶入護理交班的護理紀錄</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label12" runat="server" ForeColor="Black" Text="護理紀錄作業區間"></asp:Label>
                                    </td>
                                    <td class="style105">
                                        &nbsp;<asp:TextBox ID="txtstartdate" runat="server" MaxLength="10"
                                            Style="margin-left: 0px" onchange="txtstartdate_Changed();" 
                                            Width="100px"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtstartdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="yyyy/MM/dd" TargetControlID="txtstartdate">
                                        </asp:CalendarExtender>
                                        <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="到"></asp:Label>
                                        <asp:TextBox ID="txtenddate" runat="server" AutoPostBack="False" Height="19px" MaxLength="10" onchange="txtenddate_Changed();"
                                            Width="100px"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtenddate_CalendarExtender" runat="server" Enabled="True"
                                            Format="yyyy/MM/dd" TargetControlID="txtenddate">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td colspan="2">
                                        <asp:Button ID="btnSearch" runat="server" Height="24px" OnClick="btnSearch_Click"
                                            Text="查詢" UseSubmitBehavior="False" />
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="lblSearchquick" runat="server" ForeColor="Black" Text="快速查詢"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:Button ID="btnSearchLast1" runat="server" OnClick="btnSearchLast1_Click" Text="最近1筆" />
                                        &nbsp;&nbsp;<asp:Button ID="btnSearchLast5" runat="server" OnClick="btnSearchLast5_Click"
                                            Text="最近5筆" />
                                        &nbsp;&nbsp;<asp:Button ID="btnSearchLastDay7" runat="server" OnClick="btnSearchLastDay7_Click"
                                            Text="最近7日" />
                                        &nbsp;&nbsp;<asp:Button ID="btnSearchLastDay14" runat="server" OnClick="btnSearchLastDay14_Click"
                                            Text="最近14日" />
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="tab_state_NursingRecordSimple_M" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="txt_sql" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="txt_sql1" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="str_account" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="str_connection_id" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="str_ip_no" runat="server" Visible="False"></asp:Label>
                            <br />
                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" SelectCommand="SELECT [AREA_STATE] FROM [CODE_ROOM_AREA] ORDER BY [AREA_ID] ASC">
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name AS IP_NAME, A3.IP_NO_NEW FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO WHERE (A1.IP_NO = @ipno) AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST(@Sdate AS DateTime), 112) AND CONVERT(varchar(8), CAST(@Edate AS DateTime), 112)) AND (A1.AccountEnable = 'Y') ORDER BY A1.R_DATE DESC">
                                <SelectParameters>
                                    <asp:SessionParameter Name="ipno" SessionField="ipno" />
                                    <asp:ControlParameter ControlID="txtstartdate" Name="Sdate" PropertyName="Text" />
                                    <asp:ControlParameter ControlID="txtenddate" Name="Edate" PropertyName="Text" />
                                </SelectParameters>
                                <DeleteParameters>
                                </DeleteParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                            &nbsp;
                            <asp:Label ID="lblQueryResult" runat="server" ForeColor="#0033CC" Visible="False"></asp:Label>
                            <asp:Label ID="querytype" runat="server" CssClass="style2" Visible="False">0</asp:Label>
                            <asp:Label ID="txt_columnIndex" runat="server" Visible="False">1</asp:Label><br />
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" DataKeyNames="NO" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" Style="text-align: center"
                                Visible="False" Width="98%" OnSorting="GridView1_Sorting">
                                <Columns>
                                    <%-- 
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True"/>  
                                    --%>
                                    <asp:CommandField ButtonType="Button" SelectText="檢視資料" ShowSelectButton="True">
                                        <HeaderStyle Width="12%" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="IP_NO_NEW" HeaderText="住民號碼" SortExpression="IP_NO_NEW" />
                                    <asp:BoundField DataField="IP_NAME" HeaderText="住民姓名" SortExpression="IP_NAME" />
                                    <asp:TemplateField HeaderText="紀錄日期" SortExpression="R_DATE">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelR_DATE" runat="server" Text='<%# DGFormatRIDDATE(Convert.ToString(DataBinder.Eval(Container.DataItem,"R_DATE"))) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="True" Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="紀錄時間" SortExpression="R_TIME">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelR_TIME" runat="server" Text='<%# DGFormatRIDTIME(Convert.ToString(DataBinder.Eval(Container.DataItem,"R_TIME"))) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="True" Width="12%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NAME" HeaderText="紀錄人員" SortExpression="NAME" />
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                            <br />
                            &nbsp;&nbsp;<asp:Button ID="btnQTotalDetailView" runat="server" Height="24px" OnClick="btnQTotalDetailView_Click"
                                Text="檢視查詢後的結果" UseSubmitBehavior="False" Width="130px" />
                            &nbsp;&nbsp;<asp:Button ID="btnPrint" runat="server" Height="24px" OnClick="btnPrint_Click"
                                Text="列印查詢後的結果" UseSubmitBehavior="False" Width="130px" />
                            <br />
                            <br />
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <asp:Panel ID="Panel2" runat="server" BorderColor="White" DefaultButton="btnSAVE">
                                <asp:Panel ID="Panel3" runat="server">
                                    <br />
                                    &nbsp;<asp:Button ID="Button5" runat="server" Height="24px" OnClick="Button5_Click"
                                        Text="修改這一筆紀錄" UseSubmitBehavior="False" />
                                    <asp:Button ID="Button6" runat="server" Height="24px" OnClick="Button6_Click" Text="刪除這一筆紀錄"
                                        UseSubmitBehavior="False" />
                                    <asp:Button ID="Button8" runat="server" Height="24px" OnClick="Button8_Click" Text="將這一筆紀錄帶入護理交班"
                                        UseSubmitBehavior="False" />
                                    <asp:Button ID="Button7" runat="server" Height="24px" OnClick="Button7_Click" Text="返回查詢"
                                        UseSubmitBehavior="False" />
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="Panel1" runat="server" Enabled="False">
                                    <table border="1">
                                        <tr>
                                            <td align="center" bgcolor="#FFFFCC" class="style6">
                                                <asp:Label ID="Label6" runat="server" ForeColor="Black" Text="住民姓名(住民號碼)"></asp:Label>
                                            </td>
                                            <td class="style10">
                                                <asp:Label ID="txtIPName" runat="server" ForeColor="Black" Text=""></asp:Label>(<asp:Label
                                                    ID="txtIPNo" runat="server" ForeColor="Black" Text=""></asp:Label>)
                                                <asp:Label ID="lblIPNO" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" bgcolor="#FFFFCC" class="style6">
                                                <asp:Label ID="Label3" runat="server" ForeColor="Black" Text="紀錄日期"></asp:Label>
                                            </td>
                                            <td class="style10">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtR_Date" runat="server" MaxLength="10" onchange="txtR_Date_Changed();" Style="margin-left: 0px"
                                                            AutoPostBack="False" ></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                                            TargetControlID="txtR_Date">
                                                        </asp:CalendarExtender>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" bgcolor="#FFFFCC" class="style6" style="width: 80px">
                                                <asp:Label ID="Label4" runat="server" ForeColor="Black" Text="紀錄時間"></asp:Label>
                                            </td>
                                            <td class="style10">
                                                <asp:TextBox ID="txtR_Time" runat="server" AutoPostBack="False" onchange="txtR_Time_Changed();"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" bgcolor="#FFFFCC" class="style6" style="width: 80px">
                                                <asp:Label ID="Label149" runat="server" ForeColor="Black" Text="紀錄人員"></asp:Label>
                                            </td>
                                            <td class="style10">
                                                <asp:Label ID="txtCUserName" runat="server" ForeColor="Black"></asp:Label>
                                                <asp:Label ID="lblRecUserID" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" bgcolor="#FFFFCC" class="style6" style="width: 80px">
                                                <asp:Label ID="Label5" runat="server" ForeColor="Black" Text="個案狀況及處置" Width="60px"></asp:Label>
                                            </td>
                                            <td class="style10">
                                                <asp:TextBox ID="txtContent" runat="server" Height="85px" TextMode="MultiLine" Width="98%"></asp:TextBox><br />
                                                <asp:ImageButton ID="ibtnD0" runat="server" Height="16px" ImageUrl="~/Image/WebImage/record.png"
                                                    OnClick="btn_recent_Click" OnClientClick="return showWaitPanel_Recent();" Width="16px"
                                                    ToolTip="住民近況" />
                                                &nbsp;<asp:ImageButton ID="ibtnContent" runat="server" ImageUrl="~/Image/WebImage/gif_45_069.gif"
                                                    OnClick="ibtnContent_Click" OnClientClick="return showWaitPanel_Content();" Width="16px" />
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
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input type="button" name="btn_AccPan1_15" value="％" onclick="set_symbols(this.value)"
                                                                            class="btnsym">
                                                                    </td>
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
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input type="button" name="btn_AccPan2_15" value="〔" onclick="set_symbols(this.value)"
                                                                            class="btnsym">
                                                                    </td>
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
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input type="button" name="btn_AccPan2_29" value="＠" onclick="set_symbols(this.value)"
                                                                            class="btnsym">
                                                                    </td>
                                                                    <td>
                                                                        <input type="button" name="btn_AccPan2_30" value="＆" onclick="set_symbols(this.value)"
                                                                            class="btnsym">
                                                                    </td>
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
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSymbolsTools" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ibtnD0" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ibtnContent" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel5" runat="server" Visible="False">
                                    <table border="1">
                                        <tr>
                                            <td align="center" bgcolor="#FFFFCC" class="style6">
                                                <asp:Label ID="Label148" runat="server" ForeColor="Black" Text="護理交班內容" Width="60px"></asp:Label>
                                            </td>
                                            <td class="style10">
                                                <asp:TextBox ID="txtNurseExchangeTemp" runat="server" Font-Size="Small" Height="150px"
                                                    TextMode="MultiLine" Visible="True" Width="95%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;<asp:Button ID="btnInsertNE" runat="server" Height="24px" OnClick="btnInsertNE_Click"
                                                    Text="儲存護理交班" Visible="True" UseSubmitBehavior="False" />
                                                &nbsp;
                                                <asp:Button ID="btnNurseExchangeClear" runat="server" Height="24px" OnClick="btnNurseExchangeClear_Click"
                                                    Text="放棄護理交班" UseSubmitBehavior="False" />
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </asp:Panel>
                                &nbsp;<br />
                                &nbsp;&nbsp;<asp:Button ID="btnSAVE" runat="server" Height="24px" OnClick="btnSAVE_Click"
                                    Text="儲存" Enabled="False" />
                                <asp:Button ID="btnPrintThisRecord" runat="server" Height="24px" OnClick="btnPrintThisRecord_Click"
                                    Text="列印" />
                                <asp:Button ID="btnREWRITE" runat="server" Enabled="False" Height="24px" OnClick="btnREWRITE_Click"
                                    Text="清除" />
                                &nbsp;<asp:Label ID="Label147" runat="server" ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblPrintReviseDate" runat="server" Text="102.12修訂" Visible="False"></asp:Label>
                                <br />
                                <br />
                            </asp:Panel>
                        </asp:View>
                        &nbsp;<asp:View ID="View3" runat="server">
                            <br />
                            <asp:Repeater ID="rp_detailTable" runat="server">
                                <HeaderTemplate>
                                    <table border="1" width="100%">
                                        <tr>
                                            <td align="center" bgcolor="#CCFFCC" style="width: 8%">
                                                紀錄日期
                                            </td>
                                            <td align="center" bgcolor="#CCFFCC" style="width: 8%">
                                                紀錄時間
                                            </td>
                                            <td align="center" bgcolor="#CCFFCC" style="width: 8%">
                                                住民編號
                                            </td>
                                            <td align="center" bgcolor="#CCFFCC" style="width: 8%">
                                                姓名
                                            </td>
                                            <td align="center" bgcolor="#CCFFCC">
                                                護理紀錄內容
                                            </td>
                                            <td align="center" bgcolor="#CCFFCC" style="width: 8%">
                                                紀錄人員
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <small>
                                                <%# DGFormatRIDDATE(Convert.ToString(Eval("R_DATE")))%></small>
                                        </td>
                                        <td align="center">
                                            <small>
                                                <%# DGFormatRIDTIME(Convert.ToString(Eval("R_TIME")))%></small>
                                        </td>
                                        <td align="center">
                                            <small>
                                                <%# Eval("IP_NO_NEW")%>
                                            </small>
                                        </td>
                                        <td align="center">
                                            <small>
                                                <%# Eval("IP_NAME")%>
                                            </small>
                                        </td>
                                        <td align="left">
                                            <small>
                                                <%# DGFormatRIDCONTENT(Convert.ToString(Eval("CONTENT")))%>
                                            </small>
                                        </td>
                                        <td align="center">
                                            <small>
                                                <%# Eval("Name")%>
                                            </small>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            <br />
                            <br />
                            &nbsp;<asp:Button ID="btnPrint2_1" runat="server" Height="24px" OnClick="btnPrint2_1_Click"
                                Text="列印檢視的結果" UseSubmitBehavior="False" Width="130px" />
                            &nbsp;
                            <asp:Button ID="Button9" runat="server" Height="24px" OnClick="Button7_Click" Text="返回查詢"
                                UseSubmitBehavior="False" />
                            <br />
                            <br />
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnPrint" />
                    <asp:PostBackTrigger ControlID="btnPrintThisRecord" />
                    <asp:PostBackTrigger ControlID="btnPrint2_1" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="phrase" runat="server" style="display: none; cursor: default;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <table style="width: 100%;">
                        <tr align="center">
                            <td colspan="2" align="center">
                                <asp:Label ID="lblPhraseTitle" runat="server" ForeColor="Black" Font-Bold="true"
                                    Font-Size="Medium" Font-Names="新細明體" Text="護理紀錄常用片語"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="ImageButton2" runat="server" Height="32px" ImageUrl="~/Image/WebImage/633855842283694909.jpg"
                                    Width="31px" OnClientClick="return Exit();" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:Panel ID="Panel4" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TabContainer ID="TabContainerPHRASE" runat="server" ActiveTabIndex="0" Width="100%">
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
                                &nbsp;&nbsp;<asp:Button ID="btnSENT_Content" runat="server" Text="送出" OnClientClick="return phrase_Content();" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="recent" runat="server" style="display: none; cursor: default;">
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <ContentTemplate>
                    <table style="width: 100%;">
                        <tr align="center">
                            <td colspan="2" align="center">
                                <asp:Label ID="Label7" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="Medium"
                                    Font-Names="新細明體" Text="住民近況"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="32px" ImageUrl="~/Image/WebImage/633855842283694909.jpg"
                                    Width="31px" OnClientClick="return Exit();" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:Panel ID="Panel9" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="450px"
                                                    Height="300px" ScrollBars="Vertical">
                                                    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="" Visible="false">
                                                    </asp:TabPanel>
                                                </asp:TabContainer>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <br />
                            </td>
                            <td align="right" valign="bottom">
                                &nbsp;&nbsp;<asp:Button ID="Button4" runat="server" Text="送出" OnClientClick="return get_recent();" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
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
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
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
    </div>
    <script type="text/javascript">
        function txtstartdate_Changed() {
            var txtdate = document.getElementById("ContentPlaceHolder1_txtstartdate").value;
            var war = datecheckfun(txtdate);
            if (war == "f") {
                document.getElementById("ContentPlaceHolder1_lblShowErrMsg").innerHTML = "日期格式錯誤";
                runEffect1();
            }
        };
        function txtenddate_Changed() {
            var txtdate = document.getElementById("ContentPlaceHolder1_txtenddate").value;
            var war = datecheckfun(txtdate);
            if (war == "f") {
                document.getElementById("ContentPlaceHolder1_lblShowErrMsg").innerHTML = "日期格式錯誤";
                runEffect1();
            }
        };

        function txtR_Date_Changed() {
            var txtdate = document.getElementById("ContentPlaceHolder1_txtR_Date").value;
            var war = datecheckfun(txtdate);
            if (war == "f") {
                document.getElementById("ContentPlaceHolder1_lblShowErrMsg").innerHTML = "日期格式錯誤";
                runEffect1();
            }
        };

        function txtR_Time_Changed() {
            var txttime = document.getElementById("ContentPlaceHolder1_txtR_Time").value;
            var war = timecheckfun(txttime);
            if (war == "f") {
                document.getElementById("ContentPlaceHolder1_lblShowErrMsg").innerHTML = "時間格式錯誤";
                runEffect1();
            }
        };
        function showWaitPanel_Content() {
            var id = '<%=this.phrase.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '700px', position: 'fixed', top: '20px', left: '40px'} });
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

        function showWaitPanel_Recent() {
            var id = '<%=this.recent.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '550px', position: 'fixed', top: '20px', left: '40px'} });
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

        function phrase_Content() {
            var id = '<%=this.btnSENT_Content.ClientID%>';
            var id1 = '<%=this.txtContent.ClientID %>';
            //宣告一個字串$('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            var text = '';
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id1).value == "" && text == "")
                    text = $(this).val();
                else
                    text += "<,>" + $(this).val();
            });
            //跑完迴圈將值塞進TextBox中
            var b = document.getElementById(id1).value;
            document.getElementById(id1).value = b.substr(0, CaretPos) + text + b.substr(CaretPos, b.length);
            b = document.getElementById(id1).value;
            document.getElementById(id1).value = document.getElementById(id1).value.split("<,>");

            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            $.unblockUI();
        }

        function get_recent() {
            var id = '<%=this.Button4.ClientID%>';
            var id1 = '<%=this.txtContent.ClientID %>';
            //宣告一個字串$('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            var text = '';
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainer1"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id1).value == "" && text == "")
                    text = $(this).val();
                else
                    text += $(this).val() + "\r\n";
            });
            //跑完迴圈將值塞進TextBox中

            var b = document.getElementById(id1).value;

            document.getElementById(id1).value = b.substr(CaretPos, b.length) + text.replace(/\<\/br\>/g, "\r\n").replace(/\&nbsp\;/g, "") + b.substr(0, CaretPos);
            b = document.getElementById(id1).value;
            document.getElementById(id1).value = document.getElementById(id1).value.split("\r\n");

            $('[id^="ContentPlaceHolder1_TabContainer1"]:checked').attr('checked', false);
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
