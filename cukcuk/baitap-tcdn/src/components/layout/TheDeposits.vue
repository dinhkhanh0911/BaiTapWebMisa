<template>
  <div class="router-view">
    <div class="router-content">
      <div class="content-area-content">
        <div class="layout-dictionary">
          <div class="title-distance" :style="{ top: topTitle + 'px' }">
            <div class="header-di">
              <div class="header-tab d-flex">
                  <div class="header-tab-left d-flex">
                      <div class="header-tab-item " ref="CAProcess">
                          <router-link to="/CA/CAProcess" active-class="header-tab-active">Quy trình</router-link>
                      </div>
                      <div class="header-tab-item" ref="ReceiptPaymentList">
                          <router-link to="/CA/ReceiptPaymentList" active-class="header-tab-active">Thu,Chi tiền</router-link>
                      </div>
                  </div>
                  <div class="header-tab-right"></div>
              </div>
            </div>
            <!-- <div class="breadcrumb">
                            <div class="breadcrumb-icon mi mi-16"></div>
                            <span> Tất cả danh mục</span>
                        </div> -->
          </div>
          <div class="layout-dictionary-body d-flex alignt-center justify-space-center" @scroll="handleScroll($event)">
                <router-view />
          </div>
        </div>
      </div>
    </div>
    <BaseTooltip
      :top="topTooltip"
      :left="leftTooltip"
      v-if="isShowTooltip"
      :valueTooltip="valueTooltip"
    />
    <TheEmployeePopup
      v-if="isShowPopupEmployee"
      @showPopup="togglePopupEmployee"
      :modelPopup="employee"
      @showToast="showToast"
      @addSuccess="reloadData"
    />
    <div id="toast">
      <BaseToast
        v-for="(toast, index) in toasts"
        :key="index"
        :message="toast.message"
        :type="toast.type"
      />
    </div>
    <BasePopup
      v-if="isShowPopupInfo"
      :contentPrev="contentPopupInfo"
      @confirm="handleConfirmBtn"
      :type="typePopupInfo"
    />
  </div>
</template>
<script>
/**
* Mô tả:  Trang quy trình
* Created by: Đinh Văn Khánh - MF1112
* Created date: 10/05/2022
*/
import TheProcedure from "@/components/layout/TheProcedure.vue"
import TheReiceive_Payment from "@/components/layout/TheReiceive_Payment.vue"
export default {
    components:{
        TheProcedure,
        TheReiceive_Payment
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
    methods:{
      
    }
}
</script>

<style scoped>
.router-content {
  padding-top: 48px;
  padding-left: 20px;
  background-color: #eceef1;
}
.content-area-content {
  height: 100vh;
  width: 100%;
  position: relative;
}
.layout-dictionary {
  position: relative;
  display: flex;
  flex-direction: column;
  height: calc(100vh - 48px);
}

/*Header*/
.header-tab{
    width: 100%;
    border-bottom: 1px solid #c7c7c7;
}
.header-tab-item a{
    color: #111;
    text-decoration: none;
    font-size: 13px;
    padding: 0px 20px 15px 20px;
    display: block;
}
.header-tab-item a :hover{
    color: #2ca01c;
}
/* header tab active */
.header-tab-active{
    font-weight: 700;
    border-bottom: 3px solid #2ca01c!important;
    z-index: 100;
  
    
}

.layout-dictionary-body{
    width: 100%;
    height: 100%;
    padding-right: 0px;
    padding-top: 56px;
}
</style>