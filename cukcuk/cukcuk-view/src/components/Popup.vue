<template>
    <div id="popup" v-show="isShow" >
        <div id="modalInfo" class="modal popup " :class="typeClass[type].class" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header popup-header">
              <div class="modal-title-box">
                <h2 class="modal-title">{{title}}</h2>
              </div>
              <button id="close-modal-btn" type="button" class="close m-icon-btn" 
              data-dismiss="modal" aria-label="Close" @click="closePopup()" >
              </button>
            </div>
            <div class="modal-body popup-body">
                <div class="popup-icon" v-show="typeClass[type].isShowIconAndCloseBtn">
                    <div class="popup-icon-bg"><i class="fa fa-exclamation-triangle" aria-hidden="true"></i></div>
                </div>
                <div class="popup-content">
                    <span>{{contentPrev}} <span class="text-bold" >{{contentStrong}}</span> {{contentLast}}</span>
                </div>

            </div>
            <div class="modal-footer popup-footer">
                <button class="m-btn m-btn-default" v-show="typeClass[type].isShowIconAndCloseBtn" @click="closePopup()">
                    <span>{{closeButtonContent}}</span>
                </button>
                <button id="addStaffBtn" class="m-btn" @click="deleteEmit()">
                    <span>{{confirmButtonContent}}</span>
                </button>
                
            </div>
          </div>
        </div>
        
    </div>
    </div>
</template>

<script>

export default {
    props:{
        title:{
            type:String,
            default:'Thông báo'
        },
        type:{
            type:String,
            default:'info'
        },
        contentPrev:{
            type:String,
            default:'Thông báo thành công'
        },
        contentStrong:{
            type:String,
            default:''
        },
        contentLast:{
            type:String,
            default:''
        },
        confirmButtonContent:{
            type:String,
            default:"Ok"
        },
        closeButtonContent:{
            type:String,
            default:"Hủy"
        },
        isShow:{
            type:Boolean,
            default:false
        },
        isComfirm:{
            type:Boolean,
            default:false
        },
        name:{
            type:String,
            default:''
        }
    },
    data() {
        return {
            typeClass:{
                error:{class:"popup-error",isShowIconAndCloseBtn:true},
                warning:{class:"popup-warning",isShowIconAndCloseBtn:true},
                info:{class:"",isShowIcon:false}
            }
        }
    },
    computed:{
        
    },
    watch:{
        
    },
    methods: {
        closePopup(){
            this.$emit('Confirm',{
                name:this.name,
                isConfirm:false,
            })
        },
        deleteEmit(){
            this.$emit('Confirm',{
                name:this.name,
                isConfirm:true,
            })
        }
    },
}
</script>
<style scoped>
@keyframes movedown{
    
    from{
        transform: translateY(-100%);
    }
    to{
        transform: translateY();
    }
}
#popup{
    background-color: rgba(0, 0, 0, 0.3);
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 10000;
}
.popup{
    width: 500px;
    height: unset;
    position: fixed;
    top: calc(50% - 100px);
    left: calc(50% - 250px);
    animation:movedown linear .2s ;
}

.popup-header{
    height: 69px;
}
.popup-header h2{
    margin: 0;
}
.popup-body{
    display: flex;
    padding: 0px 24px 24px 24px;
}
.popup-content{
    width: 100%;
    display: flex;
    align-items: center;
}
.text-bold{
    font-weight: bold;
}
.popup-footer{
    background-color: #E5E5E5 ;
}

/* Chỉnh sửa icon popup */
.popup-icon{
    margin-right: 10px;
    height: 100%;
    display: flex;
    align-items: center;
}
.popup-icon-bg{
    background-color: #EBF4FF;
    height: 40px;
    width: 40px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 40px;
}
.popup-icon i{
    font-size: 20px;
}
.popup-error i{
    color: #F65454;
}
.popup-warning i{
    color: #F65454;
}

/* chỉnh sửa button popup */
.popup-footer .m-btn{
    display: flex;
    justify-content: center;
    align-items: center;
}
.popup-error .m-btn{
    background-color: #F65454;
}
.popup-error .m-btn-default{
    background-color: #fff;
}
</style>