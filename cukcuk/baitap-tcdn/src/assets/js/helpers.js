import $ from 'jquery'

const Helpers ={
    draggable:function(Id){
        $(`${Id}`).on('mousedown', function(e) {
                $(this).addClass('movable');
                var offset = $('.movable').offset();
                var mouseX = e.pageX;
                var mouseY = e.pageY;
                var posX = mouseX - offset.left;
                var posY = mouseY - offset.top;
                

                $('.movable').on('mousemove', function(event) {
                    var thisX = event.pageX - posX,
                        thisY = event.pageY - posY;

                    $('.movable').offset({
                        left: thisX,
                        top: thisY
                    });
                    
                    if(mouseX < offset.left && mouseX > offset.left + $('.movable').width()){
                        $('.movable').css("left", mouseX);
                    }
                    
                    if(mouseY < offset.top && mouseY > offset.top + $('.movable').height()){
                                $('.movable').css("top", mouseY);
                            } 
                            
                            event.preventDefault();
                            
                        });
                    }).on('mouseup', function() {
                        $(this).removeClass('movable');
                        $(this).removeClass('movable');
                });
    }
}

export default Helpers