<template>
  <div class="radio-component d-flex alignt-center" :class="classCheckbox">
    <input
      type="radio"
      class="radio-input"
      :name="nameRadio"
      :checked="this.modelValue == this.valueRadio"
      @click="handleCheck()"
    />

    <div class="radio">
      <span
        class="radio-border"
        :class="{ 'radio-border-check': isCheck }"
      ></span>
      <span class="radio-cicle" v-show="isCheck"></span>
    </div>
    <span class="radio-label">
      <span>{{ labelRadio }}</span>
    </span>
  </div>
</template>
<script>
/**
 * Mô tả:  Component checkbox
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 16/04/2022
 */
export default {
  data() {
    return {
      isCheck: false,
    };
  },
  props: {
    classCheckbox: {
      type: String,
      default: "",
    },
    nameRadio: {
      type: String,
      default: "",
    },
    valueRadio: {
      type: Number,
      default: 1,
    },
    labelRadio: {
      type: String,
      default: "",
    },
    modelValue: {
      type: Number,
      default: 1,
    },
  },
  created() {
    /**
     * Mô tả: Nếu model bằng giá trị radio gửi giá trị lên component cha
     * Set hiển thị check
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    if (this.modelValue == this.valueRadio) {
      this.emitValue();
      this.isCheck = true;
    }
  },
  watch: {
    /**
     * Mô tả: Lắng nghe giá trị model thay đổi
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    modelValue: function () {
      if (this.modelValue == this.valueRadio) {
        this.isCheck = true;
      } else {
        this.isCheck = false;
      }
    },
  },
  methods: {
    /**
     * Mô tả: Xử lý sự kiện check
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleCheck() {
      this.emitValue();
      this.isCheck = true;
    },

    /**
     * Mô tả: Gửi giá trị lên component cha
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    emitValue() {
      this.$emit("update:modelValue", this.valueRadio);
    },
  },
};
</script>
<style>
.radio-component {
  position: relative;
  height: 30px;
}
.right-10 {
  margin-right: 20px;
}
.radio-input {
  width: 20px;
  height: 20px;
  position: absolute;
  top: 50%;
  left: 0;
  transform: translateY(-50%);
  z-index: 100;
  opacity: 0;
}
.radio {
  width: 20px;
  height: 20px;
  position: relative;
}
.radio-border {
  width: 20px;
  height: 20px;
  border: 1px solid #afafaf;
  display: block;
  border-radius: 50px;
}
.radio-border-check {
  border: 1px solid #2ca01c;
}
.radio-cicle {
  position: absolute;
  width: 10px;
  height: 10px;
  background-color: #2ca01c;
  top: 50%;
  left: 50%;
  transform: translateX(-50%) translateY(-50%);
  border-radius: 40px;
}
.radio-label {
  margin-left: 10px;
}
</style>
