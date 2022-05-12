<template>
  <div class="input-container" :class="searchClass" ref="input">
    <input
      :type="type"
      :placeholder="placeholder"
      :title="title"
      :value="modelValue"
      @input="onInputHandle($event.target.value)"
      @mousemove="onMouseMoveEvent($event)"
      @mouseleave="onMouseLeave($event)"
    />
    <div class="search-btn mi mi-16"></div>
    <!-- <BaseTooltip :top="topTooltip" :left="leftTooltip" v-if="isShowTooltip" :valueTooltip="valueTooltip"/> -->
  </div>
</template>

<script>
/**
 * Mô tả: Components Input
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 07/04/2022
 */
export default {
  props: {
    type: {
      type: String,
      default: "text",
    },
    placeholder: {
      type: String,
      default: "",
    },
    classInput: {
      type: String,
      default: "",
    },
    modelValue: {
      type: String,
      default: "",
    },
    timer: {
      type: Number,
      default: 0,
    },
    searchClass: {
      type: String,
      default: "",
    },
    componentDes: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      value: "",
      error: false,
      timeOut: null,
      title: "",
    };
  },
  methods: {
    /**
     * Mô tả:  Xử lý dữ liệu khi nhập input
     * @param: dữ liệu input
     * @return: Gửi dữ liệu lên component cha
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 15/04/2022
     */
    onInputHandle(value) {
      this.removeClass("error");
      if (this.timer === 0) {
        (this.error = false), this.$emit("update:modelValue", value);
      } else {
        clearTimeout(this.timeOut);
        this.timeOut = setTimeout(() => {
          (this.error = false), this.$emit("update:modelValue", value);
        }, this.timer);
      }
    },

    /**
     * Mô tả: Thêm class error vào component
     * Thêm title lỗi
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 07/04/2022
     */
    addError(className, errorMsg) {
      this.error = true;
      this.$refs["input"].classList.add(className);
      if (errorMsg != undefined) {
        this.title = `${this.componentDes} ${errorMsg}`;
      }
    },

    /**
     * Mô tả: Xóa class error khỏi component
     * Xóa title lỗi
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 07/04/2022
     */
    removeClass(className) {
      this.$refs["input"].classList.remove(className);
      this.title = "";
      this.error = false;
    },

    /**
     * Mô tả: Focus vào component
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 07/04/2022
     */
    focus() {

      this.$refs["input"].querySelector("input").focus();
    },

    select(){
      this.$refs["input"].querySelector("input").select();
    },

    /**
     * Mô tả:  Lăng nghe khi kiện mouse move
     * Khi sự kiện xảy ra tắt bỏ sự kiên component cha
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 07/04/2022
     */
    onMouseMoveEvent(e) {
      e.stopPropagation();
    },

    onMouseLeave(e){
      this.$emit('blur',true)
    }
  },
};
</script>
<style>
@import url("@/assets/css/input/input.css");
</style>
