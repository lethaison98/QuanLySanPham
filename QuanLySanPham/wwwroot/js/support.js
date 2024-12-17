﻿$(document).ready(function () {
    // Default: application/x-www-form-urlencoded; charset=UTF-8 //// application/x-www-form-urlencoded, multipart/form-data, or text/plain
    Post = function (opts) {
        var settings = {
            url: '',
            data: {

            },
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
            },
            dataType: 'json',
            showLoading: true,
            isUploadFile: false,
            isJson: true,
            contentType: "application/json-patch+json",
            type: "Post",
            callback: function () { }
        }
        settings = $.extend(true, {}, settings, opts);
        if (settings.isJson) {
            settings.data = JSON.stringify(settings.data);
            if (typeof (settings.contentType) == "undefined") settings.contentType = 'application/json-patch+json; charset=utf-8';
        }

        if (settings.isUploadFile) {
            settings.contentType = false;
            settings.processData = false;
            settings.cache = false;
        }
        if (settings.showLoading) {
            $('#loading').show();
        }
        settings.success = function (res) {
            settings.callback(res);

            if (settings.showLoading) {
                $('#loading').hide();
            }
        }
        settings.error = function (jqXHR, error, errorThrown) {
            console.log(jqXHR, error, errorThrown)
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 401) {
                window.location.href = "/Account/Login";
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (error === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (error === 'timeout') {
                msg = 'Time out error.';
            } else if (error === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            if (settings.showLoading) {
                $('#loading').hide();
            }
        }

        $.ajax(settings);
    };

    Get = function (opts) {
        var settings = {
            url: '',
            data: {

            },
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
            },
            dataType: 'json',
            showLoading: true,
            isJson: false,
            type: "Get",
            callback: function () { }
        }

        settings = $.extend(true, {}, settings, opts);
        if (settings.showLoading) {
            $('#loading').show();
        }
        settings.success = function (data) {
            settings.callback(data);
            if (settings.showLoading) {
                $('#loading').hide();
            }
        }
        settings.error = function (jqXHR, error, errorThrown) {
            console.log(jqXHR, error, errorThrown)
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 401) {
                window.location.href = "/Account/Login";
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (error === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (error === 'timeout') {
                msg = 'Time out error.';
            } else if (error === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }

            settings.callback(jqXHR.responseJSON);
            if (settings.showLoading) {
                $('#loading').hide();
            }
        }
        $.ajax(settings);
    };
    Delete = function (opts) {
        var settings = {
            url: '',
            data: {

            },
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
            },
            dataType: 'json',
            showLoading: true,
            isJson: false,
            type: "Delete",
            callback: function () { }
        }

        settings = $.extend(true, {}, settings, opts);
        if (settings.showLoading) {
            $('#loading').show();
        }
        settings.success = function (data) {
            settings.callback(data);
            if (settings.showLoading) {
                $('#loading').hide();
            }
        }
        settings.error = function (jqXHR, error, errorThrown) {
            console.log(jqXHR, error, errorThrown)
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 401) {
                window.location.href = "/Account/Login";
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (error === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (error === 'timeout') {
                msg = 'Time out error.';
            } else if (error === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }

            settings.callback(jqXHR.responseJSON);
            if (settings.showLoading) {
                $('#loading').hide();
            }
        }
        $.ajax(settings);
    };


    ResetForm = function (formId) {
        var $form = $(formId);
        var $elms = $('input, select, textarea, .dropdown-checkboxes, [data-name]:not(.k-treeview)', $form);
        $.each($elms,
            function (i, item) {
                var type = $(item).getType();
                switch (type) {
                    case 'text':
                    case 'textarea':
                    case 'password':
                    case 'hidden':
                        $(item).val('');
                        if ($(item).data('datepicker')) $(item).datepicker("setDate", null);
                        if (type === "textarea" && typeof (CKEDITOR) != 'undefined' && CKEDITOR.instances[$(item).attr('id')]) CKEDITOR.instances[$(item).attr('id')].setData("");
                        break;
                    case 'select':
                        $(item).val($('option:first', item).val());
                        break;
                    case 'checkbox':
                        $(item).prop('checked', false);
                        break;
                    case 'dropdown-checkboxes':
                        var $this = $(item);
                        var $hida = $this.find('.hida');
                        var $mutliSelect = $this.find('.mutliSelect ul');

                        $('input[type="checkbox"]', $mutliSelect).prop('checked', false);
                        $('p > span', $hida).remove();
                        $("> span", $hida).show();
                        break;
                    default:
                        $(item).text('');
                        break;
                }

            });
        $('.autosize').trigger('autosize.resize');
        $('.has-error').removeClass('has-error see');
    };
    ValidateForm = function (formId, formSlimScroll) {
        var $form = $(formId);
        var $formSlimScroll = typeof (formSlimScroll) == "undefined" ? null : $(formSlimScroll);
        var flag = true;
        var top = 1000000000000000;

        var check = function (flag, item) {
            if (!flag) {
                $(item).parents(".ttls-content:first").removeClass('see').addClass('has-error');
                setTimeout(function () {
                    $(item).parents(".ttls-content:first").addClass('see');
                },
                    2000);
            }
        }

        var $elms = $('[required]:not(:disabled)', $form);
        if ($elms.length > 0) {
            $.each($elms, function (i, item) {
                var type = $(item).getType();
                if (type !== '') {
                    switch (type) {
                        case 'textarea':
                        case 'password':
                        case 'text':
                            var value = $(item).val().trim();
                            if ($(item).hasClass('maskPercent')) value = $(item).val().replace(/\%/g, "").trim();
                            if (value === '') {
                                flag = false;
                                check(flag, item);
                            } else {
                                $(item).parents(".ttls-content:first").removeClass('see has-error');
                            }
                            //if (flag && $(item).hasClass('number')) {
                            //    var re = /^[+-]?\d+(\.\d+)?$/;

                            //    flag = re.test($(item).val().trim());
                            //    if (!flag) $(item).parents(".ttls-content:first").addClass('see has-error');
                            //}
                            break;

                        case 'select':
                            if ($(item).val() == null || $(item).val().trim() == '-1' || $(item).val().trim() == '') {
                                flag = false;
                                $(item).parent().addClass('is-select');
                                check(flag, item);
                            } else {
                                $(item).parents(".ttls-content:first").removeClass('see has-error');
                            }
                            break;
                        case 'file':
                            if (!$(item).prop('disabled')) {
                                if ($(item).get(0).files.length == 0) {
                                    flag = false;
                                    $(item).parents(".ttls-content:first").addClass('see has-error');
                                    check(flag, item);
                                } else {
                                    $(item).parents(".ttls-content:first").removeClass('see has-error');
                                }
                            }
                            break;
                        case 'div':
                            if ($(item).prop('contenteditable')) {
                                if ($(item).text().trim() == "") {
                                    flag = false;
                                    $(item).parents(".ttls-content:first").addClass('see has-error');
                                    check(flag, item);
                                } else {
                                    $(item).parents(".ttls-content:first").removeClass('see has-error');
                                }
                            }
                            break;
                        case 'dropdown-checkboxes':
                            if ($(item).getMultiSelectValue() == '') {

                                flag = false;
                                check(flag, item);
                            } else {
                                $(item).parents(".ttls-content:first").removeClass('see has-error');
                            }
                            break;
                        default:

                            break;
                    }
                }

                if (!flag) {
                    if (top >= $(item).offset().top && $formSlimScroll == null) {
                        top = $(item).offset().top - 20;
                    } else if (top >= $(item).offset().top && $formSlimScroll != null) {
                        var childTop = $(item).offset().top;
                        top = childTop + $formSlimScroll.scrollTop() - $formSlimScroll.offset().top - 100;
                    }
                }
            });
        }

        if (!flag) {
            if ($formSlimScroll != null) {
                $formSlimScroll.slimScroll({ scrollTo: top });
            } else {
                if ($(window).width() > 768) {
                    $(window).scrollTop(top - $('.navbar-custom').height() - 10);
                } else {
                    $(window).scrollTop(top - $('.navbar-custom').height());
                }
            }
        }

        return flag;
    };
    FillFormData = function (formId, data) {
        ResetForm(formId);
        if (typeof data == 'undefined' || data == null) data = {};
        var $form = $(formId);
        $form.data("info", data);
        $.each($('[data-name]', $form), function (i, item) {

            if (data[$(item).attr('data-name')] == null) {
                if ($(item).hasClass('date-picker')) $(item).datepicker("update", "");
                data[$(item).attr('data-name')] = "";
            };
            var type = $(item).getType();
            var _value = data[$(item).attr('data-name')] != null ? data[$(item).attr('data-name')] : "";

            switch (type) {
                case 'p':
                case 'span':
                case 'strong':
                case 'b':
                    $(item).html(_value);
                    break;
                case 'div':
                    if (!$(item).hasClass('k-treeview')) $(item).html(_value);
                    break;
                case 'text':
                case 'date':
                case 'textarea':
                case 'password':
                case 'hidden':
                case 'select':
                case 'number':
                    $(item).val(_value + "");
                    break;
                case 'checkbox':
                    $(item).prop('checked', Boolean(_value));
                    break;
                case 'radio':
                    var value = $(item).val();
                    var name = $(item).attr('data-name');
                    var dataName = _value;
                    $(item).prop('checked', value == dataName + "");
                    break;
                default:

                    break;
            }
            if ($(item).hasClass('date-picker') && _value) {
                var viewmode = $(item).attr('data-date-viewmode');
                if (viewmode != 'months' && viewmode != 'years') $(item).datepicker("update", new Date(_value.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3")));

            }
            if ($(item).data('select2')) {
                $(item).trigger('change');
            }
        });
    };

    LoadFormData = function (formId, data) {
        if (typeof data == 'undefined') data = {};
        var $form = $(formId);

        $.each($('[data-name]:not(.skip)', $form), function (i, item) {
            var key = $(item).attr('data-name');
            var value = '';

            var type = $(item).getType();
            var key2;
            var value2;

            switch (type) {
                case 'text':
                    if ($(item).hasClass('maskMoney')) {
                        key2 = key + '_maskMoney';
                        value2 = $(item).val();
                        data[key2] = value2;
                        value = $(item).val().replace(/\./g, "").replace("VNĐ", "").trim();
                    } else if ($(item).hasClass('maskPercent')) {

                        value = (parseInt($(item).val().replace(/\ %/g, "").trim()) || 0) + '';
                    } else if ($(item).hasClass('maskYear')) {

                        value = (parseFloat($(item).val().replace(/\,/g, ".").replace(/\ năm/g, "").trim()) || 0) + '';
                    } else if ($(item).hasClass('selectboxit')) {
                        value = $(item).parent().prev().val();
                    } else {
                        value = $(item).val().trim();
                    }

                    break;
                case 'p':
                case 'span':
                case 'b':
                case 'i':
                case 'strong':
                    value = $(item).text().trim();
                    break;
                case 'div':
                    value = $(item).html();
                    break;
                case 'textarea':
                case 'password':
                case 'hidden':
                case 'number':
                case 'date':
                    value = $(item).val().trim();
                    break;
                case 'checkbox':
                    value = $(item).prop('checked');
                    break;
                case 'radio':
                    var name = $(item).attr('name');
                    if (name && $('input[name="' + name + '"]:checked').length > 0) {
                        value = $('input[name="' + name + '"]:checked').val();
                    }
                    else {
                        value = false;
                    }

                    break;
                case 'select':
                    if ($(item).prop('multiple')) {
                        $(item).val() ? value = $(item).val().join(',') : value = "";
                    }
                    else {
                        key2 = key + '_fieldName';
                        value2 = $(item).find('option:selected').text().trim();
                        data[key2] = value2;
                        $(item).val() ? value = $(item).val().trim() : value = "";
                    }


                    break;
                default:

                    break;
            }

            if (type === "radio") {
                if ($(item).prop('checked')) data[key] = value.trim();
            }
            else if (type === "checkbox") data[key] = value;
            else data[key] = value.trim();
        });
        return data;
    };
    $.fn.getType = function () {
        return this[0].tagName === "INPUT" ? this[0].type.toLowerCase() : this[0].tagName.toLowerCase();
        //return this[0].tagName == "INPUT" || this[0].tagName == "DIV" ? this[0].type.toLowerCase() : this[0].tagName.toLowerCase();
    }

    SetDataTable = function (opts) {

        if (typeof (opts) == "undefined") opts = {};
        if (typeof (opts.url) == "undefined") opts.url = "";
        if (typeof (opts.dom) == "undefined") opts.dom = "Blrtip";
        if (typeof (opts.ordering) == "undefined") opts.ordering = false;
        if (typeof (opts.data) == "undefined") opts.data = {};
        if (typeof (opts.data.requestData) != "function") opts.data.requestData = function () { return {} };
        if (typeof (opts.data.columns) == "undefined") opts.data.columns = [];

        if (typeof (opts.table) == "undefined") opts.table = $('<table></table>');
        if (typeof (opts.callback) != "function") opts.callback = function () { };
        if (typeof (opts.DrawCallback) != "function") opts.DrawCallback = function () { };
        if (typeof (opts.OnDraw) != "function") opts.OnDraw = function () { };
        if (typeof (opts.InfoCallback) != "function") opts.InfoCallback = function () { };
        if (typeof (opts.CreatedRow) != "function") opts.CreatedRow = function () { };
        if (typeof (opts.InitComplete) != "function") opts.InitComplete = function () { };
        if (typeof (opts.FooterCallback) != "function") opts.FooterCallback = function () { };
        if (typeof (opts.BeforeSend) != "function") opts.BeforeSend = function () { };
        var $tbl;
        var $table = $(opts.table);
        var options = {
            "ajax": {
                "url": opts.url,
                "type": opts.type,
                "dataType": 'json',
                "showLoading": true,
                "isUploadFile": false,
                "isJson": true,
                "contentType": "application/json-patch+json",
                "data": function (d) {
                    var _d = {
                        "PageSize": d.length,
                        "PageIndex": parseInt(((d.start + d.length) + 1) / d.length),
                        "keyword": d.search.value
                    }
                    if (opts.type.toUpperCase() == "POST") {
                        _d = JSON.stringify($.extend({}, _d, opts.data.requestData()));
                    } else {
                        _d = $.extend({}, _d, opts.data.requestData());
                    }

                    return _d;
                },
                dataType: 'json',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
                }
            },
            dom: opts.dom,
            ordering: opts.ordering,
            "columns": opts.data.columns,
            "initComplete": function (settings, json) {
                if (json.sessionend) {
                    window.location.href = '/';
                } else {
                    opts.InitComplete(json);
                }
            },
            "fnInfoCallback": function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                opts.InfoCallback(iTotal);
                return "Đang xem " + iStart + " đến " + iEnd + " trong tổng số " + iTotal + " mục";
            },
            "fnDrawCallback": function (oSettings) {
                opts.callback();
                opts.DrawCallback(oSettings);
                if (typeof (opts.drawDetail) == "function") {
                    $('tbody tr td.details-control', $table).on('click', function () {
                        $('tr', $table).removeClass('selected');
                        var td = $(this);
                        var tr = td.closest('tr');
                        var row = $tbl.row(tr);


                        if (row.child.isShown()) {
                            $('div.slider', row.child()).slideUp(function () {
                                tr.find('.expand-icon').removeClass('fa-minus-circle').addClass('fa-plus-circle');
                                tr.removeClass('details selected');
                                row.child.hide();
                            });
                        }
                        else {
                            row.child(opts.drawDetail(row.data()), 'no-padding').show();
                            row.child().css('position', 'relative');
                            tr.addClass('details selected');
                            tr.find('.expand-icon').removeClass('fa-plus-circle').addClass('fa-minus-circle');
                            $('div.slider', row.child()).slideDown();
                        }
                    });
                }

                $('#loading').hide();
            },
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                opts.CreatedRow(nRow, aData, iDataIndex);
            },
            "footerCallback": function (row, data, start, end, display) {
                opts.FooterCallback(row, data, start, end, display);
            },
            "preXhr": function (e, settings, data) {
                console.log(e, settings, data)
            }
        }

        if (typeof (opts.data.processData) == "function") options.ajax.dataSrc = function (json) { return opts.data.processData(json); }; //tra ve mang data object
        if (typeof (opts.data.processData2) == "function") options.ajax.dataFilter = function (json) { return opts.data.processData2(json); }; //tra ve chuoi data string, dung de phan trang 

        var defaultOption = {
            "lengthMenu": [[10, 25, 50, 100, 1000], [10, 25, 50, 100, 1000]],
            "processing": true,
            autoFill: true,
            //responsive: true,
            searchHighlight: true,
            dom: "Blrtip",
            buttons: [
                {
                    extend: 'colvis',
                    text: '<i class="fas fa-cog"></i>'
                },
                'copy',
                'excel',
                'pdf',
                'print'
            ],
            "serverSide": true,
            "ordering": false,
            "deferRender": true,
            stateSave: true,
            "ajax": {
                "url": "",
                "contentType": "application/json",
                "dataType": 'json',
                "type": "GET",
                "data": function (d) {
                    return d;
                }
            },
            "columns": [

            ],
            "initComplete": function (settings, json) {

            },
            "fnCreatedRow": function (nRow, aData, iDataIndex) {

            },
            "fnInfoCallback": function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                return "Đang xem " + iStart + " đến " + iEnd + " trong tổng số " + iTotal + " mục";
            },
            "fnDrawCallback": function (oSettings) {

            },
            "footerCallback": function (row, data, start, end, display) {
                console.log(row, data, start, end, display);
            }
        }

        options = $.extend({}, options, opts.data);
        defaultOption = $.extend({}, defaultOption, options);
        defaultOption.ajax.error = function (jqXHR, error, code) {
            console.log(jqXHR);
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 401) {
                window.location.href = "/Account/Login";
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (error === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (error === 'timeout') {
                msg = 'Time out error.';
            } else if (error === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            if (options.showLoading) {
                $('#loading').hide();
            }
        }

        $tbl = $table.on('preXhr.dt', function (e, settings, data) {
            $('#loading').show();
        }).on('error.dt', function (e, settings, techNote, message) {

        }).DataTable(defaultOption);

        if (typeof $tbl.table == 'undefined') $tbl.table = $table;
        return $tbl;
    }
    ConvertDecimalToString = function (value) {
        return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
    }
    ConvertStringToDecimal = function (value) {
        var data = parseFloat(value.replaceAll('.', '').replaceAll(',', '.'));
        if (isNaN(data)) data = 0;
        return data;
    }

    CalculateMonthBetweenDays = function (date1, date2) {
        var date1Array = date1.split("/");
        var date2Array = date2.split("/");
        var d1 = new Date(date1Array[2], date1Array[1] - 1, date1Array[0]);
        var d2 = new Date(date2Array[2], date2Array[1] - 1, date2Array[0]);
        var months;
        months = (d2.getFullYear() - d1.getFullYear()) * 12;
        months -= d1.getMonth();
        months += d2.getMonth();
        if (d2.getDate() - d1.getDate() >= 15) {
            months++;
        }
        return months <= 0 ? 0 : months;
    }

    RemoveVietnameseTones = function (str) {
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
        str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
        str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
        str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
        str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
        str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
        str = str.replace(/Đ/g, "D");
        // Some system encode vietnamese combining accent as individual utf-8 characters
        // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
        str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
        str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
        // Remove extra spaces
        // Bỏ các khoảng trắng liền nhau
        str = str.replace(/ + /g, " ");
        str = str.trim();
        // Remove punctuations
        // Bỏ dấu câu, kí tự đặc biệt
        //str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, " ");
        return str;
    };
    ConvertTextToUrl = function (str) {
        str = RemoveVietnameseTones(str).replaceAll(/[^\w\s]/gi, ' ').replaceAll(/\s\s+/g, ' ').replaceAll(' ', '-').toLowerCase();
        return str;
    };
    ConvertTextToBase64 = function (str) {
        return window.btoa(unescape(encodeURIComponent(str)));
    }

    ConvertBase64ToText = function (str) {
        return decodeURIComponent(escape(window.atob(str)));
    }

    Decrypt = function (str) {
        var randomNumber = parseInt(str[0]);
        var value = str.substring(randomNumber + 1)
        return ConvertBase64ToText(value);
    }

    Fetch = function (opts) {
        var settings = {
            method: "Post",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            callback: function () { }
        }
        settings = $.extend(true, {}, settings, opts);
        settings.data = JSON.stringify(settings.data);
        $.ajax(settings).done(function (response) {
            var res = jQuery.parseJSON(Decrypt(response));
            settings.callback(res);
        });
    };
});
