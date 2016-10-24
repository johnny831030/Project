<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NursingRecordSimple_AD.aspx.cs" Inherits="longtermcare.NursingRecordSimple.NursingRecordSimple_AD" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../../Scripts/jquery-2.0.3.js" type="text/javascript"></script>
     <script src="../../Scripts/jquery.blockUI.js" type="text/javascript"></script>
     <script src="../../Scripts/jquery-ui-1.8.24.js" type="text/javascript"></script>
    <script src="../../js/lightbox-2.6.min.js" type="text/javascript"></script>
    <link href="../../css/lightbox.css" rel="stylesheet" />
     <style type="text/css">
        .twoColFixLtHdr
        {
            text-align: center;
        }

         .style5
        {
            width: 90%;
        }
        
        .style6
        {
            color:Black;
            
        }
        
        .styleNC
        {
            font-weight: bold;
            color:Red;
        }
        
        .styleNT
        {
            font-weight: bold;
            color:Blue;
        }
        
        .styleNP
        {
            font-weight: bold;
            color:Green;
        }
        
        .toggler
        {
            position: fixed;
            top: 0px;
            left: 20px;
            width: 200px;
            height: 15px;
        }
        #effect
        {
            top: 0px;
            left: 20px;
            width: 200px;
            height: 15px;
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
            border:5px solid #EEE;
            padding:5px;
            width:100%;
            margin-left:0px;
            z-index:2;
            margin-top:0px;
        }
        .accordion_headings
        {
            padding:5px;
            background:#99CC00;
            color:#FFFFFF;
            border:1px solid #FFF;
            cursor:pointer;
            font-weight:bold;
        }
        .accordion_headings:hover
        {
            background:#00CCFF;
        }
        .accordion_child
        {
            padding:10px;
            background:#EEE;
        }
        .header_highlight
        {
            padding:5px;
            background:#00CCFF;
            color:#FFFFFF;
            font-weight:bold;
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
            font-size:4px;  
         }
                
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="Add">
        <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelMsg" runat="server">
        <ContentTemplate>
                           &nbsp;
                           <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                               onclientclick="return showWaitPanel_PreNRecordS();" Text="顯示最近護理紀錄" 
                               Height="24px" />
                           &nbsp;<asp:Label ID="tab_state_NRSAD" runat="server" 
                               Visible="False"></asp:Label>
                               <br />
                               <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                      <Triggers>
                                                          <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
                                                      </Triggers>
                                                  </asp:UpdatePanel>
                          <br />
                          <asp:Panel ID="PanelData" runat="server">
                              <table border="1" cellpadding="1" cellspacing="1" style="width: 869px">
                               <tr>
                                   <td bgcolor="#FFFFCC" align="center" class="style6">
                                      <asp:Label ID="Label2" runat="server" Text="紀錄日期" ForeColor="Black"></asp:Label>
                                   </td>
                                   <td class="style5">
                                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                   <ContentTemplate>
                                      <asp:TextBox ID="txtR_Date" runat="server" 
                                           Style="margin-left: 0px" AutoPostBack="True" 
                                           ontextchanged="txtR_Date_TextChanged"></asp:TextBox>
                                      <asp:FilteredTextBoxExtender ID="txtR_Date_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtR_Date"></asp:FilteredTextBoxExtender>
                                      <asp:CalendarExtender ID="txtR_Date_CalendarExtender" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="txtR_Date"></asp:CalendarExtender>
                                   </ContentTemplate>
                                   </asp:UpdatePanel>
                                   </td>
                              </tr>
                              <tr>
                                   <td bgcolor="#FFFFCC" align="center" class="style6">
                                      <asp:Label ID="Label3" runat="server" Text="紀錄時間" ForeColor="Black"></asp:Label>
                                   </td>
                                   <td class="style5">
                                      <asp:TextBox ID="txtR_Time" runat="server" AutoPostBack="True" 
                                           ontextchanged="txtR_Time_TextChanged"></asp:TextBox>
                                   </td>
                             </tr>
                             <tr>
                                   <td bgcolor="#FFFFCC" align="center" class="style6">
                                      <asp:Label ID="Label4" runat="server" Text="個案狀況及處置" ForeColor="Black" 
                                           Width="60px"></asp:Label>
                                   </td>
                                   <td class="style5">
                                      <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="98%" 
                                           Height="85px"></asp:TextBox>
                                       &nbsp;<asp:ImageButton ID="ibtnContent" runat="server" 
                                           ImageUrl="~/Image/WebImage/gif_45_069.gif" onclick="ibtnContent_Click" 
                                           onclientclick="return showWaitPanel_Content();" Width="16px" 
                                           Visible="False" />
                                            &nbsp;<asp:Button ID="btnSymbolsTools" runat="server" 
                                           onclick="btnSymbolsTools_Click" Text="符號表" /><br/>
                                       <asp:Panel ID="PanelSymbolsTools" runat="server" Visible="False">
                                           <asp:Accordion ID="Accordion1" HeaderCssClass="accordion_headings" HeaderSelectedCssClass="header_highlight" ContentCssClass="accordion_child" runat="server" SelectedIndex="0" FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="true" AutoSize="None">
                                             <Panes>
                                                   <asp:AccordionPane ID="AccPan1" runat="server">
                                                       <Header>常用符號</Header>
                                                       <Content>
                                                          <table style="width: 95%;">
                                                              <tr>
                                                                  <td><input type="button" name="btn_AccPan1_1" value="，" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_2" value="、" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_3" value="。" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_4" value="？" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_5" value="！" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_6" value="：" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_7" value="；" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_8" value="＜" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_9" value="＞" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_10" value="＝" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_11" value="≠" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_12" value="≒" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_13" value="≦" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_14" value="≧" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_15" value="％" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              </tr>
                                                              <tr>
                                                                  <td><input type="button" name="btn_AccPan1_16" value="‰" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_17" value="℃" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_18" value="㎎" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_19" value="㎏" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_20" value="㎝" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_21" value="㎜" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_22" value="ml" onClick="set_symbols(this.value)" class="btnsym1"></td>
                                                                  <td><input type="button" name="btn_AccPan1_23" value="㏄" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan1_24" value="mmHg" onClick="set_symbols(this.value)" class="btnsym1"></td>
                                                                  <td></td>
                                                                  <td></td>
                                                                  <td></td>
                                                                  <td></td>
                                                                  <td></td>
                                                                  <td></td>
                                                              </tr>
                                                          </table>
                                                       </Content>
                                                   </asp:AccordionPane>
                                                   <asp:AccordionPane ID="AccPan2" runat="server">
                                                   <Header>基本符號</Header>
                                                       <Content>
                                                          <table style="width: 95%;">
                                                              <tr>
                                                                  <td><input type="button" name="btn_AccPan2_1" value="，" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_2" value="、" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_3" value="。" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_4" value="．" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_5" value="？" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_6" value="！" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_7" value="﹕" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_8" value="﹔" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_9" value="『" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_10" value="』" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_11" value="「" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_12" value="」" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_13" value="｛" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_14" value="｝" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_15" value="〔" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              </tr>
                                                              <tr>
                                                                  <td><input type="button" name="btn_AccPan2_16" value="〕" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_17" value="（" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_18" value="）" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_19" value="＜" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_20" value="＞" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_21" value="‘" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_22" value="’" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_23" value="“" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_24" value="”" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_25" value="〝" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_26" value="〞" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_27" value="‵" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_28" value="′" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_29" value="＠" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_30" value="＆" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              </tr>
                                                              <tr>
                                                                  <td><input type="button" name="btn_AccPan2_31" value="＃" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_32" value="＊" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_33" value="…" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_34" value="‥" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_35" value="＄" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_36" value="～" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_37" value="─" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                                  <td><input type="button" name="btn_AccPan2_38" value="│" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              </tr>
                                                          </table>
                                                       </Content>
                                                   </asp:AccordionPane>
                                                   <asp:AccordionPane ID="AccPan3" runat="server">
                                                   <Header>單位符號</Header>
                                                       <Content>
                                                           <table style="width: 95%;">
                                                             <tr>
                                                              <td><input type="button" name="btn_AccPan3_1" value="℃" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              <td><input type="button" name="btn_AccPan3_2" value="℉" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              <td><input type="button" name="btn_AccPan3_3" value="㎎" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              <td><input type="button" name="btn_AccPan3_4" value="㎏" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              <td><input type="button" name="btn_AccPan3_5" value="㎝" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              <td><input type="button" name="btn_AccPan3_6" value="㎜" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              <td><input type="button" name="btn_AccPan3_7" value="㎡" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              <td><input type="button" name="btn_AccPan3_8" value="㎞" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              <td><input type="button" name="btn_AccPan3_9" value="ml" onClick="set_symbols(this.value)" class="btnsym1"></td>
                                                              <td><input type="button" name="btn_AccPan3_10" value="㏄" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              <td><input type="button" name="btn_AccPan3_11" value="㏕" onClick="set_symbols(this.value)" class="btnsym"></td>
                                                              <td><input type="button" name="btn_AccPan3_12" value="mmHg" onClick="set_symbols(this.value)" class="btnsym1"></td>
                                                             </tr>
                                                          </table>
                                                       </Content>
                                                   </asp:AccordionPane>
                                           </Panes>
                                           </asp:Accordion>
                                           <br />
                                       </asp:Panel>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                      <Triggers>
                                                          <asp:AsyncPostBackTrigger ControlID="btnSymbolsTools" EventName="Click" />
                                                          <asp:AsyncPostBackTrigger ControlID="ibtnContent" EventName="Click" />
                                                      </Triggers>
                                                  </asp:UpdatePanel>
                                   </td>
                             </tr>
                        </table>
                          </asp:Panel>
                           <br />
                           &nbsp;<asp:Button ID="Add" runat="server" OnClick="Add_Click" style="height: 24px" 
                               Text="儲存" Enabled="False"/>
                                
                           &nbsp;&nbsp;<asp:Button ID="btnPrint" runat="server" 
                               OnClick="btnPrint_Click" Text="列印" Height="24px" 
                               UseSubmitBehavior="False" Visible="False" />
                           &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" 
                               Text="清除" Height="24px" UseSubmitBehavior="False" />
                           &nbsp;&nbsp;<asp:Label ID="lblWarning" runat="server" ForeColor="Red"></asp:Label>
                           <asp:Label ID="lblPrintReviseDate" runat="server" Text="102.12修訂" 
                               Visible="False"></asp:Label>
                           <br />
                           <br />
                           <asp:Panel ID="PanelData1" runat="server">
                                &nbsp;&nbsp;<asp:Label ID="Label147" runat="server" Font-Bold="True" ForeColor="Black" 
                               Text="請選擇這筆護理紀錄的後續處理活動:"></asp:Label>
                           <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" ForeColor="Black" 
                               onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                               <asp:ListItem Value="0">只將這筆護理紀錄帶入<span class="styleNC">護理交班</span></asp:ListItem>
                               <asp:ListItem Value="3">只將這筆護理紀錄帶入<span class="styleNT">照會單</span></asp:ListItem>
                               <asp:ListItem Value="4">將這筆護理紀錄帶入<span class="styleNC">護理交班</span>與<span class="styleNT">照會單</span></asp:ListItem>
                           </asp:RadioButtonList>
                           <br />
                           &nbsp;<asp:Button ID="btnNExchange" runat="server" 
                               onclick="btnNExchange_Click" Text="帶入護理交班" Height="24px" 
                                    UseSubmitBehavior="False" />
                           &nbsp;<asp:Label ID="lbladdShiftExchange" runat="server" Text="N" 
                               Visible="False"></asp:Label>
                           &nbsp;<asp:Button ID="btnInsertIntoNP" runat="server" 
                                           onclick="btnInsertIntoNP_Click" Text="帶入照護計畫" Height="24px" 
                                    UseSubmitBehavior="False" />
                                       &nbsp;
                                <asp:Button ID="btnInsertIntoCT" runat="server" Height="24px" 
                                    onclick="btnInsertIntoCT_Click" Text="帶入照護會診單" UseSubmitBehavior="False" />
                                       <br />
                                       <asp:Panel ID="Panel5" runat="server" Width="99%" Visible="False">
                                           <asp:TextBox ID="txtNurseExchangeTemp" runat="server" Font-Size="Small" 
                                               Height="150px" TextMode="MultiLine" Visible="True" Width="99%"></asp:TextBox>
                                           <br />
                                           <br />
                                           &nbsp;<asp:Button ID="btnInsertNE" runat="server" onclick="btnInsertNE_Click" 
                                               Text="儲存護理交班" Visible="True" Height="24px" UseSubmitBehavior="False" />
                                               
                                           &nbsp;<asp:Button ID="btnNurseExchangeClear" runat="server" 
                                               onclick="btnNurseExchangeClear_Click" Text="放棄護理交班" Height="24px" 
                                               UseSubmitBehavior="False" /><br />
                                               <br />
                                       </asp:Panel>
                           </asp:Panel>
                           </ContentTemplate>
        <Triggers>
             <asp:PostBackTrigger ControlID="btnPrint" />
         </Triggers>
    </asp:UpdatePanel> 
                       
    </div>
        <div id="prenrecord" runat="server" style="display:none; cursor:default;">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                         <ContentTemplate>
                             <table style="width: 100%;">
                                 <tr align="left">
                                    <td>&nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Black" Text="最近護理紀錄(簡單版)瀏覽:" 
                                            Font-Size="Medium"></asp:Label></td>
                                 </tr>
                                 <tr align="left">
                                    <td>
                                         <asp:Label ID="lblShowNoSNRecord" runat="server" ForeColor="Red" Font-Size ="Larger" Visible="False" Font-Bold="True"></asp:Label>
                                         <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:TabContainer ID="TabContainerRecord" runat="server" ActiveTabIndex="0" Width="800px">
                                                     <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="" Visible="false"></asp:TabPanel>
                                                </asp:TabContainer>
                                            </td>
                                        </tr>
                                    </table>
                                    </td>
                                 </tr>
                                 <tr align="left">  
                                    <td>&nbsp;<asp:Button ID="btnPreNRecordS" runat="server" Text="確定" OnClientClick="return Exit();"/></td>
                                 </tr>
                             </table>
                         </ContentTemplate>
                    </asp:UpdatePanel>
               </div>
        <div id="phrase" runat="server" style="display:none; cursor: default;">
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                          <ContentTemplate>
                    <table style="width: 100%;">
                        <tr align="center">
                            <td align="center"> <asp:Label ID="lblPhraseTitle" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="Medium" Font-Names="新細明體" Text="護理紀錄(簡單版)常用片語"></asp:Label></td>
                            <td align="right">
                                <asp:ImageButton ID="ImageButton2" runat="server" Height="32px" ImageUrl="~/Image/WebImage/633855842283694909.jpg"
                                    Width="31px" OnClientClick="return Exit();" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel3" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TabContainer ID="TabContainerPHRASE" runat="server" ActiveTabIndex="0" Width="100%">
                                                     <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="" Visible="false"></asp:TabPanel>
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
        <div class="toggler">
            <div id="effect" class="ui-widget-content">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
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
        <script type="text/javascript">
            var CaretPos;
            $(function () {
                $().ajaxStart(
            function () {
                setTimeout($.blockUI, 0);
            }).ajaxStop($.unblockUI);
            });

            function showWaitPanel_PreNRecordS() {
                var id = '<%=this.prenrecord.ClientID %>';
                $.blockUI({ message: $(document.getElementById(id)), css: { width: '850px', position: 'fixed', top: '20px', left: '40px'} });
                document.getElementById(id11).style.display = '';
            }

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

     </script>
    </form>
</body>
</html>
