var root_expression = null;
var current = null;

$(function () {
    //setup auto category
    $("#autocategory").click(function() {
        var source = $("#Source").val();
        var date = $("#Date").val();
        var amount = $("#Amount").val();

        $.ajax({
            url: "/Transactions/Autocategory",
            data: {
                "source": source,
                "date": date,
                "amount": amount
            }
        }).done(function(id) {
            $("#CategoryId").val(id);
        });
    });

    //setup category dialog
    $('#category-edit').on('show.bs.modal', function (e) {
        var link = $(e.relatedTarget);
        var categoryId = link.data("category-id");
        var categoryName = link.data("category-name");
        var parentId = link.data("parent-id");
        var modal = $(this);
        modal.find('#Id').val(categoryId);
        modal.find('#ParentId').val(parentId);
        modal.find('#Name').val(categoryName);

    });

    $('#category-edit').on('shown.bs.modal', function (e) {
        $(this).find('#Name').select().focus();
    });


    //setup year plan edit dialog
    $('#year-plan-entry-edit').on('show.bs.modal', function (e) {
        var link = $(e.relatedTarget);
        var month = link.data("month");
        var sum = link.data("sum");
        var category = link.data("category-id");
        var modal = $(this);
        modal.find('#month').val(month);
        modal.find('#categoryId').val(category);
        modal.find('#budget').val(sum);

    });

    $('#year-plan-entry-edit').on('shown.bs.modal', function (e) {
        $(this).find('#budget').select().focus();
    });

    //setup month plan edit dialog
    $('#month-plan-entry-edit').on('show.bs.modal', function (e) {
        var link = $(e.relatedTarget);
        var day = link.data("day");
        var sum = link.data("sum");
        var category = link.data("category-id");
        var modal = $(this);
        modal.find('#day').val(day);
        modal.find('#categoryId').val(category);
        modal.find('#budget').val(sum);

    });

    $('#year-plan-entry-edit').on('shown.bs.modal', function (e) {
        $(this).find('#budget').select().focus();
    });


    // proposition editor
    function ExpressionEditor() {

        this.getExperessionHtml = function (expr) {
            var newnode = null;
            var key;
            var child;
            switch (expr.type) {
                case "and":
                    newnode = $("<div class=\"expression-node\">" +
                        "<h5><span class=\"glyphicon glyphicon-wrench\"></span>И " +
                        "<a href=\"#\" class=\"add-expression glyphicon glyphicon-plus\"></a> " +
                        "<a href=\"#\" class=\"remove-expression glyphicon glyphicon-remove\"></a></h5></div>").clone();
                    newnode.data("expression", expr);
                    for (key in expr.expressions) {
                        if (expr.expressions.hasOwnProperty(key)) {
                            child = this.getExperessionHtml(expr.expressions[key]);
                            child.data("parent-expression", expr);
                            newnode.append(child);
                        }
                    }
                    break;
                case "or":
                    newnode = $("<div class=\"expression-node\">" +
                        "<h5><span class=\"glyphicon glyphicon-wrench\"></span>ИЛИ " +
                        "<a href=\"#\" class=\"add-expression glyphicon glyphicon-plus\"></a> " +
                        "<a href=\"#\" class=\"remove-expression glyphicon glyphicon-remove\"></a></h5></div>").clone();
                    newnode.data("expression", expr);
                    for (key in expr.expressions) {
                        if (expr.expressions.hasOwnProperty(key)) {
                            child = this.getExperessionHtml(expr.expressions[key]);
                            child.data("parent-expression", expr);
                            newnode.append(child);
                        }
                    }
                    break;
                case "not":
                    newnode = $("<div class=\"expression-node\">" +
                        "<h5><span class=\"glyphicon glyphicon-wrench\"></span>НЕ " +
                        ((expr.expressions.length === 0) ? "<a href=\"#\" class=\"add-expression glyphicon glyphicon-plus\"></a> " : "") +
                        "<a href=\"#\" class=\"remove-expression glyphicon glyphicon-remove\"></a></h5></div>").clone();
                    newnode.data("expression", expr);
                    for (key in expr.expressions) {
                        if (expr.expressions.hasOwnProperty(key)) {
                            child = this.getExperessionHtml(expr.expressions[key]);
                            child.data("parent-expression", expr);
                            newnode.append(child);
                        }
                    }
                    break;
                case "atomic":
                    newnode = $("<div class=\"expression-node\">" +
                        "<h5><span class=\"glyphicon glyphicon-wrench\"></span> " + expr.name +
                        "<a href=\"#\" class=\"remove-expression glyphicon glyphicon-remove\"></a></h5></div>").clone();
                    newnode.data("expression", expr);
                    break;
                default:
                    throw "Error";
            }
            return newnode;
        }

        this.refreshPropositionEditor = function (expression) {
            var expr = JSON.parse(expression);
            root_expression = expr;
            var editor = $("#proposition-editor");

            editor.empty();
            var expressionHtml = this.getExperessionHtml(expr);
            $(".add-expression", expressionHtml).click(this.showModalHandler);
            var self = this;
            $(".remove-expression", expressionHtml).click(function() {
                var expr2Remove = $(this).closest("div.expression-node").data("expression");
                var parentExpr = $(this).closest("div.expression-node").data("parent-expression");
                if (parentExpr) {
                    var index = parentExpr.expressions.indexOf(expr2Remove);
                    if (index > -1) {
                        parentExpr.expressions.splice(index, 1);
                    }
                }
                var jsonRoot = JSON.stringify(root_expression);
                $("#Proposition").val(jsonRoot);
                self.refreshPropositionEditor($("#Proposition").val());
            });

            editor.append(expressionHtml);
        }

        this.showModalHandler = function (e) {
            current = $(this).closest("div.expression-node").data("expression");
            $("#expression-add").modal('show');
        }

        this.setup = function () {
            this.refreshPropositionEditor($("#Proposition").val());

            var self = this;
            $("#expression-add .term-button").click(function () {
                var jsonExpr = $(this).data("json");
                current.expressions.push(jsonExpr);

                var jsonRoot = JSON.stringify(root_expression);
                $("#Proposition").val(jsonRoot);
                $("#expression-add").modal('hide');
                self.refreshPropositionEditor($("#Proposition").val());
            });

        }
    }

    if ($("#Proposition").length > 0) {
        var expressionEditor = new ExpressionEditor();
        expressionEditor.setup();
    }
})