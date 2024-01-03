"use strict";
//const { param } = require("jquery");
/*const { param } = require("jquery");*/
var COMMON = (function () {
    function COMMON() {
    }
    COMMON.printLabel = function (name) {
        console.log(name);
    };
    COMMON.getTimezone = function () {
        var TimeZoneOffset, daylight_time_offset, dst, hemisphere, jan1, jan2, june1, june2, rightNow, std_time_offset, temp;
        rightNow = new Date();
        jan1 = new Date(rightNow.getFullYear(), 0, 1, 0, 0, 0, 0);
        june1 = new Date(rightNow.getFullYear(), 6, 1, 0, 0, 0, 0);
        temp = jan1.toGMTString();
        jan2 = new Date(temp.substring(0, temp.lastIndexOf(" ") - 1));
        temp = june1.toGMTString();
        june2 = new Date(temp.substring(0, temp.lastIndexOf(" ") - 1));
        std_time_offset = (jan1 - jan2) / (1000 * 60 * 60);
        daylight_time_offset = (june1 - june2) / (1000 * 60 * 60);
        dst = void 0;
        if (std_time_offset === daylight_time_offset) {
            dst = "0";
        }
        else {
            hemisphere = std_time_offset - daylight_time_offset;
            if (hemisphere >= 0) {
                std_time_offset = daylight_time_offset;
            }
            dst = "1";
        }
        TimeZoneOffset = COMMON.convertToStandardFormat(std_time_offset);
        return TimeZoneOffset;
    };
    COMMON.convertToStandardFormat = function (value) {
        var display_hours, hours, mins, secs;
        hours = parseInt(value);
        value -= parseInt(value);
        value *= 60;
        mins = parseInt(value);
        value -= parseInt(value);
        value *= 60;
        secs = parseInt(value);
        display_hours = hours;
        if (hours === 0) {
            display_hours = "00";
        }
        else if (hours > 0) {
            display_hours = (hours < 10 ? "+0" + hours : "+" + hours);
        }
        else {
            display_hours = (hours > -10 ? "-0" + Math.abs(hours) : hours);
        }
        mins = (mins < 10 ? "0" + mins : mins);
        return display_hours + ":" + mins;
    };
    COMMON.doHighlight = function (bodyText, searchTerm, highlightStartTag, highlightEndTag) {
        var i, lcBodyText, lcSearchTerm, newText;
        if ((!highlightStartTag) || (!highlightEndTag)) {
            highlightStartTag = "<font style='color:blue; background-color:yellow;'>";
            highlightEndTag = "</font>";
        }
        newText = "";
        i = -1;
        lcSearchTerm = searchTerm.toLowerCase();
        lcBodyText = bodyText.toLowerCase();
        while (bodyText.length > 0) {
            i = lcBodyText.indexOf(lcSearchTerm, i + 1);
            if (i < 0) {
                newText += bodyText;
                bodyText = "";
            }
            else {
                if (bodyText.lastIndexOf(">", i) >= bodyText.lastIndexOf("<", i)) {
                    if (lcBodyText.lastIndexOf("/script>", i) >= lcBodyText.lastIndexOf("<script", i)) {
                        newText += bodyText.substring(0, i) + highlightStartTag + bodyText.substr(i, searchTerm.length) + highlightEndTag;
                        bodyText = bodyText.substr(i + searchTerm.length);
                        lcBodyText = bodyText.toLowerCase();
                        i = -1;
                    }
                }
            }
        }
        return newText;
    };
    COMMON.split = function (val) {
        return val.split(/,\s*/);
    };
    COMMON.extractLast = function (term) {
        return COMMON.split(term).pop();
    };
    COMMON.doAjaxPOSTRedirect = function (urlAction) {
        $.ajax({
            type: "POST",
            url: urlAction,
            data: {},
            datatype: "JSON",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
                if (response.ok) {
                    window.location = response.url;
                }
                else {
                    window.alert(response.message);
                }
            }
        });
    };
    COMMON.doAjaxPostJSON = function (actionUrl, Params, target) {
        if (target.indexOf('#') < 0) {
            target = $('#' + target);
        }
        $.ajax({
            type: "POST",
            url: actionUrl,
            data: Params,
            async: false,
            success: function (response) {
                console.log(response);
                if ((target != null) && target !== "") {
                    $(target).html(response);
                }
            },
            error: function (xhr, status, error) {
                COMMON.displayError(xhr.responseText, xhr.status);
            }
        });
    };
    COMMON.displayError = function (message, status) {
        console.log("An Error Occured:" + message);
        return false;
    };
    COMMON.querystring = function (key) {
        var m, r, re;
        re = new RegExp("(?:\\?|&)" + key + "=(.*?)(?=&|$)", "gi");
        r = [];
        m = void 0;
        while ((m = re.exec(document.location.search)) != null) {
            r.push(m[1]);
        }
        return r;
    };
    COMMON.doAjaxGet = function (Title, RequestedURL, Params, Target) {
        location.href = RequestedURL;
        $.ajax({
            type: "GET",
            url: RequestedURL,
            data: Params,
            success: function (response) {
                if (Target != null) {
                    $(Target).html(response);
                }
            },
            error: function (xhr, status, error) {
                COMMON.displayError(xhr.responseText, xhr.status);
            }
        });
        return false;
    };
    COMMON.doAjaxPostWithJSONResponse = function (url, params) {
        var jsonResponse = null;
        $.ajax({
            type: "POST",
            url: url,
            data: params,
            async: false,
            success: function (response) {
                jsonResponse = response;
            },
            error: function (xhr, status, error) {
                COMMON.displayError(xhr.responseText, xhr.status);
                jsonResponse = null;
            }
        });
        return jsonResponse;
    };
    COMMON.doAjaxGetWithJSONResponse = function (url, params) {
        var response = null;
        $.ajax({
            type: "GET",
            url: url,
            data: params,
            async: false,
            success: function (data) {
                response = data;
            },
            error: function (xhr, status, error) {
                COMMON.displayError(xhr.responseText, xhr.status);
                response = null;
            }
        });
        return response;
    };
    //COMMON.doAjaxPostWithJSONResponse = function (url, param) {
    //    var response = null;
    //    $.ajax({
    //        type: "GET",
    //        url: url,
    //        data: param,
    //        async: false,
    //        //contentType:"json",
    //        success: function (data) {
    //            response = data;
    //        },
    //        error: function (xhr, status, error) {
    //            COMMON.displayError(xhr.responseText, xhr.status);
    //            response = null;
    //        }
    //    });
    //    return response;
    //};
    COMMON.doAjaxFormPostWithJSONResponse = function (formid, Target) {
        var $form = $(formid);
        var response = null;
        $.ajax({
            type: "POST",
            url: $form.attr("action"),
            data: $form.serialize(),
            async: false,
            success: function (data) {
                response = data;
            },
            error: function (xhr, status, error) {
                COMMON.displayError(xhr.responseText, xhr.status);
                response = null;
            }
        });
        return response;
    };
    COMMON.doAjaxFormPostHTMLResponse = function (formId, Target) {
        var $form = $(formId);
        var htmlResponse = null;
        if (Target !== undefined && Target !== null && Target.indexOf('#') < 0) {
            Target = $('#' + Target);
        }
        $.ajax({
            type: "POST",
            url: $form.attr("action"),
            data: $form.serialize(),
            async: Target !== undefined && Target !== null ? true : false,
            success: function (response) {
                if (Target !== null && Target !== undefined) {
                    $(Target).html(response);
                } else {
                    htmlResponse = response;
                }
            },
            error: function (xhr, status, error) {
                COMMON.displayError(xhr.responseText, xhr.status);
                htmlResponse = null;
            }
        });
        return htmlResponse;
    };
    COMMON.doAjaxPostHTMLResponse = function (url, params) {
        var htmlResponse = null;
        $.ajax({
            type: "POST",
            url: url,
            data: params,
            //async: target !== undefined && target !== null ? true : false,
            async: false,
            success: function (response) {
                htmlResponse = response;
            },
            error: function (xhr, status, error) {
                COMMON.displayError(xhr.responseText, xhr.status);
                htmlResponse = null;
            }
        });
        return htmlResponse;
    };
    COMMON.doFormPost = function (formId) {
        var $form;
        $form = $(formId);
        $.ajax({
            type: "POST",
            url: $form.attr("action"),
            data: $form.serialize(),
            success: function (response) {
                $form.window.close();
            },
            error: function (xhr, status, error) {
                COMMON.displayError(xhr.responseText, xhr.status);
            }
        });
        return false;
    };
    COMMON.getCookieByName = function (name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
    };
    COMMON.compareValues = function (value1, value2) {
        return value1 === value2;
    };
    COMMON.formatDateToUKFormat = function (date) {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        $('#datePicker').val(today);
    };
    COMMON.refreshKendoGrid = function (gridId, resetFilter) {
        if (gridId !== undefined && gridId !== null && gridId.indexOf('#') < 0) {
            gridId = $('#' + gridId);
        }
        resetFilter = resetFilter === undefined || resetFilter === null ? false : resetFilter;
        var grid = $(gridId).data('kendoGrid');
        grid.dataSource.page(1);//first reload data source -->
        //grid.dataSource.read();//first reload data source -->
        //grid.refresh();//refresh current UI -->
        if (resetFilter === true) {
            grid.dataSource.filter({});
        }
    };
    COMMON.emptyKendoGrid = function (gridId) {
        if (gridId !== undefined && gridId !== null && gridId.indexOf('#') < 0) {
            gridId = $('#' + gridId);
        }
        var grid = $(gridId).data('kendoGrid');
        grid.dataSource.data([]);
    };
    COMMON.confirmAlertWitMessages = function (confirmTitle, confirmMessage, confirmType, url, params, gridId) {
        var confirmed = false;

        swal({
            title: confirmTitle, //"Are you sure to delete this record?",
            text: confirmMessage, // "Please check before deleting the record!",
            type: confirmType, //"warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Delete",
            cancelButtonText: "Cancel"
        },
            function (isConfirm) {
                if (isConfirm) {
                    var jsonResponse = COMMON.doAjaxPostWithJSONResponse(url, params);
                    if (jsonResponse.Deleted === true) {
                        $.notify('Deleted successful', { globalPosition: 'top center', className: 'success' });
                        COMMON.refreshKendoGrid("#" + gridId);
                    }
                    confirmed = true;
                } else {
                    swal("Cancelled", "You have cancelled delete operation!", "error");
                    return false;
                }
            });
        return confirmed;
    };
    COMMON.confirmAlert = function (message, url, params, gridId) {
        var confirmed = false;

        swal({
            title: "Are you sure to delete this record?",
            text:  "Please check before deleting the record!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Delete",
            cancelButtonText: "Cancel"
        },
            function (isConfirm) {
                if (isConfirm) {
                    var jsonResponse = COMMON.doAjaxPostWithJSONResponse(url, params);
                    if (jsonResponse.Deleted === true) {
                        $.notify('Deleted successful', { globalPosition: 'top center', className: 'success' });
                        COMMON.refreshKendoGrid("#" + gridId);
                    }
                    confirmed = true;
                } else {
                    swal("Cancelled", "You have cancelled delete operation!", "error");
                    return false;
                }
            });
        return confirmed;
    };
    COMMON.alert = function (message) {
        window.alert(message);
    };
    COMMON.is = {
        array: function (a) { return Array.isArray(a); },
        object: function (a) { return stringContains(Object.prototype.toString.call(a), 'Object'); },
        pth: function (a) { return is.obj(a) && a.hasOwnProperty('totalLength'); },
        svg: function (a) { return a instanceof SVGElement; },
        input: function (a) { return a instanceof HTMLInputElement; },
        dom: function (a) { return a.nodeType || is.svg(a); },
        string: function (a) { return typeof a === 'string'; },
        number: function (a) { return typeof a === 'number'; },
        function: function (a) { return typeof a === 'function'; },
        undefined: function (a) { return typeof a === 'undefined'; },
        null: function (a) { return is.und(a) || a === null; },
        nulloremptyorundefined: function (a) { return a === null || typeof a === 'undefined' || a === ""; },
        hex: function (a) { return /(^#[0-9A-F]{6}$)|(^#[0-9A-F]{3}$)/i.test(a); },
        rgb: function (a) { return /^rgb/.test(a); },
        hsl: function (a) { return /^hsl/.test(a); },
        col: function (a) { return (is.hex(a) || is.rgb(a) || is.hsl(a)); },
        key: function (a) { return !defaultInstanceSettings.hasOwnProperty(a) && !defaultTweenSettings.hasOwnProperty(a) && a !== 'targets' && a !== 'keyframes'; },
        email: function (a) { return /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(a); },
        html: function (a) {
            var h = document.createElement('div');
            h.innerHTML = a;
            for (var c = h.childNodes, i = c.length; i--;) {
                if (c[i].nodeType == 1) return true;
            }
            return false;
        },
        json: function (a) {
            try { return JSON.parse(a) !== this.undefined || JSON.parse(a) !== this.null } catch { return false }
        },
        specialcharacter: function (a) { return !(/^[A-Za-z0-9 ]+$/.test(a)); }
    };
    COMMON.doAjaxFilePostWithJSONResponse = function (url, params) {
        var jsonResponse = null;
        $.ajax({
            type: "POST",
            url: url,
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: params,
            async: false,
            success: function (response) {
                jsonResponse = response;
            },
            error: function (xhr, status, error) {
                COMMON.displayError(xhr.responseText, xhr.status);
                jsonResponse = null;
            }
        });
        return jsonResponse;
    };
    COMMON.doAjaxFilePostLoaderWithJSONResponse = function (url, params, loaderId, hideShowClass) {
        if (loaderId != null && loaderId != '' && loaderId != undefined && loaderId.indexOf('#') < 0) {
            loaderId = $('#' + loaderId);
        }
        if (hideShowClass != null && hideShowClass != '' && hideShowClass != undefined) {
            hideShowClass = 'd-none';
        }
        var jsonResponse = null;
        $(loaderId).removeClass(hideShowClass);
        $.ajax({
            type: "POST",
            url: url,
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: params,
            async: false,
            beforeSend: function (e) {
                $(loaderId).removeClass(hideShowClass);
            },
            success: function (response) {
                jsonResponse = response;
                $(loaderId).addClass(hideShowClass);
            },
            error: function (xhr, status, error) {
                COMMON.displayError(xhr.responseText, xhr.status);
                jsonResponse = null;
                $(loaderId).addClass(hideShowClass);
            }
        });
        $(loaderId).addClass(hideShowClass);
        return jsonResponse;
    };
    return COMMON;
}()); /*CLASS COMMON End*/
(function () {
    Array.prototype.moveUp = function (value, by_) {
        var index, newPos;
        index = this.indexOf(value);
        newPos = index - (by_ || 1);
        if (index === -1) {
            throw new Error("Element not found in array");
        }
        if (newPos < 0) {
            newPos = 0;
        }
        this.splice(index, 1);
        this.splice(newPos, 0, value);
    };
    Array.prototype.moveDown = function (value, by_) {
        var index, newPos;
        index = this.indexOf(value);
        newPos = index + (by_ || 1);
        if (index === -1) {
            throw new Error("Element not found in array");
        }
        if (newPos >= this.length) {
            newPos = this.length;
        }
        this.splice(index, 1);
        this.splice(newPos, 0, value);
    };
    Date.prototype.format = function (format) {
        var hours, k, month, monthName, o, ttime;
        hours = this.getHours();
        ttime = "AM";
        if (hours === 12) {
            ttime = "PM";
        }
        else if (hours > 12) {
            hours = hours - 12;
            ttime = "PM";
        }
        month = this.getMonth() + 1;
        monthName = "";
        switch (month) {
            case 1:
                monthName = "Jan";
                break;
            case 2:
                monthName = "Feb";
                break;
            case 3:
                monthName = "Mar";
                break;
            case 4:
                monthName = "Apr";
                break;
            case 5:
                monthName = "May";
                break;
            case 6:
                monthName = "Jun";
                break;
            case 7:
                monthName = "Jul";
                break;
            case 8:
                monthName = "Aug";
                break;
            case 9:
                monthName = "Sep";
                break;
            case 10:
                monthName = "Oct";
                break;
            case 11:
                monthName = "Nov";
                break;
            case 12:
                monthName = "Dec";
        }
        o = {
            "d+": this.getDate(),
            "h+": hours,
            "m+": this.getMinutes(),
            "s+": this.getSeconds(),
            "q+": Math.floor((this.getMonth() + 3) / 3),
            S: this.getMilliseconds()
        };
        if (RegExp("(t+)").test(format)) {
            format = format.replace(RegExp.$1, ttime);
        }
        if (RegExp("(y+)").test(format)) {
            format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        if (RegExp("(M+)").test(format)) {
            if (RegExp.$1.length === 3) {
                format = format.replace(RegExp.$1, monthName);
            }
            else {
                format = format.replace(RegExp.$1, month);
            }
        }
        for (k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, (RegExp.$1.length === 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return format;
    };
}());
(function ($) {
    $.fn.serializeObject = function () {
        var a, o;
        o = {};
        a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || "");
            }
            else {
                o[this.name] = this.value || "";
            }
        });
        return o;
    };
    $.fn.localTimeFromUTC = function (format) {
        return this.each(function () {
            var currentDate, givenDate, hours, localDateString, min, offset, tagText, ttime;
            tagText = $(this).html();
            if (tagText === "") {
                return;
            }
            givenDate = new Date(tagText.split("+", 1)[0]);
            if (givenDate === "NaN") {
                return;
            }
            offset = -(givenDate.getTimezoneOffset() / 60.0);
            hours = givenDate.getHours();
            hours += offset;
            givenDate.setHours(hours);
            currentDate = new Date();
            localDateString = "";
            if (currentDate.getFullYear() === givenDate.getFullYear() && currentDate.getMonth() === givenDate.getMonth() && currentDate.getDay() === givenDate.getDay()) {
                ttime = "AM";
                if (hours === 12) {
                    ttime = "PM";
                }
                else if (hours > 12) {
                    hours = hours - 12;
                    ttime = "PM";
                }
                min = givenDate.getMinutes();
                localDateString = (hours < 10 ? "0" + hours : hours) + ":" + (min < 10 ? "0" + min : min) + " " + ttime;
            }
            else if (currentDate.getFullYear() === givenDate.getFullYear() && $(this).hasClass("ConvertUtcToLocal")) {
                localDateString = givenDate.format("MMM dd");
            }
            else {
                localDateString = givenDate.format(format);
            }
            $(this).html(localDateString);
            $(this).removeClass("ConvertUtcToLocal");
            $(this).removeClass("ConvertUtcToLocalDateTime");
        });
    };
    $.fn.wrapInTag = function (opts) {
        var o;
        o = $.extend({
            words: [],
            tag: "<strong>"
        }, opts);
        return this.each(function () {
            var html, i, len, re;
            html = $(this).html();
            i = 0;
            len = o.words.length;
            while (i < len) {
                re = new RegExp(o.words[i], "gi");
                html = html.replace(re, o.tag + "$&" + o.tag.replace("<", "</"));
                i++;
            }
            $(this).html(html);
        });
    };
})(jQuery);
$(function () {
    $("a.buttonDelete").on("click", function (event) {
        var $url, Message, currentTr;
        event.preventDefault();
        $url = $(this).attr("href");
        $title = $(this).attr("title");
        currentTr = $(this).closest("tr");
        Message = "Do you want to delete?";
        if (confirm(Message)) {
            $.ajax({
                type: "POST",
                url: $url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 30000,
                success: function (response) {
                    if (currentTr != null) {
                        currentTr.remove();
                    }
                },
                beforeSend: function () {
                },
                complete: function () { },
                error: function (xhr, status, error) {
                    COMMON.displayError(xhr.responseText, xhr.status);
                }
            });
        }
        return false;
    });
    $("a.doAjaxGet").on({
        click: function (event) {
            var $title, $url;
            event.preventDefault();
            $(this).attr("target", "_self");
            $url = $(this).attr("href");
            $title = $(this).attr("title");
            COMMON.doAjaxGet($title, $url, "");
            return false;
        }
    });

});