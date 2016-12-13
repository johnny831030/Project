<%@ Page Language="C#" MasterPageFile="~/IPMangement.Master" AutoEventWireup="true"
    CodeBehind="IP_Basic_A.aspx.cs" Inherits="longtermcare.IPBasic.IP_Basic_A" EnableEventValidation="false"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var habitchoice;
        document.onkeydown = function () {
            if (window.event)
                if (event.keyCode == 13)
                    event.keyCode = 9;
        }
        function ShowSubWin(url) {
            var frmHtml = "<iframe style='width: 100%; height:100%;' src='" + url + "'></iframe>";
            $.blockUI({
                css: { width: '35%', height: '36%', top: '33%', left: '30%' },
                message: frmHtml
            });
        }

        function setYearRange() {
            $find("calBehavior").set_selectedDate(new Date(1955, 1, 1));
        }
        //radStat_Infc_HXY
        function Stat_InfcDivY() {
            var radios = document.getElementsByName('radStat_Infc_HXY');
            if (radios.checked) {
                document.getElementById('<%=this.Div2.ClientID %>').style["display"] = "none";

            }
            else {
                document.getElementById('<%=this.Div2.ClientID %>').style["display"] = "block";
                document.getElementById('<%=this.TextBox5.ClientID %>').disabled = false;
            }
        }
        //radStat_Infc_HXY
        function Stat_InfcDivN() {
            var radios = document.getElementsByName('radStat_Infc_HXN');
            if (radios.checked) {
                document.getElementById('<%=this.Div2.ClientID %>').style["display"] = "block";
                document.getElementById('<%=this.TextBox5.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=this.Div2.ClientID %>').style["display"] = "none";
            }
        }
        //radAfter_Adms_HXY
        function After_AdmsDivY() {
            var radios = document.getElementsByName('radAfter_Adms_HXY');
            if (radios.checked) {
                document.getElementById('<%=this.Div1.ClientID %>').style["display"] = "none";
            }
            else {
                document.getElementById('<%=this.TextBox6.ClientID %>').disabled = false;
                document.getElementById('<%=this.Div1.ClientID %>').style["display"] = "block";
            }
        }
        //radAfter_Adms_HXN
        function After_AdmsDivN() {
            var radios = document.getElementsByName('radAfter_Adms_HXN');
            if (radios.checked) {
                document.getElementById('<%=this.TextBox6.ClientID %>').disabled = false;
                document.getElementById('<%=this.Div1.ClientID %>').style["display"] = "block";
            }
            else {
                document.getElementById('<%=this.Div1.ClientID %>').style["display"] = "none";
            }
        }

        var relation;
        $(function () {
            $().ajaxStart(
            function () {
                setTimeout($.blockUI, 0);
            }).ajaxStop($.unblockUI);
        });

        //片語
        function showhabitPanel(e) {
            switch (e) {
                case '0':
                    habitchoice = 'txtfavorbreak';
                    break;
                case '1':
                    habitchoice = 'txtfavorlunch';
                    break;
                case '2':
                    habitchoice = 'txtfavordinner';
                    break;
                case '3':
                    habitchoice = 'txtforbid';
                    break;
                case '4':
                    habitchoice = 'txtspecorder';
                    break;
                case '5':
                    habitchoice = 'txtmealbreak';
                    break;
                case '6':
                    habitchoice = 'txtmealsnack';
                    break;
                case '7':
                    habitchoice = 'txtmeallunch';
                    break;
                case '8':
                    habitchoice = 'txtmealtea';
                    break;
                case '9':
                    habitchoice = 'txtmealdinner';
                    break;
                case '10':
                    habitchoice = 'txtmealmnsnack';
                    break;
            }
            var id = '<%=this.phrase.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '700px', position: 'fixed', top: '20px', left: '40px' } });
            // ContentPlaceHolder1_TabIPInfo_TabPanel2_ 
            var id = 'ContentPlaceHolder1_TabIPInfo_TabPanel5_' + habitchoice;
            if (document.selection) {
                document.getElementById(id).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id).value.length);
                relation = Sel.text.length;
            }
                // Firefox support
            else if (document.getElementById(id).selectionStart || document.getElementById(id).selectionStart == '0') {
                relation = document.getElementById(id).selectionStart;
            }

        }

        function showIPBasicPanel() {
            var id = '<%=this.phrase.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '700px', position: 'fixed', top: '20px', left: '40px' } });
            var id = '<%=this.TextBox6.ClientID %>';
            if (document.selection) {
                document.getElementById(id).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id).value.length);
                relation = Sel.text.length;
            }
                // Firefox support
            else if (document.getElementById(id).selectionStart || document.getElementById(id).selectionStart == '0') {
                relation = document.getElementById(id).selectionStart;
            }
        }

        function showSignRelationPanel() {
            var id = '<%=this.phrase.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '700px', position: 'fixed', top: '20px', left: '40px' } });
            var id = '<%=this.txtSignRelate.ClientID %>';
            if (document.selection) {
                document.getElementById(id).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id).value.length);
                relation = Sel.text.length;
            }
                // Firefox support
            else if (document.getElementById(id).selectionStart || document.getElementById(id).selectionStart == '0') {
                relation = document.getElementById(id).selectionStart;
            }
        }

        function showReRelationPanel() {
            var id = '<%=this.phrase.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '700px', position: 'fixed', top: '20px', left: '40px' } });
            var id = '<%=this.txtRERelate.ClientID %>';
            if (document.selection) {
                document.getElementById(id).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id).value.length);
                relation = Sel.text.length;
            }
                // Firefox support
            else if (document.getElementById(id).selectionStart || document.getElementById(id).selectionStart == '0') {
                relation = document.getElementById(id).selectionStart;
            }
        }

        function showReRelationPanel_0() {
            var id = '<%=this.phrase.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '700px', position: 'fixed', top: '20px', left: '40px' } });
            var id = '<%=this.txtRERelate0.ClientID %>';
            if (document.selection) {
                document.getElementById(id).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id).value.length);
                relation = Sel.text.length;
            }
                // Firefox support
            else if (document.getElementById(id).selectionStart || document.getElementById(id).selectionStart == '0') {
                relation = document.getElementById(id).selectionStart;
            }
        }

        function showReRelationPanel_1() {
            var id = '<%=this.phrase.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '700px', position: 'fixed', top: '20px', left: '40px' } });
            var id = '<%=this.txtRERelate1.ClientID %>';
            if (document.selection) {
                document.getElementById(id).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id).value.length);
                relation = Sel.text.length;
            }
                // Firefox support
            else if (document.getElementById(id).selectionStart || document.getElementById(id).selectionStart == '0') {
                relation = document.getElementById(id).selectionStart;
            }
        }
        function showReRelationPanel_2() {
            var id = '<%=this.phrase.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '700px', position: 'fixed', top: '20px', left: '40px' } });
            var id = '<%=this.txtRERelate_Re3.ClientID %>';
            if (document.selection) {
                document.getElementById(id).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id).value.length);
                relation = Sel.text.length;
            }
                // Firefox support
            else if (document.getElementById(id).selectionStart || document.getElementById(id).selectionStart == '0') {
                relation = document.getElementById(id).selectionStart;
            }
        }

        function showPAY() {
            var id = '<%=this.phrase.ClientID %>';
            $.blockUI({ message: $(document.getElementById(id)), css: { width: '700px', position: 'fixed', top: '20px', left: '40px' } });
            var id = '<%=this.txtPatEvalute.ClientID %>';
            if (document.selection) {
                document.getElementById(id).focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -document.getElementById(id).value.length);
                relation = Sel.text.length;
            }
                // Firefox support
            else if (document.getElementById(id).selectionStart || document.getElementById(id).selectionStart == '0') {
                relation = document.getElementById(id).selectionStart;
            }
        }

        function phrase_IPBasic() {
            var id = '<%=this.btnSENT_IPB.ClientID%>';
            var id1 = '<%=this.TextBox6.ClientID %>';
            //宣告一個字串$('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            var text = '';
            //document.getElementById(id1).value = "";
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id1).value == "" && text == "")
                    text = $(this).val();
                else
                    text += " " + $(this).val();
            });
            //跑完迴圈將值塞進TextBox中
            var b = document.getElementById(id1).value;
            document.getElementById(id1).value = b.substr(0, relation) + text + b.substr(relation, b.length);
            //document.getElementById(id1).value = text;
            document.getElementById(id1).value = document.getElementById(id1).value.split(" ");

            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            $.unblockUI();
        }

        function phrase_habit() {
            var id = '<%=this.btnSENT_habit.ClientID%>';
            var id1 = 'ContentPlaceHolder1_TabIPInfo_TabPanel5_' + habitchoice;
            //宣告一個字串$('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            var text = '';
            document.getElementById(id1).value = "";
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id1).value == "" && text == "")
                    text = $(this).val();
                else
                    text += " " + $(this).val();
            });
            //跑完迴圈將值塞進TextBox中
            var b = document.getElementById(id1).value;
            document.getElementById(id1).value = b.substr(relation, b.length) + text + b.substr(0, relation);
            document.getElementById(id1).value = text;
            document.getElementById(id1).value = document.getElementById(id1).value.split(" ");

            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            $.unblockUI();

        }

        function phrase_SignRelation() {
            var id = '<%=this.btnSENT_Sign.ClientID%>';
            var id1 = '<%=this.txtSignRelate.ClientID %>';
            //宣告一個字串$('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            var text = '';
            document.getElementById(id1).value = "";
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id1).value == "" && text == "")
                    text = $(this).val();
                else
                    text += " " + $(this).val();
            });
            //跑完迴圈將值塞進TextBox中
            var b = document.getElementById(id1).value;
            document.getElementById(id1).value = b.substr(relation, b.length) + text + b.substr(0, relation);
            document.getElementById(id1).value = text;
            document.getElementById(id1).value = document.getElementById(id1).value.split(" ");

            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            $.unblockUI();
        }

        function phrase_ReRelation() {
            var id = '<%=this.btnSENT_Re.ClientID%>';
            var id1 = '<%=this.txtRERelate.ClientID %>';
            //宣告一個字串$('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            var text = '';
            document.getElementById(id1).value = "";
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id1).value == "" && text == "")
                    text = $(this).val();
                else
                    text += " " + $(this).val();
            });
            //跑完迴圈將值塞進TextBox中
            var b = document.getElementById(id1).value;
            document.getElementById(id1).value = b.substr(relation, b.length) + text + b.substr(0, relation);
            document.getElementById(id1).value = text;
            document.getElementById(id1).value = document.getElementById(id1).value.split(" ");

            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            $.unblockUI();
        }

        function phrase_ReRelation_0() {
            var id = '<%=this.btnSENT_Re0.ClientID%>';
            var id1 = '<%=this.txtRERelate0.ClientID %>';
            //宣告一個字串$('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            var text = '';
            document.getElementById(id1).value = "";
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id1).value == "" && text == "")
                    text = $(this).val();
                else
                    text += " " + $(this).val();
            });
            //跑完迴圈將值塞進TextBox中
            var b = document.getElementById(id1).value;
            document.getElementById(id1).value = b.substr(relation, b.length) + text + b.substr(0, relation);
            document.getElementById(id1).value = text;
            document.getElementById(id1).value = document.getElementById(id1).value.split(" ");

            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            $.unblockUI();
        }

        function phrase_ReRelation_1() {
            var id = '<%=this.btnSENT_Re1.ClientID%>';
            var id1 = '<%=this.txtRERelate1.ClientID %>';
            //宣告一個字串$('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            var text = '';
            document.getElementById(id1).value = "";
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id1).value == "" && text == "")
                    text = $(this).val();
                else
                    text += " " + $(this).val();
            });
            //跑完迴圈將值塞進TextBox中
            var b = document.getElementById(id1).value;
            document.getElementById(id1).value = b.substr(relation, b.length) + text + b.substr(0, relation);
            document.getElementById(id1).value = text;
            document.getElementById(id1).value = document.getElementById(id1).value.split(" ");

            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            $.unblockUI();
        }

        function phrase_ReRelation_2() {
            var id = '<%=this.btnSENT_Re2.ClientID%>';
            var id1 = '<%=this.txtRERelate_Re3.ClientID %>';
            //宣告一個字串$('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            var text = '';
            document.getElementById(id1).value = "";
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id1).value == "" && text == "")
                    text = $(this).val();
                else
                    text += " " + $(this).val();
            });
            //跑完迴圈將值塞進TextBox中
            var b = document.getElementById(id1).value;
            document.getElementById(id1).value = b.substr(relation, b.length) + text + b.substr(0, relation);
            document.getElementById(id1).value = text;
            document.getElementById(id1).value = document.getElementById(id1).value.split(" ");

            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            $.unblockUI();
        }

        function phrase_PAY() {
            var id = '<%=this.btnSENT_PAY.ClientID%>';
            var id1 = '<%=this.txtPatEvalute.ClientID %>';
            //宣告一個字串$('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            var text = '';
            document.getElementById(id1).value = "";
            //選擇Checkbox選擇的值跑迴圈
            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').each(function () {
                //字串=字串＋值
                if (document.getElementById(id1).value == "" && text == "")
                    text = $(this).val();
                else
                    text += " " + $(this).val();
            });
            //跑完迴圈將值塞進TextBox中
            var b = document.getElementById(id1).value;
            document.getElementById(id1).value = b.substr(relation, b.length) + text + b.substr(0, relation);
            document.getElementById(id1).value = text;
            document.getElementById(id1).value = document.getElementById(id1).value.split(" ");

            $('[id^="ContentPlaceHolder1_TabContainerPHRASE"]:checked').attr('checked', false);
            $.unblockUI();
        }

        function Exit() {
            $.unblockUI();
        }

        //控制只能輸入數字
        function TextBoxNumCheck_Int() {
            if (event.keyCode < 48 || window.event.keyCode > 57) event.returnValue = false;
        }

        //預覽照片
        function previewFile() {
            var preview = document.querySelector('#<%=Preview.ClientID %>');
            var file = document.querySelector('#<%=fupIPPic.ClientID %>').files[0];
            var reader = new FileReader();
            $('#<%=Preview.ClientID%>').attr('style', 'display:');
            //document.getElementById('Preview').style.visibility = "visible"; 
            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }

        //        function buttonGroupChange() {
        //            var radioElements = document.getElementsByName("stat");
        //            for (var i = 0; i < radioElements.length; i++) {
        //                if (radioElements[0].checked == true) {
        //                    $('#<%=TextBox5.ClientID%>').attr('Enable', 'true');
        //                }
        //                else {
        //                    $('#<%=TextBox5.ClientID%>').attr('Enable', 'false');
        //                }
        //            }
        //        }

        //**************************************
        // 台灣身份證檢查簡短版 for Javascript
        //**************************************

        function checkTwID(idin) {
            var id = idin.value;
            var MsgErrID = '<%=this.lblShowErr.ClientID %>';
            //建立字母分數陣列(A~Z)
            if (id != "") {
                var city = new Array(1, 10, 19, 28, 37, 46, 55, 64, 39, 73, 82, 2, 11, 20, 48, 29, 38, 47, 56, 65, 74, 83, 21, 3, 12, 30)
                id = id.toUpperCase();
                // 使用「正規表達式」檢驗格式
                if (id.search(/^[A-Z](1|2)\d{8}$/i) == -1) {
                    document.getElementById(MsgErrID).innerHTML = "基本格式錯誤";
                    runEffect1();
                } else {
                    //將字串分割為陣列(IE必需這麼做才不會出錯)
                    id = id.split('');
                    //計算總分   
                    var total = city[id[0].charCodeAt(0) - 65];
                    for (var i = 1; i <= 8; i++) {
                        total += eval(id[i]) * (9 - i);
                    }
                    //補上檢查碼(最後一碼)
                    total += eval(id[9]);
                    //檢查比對碼(餘數應為0);
                    if (total % 10 != 0) {
                        document.getElementById(MsgErrID).innerHTML = "身分證輸入錯誤，請再核對！";
                        runEffect1();
                    }
                    //辨識性別
                    if (id[1] == 1) {
                        $('#<%= rbSex.ClientID%>').find("input[value='1']").prop("checked", true);
                    } else {
                        $('#<%= rbSex.ClientID%>').find("input[value='2']").prop("checked", true);
                    }
                }
            }
            else {
                //document.getElementById(MsgErrID).innerHTML = "身分證是必填資料！請輸入。";
                //runEffect1();
            }
        }
        //判斷葷素，啟用控制項
        function radvege_Check() {
            var chblvege = document.getElementById('<%=this.chblvege.ClientID %>');
            var chklvege = document.getElementById('<%=this.chklvege.ClientID %>');
            var chklvege1 = document.getElementById('<%=this.chklvege1.ClientID %>');
            var txtvege = document.getElementById('<%=this.txtvege.ClientID %>');
            var txtNvege = document.getElementById('<%=this.txtNvege.ClientID %>');
            var lblNvege = document.getElementById('<%=this.lblNvege.ClientID %>');
            var chbItems = chblvege.getElementsByTagName('input');
            var chkItems = chklvege.getElementsByTagName('input');
            var chkItems1 = chklvege1.getElementsByTagName('input');

            for (var i = 0; i < chbItems.length; i++) {
                chbItems[i].removeAttribute("disabled");
            }
            for (var j = 0; j < chkItems.length; j++) {
                chkItems[j].checked = false;
                chkItems[j].disabled = true;
            }
            for (var k = 0; k < chkItems1.length; k++) {
                chkItems1[k].removeAttribute("disabled");
            }
            txtvege.removeAttribute("disabled");
            txtNvege.disabled = true;
            txtNvege.value = "";
            lblNvege.value = "";
        }
        function radNvege_Check() {
            var chblvege = document.getElementById('<%=this.chblvege.ClientID %>');
            var chklvege = document.getElementById('<%=this.chklvege.ClientID %>');
            var chklvege1 = document.getElementById('<%=this.chklvege1.ClientID %>');
            var txtvege = document.getElementById('<%=this.txtvege.ClientID %>');
            var txtNvege = document.getElementById('<%=this.txtNvege.ClientID %>');
            var lblvege = document.getElementById('<%=this.lblvege.ClientID %>');
            var chbItems = chblvege.getElementsByTagName('input');
            var chkItems = chklvege.getElementsByTagName('input');
            var chkItems1 = chklvege1.getElementsByTagName('input');

            for (var i = 0; i < chbItems.length; i++) {
                chbItems[i].checked = false;
                chbItems[i].disabled = true;
            }
            for (var j = 0; j < chkItems.length; j++) {
                chkItems[j].removeAttribute("disabled");
            }
            for (var k = 0; k < chkItems1.length; k++) {
                chkItems1[k].checked = false;
                chkItems1[k].disabled = true;
            }
            txtvege.disabled = true;
            txtvege.value = "";
            lblvege.value = "";
            txtNvege.removeAttribute("disabled");
        }
        //判斷葷素，將被選項存到TextBox
        function chklvege1_clike() {
            var lblvege = document.getElementById('<%=this.lblvege.ClientID %>');
            var chklvege1 = document.getElementById('<%=this.chklvege1.ClientID %>');
            var chkItems1 = chklvege1.getElementsByTagName('input');
            lblvege.value = "";
            for (var k = 0; k < chkItems1.length; k++) {
                if (chkItems1[k].checked == true) {
                    lblvege.value += chkItems1[k].value + ",";
                }
            }
        }
        function chklvege_clike() {
            var lblNvege = document.getElementById('<%=this.lblNvege.ClientID %>');
            var chklvege = document.getElementById('<%=this.chklvege.ClientID %>');
            var chkItems = chklvege.getElementsByTagName('input');
            lblNvege.value = "";
            for (var k = 0; k < chkItems.length; k++) {
                if (chkItems[k].checked == true) {
                    lblNvege.value += chkItems[k].value + ",";
                }
            }
        }

        //同戶籍地址
        function CopyAdd_Click(pnl1, txt1, btn1, btn2) {
            txt1.value = document.getElementById('<%=this.txtpermadr.ClientID %>').value;
            btn1.style["display"] = "none";
            txt1.style["display"] = "inline";
            pnl1.style["display"] = "none";
            btn2.style["display"] = "inline";
        }
        function ChangedAdd_Click(pnl1, txt1, btn1, btn2) {
            txt1.value = "";
            btn1.style["display"] = "inline";
            txt1.style["display"] = "none";
            pnl1.style["display"] = "inline";
            btn2.style["display"] = "none";
        }

        //本國
        function radTaiwan_CheckedChanged() {
            var pnlTaiwanadr = document.getElementById('<%=this.pnlTaiwanadr.ClientID %>');
            var pnlTaiwan = document.getElementById('<%=this.pnlTaiwan.ClientID %>');
            var pnlForeign = document.getElementById('<%=this.pnlForeign.ClientID %>');
            var pnlForeignadr = document.getElementById('<%=this.pnlForeignadr.ClientID %>');
            var txtIPID = document.getElementById('<%=this.txtIPID.ClientID %>');
            var radTaiwan = document.getElementById('<%=this.radTaiwan.ClientID %>');
            var rbSex = document.getElementById('<%=this.rbSex.ClientID %>').getElementsByTagName('input');
            pnlTaiwanadr.style["display"] = "inline";
            pnlTaiwan.style["display"] = "inline";
            txtIPID.focus();
            pnlForeign.style["display"] = "none";
            pnlForeignadr.style["display"] = "none";
            radTaiwan.focus();
            radTaiwan.style["display"] = "inline";
            rbSex[0].disabled = true;
            rbSex[1].disabled = true;
        }
        //外國
        function radForeign_CheckedChanged() {
            var pnlTaiwanadr = document.getElementById('<%=this.pnlTaiwanadr.ClientID %>');
            var pnlTaiwan = document.getElementById('<%=this.pnlTaiwan.ClientID %>');
            var pnlForeign = document.getElementById('<%=this.pnlForeign.ClientID %>');
            var pnlForeignadr = document.getElementById('<%=this.pnlForeignadr.ClientID %>');
            var txtForeign = document.getElementById('<%=this.txtForeign.ClientID %>');
            var radForeign = document.getElementById('<%=this.radForeign.ClientID %>');
            var rbSex = document.getElementById('<%=this.rbSex.ClientID %>').getElementsByTagName('input');
            pnlForeign.style["display"] = "inline";
            pnlForeignadr.style["display"] = "inline";
            pnlTaiwanadr.style["display"] = "none";
            pnlTaiwan.style["display"] = "none";
            radForeign.style["display"] = "inline";
            txtForeign.focus();
            rbSex[0].disabled = false;
            rbSex[1].disabled = false;
        }


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
            }, 3000);
        };

        $("#effect1").hide();
    </script>

    <!--BACKtoTOP-START
    <a style="display: scroll; position: fixed; bottom: 0px; right: 200px;" href="#"
        title="Back to Top" onfocus="if(this.blur)this.blur()">
        <img alt='' border='0' onmouseover="this.src='/Image/WebImage/B2T.png'" src="/Image/WebImage/B2T_medium.png"
            onmouseout="this.src='/Image/WebImage/B2T_medium.png'" /></a>
    BACKtoTOP-STOP-->
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h1>
        <asp:Label ID="LabelTitle" runat="server" Text="住民基本資料" Font-Bold="True" Font-Size="X-Large"></asp:Label>
    </h1>
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
                                ForeColor="#003300" OnClick="LinkBtnGoToAddView_Click">新增住民資料</asp:LinkButton>
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
                                OnClick="LinkBtnGoToQUView_Click">查詢/修改/刪除住民資料</asp:LinkButton>
                        </p>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="mainPanel">
        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
            <ContentTemplate>
                <div class="content">
                    <asp:Panel ID="pnlcontent" runat="server" DefaultButton="btnAddIPBasic">
                        <asp:Panel ID="pnlIPList" runat="server">
                            <br />
                            <asp:Accordion ID="Accordion1" HeaderCssClass="accordion_headings" HeaderSelectedCssClass="header_highlight" ContentCssClass="accordion_child" runat="server" SelectedIndex="0" FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40" AutoSize="None" RequireOpenedPane="false">
                                <Panes>
                                    <asp:AccordionPane ID="a1" runat="server">
                                        <Header>
                                            <asp:Label ID="Label116" runat="server" Text="最近五筆新增住民資料"></asp:Label>
                                        </Header>
                                        <Content>
                                            <asp:GridView ID="gvwIPNOInList" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                                DataKeyNames="IP_NO" DataSourceID="SqlIPNOInList" BackColor="White"
                                                BorderColor="#CCCCCC" BorderStyle="None" Width="100%" EmptyDataText="目前尚無住民紀錄"
                                                ShowHeaderWhenEmpty="True" Visible="True">
                                                <Columns>
                                                    <asp:BoundField DataField="IP_NO_NEW" HeaderText="住民編號" SortExpression="IP_NO_NEW" />
                                                    <asp:BoundField DataField="IP_NO" HeaderText="IP_NO" ReadOnly="True" SortExpression="IP_NO"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="IP_NAME" HeaderText="住民姓名" SortExpression="IP_NAME" />
                                                    <asp:BoundField DataField="IP_ID" HeaderText="身分證號" SortExpression="IP_ID" />
                                                    <asp:BoundField DataField="DOB" HeaderText="生日" SortExpression="DOB" />
                                                    <asp:BoundField DataField="ROOM_BED" HeaderText="床號" SortExpression="ROOM_BED" />
                                                    <asp:BoundField DataField="CREATE_DATE" HeaderText="新增日期" SortExpression="CREATE_DATE" />
                                                    <asp:TemplateField HeaderText="建立人員" SortExpression="CREATE_USER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# DGFormatRID3(Convert.ToString(DataBinder.Eval(Container.DataItem,"CREATE_USER"))) %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <RowStyle ForeColor="#000066" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlIPNOInList" runat="server"
                                                SelectCommand="SELECT top 5 I.IP_NO, I.IP_NO_NEW, I.IP_NAME, I.IP_ID, CONVERT(VARCHAR(10), CAST(I.DOB AS DATETIME), 111) AS DOB, ROOM.ROOM_BED, CONVERT(VARCHAR(10), CAST(I.CREATE_DATE AS DATETIME), 111) AS CREATE_DATE, I.CREATE_USER FROM IP_INFORMATION as I Left Outer Join ROOM ON I.IP_NO = ROOM.IP_NO WHERE (I.AccountEnable = 'Y') AND (ROOM.REPBED != 'N' OR ROOM.REPBED IS NULL) order by I.CREATE_DATE DESC, I.CREATE_TIME DESC">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="Y" Name="AccountEnable" Type="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                            <br />
                            <asp:Label ID="Label11" runat="server" Text="* 表示必填欄位" Font-Bold="True" ForeColor="Red"></asp:Label>
                            <table border="1" style="width: 100%; color: Black;">
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label1" runat="server" Text="住民編號"></asp:Label>
                                        <span class="style18">*</span>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="TextBoxNO" runat="server" OnTextChanged="TextBoxNO_TextChanged" TabIndex="1"
                                                    AutoPostBack="True"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:Label ID="lblIPNO" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblNO2" runat="server"></asp:Label>
                                        <asp:Label ID="Label187" runat="server" ForeColor="Red" Visible="False" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label2" runat="server" Text="住民姓名"></asp:Label>
                                        <span class="style18">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIPName" runat="server" Width="100px" TabIndex="2"></asp:TextBox>
                                    </td>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label4" runat="server" Text="出生年月日"></asp:Label>
                                        <span class="style18">*</span>
                                    </td>
                                    <td>
                                        <p>
                                            民國<asp:TextBox ID="tb_year" runat="server" Width="45px" AutoPostBack="False" MaxLength="3" TabIndex="3"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="tb_year_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers, Custom" TargetControlID="tb_year" ValidChars="0123456789">
                                            </asp:FilteredTextBoxExtender>
                                            年/
                                            <asp:DropDownList ID="ddl_month" runat="server" TabIndex="4">
                                                <asp:ListItem Value="-99">月</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                            </asp:DropDownList>
                                            月/
                                            <asp:TextBox ID="tb_day" runat="server" Width="45px" AutoPostBack="False" MaxLength="2" TabIndex="5"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="tb_day_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers, Custom" TargetControlID="tb_day" ValidChars="0123456789">
                                            </asp:FilteredTextBoxExtender>
                                            日
                                        </p>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label36" runat="server" Text="國別"></asp:Label>
                                    </td>
                                    <td class="trdata">
                                        <asp:RadioButton ID="radTaiwan" runat="server" GroupName="country" Text="本國" TabIndex="6"
                                            Checked="True" CssClass="style35" OnClick="javascript: radTaiwan_CheckedChanged();" />
                                        <asp:RadioButton ID="radForeign" runat="server" GroupName="country" Text="外國" TabIndex="6"
                                            CssClass="style35" OnClick="javascript: radForeign_CheckedChanged();" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlTaiwan" runat="server">
                                            <table style="width: 100%;" border="1">
                                                <tr>
                                                    <td bgcolor="#FFFFCC">
                                                        <asp:Label ID="Label3" runat="server" Text="身分證號碼"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtIPID" runat="server" MaxLength="10" onchange="javascript: checkTwID(this);" Width="120px" TabIndex="7"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <asp:Label ID="Label189" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlForeign" runat="server" Style="display: none">
                                            <table style="width: 100%;" border="1">
                                                <tr>
                                                    <td bgcolor="#FFFFCC">
                                                        <asp:Label ID="Label39" runat="server" Text="外國居留證"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtForeign" runat="server" MaxLength="10" TabIndex="8"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#FFFFCC">
                                                        <asp:Label ID="Label40" runat="server" Text="護照號碼"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPassportID" runat="server" MaxLength="10" TabIndex="9"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#FFFFCC">
                                                        <asp:Label ID="Label41" runat="server" Text="居留期限"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtResidence" runat="server" Style="margin-left: 0px" OnTextChanged="txtResidence_TextChanged" TabIndex="10">
                                                                </asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtResidence_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtResidence">
                                                                </asp:FilteredTextBoxExtender>
                                                                <asp:CalendarExtender ID="txtResidence_CalendarExtender" runat="server" Enabled="True"
                                                                    Format="yyyy/MM/dd" TargetControlID="txtResidence">
                                                                </asp:CalendarExtender>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#FFFFCC">
                                                        <asp:Label ID="Label43" runat="server" Text="國籍"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCountry" runat="server" TabIndex="11"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label121" runat="server" Text="性別"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rbSex" runat="server" RepeatDirection="Horizontal" TabIndex="12">
                                            <asp:ListItem Value="1">男　</asp:ListItem>
                                            <asp:ListItem Value="2">女　</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label8" runat="server" Text="身高"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHeight" runat="server" MaxLength="3" Width="50px" TabIndex="13"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtHeight_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txtHeight">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:Label ID="Label125" runat="server" Text="公分"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label9" runat="server" Text="體重"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWeight" runat="server" MaxLength="4" Width="50px" TabIndex="14"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtWeight_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtWeight" ValidChars=".">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:Label ID="Label126" runat="server" Text="公斤"></asp:Label>
                                    </td>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label6" runat="server" Text="DNR註記"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rbDNR" runat="server" RepeatDirection="Horizontal" TabIndex="12">
                                            <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                            <asp:ListItem Value="2">否</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:TextBox ID="txtDNR" runat="server" Width="30px" Visible="false" Enabled="False" TabIndex="15"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC" rowspan="2">
                                        <asp:Label ID="Label5" runat="server" Text="住民照片"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <%--預覽照片--%>
                                        <asp:Image ID="Preview" runat="server" Width="150" Height="150" Style="display: none;" />
                                        <img alt="" src="" style="width: 150px; height: 150px; display: none; float: left"
                                            id="IPPic" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:FileUpload ID="fupIPPic" runat="server" Enabled="False" onchange="previewFile()" />
                                        <br />
                                        <input type="button" id="disableAdd" runat="server" onclick="disableMsg('加入照片前請先輸入\n住民號碼及選擇檔案');" value="加入" class="disabledcolor" />
                                        <asp:Button ID="btnIPPicAdd" runat="server" OnClick="btnIPPicAdd_Click" Text="加入"
                                            UseSubmitBehavior="False" Style="display: none" />
                                        <input type="button" id="disableDelete" runat="server" onclick="disableMsg('尚未加入照片');" value="刪除" class="disabledcolor" />

                                        <asp:Button ID="btnIPPicDelete" runat="server" OnClick="btnIPPicDelete_Click" Text="刪除"
                                            UseSubmitBehavior="False" Style="display: none" />
                                        <asp:Button ID="btnCutPic" runat="server" OnClick="btnCutPic_Click" Text="裁剪" Visible="False"
                                            UseSubmitBehavior="False" />
                                        <asp:Label ID="Label190" runat="server" Text="請選擇 jpg, gif, png格式檔案"></asp:Label>
                                        &nbsp;<asp:Label ID="Label122" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label10" runat="server" Text="戶籍地址"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <asp:Panel ID="pnlTaiwanadr" runat="server">
                                            <select id="PermCity" onchange="changePermCity()"></select>
                                            <select id="PermArea" onchange="changePermArea()"></select>
                                            <asp:TextBox ID="txtpermadr" runat="server" Width="500px"></asp:TextBox>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlForeignadr" runat="server" Style="display: none">
                                            <asp:TextBox ID="txtForeignAdr" runat="server" Width="700px" TabIndex="31"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFCC">
                                        <asp:Label ID="Label33" runat="server" Text="現居地址"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="同醫療機構地址" UseSubmitBehavior="False" TabIndex="32" />
                                        <input type="button" id="Button1" runat="server" value="同戶籍地址" tabindex="33"
                                            onclick="CopyAdd_Click(ContentPlaceHolder1_Panel1, ContentPlaceHolder1_TextBox3, ContentPlaceHolder1_Button1, ContentPlaceHolder1_Button2);" />
                                        <input type="button" id="Button2" runat="server" value="變更" style="display: none" tabindex="34"
                                            onclick="ChangedAdd_Click(ContentPlaceHolder1_Panel1, ContentPlaceHolder1_TextBox3, ContentPlaceHolder1_Button1, ContentPlaceHolder1_Button2);" />
                                        <asp:TextBox ID="TextBox3" runat="server" Enabled="true" Style="display: none" Width="700px" TabIndex="35"></asp:TextBox>
                                        <br />
                                        <asp:Panel ID="Panel1" runat="server">
                                            <select id="City" onchange="changeCity()"></select>
                                            <select id="Area" onchange="changeArea()"></select>
                                            <asp:TextBox ID="txtnowadr" runat="server" Width="500px"></asp:TextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:TabContainer ID="TabIPInfo" runat="server" ActiveTabIndex="1" Width="100%">
                                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="住民基本資料">
                                    <ContentTemplate>
                                        <br />
                                        <table border="1" style="width: 100%; color: Black;">
                                            <tr>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label44" runat="server" Text="身分類別" TabIndex="51"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dropIPStatues" runat="server" AppendDataBoundItems="True" DataSourceID="SqlStatues"
                                                        DataTextField="STATUES_STATE" DataValueField="STATUES_ID" TabIndex="52" onchange="drop_change(this.id)">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtIPStatuesOther" runat="server" Enabled="False" TabIndex="53" Style="visibility: hidden;"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="txtIPStatuesOther_TextBoxWatermarkExtender" runat="server"
                                                        TargetControlID="txtIPStatuesOther" WatermarkCssClass="TextboxWatermark" WatermarkText="補充說明"
                                                        Enabled="True">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:SqlDataSource ID="SqlStatues" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                        SelectCommand="SELECT [STATUES_ID], [STATUES_STATE] FROM [CODE_IP_STATUES] WHERE [AccountEnable] = 'Y'"></asp:SqlDataSource>
                                                </td>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label45" runat="server" Text="身心障礙"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="radBarrierY" runat="server" GroupName="barrier" Text="有" TabIndex="54"></asp:RadioButton>
                                                    <asp:RadioButton ID="radBarrierN" runat="server" Checked="True" GroupName="barrier" TabIndex="54"
                                                        Text="無"></asp:RadioButton>
                                                </td>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label46" runat="server" Text="入住方式"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dropCheckMethod" runat="server" AppendDataBoundItems="True" TabIndex="55"
                                                        DataSourceID="SqlCheckMethod" DataTextField="ENTER_STATE" DataValueField="ENTER_ID">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlCheckMethod" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                        SelectCommand="SELECT [ENTER_ID], [ENTER_STATE] FROM [CODE_IP_ENTER_METHOD]"></asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label49" runat="server" Text="住民職業"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dropCareer" runat="server" AppendDataBoundItems="True" DataSourceID="SqlCareer" TabIndex="56"
                                                        DataTextField="CAREER_STATE" DataValueField="CAREER_ID">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlCareer" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                        SelectCommand="SELECT [CAREER_STATE], [CAREER_ID] FROM [CODE_IP_CAREER]"></asp:SqlDataSource>
                                                    <asp:TextBox ID="txtCareerOther" runat="server" Enabled="False" TabIndex="57"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="txtCareerOther_TextBoxWatermarkExtender" runat="server"
                                                        TargetControlID="txtCareerOther" WatermarkCssClass="TextboxWatermark" WatermarkText="職業參考表"
                                                        Enabled="True">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:Button ID="btnJobDetail" runat="server" Text="補充說明" OnClick="btnJobDetail_Click" />
                                                </td>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label47" runat="server" Text="宗教信仰"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dropBelief" runat="server" AppendDataBoundItems="True" DataSourceID="SqlBelief" TabIndex="58"
                                                        DataTextField="BELIEF_STATE" DataValueField="BELIEF_ID" onchange="javascript: drop_change(this.id);">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlBelief" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                        SelectCommand="SELECT [BELIEF_ID], [BELIEF_STATE] FROM [CODE_IP_BELIEF]"></asp:SqlDataSource>
                                                    <asp:TextBox ID="txtBeliefOther" Enabled="False" runat="server" TabIndex="59" Style="visibility: hidden;"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="txtBeliefOther_TextBoxWatermarkExtender" runat="server"
                                                        TargetControlID="txtBeliefOther" WatermarkCssClass="TextboxWatermark" WatermarkText="補充說明"
                                                        Enabled="True">
                                                    </asp:TextBoxWatermarkExtender>
                                                </td>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label48" runat="server" Text="教育程度"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dropEducation" runat="server" AppendDataBoundItems="True" DataSourceID="SqlEducation" TabIndex="60"
                                                        DataTextField="EDUCATION_STATE" DataValueField="EDUCATION_ID">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlEducation" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                        SelectCommand="SELECT [EDUCATION_ID], [EDUCATION_STATE] FROM [CODE_IP_EDUCATION]"></asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label50" runat="server" Text="溝通語言"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="Label21" runat="server" Text="主要："></asp:Label>
                                                            <asp:DropDownList ID="dropMainLanguage" runat="server" AppendDataBoundItems="True" TabIndex="61"
                                                                DataSourceID="SqlIPLanguage" DataTextField="LANGUAGE_STATE" DataValueField="LANGUAGE_ID" onchange="javascript: drop_change(this.id);">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtMainLanguage" Enabled="false" runat="server" TabIndex="62" Style="visibility: hidden;"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtMainLanguage_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtMainLanguage" WatermarkCssClass="TextboxWatermark"
                                                                WatermarkText="補充說明">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <br />
                                                            <asp:Label ID="Label22" runat="server" Text="次要："></asp:Label>
                                                            <asp:DropDownList ID="dropSecLanguage" runat="server" AppendDataBoundItems="True" TabIndex="63"
                                                                DataSourceID="SqlIPLanguage" DataTextField="LANGUAGE_STATE" DataValueField="LANGUAGE_ID" onchange="javascript: drop_change(this.id);">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtSecLanguage" Enabled="false" runat="server" TabIndex="64" Style="visibility: hidden;"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtSecLanguage_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtSecLanguage" WatermarkCssClass="TextboxWatermark"
                                                                WatermarkText="補充說明">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <br />
                                                            <asp:Label ID="Label34" runat="server" Text="第三："></asp:Label>
                                                            <asp:DropDownList ID="dropThirdLanguage" runat="server" AppendDataBoundItems="True" TabIndex="65"
                                                                DataSourceID="SqlIPLanguage" DataTextField="LANGUAGE_STATE" DataValueField="LANGUAGE_ID" onchange="javascript: drop_change(this.id);">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtThirdLanguage" Enabled="false" runat="server" TabIndex="66" Style="visibility: hidden;"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtThirdLanguage_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtThirdLanguage" WatermarkCssClass="TextboxWatermark"
                                                                WatermarkText="補充說明">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:SqlDataSource ID="SqlIPLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                                SelectCommand="SELECT [LANGUAGE_ID], [LANGUAGE_STATE] FROM [CODE_IP_LANGUAGE]"></asp:SqlDataSource>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label51" runat="server" Text="語言能力"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="dropTalkAbility" runat="server" AppendDataBoundItems="True" TabIndex="67"
                                                                DataSourceID="SqlTalkAbility" DataTextField="TALK_STATE" DataValueField="TALK_ID" onchange="javascript: drop_change(this.id);">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlTalkAbility" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                                SelectCommand="SELECT [TALK_ID], [TALK_STATE] FROM [CODE_IP_TALK_ABILITY]"></asp:SqlDataSource>
                                                            <asp:TextBox ID="txtTalkAbility" Enabled="false" runat="server" TabIndex="68" Style="visibility: hidden;"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtTalkAbility_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtTalkAbility" WatermarkCssClass="TextboxWatermark"
                                                                WatermarkText="補充說明">
                                                            </asp:TextBoxWatermarkExtender>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label52" runat="server" Text="主要經濟來源"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="dropMoney" runat="server" AppendDataBoundItems="True" DataSourceID="SqlMoney" TabIndex="69"
                                                                DataTextField="ECO_STATE" DataValueField="ECO_ID" onchange="javascript: drop_change(this.id);">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlMoney" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                                SelectCommand="SELECT [ECO_ID], [ECO_STATE] FROM [CODE_IP_ECO_SOURCE]"></asp:SqlDataSource>
                                                            <asp:TextBox ID="txtMoneyOther" Enabled="false" runat="server" TabIndex="70" Style="visibility: hidden;"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtMoneyOther_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtMoneyOther" WatermarkCssClass="TextboxWatermark"
                                                                WatermarkText="補充說明">
                                                            </asp:TextBoxWatermarkExtender>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label53" runat="server" Text="住民保險"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBoxList ID="dropInsurance" runat="server" AppendDataBoundItems="True" DataSourceID="SqlInsurance" TabIndex="71"
                                                        DataTextField="INSURANCE_STATE" DataValueField="INSURANCE_ID" onchange="check_change(this.id); ">
                                                    </asp:CheckBoxList>
                                                    <asp:TextBox ID="txtInsuranceOther" Enabled="False" runat="server"
                                                        TabIndex="72"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="txtInsuranceOther_TextBoxWatermarkExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtInsuranceOther" WatermarkCssClass="TextboxWatermark"
                                                        WatermarkText="補充說明">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:SqlDataSource ID="SqlInsurance" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                        SelectCommand="SELECT [INSURANCE_ID], [INSURANCE_STATE] FROM [CODE_IP_INSURANCE]"></asp:SqlDataSource>
                                                </td>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label54" runat="server" Text="家庭問題評估"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dropFamilyProblem" runat="server" AppendDataBoundItems="True" TabIndex="73"
                                                        DataSourceID="SqlFamilyProblem" DataTextField="PROBLEM_STATE" DataValueField="PROBLEM_ID">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlFamilyProblem" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                        SelectCommand="SELECT [PROBLEM_ID], [PROBLEM_STATE] FROM [CODE_IP_FAMILY_PROBLEM]"></asp:SqlDataSource>
                                                </td>
                                                <td bgcolor="#FFFFCC" class="style32">
                                                    <asp:Label ID="Label55" runat="server" Text="保證金"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMargin" runat="server" Text="請於入住時填寫" ForeColor="Blue"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" colspan="2">
                                                    <asp:Label ID="Label56" runat="server" Text="有無法定傳染性疾病史"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:RadioButton ID="radStat_Infc_HXY" runat="server" GroupName="stat" Text="有" onclick="Stat_InfcDivY();" TabIndex="74" />
                                                            <asp:RadioButton ID="radStat_Infc_HXN" runat="server" Checked="True" GroupName="stat" TabIndex="74"
                                                                Text="無" onclick="Stat_InfcDivN();" />
                                                            <br />
                                                            <div id="Div2" runat="server" style="display: none;">
                                                                <asp:TextBox ID="TextBox5" runat="server" Height="50px" TextMode="MultiLine" Width="98%" TabIndex="75"></asp:TextBox>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" colspan="2">
                                                    <asp:Label ID="Label57" runat="server" Text="初入住後有無追蹤疾病史 "></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:RadioButton ID="radAfter_Adms_HXY" runat="server" GroupName="after" Text="有" TabIndex="76"
                                                                onclick="After_AdmsDivY();" />
                                                            <asp:RadioButton ID="radAfter_Adms_HXN" runat="server" Checked="True" GroupName="after" TabIndex="76"
                                                                Text="無" onclick="After_AdmsDivN();" />
                                                            <br />
                                                            <div id="Div1" runat="server" style="display: none;">
                                                                <asp:TextBox ID="TextBox6" runat="server" Height="50px" TextMode="MultiLine" Width="90%" TabIndex="77"></asp:TextBox>
                                                                <asp:ImageButton ID="ibtnradAfter_AdmsPhrase" runat="server" BorderStyle="None" Height="20px"
                                                                    ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnradAfter_AdmsPhrase_Click"
                                                                    OnClientClick="return showIPBasicPanel();" ToolTip="片語" Width="20px" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="住民家庭基本資料">
                                    <ContentTemplate>
                                        <br />
                                        <table border="1" style="width: 100%; color: Black;">
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label58" runat="server" Text="婚姻狀況"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="dropMarry" runat="server" AppendDataBoundItems="True" DataSourceID="SqlMarry" TabIndex="78"
                                                                DataTextField="MARRY_STATE" DataValueField="MARRY_ID" onchange="drop_change(this.id)">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlMarry" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                                SelectCommand="SELECT [MARRY_ID], [MARRY_STATE] FROM [CODE_IP_MARRY]"></asp:SqlDataSource>
                                                            <asp:TextBox ID="txtMarryOther" runat="server" TabIndex="79" Style="visibility: hidden;"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtMarryOther_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtMarryOther" WatermarkCssClass="TextboxWatermark"
                                                                WatermarkText="補充說明">
                                                            </asp:TextBoxWatermarkExtender>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label59" runat="server" Text="配偶姓名"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMateName" runat="server" Height="16px" TabIndex="80"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label60" runat="server" Text="配偶責任感"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dropMateDuty" runat="server" AppendDataBoundItems="True" DataSourceID="SqlMateDuty"
                                                        DataTextField="DUTY_STATE" DataValueField="DUTY_ID" TabIndex="81">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlMateDuty" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                        SelectCommand="SELECT [DUTY_ID], [DUTY_STATE] FROM [CODE_IP_MATE_DUTY]"></asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label61" runat="server" Text="住民兒子數"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSon" runat="server" MaxLength="1" Width="30px" TabIndex="82"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="txtSon_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                        FilterType="Numbers" TargetControlID="txtSon">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:Label ID="Label63" runat="server" Text="人"></asp:Label>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label62" runat="server" Text="住民女兒數"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDaughter" runat="server" Width="30px" TabIndex="83"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="txtDaughter_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtDaughter">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:Label ID="Label64" runat="server" Text="人"></asp:Label>
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label65" runat="server" Text="入住原因"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="dropInReason" runat="server" AppendDataBoundItems="True" DataSourceID="SqlInReason" TabIndex="84"
                                                                DataTextField="INREASON_STATE" DataValueField="INREASON_ID" onchange="drop_change(this.id)">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlInReason" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                                SelectCommand="SELECT [INREASON_ID], [INREASON_STATE] FROM [CODE_IP_IN_REASON] WHERE ([AccountEnable] = @AccountEnable)">
                                                                <SelectParameters>
                                                                    <asp:Parameter DefaultValue="Y" Name="AccountEnable" Type="String" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            <asp:TextBox ID="txtInReasonOther" runat="server" TabIndex="85" Style="visibility: hidden;"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtInReasonOther_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtInReasonOther" WatermarkCssClass="TextboxWatermark"
                                                                WatermarkText="補充說明">
                                                            </asp:TextBoxWatermarkExtender>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label66" runat="server" Text="轉介來源"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="dropTransfer" runat="server" AppendDataBoundItems="True" DataSourceID="SqlTransfer" TabIndex="86"
                                                                DataTextField="TRANSFER_STATE" DataValueField="TRANSFER_ID" onchange="drop_change(this.id)">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlTransfer" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                                SelectCommand="SELECT [TRANSFER_ID], [TRANSFER_STATE] FROM [CODE_IP_TRANSFER]"></asp:SqlDataSource>
                                                            <asp:TextBox ID="txtTransferOther" runat="server" TabIndex="87" Style="visibility: hidden;"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtTransferOther_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtTransferOther" WatermarkCssClass="TextboxWatermark"
                                                                WatermarkText="補充說明">
                                                            </asp:TextBoxWatermarkExtender>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label67" runat="server" Text="轉介者姓名"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTransferName" runat="server" TabIndex="88"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label68" runat="server" Text="主簽約者"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSignName" runat="server" TabIndex="88"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label80" runat="server" Text="主簽約者關係"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSignRelate" runat="server" TabIndex="89"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnSignRelatePhrase_Click"
                                                        OnClientClick="return showSignRelationPanel();" ToolTip="片語" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label94" runat="server" Text="主簽約者電話1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSignTel" runat="server" TabIndex="90"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label185" runat="server" Text="主簽約者電話2"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSignTel0" runat="server" TabIndex="91"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label186" runat="server" Text="主簽約者電話3"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSignTel1" runat="server" TabIndex="92"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label115" runat="server" Text="Cu-Talk"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSIGN_CUTalk_MId" runat="server" TabIndex="93"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label191" runat="server" Text="主簽約者&lt;br /&gt;聯絡e-mail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSIGN_EMAIL" runat="server" Width="170px" TabIndex="94"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        FilterType="Custom, Numbers, LowercaseLetters" ValidChars=".@"
                                                        TargetControlID="txtSIGN_EMAIL" Enabled="True" />
                                                </td>
                                                <!--
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label192" runat="server" Text="主簽約者&lt;br /&gt;CUTalk帳號"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSIGN1_CUTalk_MId" runat="server" Width="170px"></asp:TextBox>
                                                </td>
                                                -->
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label69" runat="server" Text="主簽約者地址"></asp:Label>
                                                </td>
                                                <td colspan="5">
                                                    <input type="button" id="Button3" runat="server" value="同戶籍地址" tabindex="95"
                                                        onclick="CopyAdd_Click(ContentPlaceHolder1_TabIPInfo_TabPanel2_Panel2, ContentPlaceHolder1_TabIPInfo_TabPanel2_TextBox7, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button3, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button7);" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<input type="button" id="Button7" runat="server" value="變更" style="display: none;" tabindex="96"
                                                        onclick="ChangedAdd_Click(ContentPlaceHolder1_TabIPInfo_TabPanel2_Panel2, ContentPlaceHolder1_TabIPInfo_TabPanel2_TextBox7, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button3, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button7);" />
                                                    &nbsp;&nbsp;<asp:TextBox ID="TextBox7" runat="server" Style="display: none" Width="500px" TabIndex="97"></asp:TextBox>
                                                    <br />
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <select id="SignCity" onchange="changeSignCity()"></select>
                                                        <select id="SignArea" onchange="changeSignArea()"></select>
                                                        <asp:TextBox ID="txtsignadr" runat="server" Width="500px"></asp:TextBox>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label81" runat="server" Text="連帶保證者"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtREName" runat="server" Width="90px" TabIndex="112"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label82" runat="server" Text="連帶保證者關係"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRERelate" runat="server" TabIndex="113"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnReRelatePhrase" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnReRelatePhrase_Click"
                                                        OnClientClick="return showReRelationPanel();" ToolTip="片語" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlRePhrase" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnReRelatePhrase" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label95" runat="server" Text="連帶保證者電話1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRETel" runat="server" TabIndex="114"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label182" runat="server" Text="連帶保證者電話2"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRETel0" runat="server" TabIndex="115"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label183" runat="server" Text="連帶保證者電話3"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRETel1" runat="server" TabIndex="116"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label194" runat="server" Text="Cu-Talk"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtINVO1_CUTalk_MId" runat="server" TabIndex="117"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label193" runat="server" Text="連帶保證者&lt;br /&gt;聯絡e-mail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtINVO1_EMAIL" runat="server" Width="170px" TabIndex="118"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        FilterType="Custom, Numbers, LowercaseLetters" ValidChars=".@"
                                                        TargetControlID="txtINVO1_EMAIL" Enabled="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label83" runat="server" Text="連帶保證者地址"></asp:Label>
                                                </td>
                                                <td colspan="5">
                                                    <input type="button" id="Button8" runat="server" value="同戶籍地址" tabindex="119"
                                                        onclick="CopyAdd_Click(ContentPlaceHolder1_TabIPInfo_TabPanel2_Panel3, ContentPlaceHolder1_TabIPInfo_TabPanel2_TextBox8, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button8, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button9);" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<input type="button" id="Button9" runat="server" value="變更" style="display: none;" tabindex="120"
                                                        onclick="ChangedAdd_Click(ContentPlaceHolder1_TabIPInfo_TabPanel2_Panel3, ContentPlaceHolder1_TabIPInfo_TabPanel2_TextBox8, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button8, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button9);" />
                                                    &nbsp;&nbsp;<asp:TextBox ID="TextBox8" runat="server" Style="display: none" Width="500px" TabIndex="121"></asp:TextBox>
                                                    <br />
                                                    <asp:Panel ID="Panel3" runat="server">
                                                        <select id="ReCity" onchange="changeReCity()"></select>
                                                        <select id="ReArea" onchange="changeReArea()"></select>
                                                        <asp:TextBox ID="txtreadr" runat="server" Width="500px"></asp:TextBox>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <!-- 緊急聯絡人 -->
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label96" runat="server" Text="緊急聯絡人1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryName" runat="server" Width="90px" TabIndex="137"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label184" runat="server" Text="關係"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRERelate0" runat="server" TabIndex="138"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnReRelatePhrase0" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnReRelatePhrase0_Click"
                                                        OnClientClick="return showReRelationPanel_0();" ToolTip="片語" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlRePhrase0" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnReRelatePhrase0" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label97" runat="server" Text="電話1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryTel" runat="server" TabIndex="139"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label180" runat="server" Text="電話2"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryTel1" runat="server" TabIndex="140"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label181" runat="server" Text="電話3"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryTel0" runat="server" TabIndex="141"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label111" runat="server" Text="Cu-Talk"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEMR_CUTalk_MId" runat="server" TabIndex="142"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label195" runat="server" Text="緊急聯絡人1&lt;br /&gt;聯絡e-mail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEMR_EMAIL" runat="server" Width="170px" TabIndex="143"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        FilterType="Custom, Numbers, LowercaseLetters" ValidChars=".@"
                                                        TargetControlID="txtEMR_EMAIL" Enabled="True" />
                                                </td>
                                                <!--
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label196" runat="server" Text="緊急聯絡人1&lt;br /&gt;CUTalk帳號"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEMR_CUTalk_MId0" runat="server" Width="170px"></asp:TextBox>
                                                </td>
                                                -->
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label7" runat="server" Text="地址"></asp:Label>
                                                </td>
                                                <td colspan="5">
                                                    <input type="button" id="Button11" runat="server" value="同戶籍地址" tabindex="119"
                                                        onclick="CopyAdd_Click(ContentPlaceHolder1_TabIPInfo_TabPanel2_Panel4, ContentPlaceHolder1_TabIPInfo_TabPanel2_TextBox9, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button11, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button12);" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<input type="button" id="Button12" runat="server" value="變更" style="display: none;" tabindex="120"
                                                        onclick="ChangedAdd_Click(ContentPlaceHolder1_TabIPInfo_TabPanel2_Panel4, ContentPlaceHolder1_TabIPInfo_TabPanel2_TextBox9, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button11, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button12);" />
                                                    &nbsp;&nbsp;<asp:TextBox ID="TextBox9" runat="server" Style="display: none" Width="500px" TabIndex="121"></asp:TextBox>
                                                    <br />
                                                    <asp:Panel ID="Panel4" runat="server">
                                                        <select id="Ec1City" onchange="changeEc1City()"></select>
                                                        <select id="Ec1Area" onchange="changeEc1Area()"></select>
                                                        <asp:TextBox ID="txtec1adr" runat="server" Width="500px"></asp:TextBox>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <!-- 緊急聯絡人2 -->
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label16" runat="server" Text="緊急聯絡人2"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryName1" runat="server" Width="90px" TabIndex="156"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label20" runat="server" Text="關係"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRERelate1" runat="server" TabIndex="157"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnReRelatePhrase1" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnReRelatePhrase1_Click"
                                                        OnClientClick="return showReRelationPanel_1();" ToolTip="片語" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlRePhrase1" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnReRelatePhrase1" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label17" runat="server" Text="電話1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryTel2" runat="server" TabIndex="158"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label18" runat="server" Text="電話2"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryTel3" runat="server" TabIndex="159"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label19" runat="server" Text="電話3"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryTel4" runat="server" TabIndex="160"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label112" runat="server" Text="Cu-Talk"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEMR1_CUTalk_MId" runat="server" TabIndex="161"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label197" runat="server" Text="緊急聯絡人2&lt;br /&gt;聯絡e-mail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEMR1_EMAIL" runat="server" Width="170px" TabIndex="162"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        FilterType="Custom, Numbers, LowercaseLetters" ValidChars=".@"
                                                        TargetControlID="txtEMR1_EMAIL" Enabled="True" />
                                                </td>
                                                <!--
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label198" runat="server" Text="緊急聯絡人2&lt;br /&gt;CUTalk帳號"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEMR1_CUTalk_MId0" runat="server" Width="170px"></asp:TextBox>
                                                </td>
                                                -->
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label37" runat="server" Text="地址"></asp:Label>
                                                </td>
                                                <td colspan="5">
                                                    <input type="button" id="Button13" runat="server" value="同戶籍地址" tabindex="119"
                                                        onclick="CopyAdd_Click(ContentPlaceHolder1_TabIPInfo_TabPanel2_Panel7, ContentPlaceHolder1_TabIPInfo_TabPanel2_TextBox10, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button13, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button14);" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<input type="button" id="Button14" runat="server" value="變更" style="display: none" tabindex="119"
                                                        onclick="ChangedAdd_Click(ContentPlaceHolder1_TabIPInfo_TabPanel2_Panel7, ContentPlaceHolder1_TabIPInfo_TabPanel2_TextBox10, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button13, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button14);" />
                                                    &nbsp;&nbsp;<asp:TextBox ID="TextBox10" runat="server" Style="display: none" Width="500px" TabIndex="121"></asp:TextBox>
                                                    <br />
                                                    <asp:Panel ID="Panel7" runat="server">
                                                        <select id="Ec2City" onchange="changeEc2City()"></select>
                                                        <select id="Ec2Area" onchange="changeEc2Area()"></select>
                                                        <asp:TextBox ID="txtec2adr" runat="server" Width="500px"></asp:TextBox>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <!--緊急聯絡人3-->
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label_Em3" runat="server" Text="緊急聯絡人3"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryName_Em" runat="server" Width="90px" TabIndex="175"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label_Re3" runat="server" Text="關係"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRERelate_Re3" runat="server" TabIndex="176"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnReRelatePhrase3" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnReRelatePhrase2_Click"
                                                        OnClientClick="return showReRelationPanel_2();" ToolTip="片語" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlRePhrase3" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnReRelatePhrase3" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="LabelEm3_te1" runat="server" Text="電話1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryTel_Em3_1" runat="server" TabIndex="177"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="LabelEm3_te2" runat="server" Text="電話2"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryTel_Em3_2" runat="server" TabIndex="178"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="LabelEm3_te3" runat="server" Text="電話3"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHurryTel_Em3_3" runat="server" TabIndex="179"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label113" runat="server" Text="Cu-Talk"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEMR2_CUTalk_MId" runat="server" TabIndex="180"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label199" runat="server" Text="緊急聯絡人3&lt;br /&gt;聯絡e-mail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEMR2_EMAIL" runat="server" Width="170px" TabIndex="181"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        FilterType="Custom, Numbers, LowercaseLetters" ValidChars=".@"
                                                        TargetControlID="txtEMR2_EMAIL" Enabled="True" />
                                                </td>
                                                <!--
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label200" runat="server" Text="緊急聯絡人3&lt;br /&gt;CUTalk帳號"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEMR2_CUTalk_MId0" runat="server" Width="170px"></asp:TextBox>
                                                </td>
                                                -->
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="LabelEm3_ad" runat="server" Text="地址"></asp:Label>
                                                </td>
                                                <td colspan="5">
                                                    <input type="button" id="Button15" runat="server" value="同戶籍地址" tabindex="119"
                                                        onclick="CopyAdd_Click(ContentPlaceHolder1_TabIPInfo_TabPanel2_Panel8, ContentPlaceHolder1_TabIPInfo_TabPanel2_TextBox11, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button15, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button16);" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<input type="button" id="Button16" runat="server" value="變更" style="display: none" tabindex="120"
                                                        onclick="ChangedAdd_Click(ContentPlaceHolder1_TabIPInfo_TabPanel2_Panel8, ContentPlaceHolder1_TabIPInfo_TabPanel2_TextBox11, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button15, ContentPlaceHolder1_TabIPInfo_TabPanel2_Button16);" />
                                                    &nbsp;&nbsp;<asp:TextBox ID="TextBox11" runat="server" Style="display: none" Width="500px" TabIndex="121"></asp:TextBox>
                                                    <br />
                                                    <asp:Panel ID="Panel8" runat="server">
                                                        <select id="Ec3City" onchange="changeEc3City()"></select>
                                                        <select id="Ec3Area" onchange="changeEc3Area()"></select>
                                                        <asp:TextBox ID="txtec3adr" runat="server" Width="500px"></asp:TextBox>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <!--主要繳款人-->
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label98" runat="server" Text="主要繳款人"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPayName" runat="server" Width="90px" TabIndex="194"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label99" runat="server" Text="主要繳款人電話"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPayTel" runat="server" TabIndex="195"></asp:TextBox>
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label114" runat="server" Text="Cu-Talk"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPAY_CUTalk_MId" runat="server" TabIndex="196"></asp:TextBox>
                                                </td>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label201" runat="server" Text="主要繳款人&lt;br /&gt;聯絡e-mail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPAY_EMAIL" runat="server" Width="170px" TabIndex="197"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                        FilterType="Custom, Numbers, LowercaseLetters" ValidChars=".@"
                                                        TargetControlID="txtPAY_EMAIL" Enabled="True" />
                                                </td>
                                                <!--
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label202" runat="server" Text="主要繳款人&lt;br /&gt;CUTalk帳號"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPAY_CUTalk_MId0" runat="server" Width="170px"></asp:TextBox>
                                                </td>
                                                -->
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label100" runat="server" Text="繳款人現況評估"></asp:Label>
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="txtPatEvalute" runat="server" Height="60px" TextMode="MultiLine" TabIndex="198"
                                                        Width="95%"></asp:TextBox>
                                                    <asp:ImageButton runat="server" OnClientClick="return showPAY();" ImageAlign="Bottom"
                                                        ImageUrl="~/Image/WebImage/gif_45_069.gif" BorderStyle="None" Height="20px" ToolTip="片語"
                                                        Width="20px" ID="ibtnPAY" OnClick="ibtnPAY_Click"></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="住民費用優免設定">
                                    <ContentTemplate>
                                        <br />
                                        <table border="1" style="width: 100%; color: Black;">
                                            <tr>
                                                <td bgcolor="#FFFFCC">
                                                    <asp:Label ID="Label78" runat="server" Text="優免身份類別"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_IP_IDENTITY" runat="server" AppendDataBoundItems="True" TabIndex="199"
                                                        DataSourceID="SqlDataSource_IP_IDENTITY" DataTextField="IP_IDENTITY" DataValueField="NO">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSource_IP_IDENTITY" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
                                                        SelectCommand="SELECT [NO], [IP_IDENTITY] FROM [CODE_IP_IDENTITY] WHERE [AccountEnable] = 'Y' ORDER BY [NO]"></asp:SqlDataSource>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="膳食偏好">
                                    <ContentTemplate>
                                        <br />
                                        <table border="1" style="width: 100%; color: Black;">
                                            <tr>
                                                <td rowspan="2" colspan="2" bgcolor="#FFFFCC">葷素習慣
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="radvege" runat="server" Text="素" GroupName="chbl" onclick="radvege_Check();" TabIndex="200" />
                                                    <asp:Panel ID="pnlvege" runat="server" onclick="javascript: disableVege(ContentPlaceHolder1_TabIPInfo_TabPanel5_radvege);">
                                                        (
                                                        <asp:RadioButtonList ID="chblvege" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="True" TabIndex="200"
                                                            DataSourceID="vege2" DataTextField="CONTENTS" DataValueField="VALUE" RepeatLayout="Flow">
                                                        </asp:RadioButtonList>
                                                        )
                                                        <br />
                                                        <asp:Label ID="Label120" runat="server" Text="不吃食物: "></asp:Label>
                                                        <asp:CheckBoxList ID="chklvege1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" onclick="chklvege1_clike();" TabIndex="201"
                                                            AppendDataBoundItems="True" DataSourceID="vege2_1" DataTextField="CONTENTS" DataValueField="VALUE">
                                                        </asp:CheckBoxList>
                                                        其他:
                                                        <asp:TextBox ID="txtvege" runat="server" Width="100px" TabIndex="202"></asp:TextBox>
                                                        <asp:TextBox ID="lblvege" runat="server" Style="display: none;"></asp:TextBox>
                                                    </asp:Panel>
                                                    <asp:SqlDataSource ID="vege2" runat="server" SelectCommand="SELECT [VALUE], [CONTENTS] FROM [CODE_MEALFAVOR] WHERE [FIELDNAME] = 'CARNIVORE2' ORDER BY [NO]"></asp:SqlDataSource>
                                                    <asp:SqlDataSource ID="vege2_1" runat="server" SelectCommand="SELECT [VALUE], [CONTENTS] FROM [CODE_MEALFAVOR] WHERE [NO] = 7 OR [NO] = 8 ORDER BY [NO] "></asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="radNvege" runat="server" Text="葷" GroupName="chbl" onclick="radNvege_Check();" TabIndex="203" />
                                                    <br />
                                                    <asp:Panel ID="pnlNvege" runat="server" onclick="javascript: disableVege(ContentPlaceHolder1_TabIPInfo_TabPanel5_radNvege);">
                                                        不吃食物:

                                                        <asp:CheckBoxList ID="chklvege" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" onclick="chklvege_clike();" TabIndex="204"
                                                            AppendDataBoundItems="True" DataSourceID="vege1" DataTextField="CONTENTS" DataValueField="VALUE">
                                                        </asp:CheckBoxList>
                                                        <br />
                                                        其他:
                                                        <asp:TextBox ID="txtNvege" runat="server" Width="100px" TabIndex="205"></asp:TextBox>
                                                        <asp:TextBox ID="lblNvege" runat="server" Style="display: none;"></asp:TextBox>
                                                    </asp:Panel>
                                                    <asp:SqlDataSource ID="vege1" runat="server" SelectCommand="SELECT [VALUE], [CONTENTS] FROM [CODE_MEALFAVOR] WHERE [FIELDNAME] = 'CARNIVORE1' ORDER BY [NO]"></asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" bgcolor="#FFFFCC">季節性素食(無此限定者，請勿勾選)
                                                </td>
                                                <td>
                                                    <asp:CheckBoxList ID="chklvegetime" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="True" TabIndex="206"
                                                        DataSourceID="vege3" DataTextField="CONTENTS" DataValueField="VALUE">
                                                    </asp:CheckBoxList>
                                                    <asp:SqlDataSource ID="vege3" runat="server" SelectCommand="SELECT [VALUE], [CONTENTS] FROM [CODE_MEALFAVOR] WHERE [FIELDNAME] = 'VEGETABLE' ORDER BY [NO]"></asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td rowspan="3" bgcolor="#FFFFCC" class="style87">主食偏好
                                                </td>
                                                <td class="style84" bgcolor="#FFFFCC">早餐
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtfavorbreak" Width="220px" TabIndex="207"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_fb" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnhabitPhrase_Click"
                                                        OnClientClick="return showhabitPanel('0');" ToolTip="主食偏好" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_fb" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_fb" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style84" bgcolor="#FFFFCC">午餐
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtfavorlunch" Width="220px" TabIndex="208"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_fl" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnhabitPhrase_Click"
                                                        OnClientClick="return showhabitPanel('1');" ToolTip="主食偏好" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_fl" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_fl" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style84" bgcolor="#FFFFCC">晚餐
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtfavordinner" Width="220px" TabIndex="209"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_fd" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnhabitPhrase_Click"
                                                        OnClientClick="return showhabitPanel('2');" ToolTip="主食偏好" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_fd" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_fd" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" colspan="2">禁忌
                                                </td>
                                                <td class="style80">
                                                    <asp:TextBox runat="server" ID="txtforbid" Width="220px" TabIndex="210"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_forb" runat="server" BorderStyle="None"
                                                        Height="20px" ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif"
                                                        OnClick="ibtnhabitPhrase_Click" OnClientClick="return showhabitPanel('3');" ToolTip="膳食禁忌"
                                                        Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_forb" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_forb" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" colspan="2">特殊指示
                                                </td>
                                                <td class="style80">
                                                    <asp:TextBox runat="server" ID="txtspecorder" Width="220px" TabIndex="211"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_spc" runat="server" BorderStyle="None"
                                                        Height="20px" ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif"
                                                        OnClick="ibtnhabitPhrase_Click" OnClientClick="return showhabitPanel('4');" ToolTip="膳食特殊指示"
                                                        Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_spc" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_spc" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" rowspan="6" class="style87">各餐之要求
                                                </td>
                                                <td bgcolor="#FFFFCC" class="style84">早餐
                                                </td>
                                                <td class="style80">
                                                    <asp:TextBox runat="server" ID="txtmealbreak" Width="220px" TabIndex="212"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_mb" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnhabitPhrase_Click"
                                                        OnClientClick="return showhabitPanel('5');" ToolTip="各餐要求" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_mb" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_mb" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" class="style84">點心
                                                </td>
                                                <td class="style80">
                                                    <asp:TextBox runat="server" ID="txtmealsnack" Width="220px" TabIndex="213"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_ms" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnhabitPhrase_Click"
                                                        OnClientClick="return showhabitPanel('6');" ToolTip="各餐要求" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_ms" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_ms" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" class="style84">午餐
                                                </td>
                                                <td class="style80">
                                                    <asp:TextBox runat="server" ID="txtmeallunch" Width="220px" TabIndex="214"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_ml" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnhabitPhrase_Click"
                                                        OnClientClick="return showhabitPanel('7');" ToolTip="各餐要求" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_ml" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_ml" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" class="style84">下午茶
                                                </td>
                                                <td class="style80">
                                                    <asp:TextBox runat="server" ID="txtmealtea" Width="220px" TabIndex="215"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_mt" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnhabitPhrase_Click"
                                                        OnClientClick="return showhabitPanel('8');" ToolTip="各餐要求" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_mt" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_mt" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" class="style84">晚餐
                                                </td>
                                                <td class="style80">
                                                    <asp:TextBox runat="server" ID="txtmealdinner" Width="220px" TabIndex="216"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_md" runat="server" BorderStyle="None" Height="20px"
                                                        ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif" OnClick="ibtnhabitPhrase_Click"
                                                        OnClientClick="return showhabitPanel('9');" ToolTip="各餐要求" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_md" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_md" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFCC" class="style84">宵夜
                                                </td>
                                                <td class="style80">
                                                    <asp:TextBox runat="server" ID="txtmealmnsnack" Width="220px" TabIndex="217"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSignRelatePhrase_mmn" runat="server" BorderStyle="None"
                                                        Height="20px" ImageAlign="Bottom" ImageUrl="~/Image/WebImage/gif_45_069.gif"
                                                        OnClick="ibtnhabitPhrase_Click" OnClientClick="return showhabitPanel('10');"
                                                        ToolTip="各餐要求" Width="20px" />
                                                    <asp:UpdatePanel ID="uppnlSignPhrase_mmn" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ibtnSignRelatePhrase_mmn" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <br />
                        <table class="style29">
                            <tr>
                                <td class="style79">
                                    <input type="button" id="disableAddIPBasic" runat="server" onclick="disableMsg('住民資料已儲存');" value="儲存" class="disabledcolor" style="display: none;" />
                                    <asp:Button ID="btnAddIPBasic" runat="server" OnClick="btnAddIPBasic_Click" Text="儲存" OnClientClick="return savingMsg();" />
                                    <input type="button" id="disablePrint" runat="server" onclick="disableMsg('列印前請先儲存住民資料');" value="列印" class="disabledcolor" />
                                    <asp:Button ID="btnPrint" runat="server" Text="列印" OnClick="btnPrint_Click" Style="display: none;" />
                                    <input type="button" id="disableNext" runat="server" onclick="disableMsg('繼續新增前請先儲存住民資料');" value="繼續新增" class="disabledcolor" />
                                    <asp:Button ID="Button10" runat="server" OnClick="Button10_Click" Text="繼續新增" Style="display: none;" />
                                    <asp:Button ID="btnIPBaic" runat="server" OnClick="btnIPBaic_Click" Text="住民基本資料"
                                        Visible="False" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnAllowance" runat="server" Enabled="False" OnClick="btnAllowance_Click"
                                        Text="住民補助福利資料" Visible="False" UseSubmitBehavior="False" />
                                    <asp:Label ID="Label123" runat="server" ForeColor="Red" Font-Bold="True" Visible="False"></asp:Label>
                                </td>
                        </table>
                        <br />
                        <asp:Panel ID="Panel6" runat="server" Enabled="False">
                            <asp:Label ID="Label35" runat="server" Font-Bold="True" ForeColor="Black" Text="請選擇是否繼續填寫下列表單："></asp:Label>
                            <asp:Label ID="This_IP_NO" runat="server" Text="" Visible="False"></asp:Label>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <!--
                        <td valign="TOP">
                            <asp:RadioButton ID="rbAllowance" runat="server" Text="新增住民補助資料" 
                                AutoPostBack="True" oncheckedchanged="rbAllowance_CheckedChanged" />
                        </td>
                        -->
                                            <td valign="TOP">
                                                <input type="button" id="disableButton4" runat="server" onclick="disableMsg('填寫住民補助資料前請先儲存住民資料');" value="填寫住民補助資料" class="disabledcolor" />
                                                <asp:Button ID="Button4" runat="server" Text="填寫住民補助資料" OnClick="Button4_Click" Style="display: none;" />
                                            </td>
                                            <td valign="TOP">提醒您：<br />
                                                (1) 若不馬上填寫，請日後至<b><font size='3' color='#0000FF'>住民補助資料管理</font></b>功能進行新增；<br />
                                                (2) 若填寫後需要查詢或修改，請至<b>住民補助資料管理</b>功能進行。
                                            </td>
                                        </tr>
                                        <tr>
                                            <!--
                        <td valign="TOP">
                            <asp:RadioButton ID="rbHandicap" runat="server" Text="新增住民障礙手冊" 
                                AutoPostBack="True" oncheckedchanged="rbHandicap_CheckedChanged" />
                        </td>
                        -->
                                            <td valign="TOP">
                                                <input type="button" id="disableButton6" runat="server" onclick="disableMsg('填寫住民障礙手冊前請先儲存住民資料');" value="填寫住民障礙手冊" class="disabledcolor" />
                                                <asp:Button ID="Button6" runat="server" Text="填寫住民障礙手冊" OnClick="Button6_Click" Style="display: none;" />
                                            </td>
                                            <td valign="TOP">提醒您：<br />
                                                (1) 若不馬上填寫，請日後至<b><font size='3' color='#0000FF'>住民障礙手冊管理</font></b>功能進行新增；<br />
                                                (2) 若填寫後需要查詢或修改，請至<b>住民障礙手冊管理</b>功能進行。
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                        </asp:Panel>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="pnlCutPicture" runat="server" Visible="False">
                        <div>
                            <img alt="" src="" id="ImgCutPic" runat="server" onerror="src='/Image/IPImage/people-man.png'" />
                            x:<asp:TextBox ID="txtX" runat="server" Width="50px"></asp:TextBox>
                            y:<asp:TextBox ID="txtY" runat="server" Width="50px"></asp:TextBox>
                            x2:<asp:TextBox ID="txtX2" runat="server" Width="50px"></asp:TextBox>
                            y2:<asp:TextBox ID="txtY2" runat="server" Width="50px"></asp:TextBox>
                            w:<asp:TextBox ID="txtWidth" runat="server" Width="50px"></asp:TextBox>
                            h:<asp:TextBox ID="TextHeight" runat="server" Width="50px"></asp:TextBox>
                            <asp:Button ID="btnCutConfirm" runat="server" Text="確認" OnClick="btnCutConfirm_Click"
                                UseSubmitBehavior="False" />
                        </div>
                    </asp:Panel>
                    <br />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnPrint" />
                <asp:PostBackTrigger ControlID="btnIPPicAdd" />
                <asp:PostBackTrigger ControlID="btnIPPicDelete" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!--片語-->
    <div id="phrase" runat="server" style="display: none; cursor: default;">
        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr align="center">
                        <td colspan="2" align="center">
                            <asp:Label ID="lblPhraseTitle" runat="server" ForeColor="Black" Font-Bold="true"
                                Font-Size="Medium" Font-Names="新細明體" Text="住民管理常用片語"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ImageButton2" runat="server" Height="32px" ImageUrl="~/Image/WebImage/633855842283694909.jpg"
                                Width="31px" OnClientClick="return Exit();" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Panel ID="Panel5" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:TabContainer ID="TabContainerPHRASE" runat="server" ActiveTabIndex="0" Width="600px">
                                                <asp:TabPanel ID="TabPanelPHRASE" runat="server" HeaderText="">
                                                </asp:TabPanel>
                                            </asp:TabContainer>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                        </td>
                        <td align="right" valign="bottom">
                            <asp:Button ID="btnSENT_IPB" runat="server" Text="送出" OnClientClick="return phrase_IPBasic();" />
                            <asp:Button ID="btnSENT_Sign" runat="server" Text="送出" OnClientClick="return phrase_SignRelation();" />
                            <asp:Button ID="btnSENT_Re" runat="server" Text="送出" OnClientClick="return phrase_ReRelation();" />
                            <asp:Button ID="btnSENT_Re0" runat="server" Text="送出" OnClientClick="return phrase_ReRelation_0();" />
                            <asp:Button ID="btnSENT_Re1" runat="server" Text="送出" OnClientClick="return phrase_ReRelation_1();" />
                            <asp:Button ID="btnSENT_Re2" runat="server" Text="送出" OnClientClick="return phrase_ReRelation_2();" />
                            <asp:Button ID="btnSENT_habit" runat="server" Text="送出" OnClientClick="return phrase_habit();" />
                            <asp:Button ID="btnSENT_PAY" runat="server" Text="送出" OnClientClick="return phrase_PAY();" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="toggler">
        <div id="effect" class="ui-widget-content">
            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                <ContentTemplate>
                    <p>
                        <asp:Label ID="lblShowMsg" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="toggler1">
        <div id="effect1" class="ui-widget-content1">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <p>
                        <asp:Label ID="lblShowErr" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanelErrMsg" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        //disabled
        function disableVege(chkVege) {
            if (!chkVege.checked) {
                document.getElementById("<%=this.lblShowErr.ClientID %>").innerText = "請點選正確的飲食習慣";
                runEffect1();
            }
        }
        function disableMsg(str) {
            document.getElementById("<%=this.lblShowErr.ClientID %>").innerText = str;
            runEffect1();
        };

        //選向為其他才可輸入
        function drop_change(target_id) {
            var target = $("#" + target_id);
            if ($.trim($(target).children("option:selected").html()) == "其他") {
                $(target).next("input").css("visibility", "");
                $(target).next("input").prop("disabled", false);
            }
            else {
                $(target).next("input").css("visibility", "hidden");
                $(target).next("input").prop("disabled", true);
            }
        };

        //多選為其他才可輸入
        function check_change(target_id) {
            var target = $("#" + target_id);
            var y_n = false;
            var selected_none = false;

            $("#" + target_id + " td").each(function () {
                if ($('#ContentPlaceHolder1_TabIPInfo_TabPanel1_dropInsurance_0').is(':checked')) {
                    selected_none = true;
                } else if ($.trim($(this).children("label").html()) == "其他" && $(this).children("input:checked").length == 1) {
                    y_n = true;
                }

                //if ($.trim($(this).children("label").html()) == "其他" && $(this).children("input:checked").length == 1) {
                //    y_n = true;
                //}
            });

            //勾選"無"，關閉全部
            var list = document.getElementById('<%= dropInsurance.ClientID%>').getElementsByTagName("input");
            if (selected_none) {
                for (var i = 1; i < list.length; i++) {
                    $('#ContentPlaceHolder1_TabIPInfo_TabPanel1_dropInsurance_' + i).prop('checked', false);
                }
                $("#<%= txtInsuranceOther.ClientID %>").prop("disabled", true);
            } else {
                if (y_n) {
                    $("#<%= txtInsuranceOther.ClientID %>").prop("disabled", false);
                } else {
                    $("#<%= txtInsuranceOther.ClientID %>").prop("disabled", true);
                    $("#<%= txtInsuranceOther.ClientID %>").val("");
                }
            }

            <%--if (y_n) {
                $("#<%= txtInsuranceOther.ClientID %>").prop("disabled", false);
            } else {
                $("#<%= txtInsuranceOther.ClientID %>").prop("disabled", true);
                $("#<%= txtInsuranceOther.ClientID %>").val("");
            }--%>
        };

        function pageLoad() {
            //當頁面載完之後，用AddressSeleclList.Initialize()，傳入要綁定的縣市下拉選單ID及鄉鎮市區下拉選單ID
            AddressSeleclList.Initialize('PermCity', 'PermArea');
            AddressSeleclList.Initialize('City', 'Area');
            AddressSeleclList.Initialize('SignCity', 'SignArea');
            AddressSeleclList.Initialize('ReCity', 'ReArea');
            AddressSeleclList.Initialize('Ec1City', 'Ec1Area');
            AddressSeleclList.Initialize('Ec2City', 'Ec2Area');
            AddressSeleclList.Initialize('Ec3City', 'Ec3Area');
        };

        //戶籍地址－城市
        function changePermCity() {
            var CityValue = document.getElementById("PermCity").selectedIndex; //取得被選取的城市
            document.getElementById("ContentPlaceHolder1_txtpermadr").value = document.getElementById("PermCity").options[CityValue].value; //顯示選取的城市
        };
        //現居地址－城市
        function changeCity() {
            var CityValue = document.getElementById("City").selectedIndex; //取得被選取的城市
            document.getElementById("ContentPlaceHolder1_txtnowadr").value = document.getElementById("PermCity").options[CityValue].value; //顯示選取的城市
        };
        //主簽約者地址－城市
        function changeSignCity() {
            var CityValue = document.getElementById("SignCity").selectedIndex; //取得被選取的城市
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtsignadr").value = document.getElementById("SignCity").options[CityValue].value; //顯示選取的城市
        };
        //連帶保證者地址－城市
        function changeReCity() {
            var CityValue = document.getElementById("ReCity").selectedIndex; //取得被選取的城市
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtreadr").value = document.getElementById("ReCity").options[CityValue].value; //顯示選取的城市
        };
        //緊急連絡人1地址－城市
        function changeEc1City() {
            var CityValue = document.getElementById("Ec1City").selectedIndex; //取得被選取的城市
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec1adr").value = document.getElementById("Ec1City").options[CityValue].value; //顯示選取的城市
        };
        //緊急連絡人2地址－城市
        function changeEc2City() {
            var CityValue = document.getElementById("Ec2City").selectedIndex; //取得被選取的城市
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec2adr").value = document.getElementById("Ec2City").options[CityValue].value; //顯示選取的城市
        };
        //緊急連絡人3地址－城市
        function changeEc3City() {
            var CityValue = document.getElementById("Ec3City").selectedIndex; //取得被選取的城市
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec3adr").value = document.getElementById("Ec3City").options[CityValue].value; //顯示選取的城市
        };

        //戶籍地址－區鄉鎮
        function changePermArea() {
            var CityValue = document.getElementById("PermCity").selectedIndex; //取得被選取的城市
            var AreaValue = document.getElementById("PermArea").selectedIndex; //取得被選取的區鄉鎮
            var Zip = document.getElementById("PermArea").options[AreaValue].value.substr(0, 3);
            var Zone = document.getElementById("PermArea").options[AreaValue].value.substring(3);
            document.getElementById("ContentPlaceHolder1_txtpermadr").value = Zip + document.getElementById("PermCity").options[CityValue].value + Zone; //取得並顯示郵遞區號
            document.getElementById("ContentPlaceHolder1_txtpermadr").focus();
            var val = this.document.getElementById("ContentPlaceHolder1_txtpermadr").value; //store the value of the element
            this.document.getElementById("ContentPlaceHolder1_txtpermadr").value = ''; //clear the value of the element
            this.document.getElementById("ContentPlaceHolder1_txtpermadr").value = val; //set that value back.  
        };
        //現居地址－區鄉鎮
        function changeArea() {
            var CityValue = document.getElementById("City").selectedIndex; //取得被選取的城市
            var AreaValue = document.getElementById("Area").selectedIndex; //取得被選取的區鄉鎮
            var Zip = document.getElementById("Area").options[AreaValue].value.substr(0, 3);
            var Zone = document.getElementById("Area").options[AreaValue].value.substring(3);
            document.getElementById("ContentPlaceHolder1_txtnowadr").value = Zip + document.getElementById("City").options[CityValue].value + Zone; //取得並顯示郵遞區號
            //document.getElementById("ContentPlaceHolder1_txtnowadr").value = Zip + document.getElementById("City").options[CityValue].value + document.getElementById("Area").options[AreaValue].value; //取得並顯示郵遞區號
            document.getElementById("ContentPlaceHolder1_txtnowadr").focus();
            var val = this.document.getElementById("ContentPlaceHolder1_txtnowadr").value; //store the value of the element
            this.document.getElementById("ContentPlaceHolder1_txtnowadr").value = ''; //clear the value of the element
            this.document.getElementById("ContentPlaceHolder1_txtnowadr").value = val; //set that value back.  
        };
        //主簽約者地址－區鄉鎮
        function changeSignArea() {
            var CityValue = document.getElementById("SignCity").selectedIndex; //取得被選取的城市
            var AreaValue = document.getElementById("SignArea").selectedIndex; //取得被選取的區鄉鎮
            var Zip = document.getElementById("SignArea").options[AreaValue].value.substr(0, 3);
            var Zone = document.getElementById("SignArea").options[AreaValue].value.substring(3);
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtsignadr").value = Zip + document.getElementById("SignCity").options[CityValue].value + Zone; //取得並顯示郵遞區號
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtsignadr").focus();
            var val = this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtsignadr").value; //store the value of the element
            this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtsignadr").value = ''; //clear the value of the element
            this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtsignadr").value = val; //set that value back.  
        };
        //連帶保證者地址－區鄉鎮
        function changeReArea() {
            var CityValue = document.getElementById("ReCity").selectedIndex; //取得被選取的城市
            var AreaValue = document.getElementById("ReArea").selectedIndex; //取得被選取的區鄉鎮
            var Zip = document.getElementById("ReArea").options[AreaValue].value.substr(0, 3);
            var Zone = document.getElementById("ReArea").options[AreaValue].value.substring(3);
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtreadr").value = Zip + document.getElementById("ReCity").options[CityValue].value + Zone; //取得並顯示郵遞區號
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtreadr").focus();
            var val = this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtreadr").value; //store the value of the element
            this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtreadr").value = ''; //clear the value of the element
            this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtreadr").value = val; //set that value back.  
        };
        //緊急連絡人1地址－區鄉鎮
        function changeEc1Area() {
            var CityValue = document.getElementById("Ec1City").selectedIndex; //取得被選取的城市
            var AreaValue = document.getElementById("Ec1Area").selectedIndex; //取得被選取的區鄉鎮
            var Zip = document.getElementById("Ec1Area").options[AreaValue].value.substr(0, 3);
            var Zone = document.getElementById("Ec1Area").options[AreaValue].value.substring(3);
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec1adr").value = Zip + document.getElementById("Ec1City").options[CityValue].value + Zone; //取得並顯示郵遞區號
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec1adr").focus();
            var val = this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec1adr").value; //store the value of the element
            this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec1adr").value = ''; //clear the value of the element
            this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec1adr").value = val; //set that value back.  
        };
        //緊急連絡人2地址－區鄉鎮
        function changeEc2Area() {
            var CityValue = document.getElementById("Ec2City").selectedIndex; //取得被選取的城市
            var AreaValue = document.getElementById("Ec2Area").selectedIndex; //取得被選取的區鄉鎮
            var Zip = document.getElementById("Ec2Area").options[AreaValue].value.substr(0, 3);
            var Zone = document.getElementById("Ec2Area").options[AreaValue].value.substring(3);
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec2adr").value = Zip + document.getElementById("Ec2City").options[CityValue].value + Zone; //取得並顯示郵遞區號
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec2adr").focus();
            var val = this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec2adr").value; //store the value of the element
            this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec2adr").value = ''; //clear the value of the element
            this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec2adr").value = val; //set that value back.  
        };
        //緊急連絡人3地址－區鄉鎮
        function changeEc3Area() {
            var CityValue = document.getElementById("Ec3City").selectedIndex; //取得被選取的城市
            var AreaValue = document.getElementById("Ec3Area").selectedIndex; //取得被選取的區鄉鎮
            var Zip = document.getElementById("Ec3Area").options[AreaValue].value.substr(0, 3);
            var Zone = document.getElementById("Ec3Area").options[AreaValue].value.substring(3);
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec3adr").value = Zip + document.getElementById("Ec3City").options[CityValue].value + Zone; //取得並顯示郵遞區號
            document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec3adr").focus();
            var val = this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec3adr").value; //store the value of the element
            this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec3adr").value = ''; //clear the value of the element
            this.document.getElementById("ContentPlaceHolder1_TabIPInfo_TabPanel2_txtec3adr").value = val; //set that value back.  
        };

        function savingMsg() {
            $.blockUI({
                message: '<h2>開始儲存...</h2>', css: {
                    border: 'none',
                    padding: '0',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '5px',
                    '-moz-border-radius': '5px',
                    opacity: .5,
                    color: '#fff',
                    top: '550px'
                }
            });
            setTimeout($.unblockUI, 3000);
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
            }, 3000);
        };

        $("#effect1").hide();
    </script>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
    <script src="../../Scripts/jquery-ui-1.8.24.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-2.0.3.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="../../Scripts/AddressSelectList.js" type="text/javascript"></script>

    <style type="text/css">
        .style29 {
            width: 100%;
        }

        .style32 {
            width: 100px;
        }

        .style18 {
            font-size: large;
            color: red;
            font-weight: bold;
        }

        .mainPanel {
            padding: 0,0,1px,0;
            margin: 0,0,1px,0;
            color: #000000;
            background: #FFFFFF;
            border: solid 2px #006600;
            border-top-width: 0;
        }

        .style2 {
            width: 100%;
            height: 30px;
        }

        .style3 {
            width: 50%;
            padding: 0;
            margin: 0 auto;
        }

        .style4 {
            width: 50%;
            padding: 0;
            margin: 0 auto;
        }

        .functionitem1 {
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

        .functionitem2 {
            padding: 0 0 2px 0;
            margin: 0px;
            text-align: center;
            background: #F5F5F5;
            border: solid 1px #006600;
            border-top-width: 1px;
            border-bottom-width: 2px;
            height: 22px;
        }

        .style3 div {
            height: 1px;
            overflow: hidden;
            background: #006600;
            border-left: solid 1px #000;
            border-right: solid 1px #000;
        }

        .style4 div {
            height: 1px;
            overflow: hidden;
            background: #006600;
            border-left: solid 1px #000;
            border-right: solid 1px #000;
        }

        .row1 {
            margin: 0 5px;
            background: #000;
        }

        .row2 {
            margin: 0 3px;
            border: 0 2px;
        }

        .row3 {
            margin: 0 2px;
        }

        .row4 {
            margin: 0 1px;
            height: 2px;
        }

        .TextboxWatermark {
            color: #ACA899;
        }

        #PanelU3 {
            width: 431px;
        }


        .style79 {
            height: 28px;
        }

        .toggler {
            position: fixed;
            top: 180px;
            right: 350px;
            width: 200px;
            height: 20px;
        }

        #effect {
            position: absolute;
            top: 180px;
            right: 350px;
            width: 200px;
            height: 20px;
            padding: 0.4em;
            position: relative;
        }

            #effect p {
                margin: 0;
                padding: 0.2em;
                text-align: center;
            }

        .ui-widget-content {
            border: 1px solid #aaaaaa;
            background: #FFCC22;
            color: #222222;
        }

        .toggler1 {
            position: fixed;
            top: 180px;
            right: 350px;
            width: 200px;
            height: 20px;
        }

        #effect1 {
            position: absolute;
            top: 180px;
            right: 350px;
            width: 240px;
            /*height: 50px;*/
            padding: 0.4em;
            position: relative;
        }

            #effect1 p {
                height: 100%;
                margin: 0;
                padding: 0.2em;
                text-align: center;
            }

        .ui-widget-content1 {
            border: 1px solid #aaaaaa;
            background: #FF3333;
            color: #FFFFFF;
        }

        .style80 {
            width: 96px;
        }

        .style84 {
            width: 42px;
        }

        .style87 {
            width: 56px;
        }

        .accordion_headings {
            padding: 5px;
            background: #99CC00;
            color: #FFFFFF;
            border: 1px solid #FFF;
            cursor: pointer;
            font-weight: bold;
        }

            .accordion_headings:hover {
                background: #00CCFF;
            }

        .accordion_child {
            padding: 10px;
            background: #EEE;
        }

        .header_highlight {
            padding: 5px;
            background: #00CCFF;
            color: #FFFFFF;
            font-weight: bold;
        }

        .disabledcolor {
            /*color: graytext;
            background-color: buttonface;*/
            background: #696969;
            color: #F5F5F5;
        }
    </style>
</asp:Content>
