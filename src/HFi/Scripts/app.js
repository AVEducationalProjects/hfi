$(function(){
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

    $('#category-edit').on('shown.bs.modal', function(e) {
        $(this).find('#Name').select().focus();
    });

})