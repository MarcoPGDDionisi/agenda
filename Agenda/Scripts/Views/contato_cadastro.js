/*****************************************************************
 Carrega Inputs --> Variavel
******************************************************************/
contato_readInputs = function () {
    var InputValues = {
        IdContato: $("#IdContato").val(),
        Contato: $("#Contato").val(),
        Empresa: $("#Empresa").val(),
        Endereco: $("#Endereco").val()
    }

    return InputValues;
}
/*****************************************************************
 contato_WriteInputs
******************************************************************/
contato_WriteInputs =  function (obj) {
    $("#IdContato").val(obj.IdContato);
    $("#Contato").val(obj.Contato);
    $("#Empresa").val(obj.Empresa);
    $("#Endereco").val(obj.Endereco);
    Telefone_populateDataTable($('#IdContato').val())
    Email_populateDataTable($('#IdContato').val())
    $("#btn_telefone_incluir").click();
    $("#btn_email_incluir").click();
}

/*****************************************************************
 Evento ==>  $("#btn_contato_incluir").click()
******************************************************************/
$("#btn_contato_incluir").on("click", function () {
    $("#IdContato").val('0');
    $("#Contato").val('');
    $("#Empresa").val('');
    $("#Endereco").val('');
    $("#btn_telefone_incluir").click();
    $("#btn_email_incluir").click();
    Telefone_populateDataTable($('#IdContato').val())
    Email_populateDataTable($('#IdContato').val())

})

/*****************************************************************
 Carrega  Variavel --> Inputs
******************************************************************/
contato_readWrite = function (obj) {

    $("#IdContato").val(obj.IdContato);
    $("#Contato").val(obj.Contato);
    $("#Empresa").val(obj.Empresa);
    $("#Endereco").val(obj.Endereco);
}

/*****************************************************************
 Evento ==>  $("#btn_contato_save").click()
******************************************************************/

$("#btn_contato_save").on("click", function () {
    user_contatos = contato_readInputs();
    if (contato_validade()) {
        obj = comum.post('/Home/Contato_InsertUpdate/', { 'objList': user_contatos });

        contato_readWrite(obj.objList);
        alert("Inclusão/Alteração efetuada com Sucesso !!!");

        Telefone_populateDataTable($('#IdContato').val())
        Email_populateDataTable($('#IdContato').val())

        
    }

})

/*****************************************************************
 Evento ==>  $("#btn_contato_delete").click()
******************************************************************/
$("#btn_contato_delete").on("click", function () {
    if ($("#IdContato").val() === '0') {
        alert('Contato Inexistente !!!')

    }
    else {
        if (confirm('Confirma exclusão desse Contato ?')) {
            comum.post('/Home/Contato_Excluir/', { 'IdContato': $("#IdContato").val()});
            alert('Contato Excluído com Sucesso !!!')
            $("#btn_voltar").click();
        }
        else {

            alert('Exclusão Cancelada pelo Usuário');
        }
    }
})

/*****************************************************************
 Contato --> Valida os Dados Inseridos
******************************************************************/
contato_validade = function () {

    if ($("#Contato").val().replace(new RegExp(' ', 'g'), '') === '') {
        alert("Contato - Preenchimento Obrigatório !!!");
        return false;

    }
    return true;

}