<%@ Page Title="" Language="C#" MasterPageFile="~/Accreditation.Master" AutoEventWireup="true"
    CodeBehind="DocumentLibrary.aspx.cs" Inherits="longtermcare.Accreditations.DocumentLibrary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Scripts/vakata-jstree-7a976d1/dist/themes/default/style.min.css" />
    <style type="text/css">
        div#effect
        {
            display: none;
            width: 200px;
            height: 20px;
            padding: 0.4em;
            position: absolute;
            text-align: center;
            font-weight: bold;
            border-radius: 3px;
            z-index: 103;
            color: #222222;
        }
        .effect
        {
            background: #FFCC22;
            border: 2px solid #C7A01B;
        }
        .effect1
        {
            background: #FF3333;
            border: 2px solid #C62B2B;
        }
        input.uploader
        {
            height: 200px;
            width: 272.25px;
            z-index: 102;
            opacity: 0;
        }
        div#uploader_cover
        {
            height: 200px;
            width: 272.25px;
            border: 2px dashed rgba(235,143,0,0.6);
            border-radius: 10px;
            text-align: center;
            font-size: x-large;
            font-weight: bold;
            color: #cccccc;
            position: absolute;
        }
        input#search_q
        {
            border-radius: 4px;
            padding: 4px;
            margin: 8px 0px;
        }
        fieldset
        {
            border-radius: 4px;
            padding: 10px 25px;
            margin: 10px 0px;
            background: #F9F9F9;
            font-weight: bold;
        }
        #foldername
        {
            font-size: x-large;
        }
        tr.tr_header
        {
            font-weight: bold;
            background: #CCCCCC;
        }
        tr.fhover:hover
        {
            background-color: #B5CBD0;
            cursor: cell;
        }
        tr.selected
        {
            background-color: #B5CBD0;
        }
        div.btn
        {
            display: inline-block;
            background: #C7F593;
            padding: 4px;
            margin: 2px;
            border: 1px solid seagreen;
            border-radius: 4px;
            box-shadow: 1px 2px 4px #001F00;
        }
        div.btn:hover
        {
            background: #F9F9F9;
            box-shadow: 2px 4px 12px #001F00;
            cursor: pointer;
        }
        div#x
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
        }
        table
        {
            border-collapse: collapse;
            border-radius: 3px;
        }
        td
        {
            padding: 3px 6px;
            border: 2px solid #eeeeee;
        }
        select
        {
            padding: 5px;
            border-radius: 4px;
            background: #f9f9f9;
            appearance: none;
            -moz-appearance: none;
            -webkit-appearance: none;
        }
        select::-ms-expand
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 25%; padding-right: 12px; border-right: 3px double #999999; display: inline-block;">
        <input type="text" id="search_q" placeholder="搜尋" />
        <div id='newaroot' class='btn' style='display: none'>
            New folder</div>
        <div id="jstree">
        </div>
        <div id='uploader_cover'>
        </div>
        <input type='file' class='uploader' draggable='true' />
    </div>
    <div style="display: inline-block; position: absolute; padding: 0px 10px;">
        <fieldset id='folder'>
            <legend id="foldername">請先選擇資料夾</legend>
        </fieldset>
        <select id='eva'>
        </select>
        <select id='subnd'>
        </select>
        <div id='pass' class='btn'>
            指派檔案
        </div>
    </div>
    <div id="effect">
    </div>
    <div id="x">
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

        var nodeid = "";
        $(function () {
            treetree();
            function treetree() {
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
                        $("#jstree").append(real_tree);

                        var to = false;
                        $('#search_q').keyup(function () { //jsTree search
                            if (to) { clearTimeout(to); }
                            to = setTimeout(function () {
                                var v = $('#search_q').val();
                                $('#jstree').jstree(true).search(v);
                            }, 150);
                        });
                        var cr = false; //, "dnd"
                        var hasnode = false;
                        $("#jstree").jstree({
                            "core": {
                                "initially_open": ["j1_1"],
                                "animation": 50,
                                "check_callback": function (op) {
                                    var nodeid, root;
                                    if (op === "delete_node") { //刪除時檢核
                                        $("#jstree").find("li").each(function () {
                                            if ($(this).attr("aria-selected") === "true") {
                                                nodeid = $(this).attr("nodeid");
                                                if (!$(this).hasClass("jstree-leaf"))
                                                    hasnode = true;
                                                if ($(this).attr("par") == "0")
                                                    root = true;
                                            }
                                        });

                                        if (root) {
                                            runEffect("不能刪除根目錄", true);
                                            return false;
                                        }

                                        if (hasnode) { //有子節點
                                            runEffect("不能刪除", true);
                                            hasnode = !hasnode;
                                            return false;
                                        }
                                        else { //沒有子節點
                                            var hasfile = false;
                                            if (confirm("是否確定要刪除?")) {
                                                $.ajax({
                                                    url: 'SqlHandler.ashx',
                                                    data: {
                                                        method: "delete_node",
                                                        target: nodeid
                                                    },
                                                    async: false, type: "POST", dataType: 'text',
                                                    success: function (str) {
                                                        if (str) { //節點內有檔案不能刪除
                                                            runEffect(str, true);
                                                            hasfile = true;
                                                        }
                                                        else  //節點內沒有檔案
                                                            runEffect('刪除成功');
                                                    }
                                                });
                                            }
                                            else {
                                                return false;
                                            }
                                            if (hasfile)
                                                return false;
                                        }

                                    }
                                },
                                "themes": { "stripes": true, "dots": false }
                            },
                            "plugins": ["contextmenu", "search", "changed"]
                        }).on("create_node.jstree", function (e, data) {
                            cr = true;
                        }).on("rename_node.jstree", function (e, data) {
                            if (cr) { //create (連續新增會出事)
                                cr = !cr;
                                var par, seq, id;
                                id = "#" + data.node.id;
                                par = $(id).closest("ul").closest("li").attr("nodeid");
                                seq = $(id).closest("ul").children("li").length;
                                $.ajax({
                                    url: 'SqlHandler.ashx',
                                    data: {
                                        method: "create_node",
                                        filetype: par,
                                        filesize: seq,
                                        filename: data.node.text
                                    },
                                    async: false, type: "POST", dataType: 'text',
                                    success: function (nodeid) {
                                        $("#jstree").empty().removeAttr("class");
                                        treetree();
                                    }
                                });
                                runEffect('新增成功');
                            }
                            else { //rename
                                $.ajax({
                                    url: 'SqlHandler.ashx',
                                    data: {
                                        method: "rename_node",
                                        target: $("#" + data.node.id).attr("nodeid"),
                                        filename: data.node.text
                                    },
                                    async: false, type: "POST", dataType: 'text'
                                });
                                runEffect('重新命名成功');
                            }
                        }).on("delete_node.jstree", function (e, data) {
                        }).on("show_contextmenu.jstree", function (e, data) { //隱藏剪下貼上功能
                            $("ul.vakata-context").children("li").eq(4).hide();
                            $("ul.vakata-context").children("li").eq(5).hide();
                        }).on("changed.jstree", function (e, data) {
                            nodeid = $("#" + data.node.id).attr("nodeid");
                            $.ajax({ //讀取資料夾內檔案
                                url: 'SqlHandler.ashx',
                                data: {
                                    method: "reload_files",
                                    target: nodeid
                                },
                                asnyc: false,
                                dataType: 'text',
                                type: 'POST',
                                success: function (str) {
                                    var s = "<legend id='foldername'>" + data.node.text + "</legend>";
                                    afterreload(s, str);
                                }
                            });
                        });
                    }
                });
            }
            $("input.uploader").on("change", function () { Upload(nodeid); }); //點擊或拖放上傳檔案
            $("div#uploader_cover").css("line-height", $("input.uploader").height() + "px").html("點擊或拖放上傳檔案");
            $("#effect").position({ of: "#page" });
            $("#newaroot").on("click", function () { //已移除
                var par, seq;
                par = "0";
                seq = $("#jstree").children("ul").eq(0).children("li").length + 1;
                $.ajax({
                    url: 'SqlHandler.ashx',
                    data: {
                        method: "create_node",
                        filetype: "0",
                        filesize: seq,
                        filename: "New Root"
                    },
                    async: false, type: "POST", dataType: 'text',
                    success: function (nodeid) {
                        //                        $(id).attr("nodeid", nodeid);
                        //                        $(id).attr("par", par);
                        //                        $(id).attr("seq", seq);
                    }
                });
                runEffect('新增成功');
            });
            $.ajax({ //讀取評鑑
                url: 'SqlHandler.ashx',
                data: {
                    method: "select_eva"
                },
                type: "POST",
                dataType: 'text',
                async: false,
                success: function (data) {
                    $("#eva").append(data);
                    subnd();
                }
            });
            function subnd() {
                $.ajax({ //讀取子項次
                    url: 'SqlHandler.ashx',
                    data: {
                        method: "select_subnd",
                        str: $("#eva").find(":selected").attr("year")
                    },
                    type: "POST",
                    dataType: 'text',
                    async: false,
                    success: function (data) {
                        $("#subnd").empty();
                        $("#subnd").append(data);
                    }
                });
            }
            $("#eva").on("change", subnd);
            $("#pass").on("click", function () { //指派檔案給評鑑子項目
                var idstr = "";
                $("#folder").find("tr.fhover").each(function () {
                    if ($(this).hasClass("selected")) {
                        idstr += $(this).attr("id") + " ";
                    }
                });
                if (!idstr) {
                    runEffect("尚未選擇檔案", true);
                    return false;
                }

                $.ajax({
                    url: 'SqlHandler.ashx',
                    data: {
                        method: "copy_file",
                        evaid: $("#eva").val(),
                        subndid: $("#subnd").val(),
                        str: idstr
                    },
                    type: "POST",
                    dataType: 'text',
                    async: false,
                    success: function (data) {
                        runEffect("指派檔案成功");
                    }
                });
            });
        });

        function Upload(nodeid) {
            if (nodeid == "") {
                runEffect("請先選取資料夾", true);
                return false;
            }
            var filelist = event.target.files;
            var file = filelist[0];
            var formData = new FormData(event.form);
            formData.append(file.name, file);
            $.ajax({
                //檔案上傳到server
                url: 'SqlHandler.ashx',
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                type: 'POST',
                success: function (data) {
                    //寫入資料庫
                    $.ajax({
                        url: 'SqlHandler.ashx',
                        data: {
                            method: "upload",
                            filename: file.name,
                            filetype: file.type,
                            filesize: file.size,
                            filelastModifiedDate: file.lastModifiedDate.toLocaleDateString(),
                            target: nodeid
                        },
                        dataType: 'text',
                        type: 'POST',
                        success: function (data) {
                            runEffect("上傳成功");
                            $.ajax({
                                url: 'SqlHandler.ashx',
                                data: {
                                    method: "reload_files",
                                    target: nodeid
                                },
                                asnyc: false,
                                dataType: 'text',
                                type: 'POST',
                                success: function (str) {
                                    afterreload("", str)
                                }
                            });
                        }
                    });
                }
            });
        }

        function afterreload(s, str) {
            if (s != "")
                $("#folder").html("").append(s + str);
            else {
                $("#folder").children("table").remove();
                $("#folder").append(str);
            }
            $("tr.fhover").on("click", function () {
                if (!$(this).hasClass("selected"))
                    $(this).attr("class", "fhover selected");
                else
                    $(this).attr("class", "fhover");
            }).hover(function () {
                var id = $(this).attr("id");
                $("div#x").show().position({ my: "left", at: "right", of: $(this) }).data("id", id);
                $("div#x").hover(function () {
                    $(this).show();
                    $(this).off("click");
                    $(this).on("click", deletex);
                }, function () { $(this).hide(); });
            }, function () {
                $("div#x").hide();
            });
        }

        function deletex() { //還沒寫reload畫面
            if (confirm("是否確定要刪除?")) {
                $.ajax({
                    url: 'SqlHandler.ashx',
                    data: {
                        method: "delete_file",
                        target: $("div#x").data("id")
                    },
                    type: "POST",
                    dataType: 'text',
                    async: false,
                    success: function (data) {
                        runEffect("刪除成功");
                    }
                });
            }
        }

        function runEffect(str, err) {
            $("#effect").hide().removeClass();
            if (err) //紅框
                $("#effect").addClass("effect1");
            else //黃框
                $("#effect").addClass("effect");
            var selectedEffect = "drop";
            $("#effect").html(str);
            $("#effect").show(selectedEffect, 0, callback);
        };

        function callback() {
            setTimeout(function () {
                $("#effect:visible").fadeOut().removeClass();
            }, 3000);
        };

    </script>
</asp:Content>
