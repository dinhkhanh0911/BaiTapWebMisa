<template>
    <div class="container d-flex">
        <div class="content-left">
            <div class="content-left-top bg-while">
                <div class="content-header d-flex alignt-center justify-space-center">
                    <span>Nghiệp vụ tiền gửi</span>
                </div>
                <div class="content-left-body">
                    <div class="div-button receive" @click="showNoticeMaintain">
                        <span>Thu tiền</span>
                    </div>
                    <div class="div-button payment " @click="showNoticeMaintain">
                        <span>Chi tiền</span>
                    </div>
                    <div class="div-button audit " @click="showNoticeMaintain">
                        <span>Đối chiếu ngân hàng</span>
                    </div>
                </div>
            </div>
            <div class="content-left-bottom bg-while d-flex">
                <div class="content-option d-flex bank-account" @click="showNoticeMaintain">
                    <div class="content-icon">
                        <div class="mi mi-24"></div>
                    </div>
                    <div class="content-des">
                        <span>
                            Tài khoản ngân hàng
                        </span>
                    </div>
                </div>
                <div class="content-option d-flex customer" @click="showNoticeMaintain">
                    <div class="content-icon">
                        <div class="mi mi-24"></div>
                    </div>
                    <div class="content-des">
                        <span>
                            Khách hàng
                        </span>
                    </div>
                </div>
                <div class="content-option d-flex supplier">
                    <div class="content-icon">
                        <div class="mi mi-24"></div>
                    </div>
                    <router-link to="/suppliers"  class="content-des router-link">
                        <span>
                            Nhà cung cấp
                        </span>
                    </router-link>
                </div>
                <div class="content-option d-flex employee" @click="showNoticeMaintain" >
                    <div class="content-icon">
                        <div class="mi mi-24"></div>
                    </div>
                    <div class="content-des">
                        <span>
                            Nhân viên
                        </span>
                    </div>
                </div>
                <div class="content-option d-flex caculator" @click="showNoticeMaintain">
                    <div class="content-icon">
                        <div class="mi mi-24"></div>
                    </div>
                    <div class="content-des">
                        <span>
                            Tính tỷ giá xuất quỹ
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <div class="content-right bg-while">
            <div class="content-header d-flex alignt-center justify-space-center">
                <span>Báo cáo</span>
            </div>
            <div class="content-right-content">
                <ul>
                    <li @click="showNoticeMaintain">
                        <a>
                            Bảng kê chứng từ theo khế ướng vay
                        </a>
                    </li>
                    <li @click="showNoticeMaintain">
                        <a>
                            Bảng kê chứng từ theo khế ướng vay
                        </a>
                    </li>
                    <li @click="showNoticeMaintain">
                        <a>
                            Bảng kê chứng từ theo khế ướng vay
                        </a>
                    </li>
                    <li @click="showNoticeMaintain">
                        <a>
                            Bảng kê chứng từ theo khế ướng vay
                        </a>
                    </li>
                    <li @click="showNoticeMaintain">
                        <a>
                            Bảng kê chứng từ theo khế ướng vay
                        </a>
                    </li>
                </ul>
            </div>
            <div class="content-header d-flex alignt-center justify-space-center show-all">
                <span>Tất cả báo cáo</span>
            </div>
        </div>
    </div>
    <BasePopup
      v-if="isShowPopupInfo"
      :contentPrev="contentPopupInfo"
      @confirm="handleConfirmBtn"
      :type="typePopupInfo"
      :keyCombobox="keyCombobox"
    />
</template>
<script>

/**
* Mô tả: Tab quy trình
* Created by: Đinh Văn Khánh - MF1112
* Created date: 25/05/2022
*/
import BasePopup from '@/components/base/BasePopup.vue'
export default {
    components:{
        BasePopup
    },
    data(){
        return {
            //popup
            contentPopupInfo: "",
            isShowPopupInfo: false,
            typePopupInfo: "error",
            keyCombobox: "",
        }
    },
    methods: {
        showNoticeMaintain(){
            this.showPopupInfo(this.typePopupName.warningNotify,this.popupMsg.maintainMsg,"confirm")
        },
        /**
        * Mô tả: Hiển thị popup thông báo
        @param dạng popup, Nội dung , key
        * Created by: Đinh Văn Khánh - MF1112
        * Created date: 12/04/2022
        */
      showPopupInfo(type, errorMsg, key) {
        
        this.typePopupInfo = type;
        this.contentPopupInfo = errorMsg;
        this.keyCombobox = key;
        
        this.isShowPopupInfo = true;
      },
        /**
       * Mô tả: Xử lý sự kiện confirm popup
       * Created by: Đinh Văn Khánh - MF1112
       * Created date: 18/04/2022
       */
      handleConfirmBtn(isConfirm, key) {
            //Popup xác nhận
            if (key === "confirm") {
                this.isShowPopupInfo = false;
            }
        }
    },
}
</script>
<style scoped>

.content-header{
    height: 40px;
    border-bottom: 1px solid #c7c7c7;
}
.content-header span{
    text-transform: uppercase;
    font-weight: 700;
    color: #111;
}
.show-all{
    border-bottom: none;
    border-top: 1px solid #c7c7c7;
    padding-top: 18px;
}
.content-left{
    margin-right: 10px;
    width: 688px;

}
.content-left-body{
    position: relative;
    height: 340px;
    background-image: url('@/assets/img/qt_TienGui.09015143.svg');
}
.div-button{
    position: absolute;
    width: 90px;
    
    height: 90px;
    display: flex;
    align-items:flex-end;
    justify-content: center;
    padding-bottom: 5px;
}
.div-button:hover{
    box-shadow: 0 3px 6px rgb(0 0 0 / 25%);
    cursor: pointer;
}
.receive{
    top: 35px;
    left: 80px;
}
.payment{
    top: 215px;
    left: 80px;
}
.audit{
    top: 125px;
    left: 300px;
    width: 170px;
}

/* content bottom */
.content-left-bottom{
    height: 68px;
    margin-top: 10px;
}
.content-left-bottom .content-option:first-child{
    flex: 1;
}
.content-option{
    flex-direction: column;
    align-items: center;
    justify-content: center;
    padding: 0 20px;
    
}
.content-option:hover{
    box-shadow: 0 3px 6px rgb(0 0 0 / 25%);
    cursor: pointer;
}
.bank-account .content-icon div{
    background-position: -1096px -200px;

}
.customer .content-icon div{
    background-position: -872px -200px;
}
.supplier .content-icon div{
    background-position: -928px -200px;
}
.employee .content-icon div{
    background-position: -984px -200px;
}
.caculator .content-icon div{
    background-position: -1376px -256px;
}

/**content-right */
.content-right{
    width: 298px;
}
.content-right-content ul{
    padding: 0 20px;
}
.content-right-content ul li{
    list-style:inside;
    padding: 16px 0;
    border-bottom: 1px solid #c7c7c7;
    line-height: 40px;
    cursor: pointer;
}
.content-right-content ul li:last-child{
    border-bottom: none;
}
.content-right-content ul li a{
    font-size: 13px;
}
.router-link{
    text-decoration: none;
}
</style>
<style>
.warning-notify .popup-footer{
  justify-content: center!important;
}
</style>