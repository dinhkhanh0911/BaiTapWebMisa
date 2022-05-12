<template>
    <div class="toast" :class="typeToast[type].class">
        <div class="toast-icon">
            <i class="fa " :class="typeToast[type].icon" aria-hidden="true"></i>
        </div>
        <div class="toast-content">
            <span>{{message}}</span>
        </div>
        <div class="toast-close">
            <button id="close-toast-btn">
                <i class="fa fa-times" aria-hidden="true"></i>
            </button>
        </div>
    </div>
</template>

<script>
export default {
    data(){
        return{
            typeToast:{
                success:{
                    class:'toast-success',
                    icon:'fa-check'
                },
                error:{
                    class:'toast-error',
                    icon:'fa-exclamation-triangle'
                },
                warning:{
                    class:'toast-warning',
                    icon:'fa-exclamation-triangle'
                },
                info:{
                    class:'toast-info',
                    icon:'fa-info-circle'
                }
            }
        }
    },
    props:{
        message:{
            type:String,
            default:'Thành công'
        },
        type:{
            type:String,
            default:'success'
        }
    }
}
</script>

<style>
@keyframes moveRightToLeft{
    from{
        
        transform: translateX(100%);
    }
    to{
        
        transform: translateX(0);
    }
}
@keyframes fadeOut{
    to{
        opacity: 0;
    }
}

.toast{
    
    height: 48px;
    width: 400px;
    border-radius: 4px;
    box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.16);
    background-color: #fff;
    display: flex;
    align-items: center;
    padding-left: 16px;
    padding-right: 10px;
    
    animation: moveRightToLeft linear .5s , fadeOut 1s 3s forwards;
}
.toast + .toast{
    margin-top: 24px;
}
.toast i {
    font-size: 24px;
}
.toast-error i {
    color: #FFCECE;
}
.toast-success i {
    color: #019160;
}
.toast-info i {
    color: #D8EAFF;
}
.toast-warning i {
    color: #FFE7AF;
}
.toast-content{
    flex: 1;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 13px;
}
.toast-close button{
    border: none;
    background-color: #fff;
}
</style>