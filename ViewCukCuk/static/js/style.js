
export {enableModal,handeCheckTable}

$(document).ready(function(){
    handerInput()
    handerSetSizeMenu()
    handleModalClose()
    
})

function handerInput(){
    
    $(".input-search").click(function(e){
        e.stopPropagation();
        var inputBox = $(this).parent().parent()
        inputBox.addClass("border-color")
    })
    $(".input-search").keypress(function(e){
        e.stopPropagation();
        var inputBox = $(this).parent().parent().find(".m-time")
        inputBox.removeClass("disabled")
    })
    $(".m-time").keypress(function(e){
        e.stopPropagation();
        var inputBox = $(this).parent().parent().find(".m-time")
        console.log(inputBox)
        inputBox.removeClass("disabled")
    })
    // $('.input-date').click(function(e){
    //     e.stopPropagation();
    //     $(this).addClass('border-color')
    // })
    $(document).click(function(){
        $(".m-search-container").removeClass("border-color")
    })
}
function handeCheckTable(){
    $("#tbody tr").click(function(){
        var a = $(this).find("input:first")
        var ok = !a.is(':checked')
        a.prop("checked",ok)
        if(ok) $(this).addClass("m-checked-table")
        else $(this).removeClass("m-checked-table")
    })
}
function handerSetSizeMenu(){
    $('#m-set-size-btn').click(function(){
        $('.body-left').toggleClass("m-body-left")
        $('.body-right').toggleClass("l-body-right")
        if($(this).hasClass('min-size')){
            $(this).html(`
                <img src="../static/icon/btn-prev-page.svg" alt="">
            `)
        }
        else{
            $(this).html(`
                <img src="../static/icon/btn-next-page.svg" alt="">
            `)
        }
        $(this).toggleClass("min-size")
    })
}

function enableModal(idModal){
    $(`#${idModal}`).show()
    $('#overlay').show()
}
function disabledModal(idModal){
    $(`#${idModal}`).hide()
    $('#overlay').hide()
}
function handleModalClose(){
    $('#close-modal-btn').click(function(){
        disabledModal("modalInfo")
    })
}



