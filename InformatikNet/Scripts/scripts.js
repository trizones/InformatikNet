function addItem() {
    $("#Users option:selected").appendTo("#Recievers");
    $("#Recievers option").attr("selected", true);
};

function removeItem() {
    $("#Recievers option:selected").appendTo("#Users");
    $("#Users option").attr("selected", false);
    $("#Recievers option").prop("selected", "selected");
};

