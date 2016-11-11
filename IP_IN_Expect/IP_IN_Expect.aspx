<%@ Page Language="C#" MasterPageFile="~/IPMangement.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" EnableEventValidation="false" CodeBehind="IP_IN_Expect.aspx.cs"
    Inherits="longtermcare.IPInOut.IP_IN_Expect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <style type="text/css">
        .style22
        {
            width: 845px;
        }
        .style8
        {
            width: 160px;
        }
        .style10
        {
            width: 160px;
        }
        .style18
        {
            font-size: large;
            color: red;
            font-weight: bold;
        }
        .Watermark
        {
            color: #ACA899;
        }
        
        .mainPanel
        {
            padding: 0,0,1px,0;
            margin: 0,0,1px,0;
            color: #000000;
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
            width: 33%;
            padding: 0;
            margin: 0 auto;
        }
        .style4
        {
            width: 33%;
            padding: 0;
            margin: 0 auto;
        }
        .style5
        {
            width: 33%;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--BACKtoTOP-START-->
    <a style="display: scroll; position: fixed; bottom: 0px; right: 200px;" href="#"
        title="Back to Top" onfocus="if(this.blur)this.blur()">
        <img alt='' border='0' onmouseover="this.src='/Image/WebImage/B2T.png'" src="/Image/WebImage/B2T_medium.png"
            onmouseout="this.src='/Image/WebImage/B2T_medium.png'" /></a>
    <!--BACKtoTOP-STOP-->
    <h1>
        <asp:Label ID="Label2" runat="server" Text="預約入住管理" Font-Bold="True" Font-Size="X-Large"
            Style="text-align: center"></asp:Label>
    </h1>
    <br />
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
                                ForeColor="#003300" OnClick="LinkBtnGoToAddView_Click">新增預約入住</asp:LinkButton>
                        </p>
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
                            <asp:LinkButton ID="LinkBtnGoToQUView" runat="server" Font-Size="16px" ForeColor="#003300"
                                OnClick="LinkBtnGoToQUView_Click">查詢/修改/刪除預約入住</asp:LinkButton>
                        </p>
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
                            <asp:LinkButton ID="LinkBtnGoToRegionQueryView" runat="server" Font-Size="16px" ForeColor="#003300"
                                OnClick="LinkBtnGoToRegionQueryView_Click">(區間)查詢/列印住民預約入住</asp:LinkButton>
                        </p>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="mainPanel">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <asp:Label ID="Label28" runat="server" ForeColor="Black" Text="現有住民預約入住："></asp:Label>
        <asp:RadioButton ID="RadioButton1" runat="server" OnCheckedChanged="RadioButton1_CheckedChanged"
            Text="是" AutoPostBack="True" GroupName="rbIP" />
        <asp:RadioButton ID="RadioButton2" runat="server" OnCheckedChanged="RadioButton2_CheckedChanged"
            Text="否" AutoPostBack="True" GroupName="rbIP" />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <br />
                        <asp:Label ID="Label39" runat="server" Text="請輸入任一項查詢條件："></asp:Label>
                        <br />
                        <br />
                        <table class="style22" border="1" style="color: Black;">
                            <tr>
                                <td class="style8" bgcolor="#FFFFCC">
                                    <asp:Label ID="Label6" runat="server" Text="住民號碼"></asp:Label>
                                </td>
                                <td class="style10">
                                    <asp:TextBox ID="txtipno" runat="server"></asp:TextBox>
                                </td>
                                <td class="style8" bgcolor="#FFFFCC">
                                    <asp:Label ID="Label7" runat="server" Text="住民姓名"></asp:Label>
                                </td>
                                <td class="style10">
                                    <asp:TextBox ID="txtIPName" runat="server"></asp:TextBox>
                                </td>
                                <td class="style8" bgcolor="#FFFFCC">
                                    <asp:Label ID="Label8" runat="server" Text="身分證號"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIPID" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style8" bgcolor="#FFFFCC">
                                    <asp:Label ID="Label22" runat="server" Text="生日"></asp:Label>
                                </td>
                                <td class="style10">
                                    <asp:TextBox ID="txtDOB" runat="server" MaxLength="8"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="txtDOB_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="txtDOB" WatermarkCssClass="Watermark" WatermarkText="輸入格式yyyyMMdd">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" Enabled="True"
                                        Format="yyyyMMdd" TargetControlID="txtDOB">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="style8" bgcolor="#FFFFCC">
                                    <asp:Label ID="Label23" runat="server" Text="床號"></asp:Label>
                                </td>
                                <td class="style10">
                                    <asp:TextBox ID="txtRoomBed" runat="server"></asp:TextBox>
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="btninput" runat="server" OnClick="btnInput_Click" Text="輸入" />
                                    <asp:Button ID="btnclear" runat="server" OnClick="btnClear_Click" Text="清除" />
                                    &nbsp;<asp:Label ID="Label21" runat="server" ForeColor="Red" Font-Bold="False" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        
                        <asp:Label ID="Label40" runat="server" Text="或自表單中選取住民："></asp:Label>
                        <br />
                        <br />
                        <asp:GridView ID="gvwIPList" runat="server" AutoGenerateColumns="False" CellPadding="3"
                            DataKeyNames="IP_NO" DataSourceID="SqlDataSource1" OnPageIndexChanging="CustomersGridView_PageIndexChanged"
                            OnSelectedIndexChanged="gvwIPList_SelectedIndexChanged" AllowPaging="True" PageSize="5"
                            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px"
                            Width="845px">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" runat="server" CommandName="Select" Text="選擇" /></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="IP_NO" HeaderText="病歷號碼" SortExpression="IP_NO" Visible="False" />
                                <asp:BoundField DataField="IP_NO_NEW" HeaderText="住民編號" SortExpression="IP_NO_NEW" />
                                <asp:BoundField DataField="IP_NAME" HeaderText="姓名" SortExpression="IP_NAME" />
                                <asp:BoundField DataField="IP_ID" HeaderText="身分證號" SortExpression="IP_ID" />
                                <asp:TemplateField HeaderText="生日" SortExpression="DOB">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#  ConvertDate(Convert.ToString(DataBinder.Eval(Container.DataItem,"DOB")))  %>'>

                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SEX" HeaderText="性別" SortExpression="SEX" Visible="False" />
                                <asp:BoundField DataField="ROOM_BED" HeaderText="床號" SortExpression="ROOM_BED" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                            SelectCommand="">
                        </asp:SqlDataSource>
                        <!-- SELECT I.IP_NO, I.IP_NO_NEW, I.IP_NAME, I.IP_ID, I.DOB, ROOM.ROOM_BED FROM IP_INFORMATION as I Left Outer Join ROOM ON I.IP_NO = ROOM.IP_NO WHERE (I.AccountEnable = 'Y') order by I.IP_NO -->
                        <br /> 
                        
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <br />
                        <asp:Label ID="Label41" runat="server" Text="住民入住狀態："></asp:Label>
                        <asp:Label ID="lblipinstate" runat="server" ForeColor="Blue"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label11" runat="server" Text="* 表示必填欄位" Font-Bold="True" ForeColor="Red"></asp:Label>
                        <asp:Panel ID="Panel1" runat="server">
                            <table border="1" width="100%">
                                <tr>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label3" runat="server" Text="住民號碼"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="lblipno" runat="server" ForeColor="#3333CC" Visible="False"></asp:Label>
                                        <asp:Label ID="lblipno2" runat="server" ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label1" runat="server" Text="住民姓名"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="lblipname" runat="server" ForeColor="#3333CC"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label9" runat="server" Text="性別"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="lblipsex" runat="server" ForeColor="#3333CC"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label4" runat="server" Text="身分證號"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="lblipid" runat="server" ForeColor="#3333CC"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label5" runat="server" Text="出生年月日"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="lblipbirth" runat="server" ForeColor="#3333CC"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label38" runat="server">預約日期</asp:Label>
                                        <span class="style18">*</span>
                                    </td>
                                    <td class="style10">
                                        <asp:TextBox ID="TextBox3" runat="server" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBox3_TextBoxWatermarkExtender" runat="server"
                                            TargetControlID="TextBox3" WatermarkCssClass="Watermark" WatermarkText="輸入格式yyyy/MM/dd">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="TextBox3_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="TextBox3" Format="yyyy/MM/dd">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label17" runat="server" Text="預期入住日期"></asp:Label>
                                        <span class="style18">*</span>
                                    </td>
                                    <td class="style10">
                                        <asp:TextBox ID="txtexindate" runat="server" OnTextChanged="txtexindate_TextChanged"
                                            MaxLength="10"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="txtexindate_TextBoxWatermarkExtender" runat="server"
                                            TargetControlID="txtexindate" WatermarkCssClass="Watermark" WatermarkText="輸入格式yyyy/MM/dd">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="txtexindate_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txtexindate" Format="yyyy/MM/dd">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label19" runat="server" Text="備註"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtExpectNote" runat="server" Height="40px" TextMode="MultiLine"
                                            Width="98%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <asp:Button ID="btnConfirm" runat="server" Text="確認" OnClick="btnConfirm_Click" />
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="返回查詢" />
                        <asp:Label ID="Label20" runat="server" ForeColor="Red" Font-Bold="False" Visible="False"></asp:Label>
                        <asp:Label ID="lblWARNINGP_DATE" runat="server" Font-Bold="False" ForeColor="Red"
                            Visible="False"></asp:Label>
                        <br />
                        <br />
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <p style="font-size: medium; color: #000000">
                                    ★<asp:Label ID="Label42" runat="server" Text="請自以下表單選取潛在住民"></asp:Label>
                                </p>
                                <asp:GridView ID="gvwPotentialList" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                    DataKeyNames="NO,IP_NAME" DataSourceID="SqlPotentialList" PageSize="20" BackColor="White"
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%" EmptyDataText="目前無訪客紀錄！"
                                    ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvwPotentialList_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:Button ID="Button3" runat="server" CausesValidation="false" CommandName="Select"
                                                    Text="選擇" /></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NO" HeaderText="預約流水號" SortExpression="NO" ReadOnly="True"
                                            Visible="False" />
                                        <asp:BoundField DataField="IP_NAME" HeaderText="姓名" SortExpression="IP_NAME" />
                                        <asp:BoundField DataField="IP_ID" HeaderText="身分證號" SortExpression="IP_ID" />
                                        <asp:TemplateField HeaderText="性別" SortExpression="SEX">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%#  ConvertSex(Convert.ToString(DataBinder.Eval(Container.DataItem,"SEX")))  %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="生日" SortExpression="DOB">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%#  ConvertDate(Convert.ToString(DataBinder.Eval(Container.DataItem,"DOB")))  %>'>

                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="預期入住日期" SortExpression="EX_DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%#  ConvertDate(Convert.ToString(DataBinder.Eval(Container.DataItem,"EX_DATE")))  %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlPotentialList" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                    SelectCommand="SELECT NO, IP_NAME, IP_ID, SEX, DOB, EX_DATE FROM POTENTIAL_IP_INFO WHERE ((IN_CHECK IS NULL) OR (IN_CHECK = 'N')) AND (AccountEnable = 'Y')">
                                </asp:SqlDataSource>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                        <p style="font-size: medium; color: #000000">
                            ★<asp:Label ID="Label43" runat="server" Text="潛在住民預約入住"></asp:Label>
                        </p>
                        <br />
                        <asp:Label ID="Label12" runat="server" Text="* 表示必填欄位" Font-Bold="True" ForeColor="Red"></asp:Label>
                        <asp:Panel ID="Panel2" runat="server">
                            <table border="1" width="100%">
                                <tr>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label32" runat="server" Text="身分證號"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="lblpoipid" runat="server" ForeColor="#3333CC"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label30" runat="server" Text="住民姓名"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="lblpoipname" runat="server" ForeColor="#3333CC"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label31" runat="server" Text="性別"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="lblopipsex" runat="server" ForeColor="#3333CC"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label33" runat="server" Text="出生年月日"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="lblpoipbirth" runat="server" ForeColor="#3333CC"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label29" runat="server" Text="欲入住原因"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="lblipinreason" runat="server" ForeColor="#3333CC"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label10" runat="server">預約日期</asp:Label>
                                        <span class="style18">*</span>
                                    </td>
                                    <td class="style10">
                                        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBox1_TextBoxWatermarkExtender" runat="server"
                                            TargetControlID="TextBox1" WatermarkCssClass="Watermark" WatermarkText="輸入格式yyyy/MM/dd">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="TextBox1" Format="yyyy/MM/dd">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label34" runat="server" Text="預期入住日期"></asp:Label>
                                        <span class="style18">*</span>
                                    </td>
                                    <td class="style10">
                                        <asp:TextBox ID="txtexpindate" runat="server" MaxLength="10" OnTextChanged="txtexpindate_TextChanged"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="txtexpindate_TextBoxWatermarkExtender" runat="server"
                                            TargetControlID="txtexpindate" WatermarkCssClass="Watermark" WatermarkText="輸入格式yyyy/MM/dd">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="txtexpindate_CalendarExtender" runat="server" Enabled="True"
                                            Format="yyyy/MM/dd" TargetControlID="txtexpindate">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td bgcolor="#FFFFCC" class="style8">
                                        <asp:Label ID="Label35" runat="server" Text="備註"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtpoipinNote" runat="server" Height="40px" TextMode="MultiLine"
                                            Width="98%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <br />
                        <asp:Button ID="Button4" runat="server" Text="儲存" OnClick="Button4_Click" />
                        <asp:Button ID="Button5" runat="server" Text="返回查詢" OnClick="Button5_Click" />
                        <asp:Label ID="Label37" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                        <asp:Label ID="lblWARNING_DATE" runat="server" Font-Bold="False" ForeColor="Red"
                            Visible="False"></asp:Label>
                        <br />
                        <br />
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
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
    <asp:UpdatePanel ID="UpdatePanelErrMsg" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../../Scripts/jquery-ui-1.8.24.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-2.0.3.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript">
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

    </script>
</asp:Content>
