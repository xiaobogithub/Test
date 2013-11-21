/*
*   json2.js
*/

(function ($) {

    $.widget("ct.ordercontent", {
        options: {
            languageGuid: null,
            orderContents: null,
            orderTypeGuid: null,
            tableName: 'orderContents',
            tableHead: 'orderContentsHead',
            simpleLicenceGuid: 'c1a3bc0f-07c1-42e2-86cc-635186de8837',
            openLicenceGuid: 'a0f8fd76-8da3-4592-83fb-339d4bf5419f'
        },

        _create: function () {
            var widget = this;
            if (widget.options.orderContents == null) {
                CT.Program.GetOrderPrograms(widget, widget.options.languageGuid, function (widget, data) {
                    widget.options.orderContents = data;
                    widget.loadOrderContents(widget.options.orderContents);
                });
            }
            else {
                widget.loadOrderContents(widget.options.orderContents);
            }
        },

        loadOrderContents: function (orderContents) {
            var widget = this;
            var widgetId = widget.element[0].id;
            var tableId = widget.options.tableName;
            var tableHeadId = widget.options.tableHead;
            var typeGuid = widget.options.orderTypeGuid;
            $('#' + widgetId).data("orderContents", orderContents);

            if (typeGuid == widget.options.simpleLicenceGuid) {
                $('tr').remove('.' + tableId + 'data');
                for (var r = 0; r < orderContents.length; r++) {
                    var row = orderContents[r];
                    var rowId = tableId + '-' + r;
                    var rowTr = '<tr id="' + rowId + '" class="' + tableId + 'data" ></tr>';
                    $('#' + tableId).append(rowTr);


                    //programLicences
                    var programLicencesId = rowId + "programLicences";
                    colTd = '<td id="' + programLicencesId + '" style="padding-left:14px;">' + '<input type="text" id="' + row.ProgramGuid + '" name="' + row.ProgramGuid + '" class="licence"  onkeydown="ValidateNumer();" />' // '<asp:TextBox ID="' + row.ProgramGuid + '" runat="server"></asp:TextBox>' + '</td>';
                    $('#' + rowId).append(colTd);

                    //programName
                    var programNameId = rowId + "programName";
                    colTd = '<td id="' + programNameId + '" style="padding-left:14px;">' + row.ProgramName + '</td>';
                    $('#' + rowId).append(colTd);
                }
            }
            else {
                $('tr').remove('.' + tableId + 'data');
                for (var r = 0; r < orderContents.length; r++) {
                    var row = orderContents[r];
                    var rowId = tableId + '-' + r;
                    var rowTr = '<tr id="' + rowId + '" class="' + tableId + 'data"></tr>';
                    $('#' + tableId).append(rowTr);

                    //programLicences
                    var programLicencesId = rowId + "programLicences";
                    var checkBoxId = "chk" + r;
                    //<input type="checkbox" id="' + row.ProgramGuid + '" name="' + row.ProgramGuid + '"  style="width:80px" />
                    colTd = '<td id="' + programLicencesId + '" style="padding-left:14px;">' + '<input type="checkbox" id="' + checkBoxId + '" name="' + row.ProgramGuid + '" />';
                    $('#' + rowId).append(colTd);

                    //programName
                    var programNameId = rowId + "programName";
                    colTd = '<td id="' + programNameId + '" style="padding-left:14px;">' + row.ProgramName + '</td>';
                    $('#' + rowId).append(colTd);
                }
            }
        },

        destroy: function () {
            // Should detach click event handlers.
            $.Widget.prototype.destroy.apply(this, arguments);
        },

        _setOption: function (key, value) {
            // Should allow properties to be changed?
            $.Widget.prototype._setOption.apply(this, arguments);
        }
    });

    $.extend($.ct.ordercontent, {
        version: "@VERSION"
    });

})(jQuery);