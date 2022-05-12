<template>
  <div class="combobox-container" ref="combobox" :title="titleError">
    <div class="value-combobox">
      <input type="text" :value="valueCB" />
    </div>
    <div class="action">
      <button @click="isShow = !isShow">
        <div class="mi mi-16"></div>
      </button>
    </div>
    <div
      class="combobox-option"
      v-show="isShow"
      :class="{ dropUp: !isDropDown }"
    >
      <table :class="{ 'table-one-column': isOneColumn }">
        <thead>
          <tr>
            <th>Mã đơn vị</th>
            <th>Tên đơn vị</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(value, index) in values"
            :key="index"
            @click="handleSelect(value[id], value[name], index)"
            :class="{ active: value[id] == modelValue }"
          >
            <td>{{ value[code] }}</td>
            <td>{{ value[name] }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
/**
 * Mô tả:  Combobox
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 14/04/2022
 */
import axios from "axios";
export default {
  data() {
    return {
      isShow: false,
      valueCB: "",
      values: [],
      titleError: "",
      error: false,
    };
  },
  props: {
    Api: {
      type: String,
      default: "",
    },
    modelValue: {
      type: String,
      default: "",
    },
    id: {
      type: String,
      default: "",
    },
    name: {
      type: String,
      default: "",
    },
    code: {
      type: String,
      defaule: "",
    },
    componentDes: {
      type: String,
      default: "",
    },
    isDropDown: {
      type: Boolean,
      default: true,
    },
    valueOption: {
      type: Array,
      default: [],
    },
    isOneColumn: {
      type: Boolean,
      default: false,
    },
  },
  created() {
    //Lấy dữ liệu combobox
    //Nếu Api có, gọi Api lấy giữ liệu
    if (this.Api != "") {
      axios
        .get(this.Api)
        .then((response) => {
          if (response.status === 200) {
            this.values = response.data;
            //Nếu modelValue khác rỗng và số giá trị mảng option combobox lớn hơn không thì gán
            // giá trị input bằng giá trị value tương ứng
            if (this.modelValue !== "" && this.values.length > 0) {
              var value = this.values.find(
                (item) => item[this.id] == this.modelValue
              );
              if (value != undefined) {
                this.valueCB = value[this.name];
              }
            }
          }
        })
        .catch((e) => {
          console.log(e);
        });
    } else {
      this.values = this.valueOption;
      /**
       * Nếu Api không có, lấy mảng trong prop
       * Nếu modelValue khác rỗng và số giá trị mảng option combobox
       * lớn hơn không thì gán giá trị input bằng giá trị value tương ứng
       */
      if (this.modelValue !== "" && this.values.length > 0) {
        var value = this.values.find(
          (item) => item[this.id] == this.modelValue
        );
        if (value != undefined) {
          this.valueCB = value[this.name];
        }
      }
    }
  },
  watch: {
    /**
     * Mô tả:  Lăng nghe thay đổi giá trị model
     * Nếu model rỗng thì gán giá trị input bằng rỗng
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 14/04/2022
     */
    modelValue: function () {
      if (this.modelValue == "") {
        this.valueCB = "";
      }
    },
  },
  methods: {
    /**
     * Mô tả: Xử lý khi chọn combobox
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 14/04/2022
     */
    handleSelect(id, name, index) {
      this.isShow = false;
      this.$emit("update:modelValue", id);
      this.valueCB = name;
      this.removeClass("error");

      //Thêm class vào hàng được chọn
      var trElement = document.querySelectorAll(
        ".combobox-option table tbody tr"
      );
      console.log(trElement);
      for (var tr of trElement) {
        tr.classList.remove("active");
      }
      if (!trElement[index].classList.contains("active")) {
        trElement[index].classList.add("active");
      }
    },

    /**
     * Mô tả: Thêm class lỗi vào combobox
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 14/04/2022
     */
    addError(className, errorMsg) {
      this.error = true;
      this.$refs["combobox"].classList.add(className);
      if (errorMsg != undefined) {
        this.titleError = `${this.componentDes} ${errorMsg}`;
      }
    },

    /**
     * Mô tả: Xáo class lỗi khỏi combobox
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 14/04/2022
     */
    removeClass(className) {
      this.$refs["combobox"].classList.remove(className);
      this.error = false;
      this.titleError = "";
    },
  },
};
</script>

<style scoped>
.combobox-container {
  border: 1px solid #afafaf;
  border-radius: 2px;
  display: flex;
  height: 32px;
  position: relative;
  font-size: 13px;
}
.error {
  border: 1px solid red;
}

.value-combobox {
  flex: 1;
  height: 100%;
}
.value-combobox input {
  height: 100%;
  width: 100%;
  border: none;
  outline: none;
  border-radius: 2px;
  padding: 0 12px;
}
.action {
  height: 100%;
  width: 30px;
  background-color: bisque;
  border-radius: 2px;
  border: none;
  position: relative;
}
.action button {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  border: none;
  outline: none;
  border-radius: 2px;
  padding: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #fff;
}
.action button:hover {
  background-color: #e0e0e0;
  border-color: #e0e0e0;
}
.action button div {
  background-position: -560px -359px;

  margin: 0;
}

.combobox-option {
  position: absolute;
  left: -1px;
  right: -1px;
  top: 130%;
  background-color: #fff;
  border: 1px solid #afafaf;
  border-radius: 2px;
  z-index: 100;
}
.dropUp {
  top: unset;
  bottom: 130%;
}
.combobox-option table {
  width: 100%;
  border-spacing: 0;
}
.combobox-option table td,
.combobox-option table th {
  border: none;
  text-align: left;
  min-width: 30px;
  height: 32px;
  font-weight: 400;
}
.combobox-option table thead {
  background-color: #f4f5f8;
  color: #111;
  font-family: MISA notosans bold;
  cursor: default;
}
.table-one-column thead {
  display: none;
}
.table-one-column tbody tr td:first-child {
  display: none;
}
td,
th {
  text-align: left;
  padding: 0 14px 0 10px;
}
.combobox-option table tbody tr {
  cursor: pointer;
}
.combobox-option table tbody tr:hover td {
  background-color: #ebedf0;
  color: #35bf22;
}
.active {
  color: #fff !important;
  background-color: #2ca01c !important;
}
.active:hover > td {
  color: #fff !important;
  background-color: #2ca01c !important;
}
.parent {
  font-weight: 700;
  font-family: MISA notosans bold;
}
</style>
