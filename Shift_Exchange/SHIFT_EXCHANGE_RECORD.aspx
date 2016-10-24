<%@ Page Title="" Language="C#" MasterPageFile="~/NursingCarePlan.Master" AutoEventWireup="true"
    CodeBehind="SHIFT_EXCHANGE_RECORD.aspx.cs" Inherits="longtermcare.NursingPlan.Shift_Exchange.SHIFT_EXCHANGE_RECORD"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/jquery-2.0.3.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.min.js" type="text/javascript"></script>
    <script src="../../DateTimeCheck.js" type="text/javascript"></script>
    <script src="../../js/lightbox-2.6.min.js" type="text/javascript"></script>
    <link href="../../css/lightbox.css" rel="stylesheet" />
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
            background: #FFFFFF;
            border: solid 1px #006600;
            border-top-width: 1px;
            border-bottom-width: 0;
            border-left-width: 2px;
            height: 22px;
        }
        .functionitem2
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
        
        
        .style11
        {
            width: 10%;
        }
        .style33
        {
        }
        .style55
        {
            height: 111px;
        }
        .style66
        {
            height: 111px;
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
    <h1><asp:Label ID="lblShiftExchangeRecord" runat="server" Text="交班紀錄"></asp:Label></h1>
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
                                <asp:LinkButton ID="LinkBtnGoToAddView" runat="server" Font-Bold="True" Font-Size="16px"
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
                                    OnClick="LinkBtnGoToUView_Click">查詢/修改/刪除 雜項交班</asp:LinkButton></p>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanelMsg" runat="server">
            <ContentTemplate>
            <asp:Panel ID="Panel6" runat="server" BorderColor="White" DefaultButton="btnSave">
                <asp:Label ID="sstr_ipno" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="sstr_hid" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="sstr_account" runat="server" Visible="False"></asp:Label>
                <br />
                <asp:Button ID="Button2" runat="server" Height="24px" OnClick="Button2_Click" OnClientClick="return showWaitPanel_PreSERecord();"
                    Text="顯示最近交班紀錄" />
                <asp:Button ID="btnDito" runat="server" Height="24px" OnClick="btnDito_Click" Text="Ditto"
                    UseSubmitBehavior="False" />
                <asp:Button ID="btnNullFormPrint" runat="server" Height="24px" OnClick="btnNullFormPrint_Click"
                    Text="列印空白表單" UseSubmitBehavior="False" />
                <asp:Button ID="btnRule" runat="server" Height="24px" OnClick="btnRule_Click" Text="填寫規則"
                    UseSubmitBehavior="False" />
                <br />
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:Panel ID="PanelData" runat="server" Width="99%">
                    <table border="1" width="99%">
                        <tr>
                            <td class="style11" bgcolor="#FFFFCC">
                                <asp:Label ID="lblNowData" runat="server" Text="紀錄日期" ForeColor="Black"></asp:Label>
                            </td>
                            <td class="style33">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtShowDate" runat="server" onchange="txtShowDate_Changed();"
                                            MaxLength="10" ></asp:TextBox>
                                        <asp:CalendarExtender ID="txtShowDate_CalendarExtender" runat="server" Enabled="True"
                                            Format="yyyy/MM/dd" TargetControlID="txtShowDate">
                                        </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11" bgcolor="#FFFFCC">
                                <asp:Label ID="lblTime" runat="server" Text="紀錄時間" ForeColor="Black"></asp:Label>
                            </td>
                            <td class="style33">
                                <asp:TextBox ID="txtTime" runat="server" MaxLength="5" onchange="txtTime_Changed();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style55" bgcolor="#FFFFCC">
                                <asp:Label ID="lblContent" runat="server" Text="交班內容" ForeColor="Black"></asp:Label>
                            </td>
                            <td class="style66" width="100%">
                                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="100px" 
                                    Width="98%"></asp:TextBox>
                                <br />
                                <asp:ImageButton ID="ibtnD0" runat="server" Height="16px" Width="16px" ImageUrl="~/Image/WebImage/record.png" 
                                   OnClick="btn_recent_Click" OnClientClick="return showWaitPanel_Recent();" ToolTip="住民近況" />
                                <asp:ImageButton ID="ibtnPhraseShiftExchangeContent" runat="server" BorderStyle="None"
                                    Height="20px" ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif"
                                    OnClick="ibtnPhraseShiftExchangeContent_Click" OnClientClick="return showWaitPanel_Content();"
                                    ToolTip="片語" Width="20px" />
                                &nbsp;<asp:Button ID="btnSymbolsTools" runat="server" OnClick="btnSymbolsTools_Click"
                                    Text="符號表" />
                                <asp:Panel ID="PanelSymbolsTools" runat="server" Visible="False" Width="98%">
                                    <asp:Accordion ID="Accordion1" HeaderCssClass="accordion_headings" HeaderSelectedCssClass="header_highlight"
                                        ContentCssClass="accordion_child" runat="server" SelectedIndex="0" FadeTransitions="true"
                                        SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40"
                                        RequireOpenedPane="true" AutoSize="None" Width="100%">
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
                <asp:Button ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click" Text="儲存" />
                <asp:Button ID="btnPrint" runat="server" Height="24px" OnClick="btnPrint_Click" Text="列印"
                    UseSubmitBehavior="False" />
                <asp:Button ID="btnClear" runat="server" Height="24px" OnClick="btnClear_Click" Text="清除"
                    UseSubmitBehavior="False" />
                <asp:Button ID="btnGoBackNewAdd" runat="server" Height="24px" OnClick="btnGoBackNewAdd_Click"
                    Text="返回新增" UseSubmitBehavior="False" />
                <br />
                <br />
            </asp:Panel>
        </ContentTemplate>
            <Triggers>
                    <asp:PostBackTrigger ControlID="btnPrint" />
                </Triggers>
        </asp:UpdatePanel>
    </div>
        <div id="preserecord" runat="server" style="display: none; cursor: default;">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr align="left">
                        <td>
                            &nbsp;<asp:Label ID="Label2" runat="server" ForeColor="Black" Text="最近雜項交班紀錄瀏覽:"
                                Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label ID="lblShowNoSERecord" runat="server" ForeColor="Red" Font-Size="Larger"
                                Visible="False" Font-Bold="True"></asp:Label>
                            <br />
                            <asp:Label ID="lblSEDate" runat="server" ForeColor="Black"></asp:Label>
                            <br />
                            <asp:Label ID="lblSETime" runat="server" ForeColor="Black"></asp:Label>
                            <br />
                            <asp:Label ID="lblSEContent" runat="server" ForeColor="Black"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            &nbsp;<asp:Button ID="btnPreSERecord" runat="server" Text="確定" OnClientClick="return Exit();" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
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
        <div id="recent" runat="server" style="display: none; cursor: default;">
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <ContentTemplate>
                    <table style="width: 100%;">
                        <tr align="center">
                            <td colspan="2" align="center">
                                <asp:Label ID="Label6" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="Medium"
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
                                &nbsp;&nbsp;<asp:Button ID="Button4" runat="server" Text="送出" OnClientClick="return recent_btnSENT_SER_Content();" />
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
            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
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
    </div>
    <script type="text/javascript">
        function txtShowDate_Changed() {
            var txtdate = document.getElementById("ContentPlaceHolder1_txtShowDate").value;
            var war = datecheckfun(txtdate);
            if (war == "f") {
                document.getElementById("ContentPlaceHolder1_lblShowErrMsg").innerHTML = "日期格式錯誤";
                runEffect1();
            }
        };

        function txtTime_Changed() {
            var txttime = document.getElementById("ContentPlaceHolder1_txtTime").value;
            var war = timecheckfun(txttime);
            if (war == "f") {
                document.getElementById("ContentPlaceHolder1_lblShowErrMsg").innerHTML = "時間格式錯誤";
                runEffect1();
            }
        };
        
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
        var CaretPos;
        $(function () {
            $().ajaxStart(
            function () {
                setTimeout($.blockUI, 0);
            }).ajaxStop($.unblockUI);
        });

        function showWaitPanel_PreSERecord() {
            var id = '<%=this.preserecord.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '600px', position: 'fixed', top: '20px', left: '40px'} });
            document.getElementById(id11).style.display = '';

            var id3 = '<%=this.lblSEDate.ClientID %>';
            if (document.selection) {
                document.getElementById(id3).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id3).value.length);
                CaretPos = Sel.text.length;
            }
            // Firefox support
            else if (document.getElementById(id3).selectionStart || document.getElementById(id3).selectionStart == '0') {
                CaretPos = document.getElementById(id3).selectionStart;
            }
            var id4 = '<%=this.lblSETime.ClientID %>';
            if (document.selection) {
                document.getElementById(id4).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id4).value.length);
                CaretPos = Sel.text.length;
            }
            // Firefox support
            else if (document.getElementById(id4).selectionStart || document.getElementById(id4).selectionStart == '0') {
                CaretPos = document.getElementById(id4).selectionStart;
            }
            var id4 = '<%=this.lblShowNoSERecord.ClientID %>';
            if (document.selection) {
                document.getElementById(id4).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id4).value.length);
                CaretPos = Sel.text.length;
            }
            // Firefox support
            else if (document.getElementById(id4).selectionStart || document.getElementById(id4).selectionStart == '0') {
                CaretPos = document.getElementById(id4).selectionStart;
            }
            var id6 = '<%=this.lblSEContent.ClientID %>';
            if (document.selection) {
                document.getElementById(id6).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id6).value.length);
                CaretPos = Sel.text.length;
            }
            // Firefox support
            else if (document.getElementById(id6).selectionStart || document.getElementById(id6).selectionStart == '0') {
                CaretPos = document.getElementById(id6).selectionStart;
            }
        }

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

        function showWaitPanel_Recent() {
            var id = '<%= this.recent.ClientID%>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '550px', position: 'fixed', top: '20px', left: '40px' } });
            var id = '<%= this.txtContent.ClientID%>';
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

        // 住民照護近況
        function recent_btnSENT_SER_Content() {
            var id2 = '<%= this.Button4.ClientID%>';
            var id3 = '<%= this.txtContent.ClientID%>';
            //宣告一個字串
            var text = '';
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainer1"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id3).value == "" && text == "")
                    text = $(this).val();
                else
                    text += $(this).val() + "\r\n";
            });
            //跑完迴圈將值塞進TextBox中

            var b = document.getElementById(id3).value;

            document.getElementById(id3).value = b.substr(0, CaretPos) + text.replace(/\<\/br\>/g, "\r\n").replace(/\&nbsp\;/g, "") + b.substr(CaretPos, b.length);
            b = document.getElementById(id3).value;
            document.getElementById(id3).value = document.getElementById(id3).value.split("\r\n");

            $('[id^="ContentPlaceHolder1_TabContainer1"]:checked').attr('checked', false);
            $.unblockUI();
        }

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
