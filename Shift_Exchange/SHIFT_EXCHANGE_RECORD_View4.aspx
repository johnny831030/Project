<%@ Page Title="" Language="C#" MasterPageFile="~/NursingCarePlan.Master" AutoEventWireup="true"
    CodeBehind="SHIFT_EXCHANGE_RECORD_View4.aspx.cs" Inherits="longtermcare.NursingPlan.Shift_Exchange.SHIFT_EXCHANGE_RECORD_View4"
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
            background: #FFFFFF;
            border: solid 1px #006600;
            border-top-width: 1px;
            border-bottom-width: 0;
            border-left-width: 2px;
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
        
        
        .style8
        {
            width: 80%;
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
                                    OnClick="LinkBtnGoToUView_Click" Font-Bold="False">查詢/修改/刪除 雜項交班</asp:LinkButton></p>
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
                                    OnClick="LinkBtnGoToQView_Click" Font-Bold="False">查詢/修改/刪除 護理交班</asp:LinkButton></p>
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
                                    OnClick="LinkBtnGoToMQView_Click" Font-Bold="True">查詢交班紀錄</asp:LinkButton></p>
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
                    <asp:Panel ID="Panel6" runat="server" BorderColor="White">
                        <table class="style8">
                            <tr>
                                <td>
                                    <br />
                                    &nbsp;&nbsp;<asp:Label ID="lblSearchDate1" runat="server" ForeColor="Black" Text="請選擇欲查詢住民:"></asp:Label>
                                    <asp:DropDownList ID="DropDownListIP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListIP_SelectedIndexChanged">
                                        <asp:ListItem Value="0">此一住民</asp:ListItem>
                                        <asp:ListItem Value="1">所有住民</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:Label ID="lblSearchDate2" runat="server" ForeColor="Black" Text="請選擇欲查詢區域(或樓層):"></asp:Label>
                                    <asp:DropDownList ID="DropDownListArea" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="AREA_STATE"
                                        DataValueField="AREA_STATE" Enabled="False">
                                        <asp:ListItem Value="-99">全部</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table class="style8">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;<asp:Label ID="lblSearchDate" runat="server" ForeColor="Black" Text="請輸入欲查詢區間:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtS_DATE" runat="server" onchange="txtS_DATE_Changed();" MaxLength="10"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txtS_DATE_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtS_DATE" ValidChars="/">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:CalendarExtender ID="txtS_DATE_CalendarExtender" runat="server" Enabled="True"
                                        Format="yyyy/MM/dd" TargetControlID="txtS_DATE">
                                    </asp:CalendarExtender>
                                    <asp:Label ID="lblTo" runat="server" ForeColor="Black" Text="到"></asp:Label>
                                    <asp:TextBox ID="txtE_DATE" runat="server" 
                                        onchange="txtE_DATE_Changed();" MaxLength="10"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txtE_DATE_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtE_DATE" ValidChars="/">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:CalendarExtender ID="txtE_DATE_CalendarExtender" runat="server" Enabled="True"
                                        Format="yyyy/MM/dd" TargetControlID="txtE_DATE">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch_nonNRS" runat="server" Height="24px" OnClick="btnSearch_nonNRS_Click"
                                        Text="查詢" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnPrint" runat="server" Enabled="False" Height="24px" OnClick="btnPrint_Click"
                                        Text="列印查詢後的結果" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        &nbsp;
                        <asp:Label ID="lblQueryResult" runat="server" ForeColor="#0033CC" Visible="False"></asp:Label>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            OnSorting="GridView1_Sorting" Width="99%" Visible="False">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="80px" HeaderText="日期" SortExpression="A_DATE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNRS_Date" runat="server" Text='<%# Eval("A_DATE")%>'> </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="70px" HeaderText="床號" SortExpression="ROOM_AREA,ROOM_BED">
                                    <ItemTemplate>
                                        <asp:Label ID="lblROOM_BED" runat="server" Text='<%# Eval("ROOM_BED")%>'> </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80px" HeaderText="姓名">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_IPName" runat="server" Text='<%# Eval("IP_NAME")%>'> </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="白班">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDayShift" runat="server" Text='<%# Eval("DAY_SHIFT")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="小夜">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNightShift" runat="server" Text='<%# Eval("NIGHT_SHIFT")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="大夜">
                                    <ItemTemplate>
                                        <asp:Label ID="lnlGraveShift" runat="server" Text='<%# Eval("GRAVEYARD_SHIFT")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" VerticalAlign="Top" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                        <asp:GridView ID="GridView2" runat="server" Visible="False">
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT [AREA_STATE] FROM [CODE_ROOM_AREA] ORDER BY [AREA_ID] ASC">
                        </asp:SqlDataSource>
                        <asp:Label ID="sstr_hid" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="str_account" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="sstr_ipno" runat="server" Visible="False"></asp:Label>
                        <br />
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnPrint" />
                </Triggers>
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
        function txtS_DATE_Changed() {
            var txtdate = document.getElementById("ContentPlaceHolder1_txtS_DATE").value;
            var war = datecheckfun(txtdate);
            if (war == "f") {
                document.getElementById("ContentPlaceHolder1_lblShowErrMsg").innerHTML = "日期格式錯誤";
                runEffect1();
            }
        };
        function txtE_DATE_Changed() {
            var txtdate = document.getElementById("ContentPlaceHolder1_txtE_DATE").value;
            var war = datecheckfun(txtdate);
            if (war == "f") {
                document.getElementById("ContentPlaceHolder1_lblShowErrMsg").innerHTML = "日期格式錯誤";
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
