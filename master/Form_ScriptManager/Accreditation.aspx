<%@ Page Title="" Language="C#" MasterPageFile="~/Accreditation.Master" AutoEventWireup="true"
    CodeBehind="Accreditation.aspx.cs" Inherits="longtermcare.WebForm11" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Scripts/jquery-ui-1.11.4/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../Scripts/vakata-jstree-7a976d1/dist/themes/default/style.min.css" />
    <style type="text/css">
        #effect
        {
            display: none;
            width: 200px;
            height: 20px;
            padding: 0.4em;
            position: absolute;
            text-align: center;
            font-weight: bold;
            border: 2px solid #C7A01B;
            border-radius: 3px;
            z-index: 103;
            background: #FFCC22;
            color: #222222;
        }
        .effect1
        {
            background: #FF3333;
            border: 2px solid #C62B2B;
        }
        #filesfolder
        {
            width: 353px;
            display: none;
            position: absolute;
            font-weight: bold;
            border-radius: 10px;
            border: 2px dashed #FFCC22;
            background: #FFEEB5;
            z-index: 103;
            color: #AAA;
            padding: 10px 16px;
            cursor: pointer;
        }
        #files
        {
            position: absolute;
            font-weight: bold;
            background: #beebff;
            z-index: 103;
        }
        #filesfolder:hover
        {
            border: 2px dashed #C7A01B;
            background: #FFCC22;
            color: #363636;
        }
        .btn
        {
            background: #CCCCFF;
            display: inline-block;
            width: 155px;
            text-align: center;
            padding: 10px;
            margin: 15px 3px;
            border: 2px solid cornflowerblue;
            border-radius: 3px;
            font-weight: bold;
            box-shadow: 0px 2px 6px #3162BA;
        }
        .btn:hover, .btn_selected
        {
            background: #000088;
            color: White;
            cursor: pointer;
        }
        .btnprint
        {
            background: #c9fe94;
            width: auto;
            padding: 5px;
            border: 2px solid #7c9e5a;
            box-shadow: 0px 2px 6px #94ba6d;
        }
        .btnprint:hover
        {
            background: #5f7944;
        }
        
        .acc_div
        {
            height: 1200px;
        }
        tr#title
        {
            font-weight: bold;
            background: #000088;
            color: White;
        }
        tr.tr_header
        {
            font-weight: bold;
            background: #CCCCCC;
        }
        table
        {
            border-collapse: collapse;
            border-radius: 3px;
        }
        tr.hover:hover
        {
            background-color: lavender;
            cursor: cell;
        }
        tr.fhover:hover
        {
            background-color: lavender;
            cursor: default;
        }
        td
        {
            padding: 3px 6px;
            border: 2px solid #eeeeee;
        }
        td.file:not(:empty)
        {
            cursor: pointer;
        }
        .rad
        {
            background: #CCCCFF;
            display: inline-block;
            text-align: center;
            padding: 6px;
            margin: 2px 2px;
            border: 2px solid cornflowerblue;
            border-radius: 3px;
            font-weight: bold;
            font-size: medium;
            box-shadow: 0px 2px 6px #3162BA;
        }
        .rad:hover, .rad_selected
        {
            background: #000088;
            color: White;
            cursor: pointer;
            box-shadow: 0px 1px 3px #3162BA;
        }
        textarea.ta_expl
        {
            border-radius: 2px;
            margin-bottom: 18px;
            resize: vertical;
        }
        fieldset
        {
            border-radius: 12px;
            margin: 6px 0px;
            background: #F9F9F9;
        }
        #showscore
        {
            width: 95px;
            color: beige;
            padding: 10px 40px;
            display: inline-block;
            border-radius: 25px;
            font-weight: bold;
            background: darkslateblue;
        }
        #savescore
        {
            color: beige;
            padding: 10px 40px;
            display: inline-block;
            border-radius: 25px;
            font-weight: bold;
            cursor: pointer;
            background: darkslateblue;
        }
        #new_eva
        {
            background: #f9f9f9;
            display: inline-block;
            text-align: center;
            padding: 10px;
            margin: 5px;
            border: 2px solid #de484d;
            border-radius: 3px;
            font-weight: bold;
            cursor: pointer;
            box-shadow: 0px 2px 6px #ab151a;
        }
        #eva
        {
            padding: 20px;
            margin: 20px;
            font-weight: bold;
        }
        #new_eva:hover
        {
            background: #ffffff;
            border: 2px solid #FF0008;
        }
        select#evatype, select#evayear, input#evaname
        {
            padding: 3px;
            margin: 4px 6px;
            width: 70px;
            height: 30px;
            border-radius: 5px;
            background: #f9f9f9;
        }
        select
        {
            padding: 1px 3px;
            border-radius: 5px;
            appearance: none;
            -moz-appearance: none;
            -webkit-appearance: none;
        }
        select::-ms-expand
        {
            display: none;
        }
        input#evaname, input#evaexpl, input#casemaneger
        {
            width: 150px;
            height: 20px;
            border-radius: 5px;
            padding: 3px;
            margin: 4px 6px;
            background: #f9f9f9;
        }
        div#items
        {
            display: inline-block;
            width: 710px;
        }
        input.uploader
        {
            float: left;
            width: 385px;
            z-index: 102;
            position: absolute;
            opacity: 0;
        }
        div#uploader_cover
        {
            width: 385px;
            border: 2px dashed rgba(235,143,0,0.6);
            border-radius: 10px;
            position: absolute;
            text-align: center;
            font-size: xx-large;
            font-weight: bold;
            color: #cccccc;
            display: inline-block;
        }
        div#deletemain
        {
            display: none;
            background-image: url('X_icon.png');
            background-repeat: no-repeat;
            background-size: contain;
            background-position: right;
            height: 20px;
            width: 20px;
            position: absolute;
            z-index: 102;
            cursor: pointer;
        }
        div#check_icon
        {
            display: none;
            background-image: url('check_icon.png');
            background-repeat: no-repeat;
            background-size: contain;
            background-position: right;
            height: 20px;
            width: 20px;
            position: absolute;
            z-index: 104;
            cursor: pointer;
        }
        #sortable1, #sortable2
        {
            border: 1px solid #b5cbd0;
            min-height: 20px;
            list-style-type: none;
            margin: 20px 0px;
            padding: 5px 0 0 0;
            display: inline-table;
            margin-right: 10px;
        }
        #sortable1 li, #sortable2 li
        {
            margin: 0 5px 5px 5px;
            padding: 5px;
            font-size: 1.2em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="main_status">
        <div id="eva">
            <div id="new_eva">
                新增評鑑</div>
        </div>
    </div>
    <div id="subnd" style="display: none">
        <div id="acc_A" class="btn class ">
            行政制度與經營管理</div>
        <div id="acc_B" class="btn class ">
            生活照護與專業服務</div>
        <div id="acc_C" class="btn class ">
            環境設施與安全維護</div>
        <div id="acc_D" class="btn class ">
            權益保障</div>
        <div id="acc_E" class="btn class ">
            改進創新</div>
        <div id="printfiles" class="btn btnprint">
            列印佐證資料</div>
        <div id="acc_div" class="acc_div">
        </div>
        <div id="dialog">
        </div>
        <div id="Div1">
        </div>
    </div>
    <div id="effect">
    </div>
    <div id='deletemain'>
    </div>
    <div id='check_icon'>
    </div>
    <div id='filesfolder'>
        開啟文件庫
    </div>
    <script type="text/javascript">
        //render tree
        var _queryTreeSort = function (options) {
            var cfi, e, i, id, o, pid, rfi, ri, thisid, _i, _j, _len, _len1, _ref, _ref1;
            id = options.id || "id";
            pid = options.parentid || "parentid";
            ri = [];
            rfi = {};
            cfi = {};
            o = [];
            _ref = options.q;
            for (i = _i = 0, _len = _ref.length; _i < _len; i = ++_i) {
                e = _ref[i];
                rfi[e[id]] = i;
                if (cfi[e[pid]] == null) {
                    cfi[e[pid]] = [];
                }
                cfi[e[pid]].push(options.q[i][id]);
            }
            _ref1 = options.q;
            for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
                e = _ref1[_j];
                if (rfi[e[pid]] == null) {
                    ri.push(e[id]);
                }
            }
            while (ri.length) {
                thisid = ri.splice(0, 1);
                o.push(options.q[rfi[thisid]]);
                if (cfi[thisid] != null) {
                    ri = cfi[thisid].concat(ri);
                }
            }
            return o;
        };

        var _makeTree = function (options) {
            var children, e, id, o, pid, temp, _i, _len, _ref;
            id = options.id || "id";
            pid = options.parentid || "parentid";
            children = options.children || "children";
            temp = {};
            o = [];
            _ref = options.q;
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                e = _ref[_i];
                e[children] = [];
                temp[e[id]] = e;
                if (temp[e[pid]] != null) {
                    temp[e[pid]][children].push(e);
                } else {
                    o.push(e);
                }
            }
            return o;
        };

        var _renderTree = function (tree) {
            var e, html, _i, _len;
            html = "<ul>";
            for (_i = 0, _len = tree.length; _i < _len; _i++) {
                e = tree[_i];
                html += "<li nodeid='" + e.id + "' seq='" + e.seq + "' par='" + e.parentid + "'>" + e.name;
                if (e.children != null) {
                    html += _renderTree(e.children);
                }
                html += "</li>";
            }
            html += "</ul>";
            return html;
        };

        $(function () {
            main_status();
            new_eva_options();
            $(".class").on("click", load_accdiv);
            $("#new_eva").on("click", function () {
                if ($("#evaname").val().length > 0 && $("#casemaneger").val().length > 0) { //新增評鑑
                    $.ajax({
                        url: 'SqlHandler.ashx',
                        data: {
                            method: "new_eva",
                            target: $("#evatype").val(),
                            filesize: $("#evayear").val(),
                            filename: $("#evaname").val(),
                            str: $("#evaexpl").val(),
                            str2: $("#casemaneger").val()
                        },
                        type: "POST",
                        dataType: 'text',
                        async: false,
                        success: function (data) {
                            $("#main").remove();
                            main_status();
                            runEffect("新增成功");
                        }
                    });
                }
            });
            $("#effect").position({ of: "#page" });

            $("#filesfolder").on("click", function () { //預載文件庫畫面
                $.ajax({ //jsTreey loding
                    url: 'SqlHandler.ashx',
                    data: {
                        method: "load_tree"
                    },
                    type: "POST",
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        var result = _queryTreeSort({ q: data });
                        var tree = _makeTree({ q: result });
                        var real_tree = _renderTree(tree);
                        $("#filesfolder").empty().append(real_tree);
                    }
                });
                $("#filesfolder").off("click").jstree({
                    "core": {
                        "animation": 50,
                        "themes": { "stripes": true, "dots": false }
                    },
                    "plugins": ["changed"]
                }).on("changed.jstree", function (e, data) {
                    nodeid = $("#" + data.node.id).attr("nodeid");
                    $.ajax({ //讀取資料夾內檔案
                        url: 'SqlHandler.ashx',
                        data: {
                            method: "reload_files",
                            target: nodeid,
                            str: $("#subnd").data("year")
                        },
                        asnyc: false,
                        dataType: 'text',
                        type: 'POST',
                        success: function (str) {
                            $("table#files").remove();
                            $("#page").append(str);
                            $("i.jstree-icon").off("click").on("click", function () { $("table#files").remove(); });
                            $(".detail").hide();
                            $("table#files").position({ my: "left top", at: "right top", of: "#" + data.node.id + "_anchor" });
                            $("tr.fhover").hover(function () {
                                var id = $(this).attr("id");
                                $("div#check_icon").show().position({ my: "left", at: "right", of: $(this) }).data("id", id);
                                $("div#check_icon").hover(function () {
                                    $(this).show().off("click").on("click", copy_file);
                                }, function () { $(this).hide(); });
                            }, function () {
                                $("div#check_icon").hide();
                            }); ;

                        }
                    });
                });
            });
        });

        function copy_file() {//指派檔案
            $.ajax({
                url: 'SqlHandler.ashx',
                data: {
                    method: "copy_file",
                    subndid: $("#dialog").data("subndeid"),
                    evaid: $("#subnd").data("evaid"),
                    str: $("div#check_icon").data("id"),
                    filetype: $("#subnd").data("year")
                },
                type: "POST",
                dataType: 'text',
                async: false,
                success: function (data) {
                    runEffect("指派檔案成功");
                    $.ajax({
                        url: 'SqlHandler.ashx',
                        data: {
                            method: "reload_files",
                            subndid: $("#dialog").data("subndeid"),
                            evaid: $("#subnd").data("evaid"),
                            str: $("#subnd").data("year")
                        },
                        asnyc: false,
                        dataType: 'text',
                        type: 'POST',
                        success: function (data) {
                            $("#his_files").empty().append(data);
                            $("td.file").off("click").on("click", OpenFile);
                            $("tr.fhover").hover(function () {
                                var id = $(this).attr("id");
                                $("div#deletemain").show().position({ my: "left", at: "right", of: $(this) }).data("id", id);
                                $("div#deletemain").hover(function () {
                                    $(this).show().off("click").on("click", deletex);
                                }, function () { $(this).hide(); });
                            }, function () {
                                $("div#deletemain").hide();
                            });
                            $("div.btn_selected").click();
                        }
                    });
                }
            });
        }

        function main_status() {//評鑑紀錄
            $.ajax({
                url: 'SqlHandler.ashx',
                data: {
                    method: "main_status"
                },
                type: "POST",
                dataType: 'text',
                async: false,
                success: function (data) {
                    $("#main_status").append(data);
                    $("tr.eva").on("click", function () { Eva_selected($(this).attr("id"), $(this).attr("year")); });
                    $("tr.eva.hover").hover(function () {
                        var id = $(this).attr("id");
                        $("div#deletemain").show().position({ my: "left", at: "right", of: $(this) }).data("id", id);
                        $("div#deletemain").hover(function () {
                            $(this).show().off("click").on("click", deletemain);
                        }, function () { $(this).hide(); });
                    }, function () {
                        $("div#deletemain").hide();
                    });
                }
            });
        }

        function deletemain() {//刪除評鑑紀錄
            if (confirm("是否確定要刪除?")) {
                $.ajax({
                    url: 'SqlHandler.ashx',
                    data: {
                        method: "deletemain",
                        target: $("div#deletemain").data("id")
                    },
                    type: "POST",
                    dataType: 'text',
                    async: false,
                    success: function (data) {
                        $("#main").remove();
                        main_status();
                        runEffect("刪除成功");
                    }
                });
            }
        }

        function new_eva_options() {//評鑑類型索引
            $.ajax({
                url: 'SqlHandler.ashx',
                data: {
                    method: "evatype"
                },
                type: "POST",
                dataType: 'text',
                async: false,
                success: function (data) {
                    $("#eva").append(data);
                }
            });
            $.ajax({//評鑑年份索引
                url: 'SqlHandler.ashx',
                data: {
                    method: "evayear"
                },
                type: "POST",
                dataType: 'text',
                async: false,
                success: function (data) {
                    $("#eva").append(data);
                }
            });
        }

        function load_accdiv() {//生成項目頁面
            $("#main_status").hide();
            $(".acc_div").empty();
            $(".acc_div").css("height", "1000px");
            $(".class").attr("class", "btn class");
            $(this).attr("class", "btn class btn_selected");
            var target_div, index;
            target_div = $("#acc_div");
            switch ($.trim($(this).html())) {
                case "行政制度與經營管理":
                    index = "1";
                    break;
                case "生活照護與專業服務":
                    index = "2";
                    break;
                case "環境設施與安全維護":
                    index = "3";
                    break;
                case "權益保障":
                    index = "4";
                    break;
                case "改進創新":
                    index = "5";
                    break;
            }
            $.ajax({//評鑑項目索引
                url: 'SqlHandler.ashx',
                data: {
                    method: "acc_div",
                    target: index,
                    evaid: $("#subnd").data("evaid"),
                    str: $("#subnd").data("year")
                },
                type: "POST",
                dataType: 'text',
                asnyc: false,
                success: function (data) {
                    target_div.append(data).css("height", "auto");
                    $("td.file").off("click").on("click", OpenFile);
                    $("td.edit").off("click").on("click", function () {
                        $("#dialog").data("subndeid", $(this).closest("tr").children("td").eq(0).html())
                        OpenCell($("#dialog").data("subndeid"));
                    });
                    $("td.emp").off("change").on("change", chargeemp);
                    $("#printfiles").off("click").on("click", printfiles);
                }
            });
        }

        function printfiles() { //佐證資料列印
            $.ajax({
                url: 'SqlHandler.ashx',
                data: {
                    method: "printfiles",
                    target: $(this).children("select").val(),
                    evaid: $("#subnd").data("evaid")
                },
                async: false,
                type: "POST",
                dataType: 'text',
                success: function (data) {
                    $("#dialog").empty().append(data).dialog({
                        title: '列印',
                        width: 1024,
                        modal: true,
                        draggable: false,
                        resizable: false
                    }).append("<div id='e' class='btn btnprint'>列印</div>");
                    $("#sortable1, #sortable2").sortable({
                        items: "li:not(.ui-state-disabled)",
                        connectWith: ".connectedSortable",
                        placeholder: "ui-state-highlight"
                    }).disableSelection();
                    $("#e").off("click").on("click", function () {
                        $(window).block({ message: '檔案轉換中, 請稍候' });
                        var count = 0;
                        for (var i = 1; i < $("#sortable1").children("li").length; i++) {
                            var dlLink = "Accreditation.aspx?mode=print&filename=" + $("#sortable1").children("li").eq(i).attr("name");
                            var $ifrm = $("<iframe style='display:none' />");
                            $ifrm.attr("src", dlLink);
                            $ifrm.appendTo("body");
                            $ifrm.load(function () {
                                count++;
                                if (count == $("#sortable1").children("li").length - 1) {
                                    dlLink = "Accreditation.aspx?mode=merge";
                                    $ifrm = $("<iframe style='display:none' />");
                                    $ifrm.attr("src", dlLink);
                                    $ifrm.appendTo("body");
                                    $ifrm.load(function () {
                                        dlLink = "Accreditation.aspx?mode=delete";
                                        $ifrm = $("<iframe style='display:none' />");
                                        $ifrm.attr("src", dlLink);
                                        $ifrm.appendTo("body");
                                        $ifrm.load(function () {
                                            $(window).unblock();
                                        });
                                    });
                                }
                            });
                        }

                    })
                }
            });
        }

        function chargeemp() { //項次負責人
            $.ajax({
                url: 'SqlHandler.ashx',
                data: {
                    method: "chargeemp",
                    target: $(this).children("select").val(),
                    str: $("#subnd").data("year"),
                    str2: $(this).closest("tr").children("td").eq(0).html()
                },
                async: false,
                type: "POST",
                dataType: 'text',
                success: function (data) {
                }
            });
        }

        function Upload(subndeid) {//上傳檔案
            var filelist = event.target.files;
            var file = filelist[0];
            var formData = new FormData(event.form);
            formData.append(file.name, file);
            $.ajax({//檔案上傳到server
                url: 'SqlHandler.ashx',
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                type: 'POST',
                success: function (data) {
                    $.ajax({//寫入資料庫
                        url: 'SqlHandler.ashx',
                        data: {
                            method: "upload",
                            filename: file.name,
                            filetype: file.type,
                            filesize: file.size,
                            filelastModifiedDate: file.lastModifiedDate.toLocaleDateString(),
                            subndid: subndeid,
                            evaid: $("#subnd").data("evaid"),
                            str: $("#subnd").data("year")
                        },
                        dataType: 'text',
                        type: 'POST',
                        async: false,
                        success: function (data) {
                            runEffect("上傳成功");
                            $.ajax({//檔案增刪後, 刷新畫面
                                url: 'SqlHandler.ashx',
                                data: {
                                    method: "reload_files",
                                    subndid: subndeid,
                                    evaid: $("#subnd").data("evaid"),
                                    str: $("#subnd").data("year")
                                },
                                asnyc: false,
                                dataType: 'text',
                                type: 'POST',
                                success: function (data) {
                                    $("#his_files").empty().append(data);
                                    $("td.file").off("click").on("click", OpenFile);
                                    $("tr.fhover").hover(function () {
                                        var id = $(this).attr("id");
                                        $("div#deletemain").show().position({ my: "left", at: "right", of: $(this) }).data("id", id);
                                        $("div#deletemain").hover(function () {
                                            $(this).show().off("click").on("click", deletex);
                                        }, function () { $(this).hide(); });
                                    }, function () {
                                        $("div#deletemain").hide();
                                    });
                                    $("div.btn_selected").click();
                                }
                            });

                        }
                    });
                }
            });
        }

        function OpenFile() {//下載檔案
            if ($(this).html().length > 0) {
                var dlLink = "Accreditation.aspx?mode=download&filename=" + $(this).html() + "&target=" + $(this).attr("t");
                var $ifrm = $("<iframe style='display:none' />");
                $ifrm.attr("src", dlLink);
                $ifrm.appendTo("body");
                $ifrm.load(function () {
                    $("body").append("<div>Failed to download <i>'" + dlLink + "'</i>!");
                });
            }
        }

        function Eva_selected(evaid, year) {//評鑑選取
            $("#main_status").hide();
            $("#subnd").show();
            $("#subnd").data("evaid", evaid).data("year", year);
            $("#acc_A").click();
        }

        function OpenCell(subndeid) {//生成評鑑項目畫面
            $.ajax({
                url: 'SqlHandler.ashx',
                data: {
                    method: "items",
                    subndid: subndeid,
                    evaid: $("#subnd").data("evaid"),
                    str: $("#subnd").data("year")
                },
                async: false,
                type: "POST",
                dataType: 'text',
                success: function (data) {
                    var str = data.split("^");
                    $("#dialog").empty().append(str[1]).dialog({
                        title: str[0],
                        height: 864,
                        width: 1152,
                        modal: true,
                        draggable: false,
                        resizable: false,
                        open: function () {
                            if (subndeid.trim() == "C1.17" && $("#subnd").data("year") == '2016') {//only for year 2016 & C1.17
                                $("#Div1").dialog({
                                    width: 280,
                                    modal: true,
                                    draggable: false,
                                    resizable: false,
                                    closeOnEscape: false,
                                    buttons: {
                                        自設廚房: function () {
                                            for (var i = 12; i < 24; i++) {
                                                $("#items").find("tr").eq(i).attr("class", "remove");
                                            }
                                            $("#items").find("tr.remove").remove();
                                            $("#subnd").data("t", "A");
                                            $(this).dialog("close");
                                        },
                                        膳食外包: function () {
                                            for (var i = 0; i < 12; i++) {
                                                $("#items").find("tr").eq(i).attr("class", "remove");
                                            }
                                            $("#items").find("tr.remove").remove();
                                            $("#subnd").data("t", "B");
                                            $(this).dialog("close");
                                        }
                                    },
                                    close: function () {
                                        $("input.uploader").css("height", $("div#items").height() - 24 + "px").off("change").on("change", function () { Upload($("#dialog").data("subndeid")); });
                                        $("div#uploader_cover").css("height", $("input.uploader").height()).css("line-height", $("input.uploader").height() + "px").css("left", $("input.uploader").position().left).html("點擊或拖放上傳檔案");
                                    }
                                }).siblings('div.ui-dialog-titlebar').remove();
                            }
                            else {
                                $("#subnd").data("t", "");
                            }
                        },
                        close: function () { $("#filesfolder, table#files").hide(); $("#subnd").data("t", ""); }
                    });

                    $(".rad").on("click", function () {
                        $(this).closest("tr").children("td").attr("class", "rad");
                        $(this).attr("class", "rad rad_selected");
                        rad_value($(this));
                        var s = "";
                        $("tr.tr_rad").each(function () {
                            s += ((!$(this).data("value")) ? "2 " : $(this).data("value").toString()) + " ";
                        });
                        Score(s, subndeid);
                        $("#dialog").data("itemid", str[2]);
                    });
                }
            });
            $(".selected").click();
            $("#savescore").off("click").on("click", savescore);
            $("input.uploader").css("height", $("div#items").height() - 24 + "px").off("change").on("change", function () { Upload($("#dialog").data("subndeid")); });
            $("div#uploader_cover").css("height", $("input.uploader").height()).css("line-height", $("input.uploader").height() + "px").css("left", $("input.uploader").position().left).html("點擊或拖放上傳檔案");
            $("td.file").off("click").on("click", OpenFile);
            $("tr.fhover").hover(function () {
                var id = $(this).attr("id");
                $("div#deletemain").show().position({ my: "left", at: "right", of: $(this) }).data("id", id);
                $("div#deletemain").hover(function () {
                    $(this).show().off("click").on("click", deletex);
                }, function () { $(this).hide(); });
            }, function () {
                $("div#deletemain").hide();
            });
            $("#filesfolder").show().position({ my: "right top", at: "right top", of: $("#uploader_cover") });
        }

        function deletex() {//刪除檔案
            if (confirm("是否確定要刪除?")) {
                $.ajax({
                    url: 'SqlHandler.ashx',
                    data: {
                        method: "delete_file",
                        target: $("div#deletemain").data("id")
                    },
                    type: "POST",
                    dataType: 'text',
                    async: false,
                    success: function (data) {
                        runEffect("刪除成功");
                        $.ajax({
                            url: 'SqlHandler.ashx',
                            data: {
                                method: "reload_files",
                                subndid: $("#dialog").data("subndeid"),
                                evaid: $("#subnd").data("evaid"),
                                str: $("#subnd").data("year")
                            },
                            asnyc: false,
                            dataType: 'text',
                            type: 'POST',
                            success: function (d) {
                                $("#his_files").empty().append(d);
                                $("td.file").off("click").on("click", OpenFile);
                                $("tr.fhover").hover(function () {
                                    var id = $(this).attr("id");
                                    $("div#deletemain").show().position({ my: "left", at: "right", of: $(this) }).data("id", id);
                                    $("div#deletemain").hover(function () {
                                        $(this).show().off("click").on("click", deletex);
                                    }, function () { $(this).hide(); });
                                }, function () {
                                    $("div#deletemain").hide();
                                });
                                $("div.btn_selected").click();
                            }
                        });

                    }
                });
            }
        }

        function rad_value(rad) {//重現已評分畫面
            var tr = rad.closest("tr");
            tr.children("td").each(function () {
                if ($(this).hasClass("rad_selected"))
                    tr.data("value", $(this).attr("value"));
            });
        }

        function Score(s, subndeid) {//計分
            $.ajax({
                url: 'SqlHandler.ashx',
                data: {
                    method: "score",
                    target: s,
                    subndid: subndeid.trim() + $("#subnd").data("t"), //C1.17 
                    str: $("#subnd").data("year")
                },
                async: false,
                type: "POST",
                dataType: 'text',
                success: function (data) {
                    $("#showscore").html("項次評分: " + data);
                    $("#savescore").html("儲存");
                    $("#showscore").show();
                    $("#savescore").show();
                    $("#dialog").data("s", s);
                    $("#dialog").data("subs", data);
                }
            });
        }


        function savescore() {//儲存分數
            var ss = "";
            $("textarea.ta_expl").each(function () {
                ss += ((!$(this).val()) ? "" : $(this).val()) + "^";
            });

            $.ajax({
                url: 'SqlHandler.ashx',
                data: {
                    method: "savescore",
                    target: $("#dialog").data("subs"),
                    subndid: $("#dialog").data("subndeid"),
                    evaid: $("#subnd").data("evaid"),
                    filesize: $("#dialog").data("s"),
                    filename: $("#dialog").data("itemid"),
                    str: $("textarea#eva_result_expl").val(),
                    str2: ss,
                    filetype: $("#subnd").data("year")
                },
                async: false,
                type: "POST",
                dataType: 'text',
                success: function (data) { runEffect("儲存成功"); }
            });
            $("div.btn_selected").click();

        }

        function runEffect(str) {//黃框
            $("#effect").hide();
            var selectedEffect = "drop";
            $("#effect").html(str);
            $("#effect").show(selectedEffect, 0, callback);
        };

        function callback() {
            setTimeout(function () {
                $("#effect:visible").fadeOut();
            }, 3000);
        };

    </script>
</asp:Content>
