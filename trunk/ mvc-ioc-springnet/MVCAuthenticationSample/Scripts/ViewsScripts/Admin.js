$(document).ready(function() {
    var guiManager = new GUIManager();
    /*
    Grid related operations
    */
    var colModel = [

            { name: 'UserName', index: 'UserName', width: 90 },
            { name: 'ID', index: 'ID', hidden: true }
            ]

    var colNames =
            [
            'UserName',
            'ID'
            ];

    var grid = jQuery("#userslist").jqGrid({

        url: 'Admin/GetAllUsers',
        datatype: "json",
        width: 800,
        height: 220,
        colNames: colNames,
        colModel: colModel,
        rowNum: 10,
        rowList: [5, 10, 20, 50],
        mtype: "POST",
        enableSearch: false,
        sortable: true,
        pager: "#pager",
        pgbuttons: true,
        pginput: true,
        sortname: 'UserName',
        viewrecords: true,
        rownumbers: true,
        sortorder: 'asc',
        gridview: true,
        caption: 'Users',
        jsonReader: {
            root: "Rows",
            page: "Page",
            total: "Total",
            records: "Records",
            repeatitems: false,
            userdata: "UserData",
            id: "Id"
        }
    });

    grid.jqGrid('navGrid', '#pager', { edit: false, add: false, del: false, search: false });

    /*
    Edit user
    */
    $('#editUser').click(function() {
        var userName = guiManager.GetValueByPropName('userslist', 'UserName');
        if (userName == null) {
            alert("Please select a user from the list.");
            return;
        }
        var userID = guiManager.GetValueByPropName('userslist', 'ID');
        if (userID == null) {
            alert("Please select a user from the list.");
            return;
        }
        $('#txtUserName')[0].value = userName;
        $('#txtPassword')[0].value = '******'; //treat in the server
        var dialogEditUsers = $('#dialogAmendUsers');
        dialogEditUsers.dialog({
            autoOpen: false,
            bgiframe: true,
            modal: true,
            title: 'Edit User',
            width: 370,
            buttons: {
                'Save': function() {
                    $.ajax({
                        type: "POST",
                        url: "Admin/EditUser",
                        data: {
                            id: userID,
                            username: $('#txtUserName')[0].value,
                            password: $('#txtPassword')[0].value
                        },
                        dataType: 'json',
                        error: function(xhr) {
                            alert(xhr.responseText);
                            jQuery("#userslist").jqGrid().trigger("reloadGrid");
                        },
                        success: function(data) {
                            if (data.code == "error") {
                                alert(data.error);
                                return;
                            }
                            else {
                                jQuery("#userslist").jqGrid().trigger("reloadGrid");
                                dialogEditUsers.dialog('destroy');
                            }
                        }
                    });
                },
                'Cancel': function() {
                    $(this).dialog('close');
                }
            }
        });
        dialogEditUsers.dialog('open');
    }
        );

    /*
    delete
    */
    $('#deleteUser').click(function() {
        var userID = guiManager.GetValueByPropName('userslist', 'ID');
        if (userID == null) {
            alert("Please select a user from the list.");
            return;
        }
        var rowid = jQuery("#userslist").jqGrid('getGridParam', 'selrow')
        jQuery("#userslist").delGridRow(rowid, { afterSubmit: processAddEdit, reloadAfterSubmit: true, top: 200, left: 400, msg: "Are you sure you want to delete selected record(s)?", width: 390, url: 'Admin/DeleteUser', delData: { userID: userID }, modal: true });
    });

    /*
    add new user  
    
    */
    $('#addUser').click(function() {

        $('#txtUserName')[0].value = '';
        $('#txtPassword')[0].value = '';

        var dialogEditUsers = $('#dialogAmendUsers');
        dialogEditUsers.dialog({
            autoOpen: false,
            bgiframe: true,
            modal: true,
            title: 'Add new user',
            width: 370,
            buttons: {
                'Save': function() {
                    $.ajax({
                        type: "POST",
                        url: "Admin/AddUser",
                        data: {
                            username: $('#txtUserName')[0].value,
                            password: $('#txtPassword')[0].value
                        },
                        dataType: 'json',
                        error: function(xhr) {
                            alert(xhr.responseText);
                            jQuery("#userslist").jqGrid().trigger("reloadGrid");
                        },
                        success: function(data) {
                            if (data.code == "error") {
                                alert(data.error);
                                return;
                            }
                            else {
                                jQuery("#userslist").jqGrid().trigger("reloadGrid");
                                dialogEditUsers.dialog('destroy');
                            }
                        }
                    });
                },
                'Cancel': function() {
                    $(this).dialog('close');
                }
            }
        });
        dialogEditUsers.dialog('open');
    });
}
);

/*
process delete call back
use jqGrid delete callback
*/
function processAddEdit(response, postdata) {
    var success = true;
    var message = ""
    var new_id = "";

    var json = eval('(' + response.responseText + ')');
    if (json.code == "error") {
        message = json.error;
        success = false;
    }

    return [success, message, new_id];

}


function GUIManager() {

}
GUIManager.prototype.GetValueByPropName = function(gridName, propName) {

    var rowid = jQuery("#" + gridName).jqGrid('getGridParam', 'selrow');
    if (rowid) {

        var row = jQuery("#" + gridName).jqGrid('getRowData', rowid);

        return row[propName];
    }
}