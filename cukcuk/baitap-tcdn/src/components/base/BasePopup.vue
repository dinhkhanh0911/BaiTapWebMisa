<template>
  <div id="popup">
    <div
      id="popupInfo"
      class="popup"
      :class="typeClass.className"
      tabindex="-1"
      role="dialog"
    >
      <div class="popup-dialog" role="document">
        <div class="popup-content">
          <div class="popup-body">
            <div class="popup-icon mi mi-48"></div>
            <div class="popup-message">
              <span>
                  {{ contentPrev }}
                </span>
            </div>
          </div>
          <div class="border-bottom"></div>
          <div class="popup-footer d-flex justify-space-between popup-footer">
            <div class="popup-footer-left">
              <BaseButton
                :classBtn="'close-btn btn-default btn-sq btn-no-op'"
                
                :content="typeClass.closeBtn"
                @click="handleCloseBtn()"
              />
            </div>
            <div class="popup-footer-right d-flex">
              <div class="btn-popup">
                <BaseButton
                  :classBtn="'btn-default btn-sq btn-no-op'"
                  :content="typeClass.noBtn"
                  @click="handleClickNoOptionBtn()"
                />
              </div>
              <div class="btn-popup">
                <BaseButton
                  :classBtn="'btn-primary btn-sq btn-no-op'"
                  :content="typeClass.confirmBtn"
                  @click="handleClickAgreeBtn()"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
/**
 * Mô tả: Popup thông tin
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 18/04/2022
 */
import BaseButton from "@/components/base/BaseButton.vue";
export default {
  components: {
    BaseButton,
  },
  props: {
    title: {
      type: String,
      default: "Thông báo",
    },
    type: {
      type: String,
      default: "info",
    },
    contentPrev: {
      type: String,
      default: "Thông báo",
    },
    isComfirm: {
      type: Boolean,
      default: false,
    },
    name: {
      type: String,
      default: "",
    },
    keyCombobox: {
      type: String,
      default: "",
    },
  },
  /**
   * Mô tả:  Lắng nghe sự kiện bàn phím
   * Created by: Đinh Văn Khánh - MF1112
   * Created date: 27/04/2022
   */
  mounted() {
    
    window.addEventListener("keyup", this.handleKeyEventUp);
    window.addEventListener("keydown", this.handleKeyEventDown);
  },
  /**
   * Mô tả:  Hủy sự kiện bàn phím
   * Created by: Đinh Văn Khánh - MF1112
   * Created date: 27/04/2022
   */
  beforeUnmount() {
    window.removeEventListener("keyup", this.handleKeyEventUp);
    window.removeEventListener("keydown", this.handleKeyEventDown);
  },
  data() {
    return {
      typeClass: {},
    };
  },
  created(){
      this.typeClass = this.typePopup[`${this.type}`]
      
  },
  methods: {
    //Đóng popup
    closePopup() {
      this.$emit("Confirm", {
        isConfirm: false,
      });
    },

    /**
     * Mô tả:  Gửi emit lên component cha
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    deleteEmit() {
      this.$emit("Confirm", {
        name: this.name,
        isConfirm: true,
      });
    },

    /**
     * Mô tả: Xử lý sự kiện đóng popup
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleCloseBtn() {
      
      this.$emit("confirm", false, this.keyCombobox);
    },

    /**
     * Mô tả: Xử lý sự kiện bấm đồng ý
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleClickAgreeBtn() {
      this.$emit("confirm", true, this.keyCombobox);
    },

    /**
     * Mô tả: Xử lý sự kiện bấm không
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleClickNoOptionBtn() {
      this.$emit("confirm", 0, this.keyCombobox);
    },

    /**
     * Mô tả: Xử lý sự kiện bấm bàn phím key up
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleKeyEventUp(e) {
      if (e.keyCode === 13) {
        this.handleClickAgreeBtn();
      }
    },
    /**
     * Mô tả: Xử lý sự kiện bấm bàn phím key down
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleKeyEventDown(e) {
      if (e.keyCode === 13) {
        e.preventDefault();
      
      }
    },
  },
};
</script>
<style scoped>
@keyframes movedown {
  from {
    transform: translateY(-100%);
  }
  to {
    transform: translateY();
  }
}
#popup {
  background-color: rgba(0, 0, 0, 0.3);
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 10000;
}
.popup {
  width: 500px;
  height: unset;
  position: fixed;
  top: calc(50%);
  left: calc(50%);
  transform: translateX(-50%) translateY(-50%);
  animation: movedown linear 0.2s;
  background-color: #fff;
  border-radius: 4px;
}

.popup-body {
  display: flex;
}
.popup-content {
  width: 100%;
  padding: 32px;
}
.popup-message {
  /* display: flex;
    align-items: center; */
  padding: 12px 0px 32px 16px;
}
.border-bottom {
  border-bottom: 1px solid #b8bcc3;
  width: 100%;
  margin-bottom: 24px;
}

/* Chỉnh sửa icon popup */
.popup-icon {
  margin-right: 10px;
  height: 100%;
  display: flex;
  align-items: center;
}

/* Lỗi*/
.error .popup-icon {
  background-position: -24px -954px;
}
.error .popup-footer-left{
    display: none;
}
.error .popup-footer {
  justify-content: center;
}
.error .btn-popup:first-child {
  display: none;
}
.popup-footer {
  align-items: center;
  justify-content: center;
}

.close-btn {
  display: none;
}

/*Cảnh báo */
.warning .btn-popup:first-child {
  display: none;
}
.warning .popup-icon {
  background-position: -592px -456px;
}
.warning .close-btn {
  display: block;
}
.warning .popup-footer {
  align-items: center;
  justify-content: space-between;
}
.warning-notify .popup-footer-left{
    display: none;
    
}
.warning-notify .popup-footer{
    justify-content: right;
}

/* Thành công */
.success .popup-icon {
  background-position: -984px -456px;
}

/* Thông tin */
.infor .popup-icon {
  background-position: -832px -462px;
}
.btn-popup {
  margin-left: 10px;
}
.infor-notify .popup-footer-left{
    display: none;
    
}
.infor-notify .popup-footer{
    justify-content: right;
}
.infor-notify .popup-footer .btn-popup:first-child{
  display: none;
}
</style>

