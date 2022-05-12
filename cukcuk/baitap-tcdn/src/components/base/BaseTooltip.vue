<template>
  <div class="tooltip" v-bind:style="{ top: top + 'px', left: left + 'px' }">
    <span>{{ value }}</span>
  </div>
</template>

<script>
/**
 * Mô tả:  Tooltip
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 18/04/2022
 */
export default {
  data() {
    return {
      value: "123",
    };
  },
  props: {
    valueTooltip: {
      type: String,
      default: "Tooltip",
    },
    top: {
      type: Number,
      default: 0,
    },
    left: {
      type: Number,
      default: 0,
    },
  },
  created() {
    this.value = this.valueTooltip;
  },
};
</script>
<style>
.tooltip {
  position: fixed;
  color: #fff;
  background-color: rgb(81, 81, 81);
  z-index: 1000;
  padding: 5px;
  font-size: 12px;
}
.tooltip span {
  height: 100px;
}
</style>
