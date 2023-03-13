var itemId;
var itemList;
var path = "";
var editItemId;
var columns = [];
var editFolderId;

Noty.overrideDefaults({
    theme: 'nest',
    text: 'Başarılı bir şekilde eklendi',
    type: 'success',
    progressBar: true,
    closeWith: ['button'],
    timeout: 1500
});

$(document).ready(function () {
    path = $("#path").val();

    var getColumns = $("#path").attr("data-columnTitle").split(',')

    for (var i = 0; i < getColumns.length; i++) {
        columns.push({ "data": getColumns[i], "name": getColumns[i] })
    }

    itemList = $("#itemList").DataTable({
        "ajax": {
            "url": path + "/GetBasicJsonList",
            "type": "POST",
            "datatype": "json"
        },
        "columns": columns,
        "serverSide": "true",
        "order": [0, "asc"],
        "processing": "true",
        "info": "false",
        "language": {
            "processing": "Lütfen beyleyin...",
            //"lengthMenu": "Sayfada _MENU_ kayıt gösteriliyor",
            "zeroRecords": "Eşleşen kayıt bulunamadı",
            "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
            "infoEmpty": "Kayıt yok",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sSearch": "Ara:",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": '<i class="next"></i>',
                "sPrevious": '<i class="previous"></i>'
            },

        },
        //"dom": '<"top"l>rt<"bottom"ip><"clear">',
        "dom": '<"top">rt<"bottom"lp><"clear">',
        "fnInitComplete": function (oSettings, json) {
            addSearchControl(json);
        },
        "drawCallback": function (settings) {
            clearInput();
            addId(settings.json.data);
        }
    });

    function addSearchControl(json) {
        var timer = '';
        $("#itemList thead").append($("#itemList thead tr:first").clone());
        $("#itemList thead tr:eq(1) th").each(function (index) {
            $(this).replaceWith('<th><div class="d-flex align-items-center">'
                + '<span class="svg-icon svg-icon-1"> <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none"> <rect opacity="0.5" x="17.0365" y="15.1223" width="8.15546" height="2" rx="1" transform="rotate(45 17.0365 15.1223)" fill="currentColor"></rect> <path d="M11 19C6.55556 19 3 15.4444 3 11C3 6.55556 6.55556 3 11 3C15.4444 3 19 6.55556 19 11C19 15.4444 15.4444 19 11 19ZM11 5C7.53333 5 5 7.53333 5 11C5 14.4667 7.53333 17 11 17C14.4667 17 17 14.4667 17 11C17 7.53333 14.4667 5 11 5Z" fill="currentColor"></path></svg></span>'
                + '<input class="form-control form-control-solid w-200px ps-15" type="text" placeholder="' + $(this).html() + ' Ara"></input></th></div>');
            var searchControl = $("#itemList thead tr:eq(1) th:eq(" + index + ") input");
            searchControl.on('keyup', function () {
                clearTimeout(timer);
                timer = setTimeout(function () {
                    itemList.column(index).search(searchControl.val() == "" ? "" : searchControl.val(), true, false).draw();
                }, 1000);
            })
        })
        clearInput();
    }

    function addId(data) {
        console.log(data);
        $("#itemList tr td:last-child").each(function (index) {
            if (data.length > 0) {
                if (data[index] != "undefined") {
                    $(this).html(
                        `
                           <a href="#" class="btn btn-light-danger font-weight-bolder font-size-sm" onclick="deleteItem(\'` + data[index].id + `\')">
                                Sil
                            </a>
                            <a href="#" class="btn btn-light-success font-weight-bolder font-size-sm" onclick="editItem(\'` + data[index].id + `\')">
                                Düzenle
                            </a>
                        `
                    );
                }
            }
        })
    }

    function clearInput() {
        $("table thead tr:eq(1) th:last").html("");
    }
});

function ClearFilter() {
    $("table thead tr:eq(1) th input").val("");
    itemList.search('').columns().search('').draw();
}

function openItemModal() {
    clearForm("itemForm");
    $("#addItem").css("display", "inline-block");
    $("#updateItem").css("display", "inline-block");
    $("#itemModal").modal("show");
}

function Item() {

    var self = this;
    self.Name = "";
    self.LastName = "";
    self.Locales = [];
}


function addItem() {
    //$("#saveBtn").css("display", "inline-block");
    //$("#updateBtn").css("display", "none");
    var data = $("#itemForm").serializeArray();

    //data.find(x => x.name == "path").value = "/Content/assets/site/uploads/" + data.find(x => x.name == "name").value + "/";

    $.ajax({
        type: "POST",
        url: path + "/Create",
        dataType: "json",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        success: function (data) {
            new Noty({
                text: 'Başarılı bir şekilde eklendi',
                type: 'success',
                progressBar: false,
                closeWith: ['button']
            }).show();
            $("#userSaveModel").modal("hide");
            itemList.search('').columns().search().draw();
            clearForm("itemForm");
        }
    });
}


function deleteItem(id) {
    Swal.fire({
        title: 'Silmek istiyormusun?',
        showCancelButton: true,
        confirmButtonText: `Sil`,
        cancelButtonText: `İptal`,
    })
        .then((result) => {
            if (result.value) {
                $.ajax({
                    type: "GET",
                    url: path + '/Delete',
                    data: { Id: id },
                    success: function (data) {
                        if (data) {
                            new Noty({
                                text: 'Kayıt silindi.',
                                type: 'success',
                                progressBar: false,
                                closeWith: ['button']
                            }).show();
                            itemList.search('').columns().search().draw();
                        } else {
                            new Noty({
                                text: 'Kaydı silerken bir sorun oluştu.',
                                type: 'danger',
                                progressBar: false,
                                closeWith: ['button']
                            }).show();
                        }
                    }
                });
            } else {
                new Noty({
                    text: 'Kayıt güvende!',
                    type: 'success',
                    progressBar: false,
                    closeWith: ['button']
                }).show();
            }
        });
}

function editItem(itemId) {
    clearForm("itemForm");

    //$("#saveBtn").css("display", "none");
/*    $("#updateBtn").css("display", "inline-block");*/
    $("#userSaveModel").modal("show");

    $.ajax({
        type: "GET",
        url: path + '/GetById',
        data: { Id: itemId },
        success: function (data) {
            var ser = $('#itemForm').serializeArray();
            $.each(ser, function (i, s) {
                $("input[name =" + s.name + "]").val(data[s.name]);
                s.value = data[s.name];
            });
            editItemId = itemId;
        }
    });
}

function updateItem() {
    var data = $("#itemForm").serializeArray();
    data.push({ name: 'id', value: editItemId });

    $.ajax({
        type: "POST",
        url: path + "/Update",
        dataType: "json",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        success: function (data) {
            new Noty({
                theme: ' alert alert-success alert-styled-left p-0 bg-white',
                text: 'Kayıt başarılı bir şekilde güncellendi.',
                type: 'success',
                progressBar: false,
                closeWith: ['button']
            }).show();
            $("#userSaveModel").modal("hide");
            itemList.search('').columns().search().draw();
        }
    });
}

function clearForm(formId) {
    $("#" + formId).trigger("reset");
    $("#" + formId).find('select').val('');
}

function showMessage(text, type) {
    new Noty({
        text: text,
        type: type,
    }).show();
}

function reverseModal() {
    clearForm("itemForm");
    $("#btnSave").show();
    $("#btnUpdate").hide();
}