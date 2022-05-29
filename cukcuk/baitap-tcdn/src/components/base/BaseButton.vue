<template>
  <div class="btn-box" :class="addButton" ref="button">
    <button class="btn" :class="classBtn">
      <div class="btn-content">
        <span>{{ content }}</span>
        <div class="btn-option mi mi-14"></div>
      </div>
    </button>
    <button class="btn btn-option-2" :class="classBtn">
      <div class="btn-content">
        <div class="btn-option mi mi-14"></div>
      </div>
    </button>
  </div>
</template>
<script>
/**
* Mô tả:  Button component
@param
*@return
* Created by: Đinh Văn Khánh - MF1112
* Created date: 07/04/2022
*/
export default {
  props: {
    classBtn: {
      type: String,
      default: "btn-default",
    },
    //Nội dung button
    content: {
      type: String,
      default: "Thêm mới",
    },
    addButton: {
      type: String,
      default: "",
    },
    //Biến kiểm tra button có disable hay không
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  mounted() {
    //Khi component được mount vào component cha thì disable button option
    if (this.disabled) {
      this.$refs["button"].classList.add("disable");
    }
  },
  watch: {
    /**
     * Mô tả: Lăng nghe thay đổi disable button
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 07/04/2022
     */
    disabled: function () {
      if (this.disabled) {
        this.$refs["button"].classList.add("disable");
      } else {
        this.$refs["button"].classList.remove("disable");
      }
    },
  },
};
</script>
<style>
.addButton {
  display: flex;
}
.btn {
  height: var(--rounded-button-height);
  padding: 0 16px;
  border: 2px solid #3b3c3f;
  border-radius: 40px;
  outline: none;
}
button {
  cursor: pointer;
  white-space: nowrap;
}
.btn-option-2 {
  display: none;
}

.btn-default {
  background-color: #fff;
}
.disable .btn-default {
  background-color: #d2d3d6;
  cursor: not-allowed;
}
.addButton .btn:nth-child(1) .btn-option {
  display: none;
}
.addButton .btn-option-2 {
  display: block;
  padding: 0;
  
}
.addButton .btn-option-2 .btn-content{
  width: 50px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-left: 1px solid #fff;
  padding-right: 10px;
  padding-bottom: 2px;
}
.addButton .btn:nth-child(1) {
  border-radius: 30px 0 0 30px;
  
}
.addButton .btn:nth-child(2) {
  border-radius: 0 30px 30px 0;
}

.btn-primary {
  background-color: #2ca01c;
  color: #fff;
  border: none;
}
.btn-sq {
  border-radius: 4px;
  border: 1px solid #bbb;
}
.btn-no-op .btn-option {
  display: none;
}

.btn-default:hover {
  background-color: #d2d3d6;
}
.btn-primary:hover {
  background-color: #35bf22;
}

.btn-content {
  display: flex;
}
.btn-border .btn-content span {
  border-right: 1px solid #111;
  padding-right: 10px;
}

.btn-primary .btn-option {
  background-position: -848px -359px;
}
.btn-option {
  margin-left: 4px;
  background-position: -560px -359px;
}
</style>
